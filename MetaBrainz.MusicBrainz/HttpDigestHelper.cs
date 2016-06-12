using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MetaBrainz.MusicBrainz {

  internal static class HttpDigestHelper {

    private static readonly Regex AuthChallengeRegex;

    static HttpDigestHelper() {
      HttpDigestHelper.AuthChallengeRegex = new Regex("Digest[ \\t](?:[ \\t,]*([^ \\t\"=]+)=\"([^\"]+)\")+");
    }

    public static string GetDigest(HttpWebResponse resp, NetworkCredential credential) {
      if (credential == null)
        return null;
      Dictionary<string, string> properties = null;
      { // Parse the WWW-Authenticate header, if present
        var challenge = resp.GetResponseHeader("WWW-Authenticate");
        var info = HttpDigestHelper.AuthChallengeRegex.Match(challenge);
        if (info.Success && info.Groups.Count >= 3) {
          var keys = info.Groups[1].Captures;
          var values = info.Groups[2].Captures;
          if (keys.Count > 0 && keys.Count == values.Count) {
            properties = new Dictionary<string, string>(keys.Count);
            for (var i = keys.Count - 1; i >= 0; --i)
              properties.Add(keys[i].Value, values[i].Value);
          }
        }
      }
      if (properties == null) // Failed to get information (at least realm and nonce are required)
        return null;
      // Get the required properties
      string realm;
      if (!properties.TryGetValue("realm", out realm)) {
        Debug.Print("No realm defined for HTTP Digest.");
        return null;
      }
      string nonce;
      if (!properties.TryGetValue("nonce", out nonce)) {
        Debug.Print("No server nonce defined for HTTP Digest.");
        return null;
      }
      string opaque;
      if (!properties.TryGetValue("opaque", out opaque))
        opaque = null;
      string algorithm;
      if (!properties.TryGetValue("algorithm", out algorithm))
        algorithm = "MD5";
      if (algorithm != "MD5") {
        Debug.Print($"Unsupported HTTP Digest algorithm: {algorithm}");
        return null;
      }
      string qop = null;
      var nc = 0;
      var cnonce = string.Empty;
      {
        string text;
        if (properties.TryGetValue("qop", out text)) {
          foreach (var qopvalue in text.Split(',')) {
            qop = qopvalue.Trim();
            if (qop == "auth")
              break;
          }
          if (qop != "auth") {
            Debug.Print($"Unsupported QOP list for HTTP Digest: {text} (only auth is supported)");
            return null;
          }
        }
      }
      if (qop != null) { // We need nc and cnonce; to be stateless, we generate a new cnonce each time.
        nc = 1;
        cnonce = Guid.NewGuid().ToString("N");
      }
      var a1 = HttpDigestHelper.ComputeHash($"{credential.UserName}:{realm}:{credential.Password}");
      if (a1 == null)
        return null;
      var a2 = HttpDigestHelper.ComputeHash($"{resp.Method}:{resp.ResponseUri.PathAndQuery}");
      if (a2 == null)
        return null;
      var response = HttpDigestHelper.ComputeHash((qop == null) ? $"{a1}:{nonce}:{a2}" : $"{a1}:{nonce}:{nc:X8}:{cnonce}:{qop}:{a2}");
      if (response == null)
        return null;
      var sb = new StringBuilder($"Digest username=\"{credential.UserName}\", realm=\"{realm}\", nonce=\"{nonce}\", uri=\"{resp.ResponseUri.PathAndQuery}\", algorithm=\"{algorithm}\", response=\"{response}\"", 512);
      if (qop != null)
        sb.Append($", qop=\"{qop}\", nc={nc:X8}, cnonce=\"{cnonce}\"");
      if (opaque != null)
        sb.Append($", opaque=\"{opaque}\"");
      return sb.ToString();
    }

    private static string ComputeHash(string input) {
      var sb = new StringBuilder();
      foreach (var b in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input)))
          sb.Append(b.ToString("x2"));
      return sb.ToString();
    }

  }

}
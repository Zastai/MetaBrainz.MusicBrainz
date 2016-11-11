// This will not work until https://github.com/metabrainz/musicbrainz-server/pull/385 is merged.
//#define SUBMIT_ACCEPT_JSON
// This will cause the raw JSON response for lookups/browsed to be traced (debug builds only).
#define TRACE_JSON_RESPONSE

#if NETFX_GE_4_5 // HttpWebRequest only has GetResponseAsync in v4.5 and up

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Submissions;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    private async Task<string> ApplyDelayAsync(Func<Task<string>> request) {
      if (Query._requestDelay <= 0.0)
        return await request().ConfigureAwait(false);
      while (true) {
        Query.Lock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              return await request().ConfigureAwait(false);
            }
            finally {
              Query._lastRequestTime = DateTime.UtcNow;
            }
          }
        }
        finally {
          Query.Unlock();
        }
        await Task.Delay((int) (500 * Query._requestDelay)).ConfigureAwait(false);
      }
    }

    private async Task<string> PerformDirectRequestAsync(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: GET {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method    = "GET";
      req.Accept    = "application/json";
      req.UserAgent = this._fullUserAgent;
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        using (var response = (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false)) {
          using (var stream = response.GetResponseStream()) {
            if (stream != null) {
              var encname = response.CharacterSet;
              if (encname == null || encname.Trim().Length == 0)
                encname = "utf-8";
              var enc = Encoding.GetEncoding(encname);
              using (var sr = new StreamReader(stream, enc)) {
                var json = sr.ReadToEnd();
#if TRACE_JSON_RESPONSE
                Debug.Print($"[{DateTime.UtcNow}] => RESPONSE: <<\n{Newtonsoft.Json.JsonConvert.DeserializeObject(json)}\n>>");
#endif
                return json;
              }
            }
          }
        }
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        using (var response = (HttpWebResponse) we.Response) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, null);
            if (digest != null && this._lastDigest != digest) {
              this._lastDigest = digest;
              goto retry;
            }
          }
          var msg = Query.ExtractError(response);
          if (msg != null)
            throw new QueryException(msg, we);
        }
        throw;
      }
      // We got a response without any content (probably impossible).
      throw new QueryException("Query did not produce results.");
    }

    private async Task<string> PerformDirectSubmissionAsync(ISubmission submission) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{submission.Entity}/", $"?client={submission.Client}").Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE SUBMISSION: {submission.Method} {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = submission.Method;
#if SUBMIT_ACCEPT_JSON
      req.Accept      = "application/json";
#else
      req.Accept      = "application/xml";
#endif
      req.ContentType = "application/xml; charset=utf-8";
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      var body = submission.RequestBody;
      if (body != null) {
        Debug.Print($"[{DateTime.UtcNow}] => BODY: {body}");
        using (var rs = await req.GetRequestStreamAsync().ConfigureAwait(false)) {
          using (var sw = new StreamWriter(rs, Encoding.UTF8))
            sw.Write(body);
        }
      }
      try {
        using (var response = (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false))
          return Query.ExtractMessage(response);
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        using (var response = (HttpWebResponse) we.Response) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, null);
            if (digest != null && this._lastDigest != digest) {
              this._lastDigest = digest;
              goto retry;
            }
          }
          var msg = Query.ExtractError(response);
          if (msg != null)
            throw new QueryException(msg, we);
        }
        throw;
      }
    }

    private async Task<string> PerformRequestAsync(string entity, string id, string extra) => await this.ApplyDelayAsync(() => this.PerformDirectRequestAsync(entity, id, extra)).ConfigureAwait(false);

    internal async Task<string> PerformSubmissionAsync(ISubmission submission) => await this.ApplyDelayAsync(() => this.PerformDirectSubmissionAsync(submission)).ConfigureAwait(false);

  }

}

#endif

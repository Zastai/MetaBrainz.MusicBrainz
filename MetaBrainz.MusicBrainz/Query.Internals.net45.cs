using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Submissions;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  public sealed partial class Query {

    #region Delay Processing

    private static async Task<HttpWebResponse> ApplyDelayAsync(Func<Task<HttpWebResponse>> request) {
      if (Query._requestDelay <= 0.0)
        return await request().ConfigureAwait(false);
      Task<HttpWebResponse> task = null;
      while (task == null) {
        Query.Lock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              task = request();
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
      return await task.ConfigureAwait(false);
    }

    #endregion

    #region Asynchronous Requests

    private async Task<HttpWebResponse> PerformRequestAsync(Uri uri, string method, string accept, string contentType = null, string body = null) {
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method} {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = method;
      req.Accept      = accept;
      req.UserAgent   = this._fullUserAgent;
      if (contentType != null)
        req.ContentType = contentType;
      if (body != null) {
        Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
        using (var rs = await req.GetRequestStreamAsync().ConfigureAwait(false)) {
          using (var sw = new StreamWriter(rs, Encoding.UTF8))
            sw.Write(body);
        }
      }
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        return (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false);
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        using (var response = (HttpWebResponse) we.Response) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, null);
            if (digest != null && this._lastDigest != digest) {
              ((IDisposable) response).Dispose();
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

    internal async Task<string> PerformRequestAsync(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      var task = Query.ApplyDelayAsync(() => this.PerformRequestAsync(uri, "GET", Query.JsonContentType));
      using (var response = await task.ConfigureAwait(false)) {
        if (!response.ContentType.StartsWith(Query.JsonContentType)) // FIXME: Should validate a little more than that, really
          throw new QueryException($"Invalid response received: bad content type ({response.ContentType}).");
        using (var stream = response.GetResponseStream()) {
          if (stream == null)
            return string.Empty;
          var encname = response.CharacterSet;
          if (encname == null || encname.Trim().Length == 0)
            encname = "utf-8";
          var enc = Encoding.GetEncoding(encname);
          using (var sr = new StreamReader(stream, enc)) {
            var json = sr.ReadToEnd();
            Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <<\n{JsonConvert.DeserializeObject(json)}\n>>");
            return json;
          }
        }
      }
    }

    internal async Task<string> PerformSubmissionAsync(ISubmission submission) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{submission.Entity}/", $"?client={submission.Client}").Uri;
      var task = Query.ApplyDelayAsync(() => this.PerformRequestAsync(uri, submission.Method, Query.JsonContentType, submission.ContentType, submission.RequestBody));
      using (var response = await task.ConfigureAwait(false))
        return Query.ExtractMessage(response);
    }

    #endregion

  }

}

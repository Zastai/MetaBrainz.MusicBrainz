using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;

namespace MetaBrainz.MusicBrainz;

internal static class Utils {

  public static async Task<QueryException> CreateQueryExceptionForAsync(HttpResponseMessage response,
                                                                        CancellationToken cancellationToken = default) {
    string? errorInfo = null;
    if (response.Content.Headers.ContentLength > 0) {
      errorInfo = await Utils.GetStringContentAsync(response, cancellationToken).ConfigureAwait(false);
      if (string.IsNullOrWhiteSpace(errorInfo)) {
        Debug.Print($"[{DateTime.UtcNow}] => NO ERROR RESPONSE TEXT");
        errorInfo = null;
      }
      else {
        var handled = false;
        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (mediaType is not null) {
          if (mediaType.StartsWith("application/json")) {
            using var doc = JsonSerializer.Deserialize<JsonDocument>(errorInfo);
            if (doc is not null && doc.RootElement.ValueKind == JsonValueKind.Object) {
              // MusicBrainz error response: { "error": "error_id", "help": "this is an error" }
              // OAuth2 error response: { "error": "error_id", "error_description": "this is an error" }
              string? error = null;
              string? errorDescription = null;
              handled = true;
              foreach (var prop in doc.RootElement.EnumerateObject()) {
                switch (prop.Name) {
                  case "error":
                    error = prop.Value.GetString();
                    break;
                  case "error_description":
                  case "help":
                    errorDescription = prop.Value.GetString();
                    break;
                  default:
                    handled = false;
                    break;
                }
                if (!handled) {
                  break;
                }
              }
              if (handled && error is not null && errorDescription is not null) {
                Debug.Print($"[{DateTime.UtcNow}] => ERROR: '{error}' DESCRIPTION/HELP: '{errorDescription}'");
                errorInfo = $"{error} ({errorDescription})";
              }
            }
          }
        }
        if (!handled) {
          Debug.Print($"[{DateTime.UtcNow}] => ERROR RESPONSE TEXT: {TextUtils.FormatMultiLine(errorInfo)}");
        }
      }
    }
    else {
      Debug.Print($"[{DateTime.UtcNow}] => NO ERROR RESPONSE CONTENT");
    }
    return new QueryException(response.StatusCode, response.ReasonPhrase, errorInfo);
  }

  private static string GetContentEncoding(HttpContentHeaders contentHeaders) {
    var characterSet = contentHeaders.ContentEncoding.FirstOrDefault();
    if (string.IsNullOrWhiteSpace(characterSet)) {
      // Fall back on the charset portion of the content type.
      // FIXME: Should this check the media type?
      characterSet = contentHeaders.ContentType?.CharSet;
    }
    if (string.IsNullOrWhiteSpace(characterSet)) {
      characterSet = null;
    }
    return characterSet?.ToLowerInvariant() ?? "utf-8";
  }

  public static async Task<string> GetStringContentAsync(HttpResponseMessage response,
                                                         CancellationToken cancellationToken = default) {
    var content = response.Content;
    Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({content.Headers.ContentType}): {content.Headers.ContentLength} bytes");
#if NET
    var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
    await using var _ = stream.ConfigureAwait(false);
#elif NETSTANDARD2_1_OR_GREATER
    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
    await using var _ = stream.ConfigureAwait(false);
#else
    using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#endif
#if !NET
    if (stream is null) {
      return "";
    }
#endif
    var characterSet = Utils.GetContentEncoding(content.Headers);
    using var sr = new StreamReader(stream, Encoding.GetEncoding(characterSet), false, 1024, true);
    // This is not (yet?) cancelable
    var text = await sr.ReadToEndAsync().ConfigureAwait(false);
    Debug.Print($"[{DateTime.UtcNow}] => RESPONSE TEXT: {TextUtils.FormatMultiLine(text)}");
    return text;
  }

}

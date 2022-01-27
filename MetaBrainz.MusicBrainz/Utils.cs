using System;
using System.Diagnostics;
using System.Net.Http;
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
      errorInfo = await HttpUtils.GetStringContentAsync(response, cancellationToken).ConfigureAwait(false);
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

}

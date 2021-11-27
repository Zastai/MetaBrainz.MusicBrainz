using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz;

internal static class Utils {

  public static QueryException CreateQueryExceptionFor(HttpResponseMessage response) {
    string? errorInfo = null;
    if (response.Content.Headers.ContentLength > 0) {
      errorInfo = Utils.ResultOf(Utils.GetStringContentAsync(response));
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
                Debug.Print($"[{DateTime.UtcNow}] => ERROR: '{error}' DESCRIPTION: '{errorDescription}'");
                errorInfo = $"{error}: {errorDescription}";
              }
            }
          }
        }
        if (!handled) {
          Debug.Print($"[{DateTime.UtcNow}] => ERROR RESPONSE TEXT: {Utils.FormatMultiLine(errorInfo)}");
        }
      }
    }
    else {
      Debug.Print($"[{DateTime.UtcNow}] => NO ERROR RESPONSE CONTENT");
    }
    return new QueryException(response.StatusCode, response.ReasonPhrase, errorInfo);
  }

  public static ProductInfoHeaderValue CreateUserAgentHeader<T>() {
    var an = typeof(T).Assembly.GetName();
    return new ProductInfoHeaderValue(an.Name ?? "*Unknown Assembly*", an.Version?.ToString());
  }

  public static string FormatMultiLine(string text) {
    const string prefix = "<<";
    const string suffix = ">>";
    const string sep = "\n  ";
    char[] newlines = { '\r', '\n' };
    text = text.Replace("\r\n", "\n").TrimEnd(newlines);
    var lines = text.Split(newlines);
    return lines.Length switch {
      0 => prefix + suffix,
      1 => prefix + lines[0] + suffix,
      _ => prefix + sep + string.Join(sep, lines) + "\n" + suffix
    };
  }

  public static string GetContentEncoding(HttpContentHeaders contentHeaders) {
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

  public static async Task<T> GetJsonContentAsync<T>(HttpResponseMessage response, JsonSerializerOptions options) {
    var content = response.Content;
    Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({content.Headers.ContentType}): {content.Headers.ContentLength} bytes");
#if NET || NETSTANDARD2_1_OR_GREATER
    var stream = await response.Content.ReadAsStreamAsync();
    await using var _ = stream.ConfigureAwait(false);
#else
    using var stream = await content.ReadAsStreamAsync();
#endif
    if (stream is null || stream.Length == 0) {
      throw new QueryException(HttpStatusCode.NoContent, "Response contained no data.");
    }
    var characterSet = Utils.GetContentEncoding(content.Headers);
#if !DEBUG
    if (characterSet == "utf-8") { // Directly use the stream
      var jsonObject = await JsonSerializer.DeserializeAsync<T>(stream, options);
      return jsonObject ?? throw new JsonException("The received content was null.");
    }
#endif
    var enc = Encoding.GetEncoding(characterSet);
    using var sr = new StreamReader(stream, enc, false, 1024, true);
    var json = await sr.ReadToEndAsync().ConfigureAwait(false);
    Debug.Print($"[{DateTime.UtcNow}] => JSON: {JsonUtils.Prettify(json)}");
    {
      var jsonObject = JsonUtils.Deserialize<T>(json, options);
      return jsonObject ?? throw new JsonException("The received content was null.");
    }
  }

  private static async Task<string> GetStringContentAsync(HttpResponseMessage response) {
    var content = response.Content;
    Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({content.Headers.ContentType}): {content.Headers.ContentLength} bytes");
#if NET || NETSTANDARD2_1_OR_GREATER
    var stream = await content.ReadAsStreamAsync();
    await using var _ = stream.ConfigureAwait(false);
#else
    using var stream = await response.Content.ReadAsStreamAsync();
#endif
#if !NET
    if (stream == null) {
      return "";
    }
#endif
    var characterSet = Utils.GetContentEncoding(content.Headers);
    using var sr = new StreamReader(stream, Encoding.GetEncoding(characterSet), false, 1024, true);
    var text = await sr.ReadToEndAsync().ConfigureAwait(false);
    Debug.Print($"[{DateTime.UtcNow}] => RESPONSE TEXT: {Utils.FormatMultiLine(text)}");
    return text;
  }

  public static void ResultOf(Task task) => task.ConfigureAwait(false).GetAwaiter().GetResult();

  public static T ResultOf<T>(Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();

}

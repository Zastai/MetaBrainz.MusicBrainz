using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;

namespace MetaBrainz.MusicBrainz;

/// <summary>An error reported by the MusicBrainz web service.</summary>
[Serializable]
public sealed class QueryException : Exception {

  /// <summary>The HTTP status code for the exception.</summary>
  public readonly HttpStatusCode Code;

  /// <summary>The reason phrase for the exception, if available.</summary>
  public readonly string? Reason;

  /// <summary>Creates a new query exception.</summary>
  /// <param name="code">The HTTP message code for the error.</param>
  /// <param name="reason">The reason phrase for the error.</param>
  /// <param name="message">A further error message.</param>
  /// <param name="cause">The exception that caused the error (if any).</param>
  public QueryException(HttpStatusCode code, string? reason = null, string? message = null,
                        Exception? cause = null) : base(message ?? reason, cause) {
    this.Code = code;
    this.Reason = reason;
  }

  /// <summary>Creates a new <see cref="QueryException"/> instance using the specified message and cause.</summary>
  /// <param name="message">The message for the exception.</param>
  /// <param name="cause">The exception that caused this one to be created.</param>
  public QueryException(string message, Exception? cause = null) : base(message, cause) {
    this.Code = HttpStatusCode.BadRequest;
    this.Reason = "Bad Request";
  }

  #region Creation Based on HTTP Response

  /// <summary>Creates a <see cref="QueryException"/> based on error information from an HTTP response.</summary>
  /// <param name="response">The response to get error information from.</param>
  /// <returns>A newly created <see cref="QueryException"/> based on <paramref name="response"/>.</returns>
  public static QueryException FromResponse(HttpResponseMessage response)
    => AsyncUtils.ResultOf(QueryException.FromResponseAsync(response));

  /// <summary>Creates a <see cref="QueryException"/> based on error information from an HTTP response.</summary>
  /// <param name="response">The response to get error information from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A newly created <see cref="QueryException"/> based on <paramref name="response"/>.</returns>
  public static async Task<QueryException> FromResponseAsync(HttpResponseMessage response,
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

  #endregion

  #region ISerializable

  /// <inheritdoc />
  public QueryException(SerializationInfo info, StreamingContext context) : base(info, context) {
    this.Code = (HttpStatusCode) info.GetInt32("query:code");
    this.Reason = info.GetString("query:reason") ?? "???";
  }

  /// <inheritdoc />
  public override void GetObjectData(SerializationInfo info, StreamingContext context) {
    base.GetObjectData(info, context);
    info.AddValue("query:code", (int) this.Code);
    info.AddValue("query:reason", this.Reason);
  }

  #endregion

}

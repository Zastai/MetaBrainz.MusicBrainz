using System;
using System.Net;
using System.Runtime.Serialization;

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

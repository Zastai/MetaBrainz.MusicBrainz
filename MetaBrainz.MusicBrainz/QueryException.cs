using System;

namespace MetaBrainz.MusicBrainz {

  /// <summary>An error reported by the MusicBrainz web service.</summary>
  [Serializable]
  public sealed class QueryException : Exception {

    /// <summary>Creates a new <see cref="QueryException"/> instance using the specified message.</summary>
    /// <param name="message">The message for the exception.</param>
    public QueryException(string message) : base(message) { }

    /// <summary>Creates a new <see cref="QueryException"/> instance using the specified message and cause.</summary>
    /// <param name="message">The message for the exception.</param>
    /// <param name="cause">The exception that caused this one to be created.</param>
    public QueryException(string message, Exception cause) : base(message, cause) { }

  }

}

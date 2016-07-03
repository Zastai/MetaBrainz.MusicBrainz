using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz {

  /// <summary>An exception raised by a MusicBrainz web service query.</summary>
  [Serializable]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed class QueryException : Exception {

    /// <summary>Creates a new <see cref="QueryException"/> instance using the specified message.</summary>
    /// <param name="message">The message for the exception.</param>
    public QueryException(string message) : base(message) { }

    /// <summary>Creates a new <see cref="QueryException"/> instance using the specified message and cause.</summary>
    /// <param name="message">The message for the exception.</param>
    /// <param name="cause">The exception that caused this one to be created.</param>
    public QueryException(string message, Exception cause) : base(message, cause) { }

    /// <summary>Creates a new <see cref="QueryException"/> instance based on the specified "raw" <see cref="WebException"/>.</summary>
    /// <param name="exception">The exception to interpret as a MusicBrainz web service query error.</param>
    public QueryException(WebException exception) : base(QueryException.GetErrorInfo(exception)?.Message ?? exception.Message, exception) { }

    private static ErrorInfo GetErrorInfo(WebException exception) {
      try {
        using (var stream = exception?.Response?.GetResponseStream()) {
          if (stream != null)
            return (ErrorInfo) new XmlSerializer(typeof(ErrorInfo)).Deserialize(stream);
        }
      }
      catch { }
      return null;
    }

    /// <summary>Class representing an error response from the MusicBrainz web service.</summary>
    [Serializable]
    [XmlRoot("error")]
    public sealed class ErrorInfo {

      /// <summary>The lines of text for the error.</summary>
      [XmlElement("text")] public string[] Lines;

      /// <summary>The message for the error (i.e. <see cref="Lines"/> as a single multi-line string).</summary>
      public string Message => string.Join(Environment.NewLine, this.Lines);

#if DEBUG

      /// <summary>Any attributes found on the <c>&lt;error&gt;</c> element (none are expected).</summary>
      [XmlAnyAttribute]
      public XmlAttribute[] Attributes;

      /// <summary>Any elements (other than <c>&lt;text&gt;</c>) found below the <c>&lt;error&gt;</c> element (none are expected).</summary>
      [XmlAnyElement]
      public XmlElement[] OtherElements;

#endif

    }

  }

}

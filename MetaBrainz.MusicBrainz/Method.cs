using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Enumeration of the supported request methods.</summary>  
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  internal enum Method {
    /// <summary>HTTP DELETE: Delete a resource.</summary>
    DELETE,
    /// <summary>HTTP GET: Request data from a resource.</summary>
    GET,
    /// <summary>HTTP POST: Submit data to a resource.</summary>
    POST,
    /// <summary>HTTP PUT: Upload data representing a resource.</summary>
    PUT,
  }

}

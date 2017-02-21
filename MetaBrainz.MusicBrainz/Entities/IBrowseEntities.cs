using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

#if NETFX_GE_4_5
using System.Threading.Tasks;
#endif

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>The result of a browse request for a specific entity type.</summary>
  /// <typeparam name="T">The type of entity being browsed.</typeparam>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IBrowseEntities<T> where T : IEntity {

    /// <summary>
    ///   The maximum number of results to be returned from a single web request for this browse (i.e. the maximum number of elements in <see cref="Results"/>).
    ///   Valid range is 1-100; if not specifically set, the server's default (normally 25) is used.
    /// </summary>
    /// <remarks>Setting this only affects further web requests made via calls to <see cref="Next()"/> and/or <see cref="Previous()"/>.</remarks>
    int? Limit { get; set; }

    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the next set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>This browse request (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    IBrowseEntities<T> Next();

#if NETFX_GE_4_5
    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the next set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>An asynchronous task returning this browse request (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    Task<IBrowseEntities<T>> NextAsync();
#endif

    /// <summary>
    ///   The offset to use for the next request (via <see cref="Next()"/> and/or <see cref="Previous()"/>), or null to continue where the current results end.
    /// </summary>
    /// <remarks>This is reset to null when a request is made, so when set to a specific value, that value is only used once.</remarks>
    int? NextOffset { get; set; }

    /// <summary>The starting offset within the total set of matches for the current result set.</summary>
    int Offset { get; }

    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the previous set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>This browse request (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    IBrowseEntities<T> Previous();

#if NETFX_GE_4_5
    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the previous set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>An asynchronous task returning this browse request (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    Task<IBrowseEntities<T>> PreviousAsync();
#endif

    /// <summary>The current results of the browse.</summary>
#if NETFX_LT_4_5
    IEnumerable<T> Results { get; }
#else
    IReadOnlyList<T> Results { get; }
#endif

    /// <summary>The total number of matches for the browse request.</summary>
    int TotalResults { get; }

  }

}

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>The results for a query that supports paging (i.e. search or browse), returned one page at a time.</summary>
/// <typeparam name="TResults">The specific type of query results.</typeparam>
/// <typeparam name="TItem">The type of item being returned.</typeparam>
[PublicAPI]
public interface IPagedQueryResults<TResults, out TItem> : IJsonBasedObject
where TResults : IPagedQueryResults<TResults, TItem> {

  /// <summary>
  /// The maximum number of results to be returned from a single web request (i.e. the maximum number of elements in
  /// <see cref="Results"/>).<br/>
  /// Valid range is 1-100; if not specifically set, the server's default (normally 25) is used.
  /// </summary>
  /// <remarks>
  /// Setting this only affects further web requests made via calls to <see cref="Next()"/> and/or <see cref="Previous()"/>.
  /// </remarks>
  int? Limit { get; set; }

  /// <summary>
  /// Queries the MusicBrainz server (using the same <see cref="Query"/> object used for the original request) for the next set
  /// of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
  /// </summary>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  TResults Next();

  /// <summary>
  /// Queries the MusicBrainz server (using the same <see cref="Query"/> object used for the original request) for the next set
  /// of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
  /// </summary>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  Task<TResults> NextAsync();

  /// <summary>
  /// The offset to use for the next request (via <see cref="Next()"/> and/or <see cref="Previous()"/>), or <see langword="null"/>
  /// to continue where the current results end.
  /// </summary>
  /// <remarks>
  /// This is reset to <see langword="null"/> when a request is made, so when set to a specific value, that value is only used once.
  /// </remarks>
  int? NextOffset { get; set; }

  /// <summary>The starting offset within the total set of matches for the current result set.</summary>
  int Offset { get; }

  /// <summary>
  /// Queries the MusicBrainz server (using the same <see cref="Query"/> object used for the original request) for the previous set
  /// of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
  /// </summary>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  TResults Previous();

  /// <summary>
  /// Queries the MusicBrainz server (using the same <see cref="Query"/> object used for the original request) for the previous set
  /// of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
  /// </summary>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  Task<TResults> PreviousAsync();

  /// <summary>The current results.</summary>
  IReadOnlyList<TItem> Results { get; }

  /// <summary>The total number of matches.</summary>
  int TotalResults { get; }

}

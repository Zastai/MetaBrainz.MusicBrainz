using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.Common;
using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>The results for a query that supports paging (i.e. search or browse), returned one page at a time.</summary>
/// <typeparam name="TResults">The specific type of query results.</typeparam>
/// <typeparam name="TItem">The type of item being returned.</typeparam>
[PublicAPI]
public interface IPagedQueryResults<TResults, out TItem> : IJsonBasedObject where TResults : IPagedQueryResults<TResults, TItem> {

  /// <summary>Gets the streaming form of this set of query results.</summary>
  /// <returns>
  /// The streaming form of this set of query results, suitable for enumeration (e.g. by <c>foreach</c> or <c>await foreach</c>).
  /// </returns>
  /// <remarks>
  /// Enumerating the return value is equivalent to enumerating this result set and then calling <see cref="NextAsync"/> and
  /// processing those results, and so on until there are no more results to process.<br/>
  /// Operating on this set of paged results while the streaming form is in use is not recommended, because it will interfere with
  /// the streaming enumeration.
  /// </remarks>
  IStreamingQueryResults<TItem> AsStream();

  /// <summary>Indicates whether these results are active (i.e. at least one request has been issued for them).</summary>
  internal bool IsActive { get; }

  /// <summary>
  /// The maximum number of results to be returned from a single web request (i.e. the maximum number of elements in
  /// <see cref="Results"/>).<br/>
  /// Valid range is 1-100; if not specifically set, the server's default (normally 25) is used.
  /// </summary>
  /// <remarks>
  /// Setting this only affects further web requests made via calls to <see cref="NextAsync"/> and/or <see cref="PreviousAsync"/>.
  /// </remarks>
  int? Limit { get; set; }

  /// <summary>
  /// Queries the MusicBrainz server (using the same <see cref="Query"/> object used for the original request) for the next set
  /// of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  Task<TResults> NextAsync(CancellationToken cancellationToken = default);

  /// <summary>
  /// The offset to use for the next request (via <see cref="NextAsync"/> and/or <see cref="PreviousAsync"/>), or
  /// <see langword="null"/> to continue where the current results end.
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
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>This result set (with updated values).</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  Task<TResults> PreviousAsync(CancellationToken cancellationToken = default);

  /// <summary>The current results.</summary>
  IReadOnlyList<TItem> Results { get; }

  /// <summary>The total number of matches.</summary>
  int TotalResults { get; }

}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal abstract class SearchResults<T> : PagedQueryResults<ISearchResults<ISearchResult<T>>, ISearchResult<T>, RawResults>,
                                           ISearchResults<ISearchResult<T>> {

  protected SearchResults(Query query, string endpoint, string queryString, int? limit, int? offset, bool simple,
                          Func<RawResults?, IReadOnlyList<ISearchResult<T>>?> get) : base(query, endpoint, null, limit, offset) {
    this._get = get;
    this._options["query"] = Uri.EscapeDataString(queryString);
    if (simple) {
      this._options["dismax"] = "true";
    }
  }

  private readonly Func<RawResults?, IReadOnlyList<ISearchResult<T>>?> _get;

  private readonly Dictionary<string, string> _options = new();

  public DateTimeOffset? Created => this.CurrentResult?.Created;

  protected sealed override async Task<ISearchResults<ISearchResult<T>>> DeserializeAsync(HttpResponseMessage response,
                                                                                          CancellationToken cancellationToken) {
    var task = JsonUtils.GetJsonContentAsync<RawResults>(response, Query.JsonReaderOptions, cancellationToken);
    this.CurrentResult = await task.ConfigureAwait(false);
    if (this.Offset != this.CurrentResult.Offset) {
      Query.TraceSource.TraceEvent(TraceEventType.Verbose, 200, "Unexpected offset in search results: {0} != {1}.", this.Offset,
                                   this.CurrentResult.Offset);
    }
    return this;
  }

  protected sealed override IReadOnlyDictionary<string, string> FullOptions() {
    if (this.Offset > 0) {
      this._options["offset"] = this.Offset.ToString(CultureInfo.InvariantCulture);
    }
    else {
      this._options.Remove("offset");
    }
    if (this.Limit is not null) {
      this._options["limit"] = this.Limit.Value.ToString(CultureInfo.InvariantCulture);
    }
    else {
      this._options.Remove("limit");
    }
    return this._options;
  }

  public sealed override IReadOnlyList<ISearchResult<T>> Results => this._get.Invoke(this.CurrentResult) ?? [];

  public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

  public sealed override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}

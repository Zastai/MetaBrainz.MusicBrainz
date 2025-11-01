using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseResults<T>(Query query, string endpoint, string? value, ReadOnlyQueryOptions? options, int? limit,
                                         int? offset, Func<RawResults?, IReadOnlyList<T>?> getter)
  : PagedQueryResults<IBrowseResults<T>, T, RawResults>(query, endpoint, value, limit, offset), IBrowseResults<T>
  where T : IEntity {

  private readonly QueryOptions _options = options is null ? [] : new QueryOptions(options);

  protected sealed override async Task<IBrowseResults<T>> DeserializeAsync(HttpResponseMessage response,
                                                                           CancellationToken cancellationToken) {
    var task = JsonUtils.GetJsonContentAsync<RawResults>(response, Query.JsonReaderOptions, cancellationToken);
    this.CurrentResult = await task.ConfigureAwait(false);
    return this;
  }

  protected sealed override ReadOnlyQueryOptions FullOptions() {
    if (this.Offset > 0) {
      this._options["offset"] = [ this.Offset.ToString(CultureInfo.InvariantCulture) ];
    }
    else {
      this._options.Remove("limit");
    }
    if (this.Limit is not null) {
      this._options["limit"] = [ this.Limit.Value.ToString(CultureInfo.InvariantCulture) ];
    }
    else {
      this._options.Remove("limit");
    }
    return this._options;
  }

  public sealed override IReadOnlyList<T> Results => getter.Invoke(this.CurrentResult) ?? [];

  public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

  public sealed override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}

using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseResults<T>(Query query, string endpoint, string? value, IReadOnlyDictionary<string, string>? options,
                                         int? limit = null, int? offset = null)
  : PagedQueryResults<IBrowseResults<T>, T, BrowseResult>(query, endpoint, value, limit, offset), IBrowseResults<T>
  where T : IEntity {

  private readonly Dictionary<string, string> _options = options is null ? [] : new Dictionary<string, string>(options);

  protected sealed override async Task<IBrowseResults<T>> DeserializeAsync(HttpResponseMessage response,
                                                                           CancellationToken cancellationToken) {
    var task = JsonUtils.GetJsonContentAsync<BrowseResult>(response, Query.JsonReaderOptions, cancellationToken);
    this.CurrentResult = await task.ConfigureAwait(false);
    return this;
  }

  protected sealed override IReadOnlyDictionary<string, string> FullOptions() {
    this._options["offset"] = this.Offset.ToString(CultureInfo.InvariantCulture);
    if (this.Limit is not null) {
      this._options["limit"] = this.Limit.Value.ToString(CultureInfo.InvariantCulture);
    }
    else {
      this._options.Remove("limit");
    }
    return this._options;
  }

  public override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}

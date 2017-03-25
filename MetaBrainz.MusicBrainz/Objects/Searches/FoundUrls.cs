using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  using Interface = ISearchResults<IFoundUrl>;
  #if NETFX_LT_4_5
  using Results   = IEnumerable<IFoundUrl>;
  #else
  using Results   = IReadOnlyList<IFoundUrl>;
  #endif

  internal sealed partial class FoundUrls : SearchResults<IFoundUrl> {

    public FoundUrls(Query query, string queryString, int? limit = null, int? offset = null) : base(query, "url", queryString, limit, offset) { }

    public override DateTime? Created => this._currentResult?.created;

    public override Results Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    public override Interface Next() {
      var json = base.NextResponse(this._currentResult?.results.Length ?? 0);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override Interface Previous() {
      var json = base.PreviousResponse();
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("urls",    Required = Required.Always)] public Url[]    results;
      [JsonProperty("count",   Required = Required.Always)] public int      count;
      [JsonProperty("created", Required = Required.Always)] public DateTime created;
      [JsonProperty("offset",  Required = Required.Always)] public int      offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

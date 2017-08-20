using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundSeries : SearchResults<IFoundSeries> {

    public FoundSeries(Query query, string queryString, int? limit = null, int? offset = null) : base(query, "series", queryString, limit, offset) { }

    public override DateTime? Created => this._currentResult?.created;

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override ISearchResults<IFoundSeries> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override IReadOnlyList<IFoundSeries> Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("count",   Required = Required.Always)]       public int       count;
      [JsonProperty("created", Required = Required.DisallowNull)] public DateTime? created;
      [JsonProperty("offset",  Required = Required.Always)]       public int       offset;
      [JsonProperty("series",  Required = Required.Always)]       public Series[]  results;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAreas : SearchResults<IFoundArea> {

    public FoundAreas(Query query, string queryString, int? limit = null, int? offset = null) : base(query, "area", queryString, limit, offset) { }

    public override DateTime? Created => this._currentResult?.created;

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override ISearchResults<IFoundArea> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override IReadOnlyList<IFoundArea> Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("areas",   Required = Required.Always)]       public Area[]    results;
      [JsonProperty("count",   Required = Required.Always)]       public int       count;
      [JsonProperty("created", Required = Required.DisallowNull)] public DateTime? created;
      [JsonProperty("offset",  Required = Required.Always)]       public int       offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundLabels : SearchResults<IFoundLabel> {

    public FoundLabels(Query query, string queryString, int? limit = null, int? offset = null) : base(query, "label", queryString, limit, offset) { }

    public override DateTime? Created => this._currentResult?.created;

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override ISearchResults<IFoundLabel> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override IReadOnlyList<IFoundLabel> Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("count",   Required = Required.Always)]       public int      count;
      [JsonProperty("created", Required = Required.DisallowNull)] public DateTime? created;
      [JsonProperty("labels",  Required = Required.Always)]       public Label[]  results;
      [JsonProperty("offset",  Required = Required.Always)]       public int      offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

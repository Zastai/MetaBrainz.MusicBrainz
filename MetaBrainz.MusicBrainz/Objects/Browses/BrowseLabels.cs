using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseLabels : BrowseResults<ILabel> {

    public BrowseLabels(Query query, string extra, int? limit = null, int? offset = null) : base(query, "label", null, extra, limit, offset) { }

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override IBrowseResults<ILabel> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override IReadOnlyList<ILabel> Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("labels",       Required = Required.Always)] public Label[] results;
      [JsonProperty("label-count",  Required = Required.Always)] public int     count;
      [JsonProperty("label-offset", Required = Required.Always)] public int     offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

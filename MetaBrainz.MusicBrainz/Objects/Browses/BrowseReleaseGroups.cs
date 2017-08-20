using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseReleaseGroups : BrowseResults<IReleaseGroup> {

    public BrowseReleaseGroups(Query query, string extra, int? limit = null, int? offset = null) : base(query, "release-group", null, extra, limit, offset) { }

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override IBrowseResults<IReleaseGroup> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override IReadOnlyList<IReleaseGroup> Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("release-groups",       Required = Required.Always)] public ReleaseGroup[] results;
      [JsonProperty("release-group-count",  Required = Required.Always)] public int            count;
      [JsonProperty("release-group-offset", Required = Required.Always)] public int            offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

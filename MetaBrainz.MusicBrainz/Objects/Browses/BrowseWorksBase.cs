using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal abstract class BrowseWorksBase : BrowseResults<IWork> {

    protected BrowseWorksBase(Query query, string endpoint, string value, string extra, int? limit = null, int? offset = null) : base(query, endpoint, value, extra, limit, offset) { }

    protected override int CurrentResultCount => this._currentResult?.results.Length ?? 0;

    protected override IBrowseResults<IWork> Deserialize(string json) {
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public sealed override IReadOnlyList<IWork> Results => this._currentResult?.results;

    public sealed override int TotalResults => this._currentResult?.count ?? 0;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("works",       Required = Required.Always)] public Work[] results;
      [JsonProperty("work-count",  Required = Required.Always)] public int    count;
      [JsonProperty("work-offset", Required = Required.Always)] public int    offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}

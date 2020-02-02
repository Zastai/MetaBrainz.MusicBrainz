using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal abstract class BrowseWorksBase : BrowseResults<IWork, BrowseWorksBase.JSON> {

    protected BrowseWorksBase(Query query, string endpoint, string value, string extra, int? limit = null, int? offset = null)
    : base(query, endpoint, value, extra, limit, offset) {
    }

    public sealed override IReadOnlyList<IWork> Results => this.CurrentResult?.Results;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("works")]
      public Work[] Results { get; set; }

      [JsonPropertyName("work-count")]
      public override int Count { get; set; }

      [JsonPropertyName("work-offset")]
      public override int Offset { get; set; }

    }

  }

}

using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseReleaseGroups : BrowseResults<IReleaseGroup, BrowseReleaseGroups.JSON> {

    public BrowseReleaseGroups(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "release-group", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IReleaseGroup> Results => this.CurrentResult?.Results;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("release-groups")]
      public ReleaseGroup[] Results { get; set; }

      [JsonPropertyName("release-group-count")]
      public override int Count { get; set; }

      [JsonPropertyName("release-group-offset")]
      public override int Offset { get; set; }

    }

  }

}

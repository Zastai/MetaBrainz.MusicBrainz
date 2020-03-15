using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseLabels : BrowseResults<ILabel, BrowseLabels.JSON> {

    public BrowseLabels(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "label", null, extra, limit, offset) {
    }

    public override IReadOnlyList<ILabel> Results => this.CurrentResult?.Results ?? Array.Empty<ILabel>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("labels")]
      public Label[]? Results { get; set; }

      [JsonPropertyName("label-count")]
      public override int Count { get; set; }

      [JsonPropertyName("label-offset")]
      public override int Offset { get; set; }

    }

  }

}

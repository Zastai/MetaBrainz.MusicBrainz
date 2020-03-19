using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseAreas : BrowseResults<IArea, BrowseAreas.JSON> {

    public BrowseAreas(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "area", null, extra, limit, offset)
    { }

    public override IReadOnlyList<IArea> Results => this.CurrentResult?.Results ?? Array.Empty<IArea>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("areas")]
      public Area[]? Results { get; set; }

      [JsonPropertyName("area-count")]
      public override int Count { get; set; }

      [JsonPropertyName("area-offset")]
      public override int Offset { get; set; }

    }

  }

}

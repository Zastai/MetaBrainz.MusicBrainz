using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowsePlaces : BrowseResults<IPlace, BrowsePlaces.JSON> {

    public BrowsePlaces(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "place", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IPlace> Results => this.CurrentResult?.Results ?? Array.Empty<IPlace>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("places")]
      public Place[]? Results { get; set; }

      [JsonPropertyName("place-count")]
      public override int Count { get; set; }

      [JsonPropertyName("place-offset")]
      public override int Offset { get; set; }

    }

  }

}

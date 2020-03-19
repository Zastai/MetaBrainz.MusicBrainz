using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseArtists : BrowseResults<IArtist, BrowseArtists.JSON> {

    public BrowseArtists(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "artist", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IArtist> Results => this.CurrentResult?.Results ?? Array.Empty<IArtist>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("artists")]
      public Artist[]? Results { get; set; }

      [JsonPropertyName("artist-count")]
      public override int Count { get; set; }

      [JsonPropertyName("artist-offset")]
      public override int Offset { get; set; }

    }

  }

}

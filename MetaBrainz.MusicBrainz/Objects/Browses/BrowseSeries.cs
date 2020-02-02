using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseSeries : BrowseResults<ISeries, BrowseSeries.JSON> {

    public BrowseSeries(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "series", null, extra, limit, offset) {
    }

    public override IReadOnlyList<ISeries> Results => this.CurrentResult?.Results;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("series")]
      public Series[] Results { get; set; }

      [JsonPropertyName("series-count")]
      public override int Count { get; set; }

      [JsonPropertyName("series-offset")]
      public override int Offset { get; set; }

    }

  }

}

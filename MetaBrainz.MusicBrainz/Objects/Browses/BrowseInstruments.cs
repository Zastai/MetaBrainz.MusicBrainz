using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseInstruments : BrowseResults<IInstrument, BrowseInstruments.JSON> {

    public BrowseInstruments(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "instrument", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IInstrument> Results => this.CurrentResult?.Results;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("instruments")]
      public Instrument[] Results { get; set; }

      [JsonPropertyName("instrument-count")]
      public override int Count { get; set; }

      [JsonPropertyName("instrument-offset")]
      public override int Offset { get; set; }

    }

  }

}

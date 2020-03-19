using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseRecordings : BrowseResults<IRecording, BrowseRecordings.JSON> {

    public BrowseRecordings(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "recording", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IRecording> Results => this.CurrentResult?.Results ?? Array.Empty<IRecording>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("recordings")]
      public Recording[]? Results { get; set; }

      [JsonPropertyName("recording-count")]
      public override int Count { get; set; }

      [JsonPropertyName("recording-offset")]
      public override int Offset { get; set; }

    }

  }

}

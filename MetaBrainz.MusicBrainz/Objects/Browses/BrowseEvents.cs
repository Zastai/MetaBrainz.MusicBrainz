using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseEvents : BrowseResults<IEvent, BrowseEvents.JSON> {

    public BrowseEvents(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "event", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IEvent> Results => this.CurrentResult?.Results ?? Array.Empty<IEvent>();

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("events")]
      public Event[]? Results { get; set; }

      [JsonPropertyName("event-count")]
      public override int Count { get; set; }

      [JsonPropertyName("event-offset")]
      public override int Offset { get; set; }

    }

  }

}

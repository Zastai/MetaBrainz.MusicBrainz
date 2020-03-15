using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Url : Entity, IFoundUrl {

    public override EntityType EntityType => EntityType.Url;

    public IReadOnlyList<IRelationship>? Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[]? TheRelationships { get; set; }

    [JsonPropertyName("resource")]
    public Uri? Resource { get; set; }

    public override string ToString() => this.Resource?.ToString() ?? string.Empty;

  }

}

using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal abstract class Entity : SearchResult, IEntity {

    public abstract EntityType EntityType { get; }

    [JsonPropertyName("id")]
    public Guid MbId { get; set; }


  }

}

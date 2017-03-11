using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_LT_4_5
  using RelationshipList = IEnumerable<IRelationship>;
  #else
  using RelationshipList = IReadOnlyList<IRelationship>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Url : IUrl {

    public EntityType EntityType => EntityType.Url;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    [JsonProperty("resource", Required = Required.Always)]
    public Uri Resource { get; private set; }

    public override string ToString() => this.Resource.ToString();

  }

}

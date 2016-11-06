using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Url : IUrl {

    public EntityType EntityType => EntityType.Url;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations")]
    private Relationship[] _relationships = null;

    [JsonProperty("resource", Required = Required.Always)]
    public Uri Resource { get; private set; }

    public override string ToString() => this.Resource.ToString();

  }

}

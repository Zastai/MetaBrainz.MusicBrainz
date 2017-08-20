using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Url : SearchResult, IFoundUrl {

    public EntityType EntityType => EntityType.Url;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    [JsonProperty("resource", Required = Required.Always)]
    public Uri Resource { get; private set; }

    public override string ToString() => this.Resource.ToString();

  }

}

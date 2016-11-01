using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Url : IUrl {

    public EntityType EntityType => EntityType.Url;

    public Guid MbId => this._json.id;

    public IEnumerable<IRelationship> Relationships => this._json.relations.WrapArray(ref this._relationships, j => new Relationship(j));

    private Relationship[] _relationships;

    public Uri Resource => this._json.resource;

    #region JSON-Based Construction

    internal Url(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty] public Relationship.JSON[] relations;
      [JsonProperty(Required = Required.Always)] public Uri resource;
    }

    #endregion

  }

}

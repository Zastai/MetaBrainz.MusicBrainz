using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Url : IUrl {

    public string ID => this.MBID.ToString("D");

    public Guid MBID => this._json.id;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

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
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty(Required = Required.Always)] public Uri resource;
    }

    #endregion

  }

}

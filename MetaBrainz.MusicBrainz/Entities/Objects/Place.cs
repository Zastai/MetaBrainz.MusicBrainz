using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class Place : IPlace {

    public string ID => this.MBID.ToString("D");

    public Guid MBID => this._json.id;

    public string Address => this._json.address;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;

    public string Annotation => this._json.annotation;

    public IArea Area => this._json.area.WrapObject(ref this._area, j => new Area(j));

    private Area _area;

    public ICoordinates Coordinates => this._json.coordinates.WrapObject(ref this._coordinates, j => new Coordinates(j));

    private Coordinates _coordinates;

    public string Disambiguation => this._json.disambiguation;

    public ILifeSpan LifeSpan => this._json.lifespan.WrapObject(ref this._lifeSpan, j => new LifeSpan(j));

    private LifeSpan _lifeSpan;

    public string Name => this._json.name;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public string SortName  => this._json.sort_name;

    public IEnumerable<ITag> Tags => this._json.tags.WrapArray(ref this._tags, j => new Tag(j));

    private Tag[] _tags;

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    public IEnumerable<IUserTag> UserTags => this._json.user_tags.WrapArray(ref this._userTags, j => new UserTag(j));

    private UserTag[] _userTags;

    #region JSON-Based Construction

    internal Place(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string address;
      [JsonProperty] public Alias.JSON[] aliases;
      [JsonProperty] public string annotation;
      [JsonProperty] public Area.JSON area;
      [JsonProperty] public Coordinates.JSON coordinates;
      [JsonProperty] public string disambiguation;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty("life-span")] public LifeSpan.JSON lifespan;
      [JsonProperty(Required = Required.Always)] public string name;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty("sort-name")] public string sort_name;
      [JsonProperty] public Tag.JSON[] tags;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty("user-tags")] public UserTag.JSON[] user_tags;
    }

    #endregion

  }

}

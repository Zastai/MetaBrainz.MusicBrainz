using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Label : ILabel {

    public string Id => this.MbId.ToString("D");

    public Guid MbId => this._json.id;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;

    public string Annotation => this._json.annotation;

    public IArea Area => this._json.area.WrapObject(ref this._area, j => new Area(j));

    private Area _area;

    public string Country => this._json.country;

    public string Disambiguation => this._json.disambiguation;

    public IEnumerable<string> Ipis => this._json.ipis;

    public int? LabelCode => this._json.label_code;

    public ILifeSpan LifeSpan => this._json.lifespan.WrapObject(ref this._lifeSpan, j => new LifeSpan(j));

    private LifeSpan _lifeSpan;

    public string Name => this._json.name;

    public IRating Rating => this._json.rating.WrapObject(ref this._rating, j => new Rating(j));

    private Rating _rating;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public IEnumerable<IRelease> Releases => this._json.releases.WrapArray(ref this._releases, j => new Release(j));

    private Release[] _releases;

    public string SortName  => this._json.sort_name;

    public IEnumerable<ITag> Tags => this._json.tags.WrapArray(ref this._tags, j => new Tag(j));

    private Tag[] _tags;

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    public IUserRating UserRating => this._json.user_rating.WrapObject(ref this._userRating, j => new UserRating(j));

    private UserRating _userRating;

    public IEnumerable<IUserTag> UserTags => this._json.user_tags.WrapArray(ref this._userTags, j => new UserTag(j));

    private UserTag[] _userTags;

    #region JSON-Based Construction

    internal Label(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Alias.JSON[] aliases;
      [JsonProperty] public string annotation;
      [JsonProperty] public Area.JSON area;
      [JsonProperty] public string country;
      [JsonProperty] public string disambiguation;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty] public string[] ipis;
      [JsonProperty("label-code")] public int? label_code;
      [JsonProperty("life-span")] public LifeSpan.JSON lifespan;
      [JsonProperty(Required = Required.Always)] public string name;
      [JsonProperty] public Rating.JSON rating;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty] public Release.JSON[] releases;
      [JsonProperty("sort-name")] public string sort_name;
      [JsonProperty] public Tag.JSON[] tags;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty("user-rating")] public UserRating.JSON user_rating;
      [JsonProperty("user-tags")] public UserTag.JSON[] user_tags;
    }

    #endregion

  }

}

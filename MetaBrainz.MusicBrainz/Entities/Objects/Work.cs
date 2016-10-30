using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Work : IWork {

    public EntityType EntityType => EntityType.Work;

    public string Id => this.MbId.ToString("D");

    public Guid MbId => this._json.id;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;
    public string Annotation => this._json.annotation;

    public IEnumerable<IWorkAttribute> Attributes => this._json.attributes.WrapArray(ref this._attributes, j => new WorkAttribute(j));

    private WorkAttribute[] _attributes;

    public string Disambiguation => this._json.disambiguation;

    public IEnumerable<string> Iswcs => this._json.iswcs;

    public string Language => this._json.language;

    public IRating Rating => this._json.rating.WrapObject(ref this._rating, j => new Rating(j));

    private Rating _rating;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public IEnumerable<ITag> Tags => this._json.tags.WrapArray(ref this._tags, j => new Tag(j));

    private Tag[] _tags;

    public string Title => this._json.title;

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    public IUserRating UserRating => this._json.user_rating.WrapObject(ref this._userRating, j => new UserRating(j));

    private UserRating _userRating;

    public IEnumerable<IUserTag> UserTags => this._json.user_tags.WrapArray(ref this._userTags, j => new UserTag(j));

    private UserTag[] _userTags;

    #region JSON-Based Construction

    internal Work(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Alias.JSON[] aliases;
      [JsonProperty] public string annotation;
      [JsonProperty] public WorkAttribute.JSON[] attributes;
      [JsonProperty] public string disambiguation;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty] public string[] iswcs;
      [JsonProperty] public string language;
      [JsonProperty] public Rating.JSON rating;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty] public Tag.JSON[] tags;
      [JsonProperty(Required = Required.Always)] public string title;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty("user-rating")] public UserRating.JSON user_rating;
      [JsonProperty("user-tags")] public UserTag.JSON[] user_tags;
    }

    #endregion

  }

}

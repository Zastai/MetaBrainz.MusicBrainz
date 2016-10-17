using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class ReleaseGroup : IReleaseGroup {

    public string Id => this.MbId.ToString("D");

    public Guid MbId => this._json.id;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;

    public string Annotation => this._json.annotation;

    public IEnumerable<INameCredit> ArtistCredit => this._json.artist_credit.WrapArray(ref this._artistCredit, j => new NameCredit(j));

    private NameCredit[] _artistCredit;

    public string Disambiguation => this._json.disambiguation;

    public string FirstReleaseDate => this._json.first_release_date;

    public string PrimaryType => this._json.primary_type;

    public Guid? PrimaryTypeId => this._json.primary_type_id;

    public IRating Rating => this._json.rating.WrapObject(ref this._rating, j => new Rating(j));

    private Rating _rating;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public IEnumerable<IRelease> Releases => this._json.releases.WrapArray(ref this._releases, j => new Release(j));

    private Release[] _releases;

    public IEnumerable<string> SecondaryTypes => this._json.secondary_types;

    public IEnumerable<Guid> SecondaryTypeIds => this._json.secondary_type_ids;

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

    internal ReleaseGroup(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Alias.JSON[] aliases;
      [JsonProperty] public string annotation;
      [JsonProperty("artist-credit")] public NameCredit.JSON[] artist_credit;
      [JsonProperty] public string disambiguation;
      [JsonProperty("first-release-date")] public string first_release_date;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty("primary-type")] public string primary_type;
      [JsonProperty("primary-type-id")] public Guid? primary_type_id;
      [JsonProperty] public Rating.JSON rating;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty] public Release.JSON[] releases;
      [JsonProperty("secondary-types")] public string[] secondary_types;
      [JsonProperty("secondary-type-ids")] public Guid[] secondary_type_ids;
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Recording : IRecording {

    public string Id => this.MbId.ToString("D");

    public Guid MbId => this._json.id;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;

    public string Annotation => this._json.annotation;

    public IEnumerable<INameCredit> ArtistCredit => this._json.artist_credit.WrapArray(ref this._artistCredit, j => new NameCredit(j));

    private NameCredit[] _artistCredit;

    public string Disambiguation => this._json.disambiguation;

    public IEnumerable<string> Isrcs => this._json.isrcs;

    public int? Length => this._json.length;

    public IRating Rating => this._json.rating.WrapObject(ref this._rating, j => new Rating(j));

    private Rating _rating;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public IEnumerable<IRelease> Releases => this._json.releases.WrapArray(ref this._releases, j => new Release(j));

    private Release[] _releases;

    public IEnumerable<ITag> Tags => this._json.tags.WrapArray(ref this._tags, j => new Tag(j));

    private Tag[] _tags;

    public string Title => this._json.title;

    public IUserRating UserRating => this._json.user_rating.WrapObject(ref this._userRating, j => new UserRating(j));

    private UserRating _userRating;

    public IEnumerable<IUserTag> UserTags => this._json.user_tags.WrapArray(ref this._userTags, j => new UserTag(j));

    private UserTag[] _userTags;


    public bool? Video => this._json.video;

    #region JSON-Based Construction

    internal Recording(JSON json) {
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
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty] public string[] isrcs;
      [JsonProperty] public int? length;
      [JsonProperty] public Rating.JSON rating;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty] public Release.JSON[] releases;
      [JsonProperty] public Tag.JSON[] tags;
      [JsonProperty(Required = Required.Always)] public string title;
      [JsonProperty("user-rating")] public UserRating.JSON user_rating;
      [JsonProperty("user-tags")] public UserTag.JSON[] user_tags;
      [JsonProperty] public bool? video;
    }

    #endregion

  }

}

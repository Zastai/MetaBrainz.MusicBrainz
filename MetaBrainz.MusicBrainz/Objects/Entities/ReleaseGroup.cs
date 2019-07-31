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
  internal sealed class ReleaseGroup : SearchResult, IFoundReleaseGroup {

    public EntityType EntityType => EntityType.ReleaseGroup;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.Default)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public IReadOnlyList<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.Default)]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("disambiguation", Required = Required.Default)]
    public string Disambiguation { get; private set; }

    [JsonProperty("first-release-date", Required = Required.Default)]
    public PartialDate FirstReleaseDate { get; private set; }

    public IReadOnlyList<ITag> Genres => this._genres;

    [JsonProperty("genres", Required = Required.DisallowNull)]
    private Tag[] _genres = null;

    [JsonProperty("primary-type", Required = Required.Default)]
    public string PrimaryType { get; private set; }

    [JsonProperty("primary-type-id", Required = Required.Default)]
    public Guid? PrimaryTypeId { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.Default)]
    private Rating _rating = null;

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.Default)]
    private Relationship[] _relationships = null;

    public IReadOnlyList<IRelease> Releases => this._releases;

    [JsonProperty("releases", Required = Required.Default)]
    private Release[] _releases = null;

    [JsonProperty("secondary-types", Required = Required.Default)]
    public IReadOnlyList<string> SecondaryTypes { get; private set; }

    [JsonProperty("secondary-type-ids", Required = Required.Default)]
    public IReadOnlyList<Guid> SecondaryTypeIds { get; private set; }

    public IReadOnlyList<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.Default)]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Default)]
    public string Title { get; private set; }

    public IReadOnlyList<IUserTag> UserGenres => this._userGenres;

    [JsonProperty("user-genres", Required = Required.Default)]
    private UserTag[] _userGenres = null;

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.Default)]
    private UserRating _userRating = null;

    public IReadOnlyList<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.Default)]
    private UserTag[] _userTags = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // - the first release date is not serialized
    // - the primary type ID is not serialized
    // - the secondary types are not serialized when not top-level
    // - the secondary type IDs are not serialized
    // - the title is not serialized when not top-level
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

    public override string ToString() {
      var text = string.Empty;
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (!string.IsNullOrEmpty(this.PrimaryType))
        text += " (" + this.PrimaryType + ")";
      return text;
    }

  }

}

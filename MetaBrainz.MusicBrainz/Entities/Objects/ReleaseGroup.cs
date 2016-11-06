using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class ReleaseGroup : IReleaseGroup {

    public EntityType EntityType => EntityType.ReleaseGroup;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IEnumerable<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases")]
    private Alias[] _aliases = null;

    [JsonProperty("annotation")]
    public string Annotation { get; private set; }

    public IEnumerable<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit")]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("disambiguation")]
    public string Disambiguation { get; private set; }

    [JsonProperty("first-release-date")]
    public PartialDate FirstReleaseDate { get; private set; }

    [JsonProperty("primary-type")]
    public string PrimaryType { get; private set; }

    [JsonProperty("primary-type-id")]
    public Guid? PrimaryTypeId { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating")]
    private Rating _rating = null;

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations")]
    private Relationship[] _relationships = null;

    public IEnumerable<IRelease> Releases => this._releases;

    [JsonProperty("releases")]
    private Release[] _releases = null;

    [JsonProperty("secondary-types")]
    public IEnumerable<string> SecondaryTypes { get; private set; }

    [JsonProperty("secondary-type-ids")]
    public IEnumerable<Guid> SecondaryTypeIds { get; private set; }

    public IEnumerable<ITag> Tags => this._tags;

    [JsonProperty("tags")]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("type-id")]
    public Guid? TypeId { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating")]
    private UserRating _userRating = null;

    public IEnumerable<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags")]
    private UserTag[] _userTags = null;

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

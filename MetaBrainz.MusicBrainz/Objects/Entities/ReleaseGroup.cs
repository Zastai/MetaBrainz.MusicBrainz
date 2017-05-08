using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_GE_4_5
  using AliasList        = IReadOnlyList<IAlias>;
  using GuidList         = IReadOnlyList<Guid>;
  using NameCreditList   = IReadOnlyList<INameCredit>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using ReleaseList      = IReadOnlyList<IRelease>;
  using StringList       = IReadOnlyList<string>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  #else
  using AliasList        = IEnumerable<IAlias>;
  using GuidList         = IEnumerable<Guid>;
  using NameCreditList   = IEnumerable<INameCredit>;
  using RelationshipList = IEnumerable<IRelationship>;
  using ReleaseList      = IEnumerable<IRelease>;
  using StringList       = IEnumerable<string>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class ReleaseGroup : IReleaseGroup {

    public EntityType EntityType => EntityType.ReleaseGroup;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public NameCreditList ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.DisallowNull)]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    [JsonProperty("first-release-date", Required = Required.AllowNull)]
    public PartialDate FirstReleaseDate { get; private set; }

    [JsonProperty("primary-type", Required = Required.AllowNull)]
    public string PrimaryType { get; private set; }

    [JsonProperty("primary-type-id", Required = Required.AllowNull)]
    public Guid? PrimaryTypeId { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public ReleaseList Releases => this._releases;

    [JsonProperty("releases", Required = Required.DisallowNull)]
    private Release[] _releases = null;

    [JsonProperty("secondary-types", Required = Required.Always)]
    public StringList SecondaryTypes { get; private set; }

    [JsonProperty("secondary-type-ids", Required = Required.Always)]
    public GuidList SecondaryTypeIds { get; private set; }

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.DisallowNull)]
    private UserRating _userRating = null;

    public UserTagList UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
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

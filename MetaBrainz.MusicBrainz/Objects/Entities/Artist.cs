using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_GE_4_5
  using AliasList        = IReadOnlyList<IAlias>;
  using RecordingList    = IReadOnlyList<IRecording>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using ReleaseGroupList = IReadOnlyList<IReleaseGroup>;
  using ReleaseList      = IReadOnlyList<IRelease>;
  using StringList       = IReadOnlyList<string>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  using WorkList         = IReadOnlyList<IWork>;
  #else
  using AliasList        = IEnumerable<IAlias>;
  using RecordingList    = IEnumerable<IRecording>;
  using RelationshipList = IEnumerable<IRelationship>;
  using ReleaseGroupList = IEnumerable<IReleaseGroup>;
  using ReleaseList      = IEnumerable<IRelease>;
  using StringList       = IEnumerable<string>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  using WorkList         = IEnumerable<IWork>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Artist : IArtist {

    public EntityType EntityType => EntityType.Artist;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public IArea Area => this._area;

    [JsonProperty("area", Required = Required.Default)]
    private Area _area = null;

    public IArea BeginArea => this._beginArea;

    [JsonProperty("begin_area", Required = Required.Default)]
    private Area _beginArea = null;

    [JsonProperty("country", Required = Required.Default)]
    public string Country { get; private set; }

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    public IArea EndArea => this._endArea;

    [JsonProperty("end_area", Required = Required.Default)]
    private Area _endArea = null;

    [JsonProperty("gender", Required = Required.Default)]
    public string Gender { get; private set; }

    [JsonProperty("gender-id", Required = Required.Default)]
    public Guid? GenderId { get; private set; }

    [JsonProperty("ipis", Required = Required.DisallowNull)]
    public StringList Ipis { get; private set; }

    [JsonProperty("isnis", Required = Required.DisallowNull)]
    public StringList Isnis { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public RecordingList Recordings => this._recordings;

    [JsonProperty("recordings", Required = Required.DisallowNull)]
    private Recording[] _recordings = null;

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public ReleaseGroupList ReleaseGroups => this._releaseGroups;

    [JsonProperty("release-groups", Required = Required.DisallowNull)]
    private ReleaseGroup[] _releaseGroups = null;

    public ReleaseList Releases => this._releases;

    [JsonProperty("releases", Required = Required.DisallowNull)]
    private Release[] _releases = null;

    [JsonProperty("sort-name", Required = Required.AllowNull)]
    public string SortName { get; private set; }

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("type", Required = Required.Default)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.DisallowNull)]
    private UserRating _userRating = null;

    public UserTagList UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    public WorkList Works => this._works;

    [JsonProperty("works", Required = Required.DisallowNull)]
    private Work[] _works = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

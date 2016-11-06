using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Artist : IArtist {

    public EntityType EntityType => EntityType.Artist;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IEnumerable<IAlias> Aliases => this._aliases;

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

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    public IArea EndArea => this._endArea;

    [JsonProperty("end_area", Required = Required.Default)]
    private Area _endArea = null;

    [JsonProperty("gender", Required = Required.Default)]
    public string Gender { get; private set; }

    [JsonProperty("gender-id", Required = Required.Default)]
    public Guid? GenderId { get; private set; }

    [JsonProperty("ipis", Required = Required.DisallowNull)]
    public IEnumerable<string> Ipis { get; private set; }

    [JsonProperty("isnis", Required = Required.DisallowNull)]
    public IEnumerable<string> Isnis { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public IEnumerable<IRecording> Recordings => this._recordings;

    [JsonProperty("recordings", Required = Required.DisallowNull)]
    private Recording[] _recordings = null;

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public IEnumerable<IReleaseGroup> ReleaseGroups => this._releaseGroups;

    [JsonProperty("release-groups", Required = Required.DisallowNull)]
    private ReleaseGroup[] _releaseGroups = null;

    public IEnumerable<IRelease> Releases => this._releases;

    [JsonProperty("releases", Required = Required.DisallowNull)]
    private Release[] _releases = null;

    [JsonProperty("sort-name", Required = Required.AllowNull)]
    public string SortName { get; private set; }

    public IEnumerable<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("type", Required = Required.Default)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.DisallowNull)]
    private UserRating _userRating = null;

    public IEnumerable<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    public IEnumerable<IWork> Works => this._works;

    [JsonProperty("works", Required = Required.DisallowNull)]
    private Work[] _works = null;

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

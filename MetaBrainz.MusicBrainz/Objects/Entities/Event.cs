using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_GE_4_5
  using AliasList        = IReadOnlyList<IAlias>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  #else
  using AliasList        = IEnumerable<IAlias>;
  using RelationshipList = IEnumerable<IRelationship>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Event : SearchResult, IFoundEvent {

    public EntityType EntityType => EntityType.Event;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public bool Cancelled => this._cancelled.GetValueOrDefault();

    [JsonProperty("cancelled", Required = Required.DisallowNull)]
    private bool? _cancelled = null;

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    [JsonProperty("setlist", Required = Required.DisallowNull)]
    public string Setlist { get; private set; }

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("time", Required = Required.DisallowNull)]
    public string Time { get; private set; }

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

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // - the setlist is not serialized when not set (instead of being serialized as an empty string)
    // - the time is not serialized when not set (instead of being serialized as an empty string)
    // - the type is not serialized when not set (instead of being serialized as null)
    // - the type ID is not serialized
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // - the Cancelled flag is not serialized when not set
    // => Adjusted the Required flags for the property (to allow its omission), and added a nullable backing field.

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

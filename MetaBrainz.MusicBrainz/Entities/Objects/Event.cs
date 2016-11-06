using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Event : IEvent {

    public EntityType EntityType => EntityType.Event;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IEnumerable<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases")]
    private Alias[] _aliases = null;

    [JsonProperty("annotation")]
    public string Annotation { get; private set; }

    [JsonProperty("cancelled")]
    public bool? Cancelled { get; private set; }

    [JsonProperty("disambiguation")]
    public string Disambiguation { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span")]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating")]
    private Rating _rating = null;

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations")]
    private Relationship[] _relationships = null;

    [JsonProperty("setlist")]
    public string Setlist { get; private set; }

    [JsonProperty("sort-name")]
    public string SortName { get; private set; }

    public IEnumerable<ITag> Tags => this._tags;

    [JsonProperty("tags")]
    private Tag[] _tags = null;

    [JsonProperty("time")]
    public string Time { get; private set; }

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
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

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
  internal sealed class Work : SearchResult, IFoundWork {

    public EntityType EntityType => EntityType.Work;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.Default)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public IReadOnlyList<IWorkAttribute> Attributes => this._attributes;

    [JsonProperty("attributes", Required = Required.Default)]
    private WorkAttribute[] _attributes = null;

    [JsonProperty("disambiguation", Required = Required.Default)]
    public string Disambiguation { get; private set; }

    public IReadOnlyList<ITag> Genres => this._genres;

    [JsonProperty("genres", Required = Required.DisallowNull)]
    private Tag[] _genres = null;

    [JsonProperty("iswcs", Required = Required.Default)]
    public IReadOnlyList<string> Iswcs { get; private set; }

    [JsonProperty("language", Required = Required.Default)]
    public string Language { get; private set; }

    [JsonProperty("languages", Required = Required.Default)]
    public IReadOnlyList<string> Languages { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.Default)]
    private Rating _rating = null;

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.Default)]
    private Relationship[] _relationships = null;

    public IReadOnlyList<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.Default)]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    [JsonProperty("type", Required = Required.Default)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

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
    // - the attributes are not serialized when not set (instead of being serialized as an empty array)
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // - the ISWC list is not serialized
    // - the language is not serialized when not set (instead of being serialized as null)
    // - the language list is not serialized
    // - the type and type ID are not serialized
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

    public override string ToString() {
      var text = this.Title ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Artist : Entity, IFoundArtist {

    public override EntityType EntityType => EntityType.Artist;

    [JsonConverter(typeof(JsonInterfaceListConverter<IAlias, Alias>))]
    [JsonPropertyName("aliases")]
    public IReadOnlyList<IAlias>? Aliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IArea, Area>))]
    [JsonPropertyName("area")]
    public IArea? Area { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IArea, Area>))]
    [JsonPropertyName("begin-area")]
    public IArea? BeginArea { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IArea, Area>))]
    [JsonPropertyName("end-area")]
    public IArea? EndArea { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("gender-id")]
    public Guid? GenderId { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("genres")]
    public IReadOnlyList<ITag>? Genres { get; set; }

    [JsonPropertyName("ipis")]
    public IReadOnlyList<string>? Ipis { get; set; }

    [JsonPropertyName("isnis")]
    public IReadOnlyList<string>? Isnis { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<ILifeSpan, LifeSpan>))]
    [JsonPropertyName("life-span")]
    public ILifeSpan? LifeSpan { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IRating, Rating>))]
    [JsonPropertyName("rating")]
    public IRating? Rating { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IRecording, Recording>))]
    [JsonPropertyName("recordings")]
    public IReadOnlyList<IRecording>? Recordings { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IRelationship, Relationship>))]
    [JsonPropertyName("relations")]
    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IReleaseGroup, ReleaseGroup>))]
    [JsonPropertyName("release-groups")]
    public IReadOnlyList<IReleaseGroup>? ReleaseGroups { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IRelease, Release>))]
    [JsonPropertyName("releases")]
    public IReadOnlyList<IRelease>? Releases { get; set; }

    [JsonPropertyName("sort-name")]
    public string? SortName { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("tags")]
    public IReadOnlyList<ITag>? Tags { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-genres")]
    public IReadOnlyList<IUserTag>? UserGenres { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IUserRating, UserRating>))]
    [JsonPropertyName("user-rating")]
    public IUserRating? UserRating { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-tags")]
    public IReadOnlyList<IUserTag>? UserTags { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IWork, Work>))]
    [JsonPropertyName("works")]
    public IReadOnlyList<IWork>? Works { get; set; }

    #region Compatibility

    // MBS-10072: begin-area is now used everywhere, but it used to be begin_area
    [JsonConverter(typeof(JsonInterfaceConverter<IArea, Area>))]
    [JsonPropertyName("begin_area")]
    [Obsolete("To be removed when the server stops sending it.")]
    public IArea? OldBeginArea { set => this.BeginArea = value; }

    // MBS-10072: end-area is now used everywhere, but it used to be end_area
    [JsonConverter(typeof(JsonInterfaceConverter<IArea, Area>))]
    [JsonPropertyName("end_area")]
    [Obsolete("To be removed when the server stops sending it.")]
    public IArea? OldEndArea { set => this.EndArea = value; }

    #endregion

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      if (this.Name != null)
        text += this.Name;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      if (this.Type != null)
        text += $" ({this.Type})";
      return text;
    }

  }

}

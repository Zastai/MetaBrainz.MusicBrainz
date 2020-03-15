using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Label : Entity, IFoundLabel {

    public override EntityType EntityType => EntityType.Label;

    public IReadOnlyList<IAlias>? Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[]? TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    public IArea? Area => this.TheArea;

    [JsonPropertyName("area")]
    public Area? TheArea { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    public IReadOnlyList<ITag>? Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[]? TheGenres { get; set; }

    [JsonPropertyName("ipis")]
    public IReadOnlyList<string>? Ipis { get; set; }

    [JsonPropertyName("isnis")]
    public IReadOnlyList<string>? Isnis { get; set; }

    [JsonPropertyName("label-code")]
    public int? LabelCode { get; set; }

    public ILifeSpan? LifeSpan => this.TheLifeSpan;

    [JsonPropertyName("life-span")]
    public LifeSpan? TheLifeSpan { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public IRating? Rating => this.TheRating;

    [JsonPropertyName("rating")]
    public Rating? TheRating { get; set; }

    public IReadOnlyList<IRelationship>? Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[]? TheRelationships { get; set; }

    public IReadOnlyList<IRelease>? Releases => this.TheReleases;

    [JsonPropertyName("releases")]
    public Release[]? TheReleases { get; set; }

    // The name is serialized as 'sort-name' too, probably for historical reasons.
    [JsonPropertyName("sort-name")]
    public string? SortName { get; set; }

    public IReadOnlyList<ITag>? Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[]? TheTags { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public IReadOnlyList<IUserTag>? UserGenres => this.TheUserGenres;

    [JsonPropertyName("user-genres")]
    public UserTag[]? TheUserGenres { get; set; }

    public IUserRating? UserRating => this.TheUserRating;

    [JsonPropertyName("user-rating")]
    public UserRating? TheUserRating { get; set; }

    public IReadOnlyList<IUserTag>? UserTags => this.TheUserTags;

    [JsonPropertyName("user-tags")]
    public UserTag[]? TheUserTags { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      if (this.Name != null)
        text += this.Name;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

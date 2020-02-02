using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Work : Entity, IFoundWork {

    public override EntityType EntityType => EntityType.Work;

    public IReadOnlyList<IAlias> Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[] TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string Annotation { get; set; }

    public IReadOnlyList<IWorkAttribute> Attributes => this.TheAttributes;

    [JsonPropertyName("attributes")]
    public WorkAttribute[] TheAttributes { get; set; }

    [JsonPropertyName("disambiguation")]
    public string Disambiguation { get; set; }

    public IReadOnlyList<ITag> Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[] TheGenres { get; set; }

    [JsonPropertyName("iswcs")]
    public IReadOnlyList<string> Iswcs { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("languages")]
    public IReadOnlyList<string> Languages { get; set; }

    public IRating Rating => this.TheRating;

    [JsonPropertyName("rating")]
    public Rating TheRating { get; set; }

    public IReadOnlyList<IRelationship> Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[] TheRelationships { get; set; }

    public IReadOnlyList<ITag> Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[] TheTags { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public IReadOnlyList<IUserTag> UserGenres => this.TheUserGenres;

    [JsonPropertyName("user-genres")]
    public UserTag[] TheUserGenres { get; set; }

    public IUserRating UserRating => this.TheUserRating;

    [JsonPropertyName("user-rating")]
    public UserRating TheUserRating { get; set; }

    public IReadOnlyList<IUserTag> UserTags => this.TheUserTags;

    [JsonPropertyName("user-tags")]
    public UserTag[] TheUserTags { get; set; }

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

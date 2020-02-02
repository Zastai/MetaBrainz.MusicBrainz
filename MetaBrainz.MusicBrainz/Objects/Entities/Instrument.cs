using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Instrument : Entity, IFoundInstrument {

    public override EntityType EntityType => EntityType.Instrument;

    public IReadOnlyList<IAlias> Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[] TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string Annotation { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("disambiguation")]
    public string Disambiguation { get; set; }

    public IReadOnlyList<ITag> Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[] TheGenres { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public IReadOnlyList<IRelationship> Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[] TheRelationships { get; set; }

    public IReadOnlyList<ITag> Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[] TheTags { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public IReadOnlyList<IUserTag> UserGenres => this.TheUserGenres;

    [JsonPropertyName("user-genres")]
    public UserTag[] TheUserGenres { get; set; }

    public IReadOnlyList<IUserTag> UserTags => this.TheUserTags;

    [JsonPropertyName("user-tags")]
    public UserTag[] TheUserTags { get; set; }

    public override string ToString() {
      var text = this.SearchScore.HasValue ? $"[Score: {this.SearchScore.Value}] " : "";
      text += this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

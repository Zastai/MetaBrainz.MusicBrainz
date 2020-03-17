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

    [JsonPropertyName("aliases")]
    public IReadOnlyList<IAlias>? Aliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    [JsonPropertyName("genres")]
    public IReadOnlyList<ITag>? Genres { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("relations")]
    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    [JsonPropertyName("tags")]
    public IReadOnlyList<ITag>? Tags { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    [JsonPropertyName("user-genres")]
    public IReadOnlyList<IUserTag>? UserGenres { get; set; }

    [JsonPropertyName("user-tags")]
    public IReadOnlyList<IUserTag>? UserTags { get; set; }

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

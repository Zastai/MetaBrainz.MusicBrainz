using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Release : Entity, IFoundRelease {

    public override EntityType EntityType => EntityType.Release;

    public IReadOnlyList<IAlias>? Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[]? TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    public IReadOnlyList<INameCredit>? ArtistCredit => this.TheArtistCredit;

    [JsonPropertyName("artist-credit")]
    public NameCredit[]? TheArtistCredit { get; set; }

    [JsonPropertyName("asin")]
    public string? Asin { get; set; }

    [JsonPropertyName("barcode")]
    public string? BarCode { get; set; }

    public IReadOnlyList<ICollection>? Collections => this.TheCollections;

    [JsonPropertyName("collections")]
    public Collection[]? TheCollections { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    public ICoverArtArchive? CoverArtArchive => this.TheCoverArtArchive;

    [JsonPropertyName("cover-art-archive")]
    public CoverArtArchive? TheCoverArtArchive { get; set; }

    [JsonPropertyName("date")]
    public PartialDate? Date { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    public IReadOnlyList<ITag>? Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[]? TheGenres { get; set; }

    public IReadOnlyList<ILabelInfo>? LabelInfo => this.TheLabelInfo;

    [JsonPropertyName("label-info")]
    public LabelInfo[]? TheLabelInfo { get; set; }

    public IReadOnlyList<IMedium>? Media => this.TheMedia;

    [JsonPropertyName("media")]
    public Medium[]? TheMedia { get; set; }

    [JsonPropertyName("packaging")]
    public string? Packaging { get; set; }

    [JsonPropertyName("packaging-id")]
    public Guid? PackagingId { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    public IReadOnlyList<IRelationship>? Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[]? TheRelationships { get; set; }

    public IReadOnlyList<IReleaseEvent>? ReleaseEvents => this.TheReleaseEvents;

    [JsonPropertyName("release-events")]
    public ReleaseEvent[]? TheReleaseEvents { get; set; }

    public IReleaseGroup? ReleaseGroup => this.TheReleaseGroup;

    [JsonPropertyName("release-group")]
    public ReleaseGroup? TheReleaseGroup { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status-id")]
    public Guid? StatusId { get; set; }

    public IReadOnlyList<ITag>? Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[]? TheTags { get; set; }

    public ITextRepresentation? TextRepresentation => this.TheTextRepresentation;

    [JsonPropertyName("text-representation")]
    public TextRepresentation? TheTextRepresentation { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    public IReadOnlyList<IUserTag>? UserGenres => this.TheUserGenres;

    [JsonPropertyName("user-genres")]
    public UserTag[]? TheUserGenres { get; set; }

    public IReadOnlyList<IUserTag>? UserTags => this.TheUserTags;

    [JsonPropertyName("user-tags")]
    public UserTag[]? TheUserTags { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      return text;
    }

  }

}

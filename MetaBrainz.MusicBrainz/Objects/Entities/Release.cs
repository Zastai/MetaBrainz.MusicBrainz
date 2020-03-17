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

    [JsonConverter(typeof(JsonInterfaceListConverter<IAlias, Alias>))]
    [JsonPropertyName("aliases")]
    public IReadOnlyList<IAlias>? Aliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<INameCredit, NameCredit>))]
    [JsonPropertyName("artist-credit")]
    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    [JsonPropertyName("asin")]
    public string? Asin { get; set; }

    [JsonPropertyName("barcode")]
    public string? BarCode { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ICollection, Collection>))]
    [JsonPropertyName("collections")]
    public IReadOnlyList<ICollection>? Collections { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<ICoverArtArchive, CoverArtArchive>))]
    [JsonPropertyName("cover-art-archive")]
    public ICoverArtArchive? CoverArtArchive { get; set; }

    [JsonPropertyName("date")]
    public PartialDate? Date { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("genres")]
    public IReadOnlyList<ITag>? Genres { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ILabelInfo, LabelInfo>))]
    [JsonPropertyName("label-info")]
    public IReadOnlyList<ILabelInfo>? LabelInfo { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IMedium, Medium>))]
    [JsonPropertyName("media")]
    public IReadOnlyList<IMedium>? Media { get; set; }

    [JsonPropertyName("packaging")]
    public string? Packaging { get; set; }

    [JsonPropertyName("packaging-id")]
    public Guid? PackagingId { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IRelationship, Relationship>))]
    [JsonPropertyName("relations")]
    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IReleaseEvent, ReleaseEvent>))]
    [JsonPropertyName("release-events")]
    public IReadOnlyList<IReleaseEvent>? ReleaseEvents { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<IReleaseGroup, ReleaseGroup>))]
    [JsonPropertyName("release-group")]
    public IReleaseGroup? ReleaseGroup { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status-id")]
    public Guid? StatusId { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("tags")]
    public IReadOnlyList<ITag>? Tags { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<ITextRepresentation, TextRepresentation>))]
    [JsonPropertyName("text-representation")]
    public ITextRepresentation? TextRepresentation { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-genres")]
    public IReadOnlyList<IUserTag>? UserGenres { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-tags")]
    public IReadOnlyList<IUserTag>? UserTags { get; set; }

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

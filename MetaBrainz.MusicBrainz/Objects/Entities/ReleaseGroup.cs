using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class ReleaseGroup : Entity, IFoundReleaseGroup {

    public override EntityType EntityType => EntityType.ReleaseGroup;

    public IReadOnlyList<IAlias> Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[] TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string Annotation { get; set; }

    public IReadOnlyList<INameCredit> ArtistCredit => this.TheArtistCredit;

    [JsonPropertyName("artist-credit")]
    public NameCredit[] TheArtistCredit { get; set; }

    [JsonPropertyName("disambiguation")]
    public string Disambiguation { get; set; }

    [JsonPropertyName("first-release-date")]
    public PartialDate FirstReleaseDate { get; set; }

    public IReadOnlyList<ITag> Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[] TheGenres { get; set; }

    [JsonPropertyName("primary-type")]
    public string PrimaryType { get; set; }

    [JsonPropertyName("primary-type-id")]
    public Guid? PrimaryTypeId { get; set; }

    public IRating Rating => this.TheRating;

    [JsonPropertyName("rating")]
    public Rating TheRating { get; set; }

    public IReadOnlyList<IRelationship> Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[] TheRelationships { get; set; }

    public IReadOnlyList<IRelease> Releases => this.TheReleases;

    [JsonPropertyName("releases")]
    public Release[] TheReleases { get; set; }

    [JsonPropertyName("secondary-types")]
    public IReadOnlyList<string> SecondaryTypes { get; set; }

    [JsonPropertyName("secondary-type-ids")]
    public IReadOnlyList<Guid> SecondaryTypeIds { get; set; }

    public IReadOnlyList<ITag> Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[] TheTags { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

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
      var text = string.Empty;
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (!string.IsNullOrEmpty(this.PrimaryType))
        text += " (" + this.PrimaryType + ")";
      return text;
    }

  }

}

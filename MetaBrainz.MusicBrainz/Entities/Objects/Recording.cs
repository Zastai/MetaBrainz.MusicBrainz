using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Recording : IRecording {

    public EntityType EntityType => EntityType.Recording;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IEnumerable<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases")]
    private Alias[] _aliases = null;

    [JsonProperty("annotation")]
    public string Annotation { get; private set; }

    public IEnumerable<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit")]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("disambiguation")]
    public string Disambiguation { get; private set; }

    [JsonProperty("isrcs")]
    public IEnumerable<string> Isrcs { get; private set; }

    [JsonProperty("length")]
    public int? Length { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating")]
    private Rating _rating = null;

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations")]
    private Relationship[] _relationships = null;

    public IEnumerable<IRelease> Releases => this._releases;

    [JsonProperty("releases")]
    private Release[] _releases = null;

    public IEnumerable<ITag> Tags => this._tags;

    [JsonProperty("tags")]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating")]
    private UserRating _userRating = null;

    public IEnumerable<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags")]
    private UserTag[] _userTags = null;

    [JsonProperty("video")]
    public bool? Video { get; private set; }

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
      if (this.Length.HasValue)
        text += $" ({new TimeSpan(0, 0, 0, 0, this.Length.Value)})";
      return text;
    }

  }

}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Release : IRelease {

    public EntityType EntityType => EntityType.Release;

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

    [JsonProperty("asin")]
    public string Asin { get; private set; }

    [JsonProperty("barcode")]
    public string BarCode { get; private set; }

    public IEnumerable<ICollection> Collections => this._collections;

    [JsonProperty("collections")]
    private Collection[] _collections = null;

    [JsonProperty("country")]
    public string Country { get; private set; }

    public ICoverArtArchive CoverArtArchive => this._coverArtArchive;

    [JsonProperty("cover-art-archive")]
    private CoverArtArchive _coverArtArchive = null;

    [JsonProperty("date")]
    public PartialDate Date { get; private set; }

    [JsonProperty("disambiguation")]
    public string Disambiguation { get; private set; }

    public IEnumerable<ILabelInfo> LabelInfo => this._labelInfo;

    [JsonProperty("label-info")]
    private LabelInfo[] _labelInfo = null;

    public IEnumerable<IMedium> Media => this._media;

    [JsonProperty("media")]
    private Medium[] _media = null;

    [JsonProperty("packaging")]
    public string Packaging { get; private set; }

    [JsonProperty("packaging-id")]
    public Guid? PackagingId { get; private set; }

    [JsonProperty("quality")]
    public string Quality { get; private set; }

    public IEnumerable<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations")]
    private Relationship[] _relationships = null;

    public IEnumerable<IReleaseEvent> ReleaseEvents => this._releaseEvents;

    [JsonProperty("release-events")]
    private ReleaseEvent[] _releaseEvents = null;

    public IReleaseGroup ReleaseGroup => this._releaseGroup;

    [JsonProperty("release-group")]
    private ReleaseGroup _releaseGroup = null;

    [JsonProperty("status")]
    public string Status { get; private set; }

    [JsonProperty("status-id")]
    public Guid? StatusId { get; private set; }

    public IEnumerable<ITag> Tags => this._tags;

    [JsonProperty("tags")]
    private Tag[] _tags = null;

    public ITextRepresentation TextRepresentation => this._textRepresentation;

    [JsonProperty("text-representation")]
    private TextRepresentation _textRepresentation = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public IEnumerable<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags")]
    private UserTag[] _userTags = null;

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
      return text;
    }

  }

}

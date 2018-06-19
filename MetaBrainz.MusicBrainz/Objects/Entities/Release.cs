using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Release : SearchResult, IFoundRelease {

    public EntityType EntityType => EntityType.Release;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public IReadOnlyList<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.Default)]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("asin", Required = Required.Default)]
    public string Asin { get; private set; }

    [JsonProperty("barcode", Required = Required.Default)]
    public string BarCode { get; private set; }

    public IReadOnlyList<ICollection> Collections => this._collections;

    [JsonProperty("collections", Required = Required.DisallowNull)]
    private Collection[] _collections = null;

    [JsonProperty("country", Required = Required.Default)]
    public string Country { get; private set; }

    public ICoverArtArchive CoverArtArchive => this._coverArtArchive;

    [JsonProperty("cover-art-archive", Required = Required.DisallowNull)]
    private CoverArtArchive _coverArtArchive = null;

    [JsonProperty("date", Required = Required.Default)]
    public PartialDate Date { get; private set; }

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    public IReadOnlyList<ILabelInfo> LabelInfo => this._labelInfo;

    [JsonProperty("label-info", Required = Required.DisallowNull)]
    private LabelInfo[] _labelInfo = null;

    public IReadOnlyList<IMedium> Media => this._media;

    [JsonProperty("media", Required = Required.DisallowNull)]
    private Medium[] _media = null;

    [JsonProperty("packaging", Required = Required.Default)]
    public string Packaging { get; private set; }

    [JsonProperty("packaging-id", Required = Required.Default)]
    public Guid? PackagingId { get; private set; }

    [JsonProperty("quality", Required = Required.DisallowNull)]
    public string Quality { get; private set; }

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public IReadOnlyList<IReleaseEvent> ReleaseEvents => this._releaseEvents;

    [JsonProperty("release-events", Required = Required.DisallowNull)]
    private ReleaseEvent[] _releaseEvents = null;

    public IReleaseGroup ReleaseGroup => this._releaseGroup;

    [JsonProperty("release-group", Required = Required.Default)]
    private ReleaseGroup _releaseGroup = null;

    [JsonProperty("status", Required = Required.AllowNull)]
    public string Status { get; private set; }

    [JsonProperty("status-id", Required = Required.Default)]
    public Guid? StatusId { get; private set; }

    public IReadOnlyList<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    public ITextRepresentation TextRepresentation => this._textRepresentation;

    [JsonProperty("text-representation", Required = Required.DisallowNull)]
    private TextRepresentation _textRepresentation = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public IReadOnlyList<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the barcode is not always serialized (but sometimes serialized as empty string)
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // - the packaging is not serialized when not set (instead of being serialized as an empty string)
    // - the packaging ID is not serialized
    // - the quality is not serialized
    // - the status ID is not serialized
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

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

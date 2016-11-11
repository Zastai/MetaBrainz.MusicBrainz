using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  #if NETFX_LT_4_5
  using AliasList        = IEnumerable<IAlias>;
  using CollectionList   = IEnumerable<ICollection>;
  using LabelInfoList    = IEnumerable<ILabelInfo>;
  using MediumList       = IEnumerable<IMedium>;
  using NameCreditList   = IEnumerable<INameCredit>;
  using RelationshipList = IEnumerable<IRelationship>;
  using ReleaseEventList = IEnumerable<IReleaseEvent>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  #else
  using AliasList        = IReadOnlyList<IAlias>;
  using CollectionList   = IReadOnlyList<ICollection>;
  using LabelInfoList    = IReadOnlyList<ILabelInfo>;
  using MediumList       = IReadOnlyList<IMedium>;
  using NameCreditList   = IReadOnlyList<INameCredit>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using ReleaseEventList = IReadOnlyList<IReleaseEvent>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Release : IRelease {

    public EntityType EntityType => EntityType.Release;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public NameCreditList ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.DisallowNull)]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("asin", Required = Required.Default)]
    public string Asin { get; private set; }

    [JsonProperty("barcode", Required = Required.AllowNull)]
    public string BarCode { get; private set; }

    public CollectionList Collections => this._collections;

    [JsonProperty("collections", Required = Required.DisallowNull)]
    private Collection[] _collections = null;

    [JsonProperty("country", Required = Required.Default)]
    public string Country { get; private set; }

    public ICoverArtArchive CoverArtArchive => this._coverArtArchive;

    [JsonProperty("cover-art-archive", Required = Required.DisallowNull)]
    private CoverArtArchive _coverArtArchive = null;

    [JsonProperty("date", Required = Required.Default)]
    public PartialDate Date { get; private set; }

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    public LabelInfoList LabelInfo => this._labelInfo;

    [JsonProperty("label-info", Required = Required.DisallowNull)]
    private LabelInfo[] _labelInfo = null;

    public MediumList Media => this._media;

    [JsonProperty("media", Required = Required.DisallowNull)]
    private Medium[] _media = null;

    [JsonProperty("packaging", Required = Required.AllowNull)]
    public string Packaging { get; private set; }

    [JsonProperty("packaging-id", Required = Required.AllowNull)]
    public Guid? PackagingId { get; private set; }

    [JsonProperty("quality", Required = Required.Always)]
    public string Quality { get; private set; }

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public ReleaseEventList ReleaseEvents => this._releaseEvents;

    [JsonProperty("release-events", Required = Required.DisallowNull)]
    private ReleaseEvent[] _releaseEvents = null;

    public IReleaseGroup ReleaseGroup => this._releaseGroup;

    [JsonProperty("release-group", Required = Required.Default)]
    private ReleaseGroup _releaseGroup = null;

    [JsonProperty("status", Required = Required.AllowNull)]
    public string Status { get; private set; }

    [JsonProperty("status-id", Required = Required.AllowNull)]
    public Guid? StatusId { get; private set; }

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    public ITextRepresentation TextRepresentation => this._textRepresentation;

    [JsonProperty("text-representation", Required = Required.Always)]
    private TextRepresentation _textRepresentation = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public UserTagList UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
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

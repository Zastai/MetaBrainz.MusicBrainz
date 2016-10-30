using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Release : IRelease {

    public EntityType EntityType => EntityType.Release;

    public string Id => this.MbId.ToString("D");

    public Guid MbId => this._json.id;

    public IEnumerable<IAlias> Aliases => this._json.aliases.WrapArray(ref this._aliases, j => new Alias(j));

    private Alias[] _aliases;

    public string Annotation => this._json.annotation;

    public IEnumerable<INameCredit> ArtistCredit => this._json.artist_credit.WrapArray(ref this._artistCredit, j => new NameCredit(j));

    private NameCredit[] _artistCredit;

    public string Asin => this._json.asin;

    public string BarCode => this._json.barcode;

    public IEnumerable<ICollection> Collections => this._json.collections.WrapArray(ref this._collections, j => new Collection(j));

    private Collection[] _collections;

    public string Country => this._json.country;

    public ICoverArtArchive CoverArtArchive => this._json.cover_art_archive.WrapObject(ref this._coverArtArchive, j => new CoverArtArchive(j));

    private CoverArtArchive _coverArtArchive;

    public string Date => this._json.date;

    public string Disambiguation => this._json.disambiguation;

    public IEnumerable<ILabelInfo> LabelInfo => this._json.label_info.WrapArray(ref this._labelInfo, j => new LabelInfo(j));

    private LabelInfo[] _labelInfo;

    public IEnumerable<IMedium> Media => this._json.media.WrapArray(ref this._media, j => new Medium(j));

    private Medium[] _media;

    public string Packaging => this._json.packaging;

    public Guid? PackagingId => this._json.packaging_id;

    public string Quality => this._json.quality;

    public IEnumerable<IRelation> Relations => this._json.relations.WrapArray(ref this._relations, j => new Relation(j));

    private Relation[] _relations;

    public IEnumerable<IReleaseEvent> ReleaseEvents => this._json.release_events.WrapArray(ref this._releaseEvents, j => new ReleaseEvent(j));

    private ReleaseEvent[] _releaseEvents;

    public IReleaseGroup ReleaseGroup => this._json.release_group.WrapObject(ref this._releaseGroup, j => new ReleaseGroup(j));

    private ReleaseGroup _releaseGroup;

    public string Status => this._json.status;

    public Guid? StatusId => this._json.status_id;

    public IEnumerable<ITag> Tags => this._json.tags.WrapArray(ref this._tags, j => new Tag(j));

    private Tag[] _tags;

    public ITextRepresentation TextRepresentation => this._json.text_representation.WrapObject(ref this._textRepresentation, j => new TextRepresentation(j));

    private TextRepresentation _textRepresentation;

    public string Title => this._json.title;

    public IEnumerable<IUserTag> UserTags => this._json.user_tags.WrapArray(ref this._userTags, j => new UserTag(j));

    private UserTag[] _userTags;

    #region JSON-Based Construction

    internal Release(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Alias.JSON[] aliases;
      [JsonProperty] public string annotation;
      [JsonProperty] public string asin;
      [JsonProperty] public string barcode;
      [JsonProperty("cover-art-archive")] public CoverArtArchive.JSON cover_art_archive;
      [JsonProperty] public string date;
      [JsonProperty("artist-credit")] public NameCredit.JSON[] artist_credit;
      [JsonProperty] public Collection.JSON[] collections;
      [JsonProperty] public string country;
      [JsonProperty] public string disambiguation;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty("label-info")] public LabelInfo.JSON[] label_info;
      [JsonProperty] public Medium.JSON[] media;
      [JsonProperty] public string packaging;
      [JsonProperty("packaging-id")] public Guid? packaging_id;
      [JsonProperty] public string quality;
      [JsonProperty] public Relation.JSON[] relations;
      [JsonProperty("release-events")] public ReleaseEvent.JSON[] release_events;
      [JsonProperty("release-group")] public ReleaseGroup.JSON release_group;
      [JsonProperty] public string status;
      [JsonProperty("status-id")] public Guid? status_id;
      [JsonProperty("text-representation")] public TextRepresentation.JSON text_representation;
      [JsonProperty(Required = Required.Always)] public string title;
      [JsonProperty] public Tag.JSON[] tags;
      [JsonProperty("user-tags")] public UserTag.JSON[] user_tags;
    }

    #endregion

  }

}

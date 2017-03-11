using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Collection : ICollection {

    public EntityType EntityType => EntityType.Collection;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    [JsonProperty("editor", Required = Required.Always)]
    public string Editor { get; private set; }

    public EntityType ContentType => this._entityType ?? HelperMethods.SetFrom(out this._entityType, this.ContentTypeText);

    private EntityType? _entityType;

    [JsonProperty("entity-type", Required = Required.Always)]
    public string ContentTypeText { get; private set; }

    public int ItemCount => this._areaCount
                          + this._artistCount
                          + this._eventCount
                          + this._instrumentCount
                          + this._labelCount
                          + this._placeCount
                          + this._recordingCount
                          + this._releaseCount
                          + this._releaseGroupCount
                          + this._seriesCount
                          + this._workCount;

    [JsonProperty("area-count",          Required = Required.DisallowNull)] private int _areaCount         = 0;
    [JsonProperty("artist-count",        Required = Required.DisallowNull)] private int _artistCount       = 0;
    [JsonProperty("event-count",         Required = Required.DisallowNull)] private int _eventCount        = 0;
    [JsonProperty("instrument-count",    Required = Required.DisallowNull)] private int _instrumentCount   = 0;
    [JsonProperty("label-count",         Required = Required.DisallowNull)] private int _labelCount        = 0;
    [JsonProperty("place-count",         Required = Required.DisallowNull)] private int _placeCount        = 0;
    [JsonProperty("recording-count",     Required = Required.DisallowNull)] private int _recordingCount    = 0;
    [JsonProperty("release-count",       Required = Required.DisallowNull)] private int _releaseCount      = 0;
    [JsonProperty("release-group-count", Required = Required.DisallowNull)] private int _releaseGroupCount = 0;
    [JsonProperty("series-count",        Required = Required.DisallowNull)] private int _seriesCount       = 0;
    [JsonProperty("work-count",          Required = Required.DisallowNull)] private int _workCount         = 0;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Always)]
    public Guid? TypeId { get; private set; }

    public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

  }

}

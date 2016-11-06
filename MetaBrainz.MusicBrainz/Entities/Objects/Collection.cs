using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Collection : ICollection {

    public EntityType EntityType => EntityType.Collection;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    [JsonProperty("editor")]
    public string Editor { get; private set; }

    public EntityType ContentType => this._entityType ?? HelperMethods.SetFrom(out this._entityType, this.ContentTypeText);

    private EntityType? _entityType;

    [JsonProperty("entity-type")]
    public string ContentTypeText { get; private set; }

    public int ItemCount => this._areaCount
                          + this._artistCount
                          + this._eventCount
                          + this._instrumentCount
                          + this._labelCount
                          + this._placeCount
                          + this._recordingCount
                          + this._releaseCount
                          + this._release_groupCount
                          + this._seriesCount
                          + this._workCount;

    [JsonProperty("area-count")]          private int _areaCount          = 0;
    [JsonProperty("artist-count")]        private int _artistCount        = 0;
    [JsonProperty("event-count")]         private int _eventCount         = 0;
    [JsonProperty("instrument-count")]    private int _instrumentCount    = 0;
    [JsonProperty("label-count")]         private int _labelCount         = 0;
    [JsonProperty("place-count")]         private int _placeCount         = 0;
    [JsonProperty("recording-count")]     private int _recordingCount     = 0;
    [JsonProperty("release-count")]       private int _releaseCount       = 0;
    [JsonProperty("release-group-count")] private int _release_groupCount = 0;
    [JsonProperty("series-count")]        private int _seriesCount        = 0;
    [JsonProperty("work-count")]          private int _workCount          = 0;

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("type-id")]
    public Guid? TypeId { get; private set; }

    public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

  }

}

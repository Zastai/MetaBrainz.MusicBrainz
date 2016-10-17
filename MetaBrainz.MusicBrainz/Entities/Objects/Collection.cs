using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Collection : ICollection {

    public string ID => this.MBID.ToString("D");

    public Guid MBID => this._json.id;

    public string Editor => this._json.editor;

    public string EntityType => this._json.entity_type;

    public int ItemCount =>   this._json.area_count
                            + this._json.artist_count
                            + this._json.event_count
                            + this._json.instrument_count
                            + this._json.label_count
                            + this._json.place_count
                            + this._json.recording_count
                            + this._json.release_count
                            + this._json.release_group_count
                            + this._json.series_count
                            + this._json.work_count;

    public string Name => this._json.name;

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    #region JSON-Based Construction

    internal Collection(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty("area-count")] public int area_count;
      [JsonProperty("artist-count")] public int artist_count;
      [JsonProperty] public string editor;
      [JsonProperty("entity-type")] public string entity_type;
      [JsonProperty("event-count")] public int event_count;
      [JsonProperty(Required = Required.Always)] public Guid id;
      [JsonProperty("instrument-count")] public int instrument_count;
      [JsonProperty("label-count")] public int label_count;
      [JsonProperty] public string name;
      [JsonProperty("place-count")] public int place_count;
      [JsonProperty("recording-count")] public int recording_count;
      [JsonProperty("release-count")] public int release_count;
      [JsonProperty("release-group-count")] public int release_group_count;
      [JsonProperty("series-count")] public int series_count;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty("work-count")] public int work_count;
    }

    #endregion

  }

}

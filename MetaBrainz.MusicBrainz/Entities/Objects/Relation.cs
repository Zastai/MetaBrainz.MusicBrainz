using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Relation : IRelation {

    public IEnumerable<string> Attributes => this._json.attributes;

    public IDictionary<string, string> AttributeValues => this._json.attribute_values;

    public string Begin => this._json.begin;

    public string Direction => this._json.direction;

    public string End => this._json.end;

    public bool? Ended => this._json.ended;

    public IMbEntity Item {
      get {
        if (this._item != null)
          return this._item;
        switch (this._json.target_type) {
          case "area":          return this._json.area         .WrapObject(ref this._item, j => new Area        (j));
          case "artist":        return this._json.artist       .WrapObject(ref this._item, j => new Artist      (j));
          case "event":         return this._json.event_       .WrapObject(ref this._item, j => new Event       (j));
          case "instrument":    return this._json.instrument   .WrapObject(ref this._item, j => new Instrument  (j));
          case "label":         return this._json.label        .WrapObject(ref this._item, j => new Label       (j));
          case "place":         return this._json.place        .WrapObject(ref this._item, j => new Place       (j));
          case "recording":     return this._json.recording    .WrapObject(ref this._item, j => new Recording   (j));
          case "release":       return this._json.release      .WrapObject(ref this._item, j => new Release     (j));
          case "release_group": return this._json.release_group.WrapObject(ref this._item, j => new ReleaseGroup(j));
          case "series":        return this._json.series       .WrapObject(ref this._item, j => new Series      (j));
          case "url":           return this._json.url          .WrapObject(ref this._item, j => new Url         (j));
          case "work":          return this._json.work         .WrapObject(ref this._item, j => new Work        (j));
        }
        return null;
      }
    }

    private IMbEntity _item;

    public int? OrderingKey => this._json.ordering_key;

    public string SourceCredit => this._json.source_credit;

    public string TargetCredit => this._json.target_credit;

    public string TargetType => this._json.target_type;

    public string Type => this._json.type;

    public Guid? TypeId  => this._json.type_id;

    #region JSON-Based Construction

    internal Relation(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Area.JSON area;
      [JsonProperty] public Artist.JSON artist;
      [JsonProperty] public string[] attributes;
      [JsonProperty("attribute-values")] public Dictionary<string, string> attribute_values;
      [JsonProperty] public string begin;
      [JsonProperty(Required = Required.Always)] public string direction;
      [JsonProperty] public string end;
      [JsonProperty] public bool? ended;
      [JsonProperty("event")] public Event.JSON event_;
      [JsonProperty] public Instrument.JSON instrument;
      [JsonProperty] public Label.JSON label;
      [JsonProperty("ordering-key")] public int? ordering_key;
      [JsonProperty] public Place.JSON place;
      [JsonProperty] public Recording.JSON recording;
      [JsonProperty] public Release.JSON release;
      [JsonProperty] public ReleaseGroup.JSON release_group;
      [JsonProperty] public Series.JSON series;
      [JsonProperty("source-credit")] public string source_credit;
      [JsonProperty("target-credit")] public string target_credit;
      [JsonProperty("target-type", Required = Required.Always)] public string target_type;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty] public Url.JSON url;
      [JsonProperty] public Work.JSON work;
    }

    #endregion

  }

}

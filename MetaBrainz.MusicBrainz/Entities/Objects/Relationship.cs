using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Relationship : IRelationship {

    [JsonProperty("area")]
    private Area _area = null;

    [JsonProperty("artist")]
    private Artist _artist = null;

    [JsonProperty("attributes")]
    public IEnumerable<string> Attributes { get; private set; }

    [JsonProperty("attribute-credits")]
    public IDictionary<string, string> AttributeCredits { get; private set; }

    [JsonProperty("attribute-values")]
    public IDictionary<string, string> AttributeValues { get; private set; }

    [JsonProperty("begin")]
    public PartialDate Begin { get; private set; }

    [JsonProperty("direction")]
    public string Direction { get; private set; }

    [JsonProperty("end")]
    public PartialDate End { get; private set; }

    [JsonProperty("ended")]
    public bool Ended { get; private set; }

    [JsonProperty("event")]
    private Event _event = null;

    [JsonProperty("instrument")]
    private Instrument _instrument = null;

    [JsonProperty("label")]
    private Label _label= null;

    [JsonProperty("ordering-key")]
    public int? OrderingKey { get; private set; }

    [JsonProperty("place")]
    private Place _place = null;

    [JsonProperty("recording")]
    private Recording _recording = null;

    [JsonProperty("release")]
    private Release _release = null;

    [JsonProperty("release_group")]
    private ReleaseGroup _releaseGroup = null;

    [JsonProperty("series")]
    private Series _series = null;

    [JsonProperty("source-credit")]
    public string SourceCredit { get; private set; }

    public IRelatableEntity Target {
      get {
        switch (this.TargetType) {
          case EntityType.Area:         return this._area;
          case EntityType.Artist:       return this._artist;
          case EntityType.Event:        return this._event;
          case EntityType.Instrument:   return this._instrument;
          case EntityType.Label:        return this._label;
          case EntityType.Place:        return this._place;
          case EntityType.Recording:    return this._recording;
          case EntityType.Release:      return this._release;
          case EntityType.ReleaseGroup: return this._releaseGroup;
          case EntityType.Series:       return this._series;
          case EntityType.Url:          return this._url;
          case EntityType.Work:         return this._work;
          default:                      return null;
        }
      }
    }

    [JsonProperty("target-credit")]
    public string TargetCredit { get; private set; }

    public EntityType TargetType => this._targetType ?? HelperMethods.SetFrom(out this._targetType, this.TargetTypeText);

    private EntityType? _targetType;

    [JsonProperty("target-type", Required = Required.Always)]
    public string TargetTypeText { get; private set; }

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("type-id")]
    public Guid? TypeId { get; private set; }

    [JsonProperty("url")]
    private Url _url = null;

    [JsonProperty("work")]
    private Work _work = null;

    public override string ToString() => $"{this.Type} → {this.TargetType}: {this.Target}";

  }

}

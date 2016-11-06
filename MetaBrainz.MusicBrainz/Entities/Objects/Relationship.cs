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

    public IArea Area => this._area;

    [JsonProperty("area", Required = Required.DisallowNull)]
    private Area _area = null;

    public IArtist Artist => this._artist;

    [JsonProperty("artist", Required = Required.DisallowNull)]
    private Artist _artist = null;

    [JsonProperty("attributes", Required = Required.Always)]
    public IEnumerable<string> Attributes { get; private set; }

    [JsonProperty("attribute-credits", Required = Required.DisallowNull)]
    public IDictionary<string, string> AttributeCredits { get; private set; }

    [JsonProperty("attribute-values", Required = Required.Always)]
    public IDictionary<string, string> AttributeValues { get; private set; }

    [JsonProperty("begin", Required = Required.AllowNull)]
    public PartialDate Begin { get; private set; }

    [JsonProperty("direction", Required = Required.Always)]
    public string Direction { get; private set; }

    [JsonProperty("end", Required = Required.AllowNull)]
    public PartialDate End { get; private set; }

    [JsonProperty("ended", Required = Required.Always)]
    public bool Ended { get; private set; }

    public IEvent Event => this._event;

    [JsonProperty("event", Required = Required.DisallowNull)]
    private Event _event = null;

    public IInstrument Instrument => this._instrument;

    [JsonProperty("instrument", Required = Required.DisallowNull)]
    private Instrument _instrument = null;

    public ILabel Label => this._label;

    [JsonProperty("label", Required = Required.DisallowNull)]
    private Label _label= null;

    [JsonProperty("ordering-key", Required = Required.DisallowNull)]
    public int? OrderingKey { get; private set; }

    public IPlace Place => this._place;

    [JsonProperty("place", Required = Required.DisallowNull)]
    private Place _place = null;

    public IRecording Recording => this._recording;

    [JsonProperty("recording", Required = Required.DisallowNull)]
    private Recording _recording = null;

    public IRelease Release => this._release;

    [JsonProperty("release", Required = Required.DisallowNull)]
    private Release _release = null;

    public IReleaseGroup ReleaseGroup => this._releaseGroup;

    [JsonProperty("release_group", Required = Required.DisallowNull)]
    private ReleaseGroup _releaseGroup = null;

    public ISeries Series => this._series;

    [JsonProperty("series", Required = Required.DisallowNull)]
    private Series _series = null;

    [JsonProperty("source-credit", Required = Required.Always)]
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

    [JsonProperty("target-credit", Required = Required.Always)]
    public string TargetCredit { get; private set; }

    public EntityType TargetType => this._targetType ?? HelperMethods.SetFrom(out this._targetType, this.TargetTypeText);

    private EntityType? _targetType;

    [JsonProperty("target-type", Required = Required.Always)]
    public string TargetTypeText { get; private set; }

    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Always)]
    public Guid? TypeId { get; private set; }

    public IUrl Url => this._url;

    [JsonProperty("url", Required = Required.DisallowNull)]
    private Url _url = null;

    public IWork Work => this._work;

    [JsonProperty("work", Required = Required.DisallowNull)]
    private Work _work = null;

    public override string ToString() => $"{this.Type} → {this.TargetType}: {this.Target}";

  }

}

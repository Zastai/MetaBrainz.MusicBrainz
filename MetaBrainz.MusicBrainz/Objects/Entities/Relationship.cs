using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Relationship : JsonBasedObject, IRelationship {

    public IArea Area => this.TheArea;

    [JsonPropertyName("area")]
    public Area TheArea { get; set; }

    public IArtist Artist => this.TheArtist;

    [JsonPropertyName("artist")]
    public Artist TheArtist { get; set; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<string> Attributes { get; set; }

    [JsonPropertyName("attribute-credits")]
    public IReadOnlyDictionary<string, string> AttributeCredits { get; set; }

    [JsonPropertyName("attribute-ids")]
    public IReadOnlyDictionary<string, Guid> AttributeIds { get; set; }

    [JsonPropertyName("attribute-values")]
    public IReadOnlyDictionary<string, string> AttributeValues { get; set; }

    [JsonPropertyName("begin")]
    public PartialDate Begin { get; set; }

    [JsonPropertyName("direction")]
    public string Direction { get; set; }

    [JsonPropertyName("end")]
    public PartialDate End { get; set; }

    [JsonPropertyName("ended")]
    public bool Ended { get; set; }

    public IEvent Event => this.TheEvent;

    [JsonPropertyName("event")]
    public Event TheEvent { get; set; }

    public IInstrument Instrument => this.TheInstrument;

    [JsonPropertyName("instrument")]
    public Instrument TheInstrument { get; set; }

    public ILabel Label => this.TheLabel;

    [JsonPropertyName("label")]
    public Label TheLabel { get; set; }

    [JsonPropertyName("ordering-key")]
    public int? OrderingKey { get; set; }

    public IPlace Place => this.ThePlace;

    [JsonPropertyName("place")]
    public Place ThePlace { get; set; }

    public IRecording Recording => this.TheRecording;

    [JsonPropertyName("recording")]
    public Recording TheRecording { get; set; }

    public IRelease Release => this.TheRelease;

    [JsonPropertyName("release")]
    public Release TheRelease { get; set; }

    public IReleaseGroup ReleaseGroup => this.TheReleaseGroup;

    [JsonPropertyName("release_group")]
    public ReleaseGroup TheReleaseGroup { get; set; }

    public ISeries Series => this.TheSeries;

    [JsonPropertyName("series")]
    public Series TheSeries { get; set; }

    [JsonPropertyName("source-credit")]
    public string SourceCredit { get; set; }

    public IRelatableEntity Target {
      get {
        switch (this.TargetType) {
          case EntityType.Area:         return this.TheArea;
          case EntityType.Artist:       return this.TheArtist;
          case EntityType.Event:        return this.TheEvent;
          case EntityType.Instrument:   return this.TheInstrument;
          case EntityType.Label:        return this.TheLabel;
          case EntityType.Place:        return this.ThePlace;
          case EntityType.Recording:    return this.TheRecording;
          case EntityType.Release:      return this.TheRelease;
          case EntityType.ReleaseGroup: return this.TheReleaseGroup;
          case EntityType.Series:       return this.TheSeries;
          case EntityType.Url:          return this.TheUrl;
          case EntityType.Work:         return this.TheWork;
          default:                      return null;
        }
      }
    }

    [JsonPropertyName("target-credit")]
    public string TargetCredit { get; set; }

    public EntityType TargetType => this._targetType ??= HelperMethods.ParseEntityType(this.TargetTypeText);

    private EntityType? _targetType;

    [JsonPropertyName("target-type")]
    public string TargetTypeText { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public IUrl Url => this.TheUrl;

    [JsonPropertyName("url")]
    public Url TheUrl { get; set; }

    public IWork Work => this.TheWork;

    [JsonPropertyName("work")]
    public Work TheWork { get; set; }

    public override string ToString() => $"{this.Type} → {this.TargetType}: {this.Target}";

  }

}

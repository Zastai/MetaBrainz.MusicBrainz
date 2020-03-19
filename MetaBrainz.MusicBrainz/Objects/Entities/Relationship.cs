using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Relationship : JsonBasedObject, IRelationship {

    [JsonPropertyName("area")]
    public IArea? Area { get; set; }

    [JsonPropertyName("artist")]
    public IArtist? Artist { get; set; }

    [JsonPropertyName("attributes")]
    public IReadOnlyList<string>? Attributes { get; set; }

    [JsonPropertyName("attribute-credits")]
    public IReadOnlyDictionary<string, string>? AttributeCredits { get; set; }

    [JsonPropertyName("attribute-ids")]
    public IReadOnlyDictionary<string, Guid>? AttributeIds { get; set; }

    [JsonPropertyName("attribute-values")]
    public IReadOnlyDictionary<string, string>? AttributeValues { get; set; }

    [JsonPropertyName("begin")]
    public PartialDate? Begin { get; set; }

    [JsonPropertyName("direction")]
    public string? Direction { get; set; }

    [JsonPropertyName("end")]
    public PartialDate? End { get; set; }

    [JsonPropertyName("ended")]
    public bool Ended { get; set; }

    [JsonPropertyName("event")]
    public IEvent? Event { get; set; }

    [JsonPropertyName("instrument")]
    public IInstrument? Instrument { get; set; }

    [JsonPropertyName("label")]
    public ILabel? Label { get; set; }

    [JsonPropertyName("ordering-key")]
    public int? OrderingKey { get; set; }

    [JsonPropertyName("place")]
    public IPlace? Place { get; set; }

    [JsonPropertyName("recording")]
    public IRecording? Recording { get; set; }

    [JsonPropertyName("release")]
    public IRelease? Release { get; set; }

    [JsonPropertyName("release_group")]
    public IReleaseGroup? ReleaseGroup { get; set; }

    [JsonPropertyName("series")]
    public ISeries? Series { get; set; }

    [JsonPropertyName("source-credit")]
    public string? SourceCredit { get; set; }

    public IRelatableEntity? Target => this.TargetType switch {
      EntityType.Area         => this.Area,
      EntityType.Artist       => this.Artist,
      EntityType.Event        => this.Event,
      EntityType.Instrument   => this.Instrument,
      EntityType.Label        => this.Label,
      EntityType.Place        => this.Place,
      EntityType.Recording    => this.Recording,
      EntityType.Release      => this.Release,
      EntityType.ReleaseGroup => this.ReleaseGroup,
      EntityType.Series       => this.Series,
      EntityType.Url          => this.Url,
      EntityType.Work         => this.Work,
      _                       => null
    };

    [JsonPropertyName("target-credit")]
    public string? TargetCredit { get; set; }

    [JsonPropertyName("target")]
    public Guid? TargetId { get; set; }

    public EntityType TargetType => this._targetType ??= HelperMethods.ParseEntityType(this.TargetTypeText);

    private EntityType? _targetType;

    [JsonPropertyName("target-type")]
    public string? TargetTypeText { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    [JsonPropertyName("url")]
    public IUrl? Url { get; set; }

    [JsonPropertyName("work")]
    public IWork? Work { get; set; }

    public override string ToString() => $"{this.Type} → {this.TargetType}: {this.Target}";

  }

}

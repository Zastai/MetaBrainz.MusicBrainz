using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Relationship : JsonBasedObject, IRelationship {

  public IArea? Area { get; init; }

  public IArtist? Artist { get; init; }

  public IReadOnlyList<string>? Attributes { get; init; }

  public IReadOnlyDictionary<string, string>? AttributeCredits { get; init; }

  public IReadOnlyDictionary<string, Guid>? AttributeIds { get; init; }

  public IReadOnlyDictionary<string, string>? AttributeValues { get; init; }

  public PartialDate? Begin { get; init; }

  public string? Direction { get; init; }

  public PartialDate? End { get; init; }

  public bool Ended { get; init; }

  public IEvent? Event { get; init; }

  public IInstrument? Instrument { get; init; }

  public ILabel? Label { get; init; }

  public int? OrderingKey { get; init; }

  public IPlace? Place { get; init; }

  public IRecording? Recording { get; init; }

  public IRelease? Release { get; init; }

  public IReleaseGroup? ReleaseGroup { get; init; }

  public ISeries? Series { get; init; }

  public string? SourceCredit { get; init; }

  public IRelatableEntity? Target => this.TargetType switch {
    EntityType.Area => this.Area,
    EntityType.Artist => this.Artist,
    EntityType.Event => this.Event,
    EntityType.Instrument => this.Instrument,
    EntityType.Label => this.Label,
    EntityType.Place => this.Place,
    EntityType.Recording => this.Recording,
    EntityType.Release => this.Release,
    EntityType.ReleaseGroup => this.ReleaseGroup,
    EntityType.Series => this.Series,
    EntityType.Url => this.Url,
    EntityType.Work => this.Work,
    _ => null
  };

  public string? TargetCredit { get; init; }

  public Guid? TargetId { get; init; }

  public EntityType? TargetType { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public IUrl? Url { get; init; }

  public IWork? Work { get; init; }

  public override string ToString() => $"{this.Type} → {this.TargetType}: {this.Target}";

}

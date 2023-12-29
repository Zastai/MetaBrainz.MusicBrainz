using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class WorkAttribute : JsonBasedObject, IWorkAttribute {

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public string? Value { get; init; }

  public Guid? ValueId { get; init; }

  public override string ToString() => $"{this.Type}: {this.Value}";

}

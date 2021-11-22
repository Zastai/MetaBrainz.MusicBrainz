using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class WorkAttribute : JsonBasedObject, IWorkAttribute {

  public string? Type { get; set; }

  public Guid? TypeId { get; set; }

  public string? Value { get; set; }

  public Guid? ValueId { get; set; }

  public override string ToString() => $"{this.Type}: {this.Value}";

}
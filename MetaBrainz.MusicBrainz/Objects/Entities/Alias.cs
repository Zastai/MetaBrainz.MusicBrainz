using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Alias : JsonBasedObject, IAlias {

  public Alias(string name, bool primary) {
    this.Name = name;
    this.Primary = primary;
  }

  public PartialDate? Begin { get; init; }

  public PartialDate? End { get; init; }

  public bool Ended { get; init; }

  public string? Locale { get; init; }

  public string Name { get; }

  public bool Primary { get; }

  public string? SortName { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public override string ToString() {
    var text = this.Name;
    if (!string.IsNullOrEmpty(this.Type)) {
      text += $" ({this.Type})";
    }
    return text;
  }

}

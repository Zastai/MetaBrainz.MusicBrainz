using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Alias : JsonBasedObject, IAlias {

    public Alias(string name, bool primary) {
      this.Name = name;
      this.Primary = primary;
    }

    public PartialDate? Begin { get; set; }

    public PartialDate? End { get; set; }

    public bool Ended { get; set; }

    public string? Locale { get; set; }

    public string Name { get; }

    public bool Primary { get; }

    public string? SortName { get; set; }

    public string? Type { get; set; }

    public Guid? TypeId { get; set; }

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Type))
        text += $" ({this.Type})";
      return text;
    }

  }

}

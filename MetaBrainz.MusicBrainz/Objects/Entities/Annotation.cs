using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Annotation : JsonBasedObject, IAnnotation {

  public Guid? Entity { get; set; }

  public string? Name { get; set; }

  public string? Text { get; set; }

  public EntityType? Type { get; set; }

  public override string? ToString() => this.Text;

}

﻿using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Annotation : JsonBasedObject, IAnnotation {

  public Guid? Entity { get; init; }

  public string? Name { get; init; }

  public string? Text { get; init; }

  public EntityType? Type { get; init; }

  public override string? ToString() => this.Text;

}

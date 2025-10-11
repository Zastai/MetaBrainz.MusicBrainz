using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Url() : Entity(EntityType.Url), IUrl {

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public required Uri Resource { get; init; }

  public override string ToString() => this.Resource.ToString();

}

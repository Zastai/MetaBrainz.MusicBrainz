using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Url : Entity, IUrl {

  public Url(Guid id, Uri resource) : base(EntityType.Url, id) {
    this.Resource = resource;
  }

  public IReadOnlyList<IRelationship>? Relationships { get; set; }

  public Uri Resource { get; }

  public override string ToString() => this.Resource.ToString();

}

using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing items to a collection.</summary>
[PublicAPI]
public sealed class CollectionModification : CollectionModificationBase<CollectionModification, IEntity> {

  internal CollectionModification(Query query, string client, Guid id, EntityType type) : base(query, client, id, type) {
  }

}

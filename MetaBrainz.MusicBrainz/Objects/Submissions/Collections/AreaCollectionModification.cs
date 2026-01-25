using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing areas to/from a collection.</summary>
[PublicAPI]
public sealed class AreaCollectionModification : CollectionModificationBase<AreaCollectionModification, IArea> {

  internal AreaCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Area) {
  }

}

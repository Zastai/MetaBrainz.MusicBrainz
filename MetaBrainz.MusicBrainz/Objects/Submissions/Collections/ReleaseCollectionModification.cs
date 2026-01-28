using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing releases to/from a collection.</summary>
[PublicAPI]
public sealed class ReleaseCollectionModification : CollectionModification<ReleaseCollectionModification, IRelease> {

  internal ReleaseCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Release) {
  }

}

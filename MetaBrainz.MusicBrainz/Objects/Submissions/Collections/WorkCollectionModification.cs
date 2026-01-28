using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing works to/from a collection.</summary>
[PublicAPI]
public sealed class WorkCollectionModification : CollectionModification<WorkCollectionModification, IWork> {

  internal WorkCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Work) {
  }

}

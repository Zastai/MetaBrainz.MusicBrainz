using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing release groups to/from a collection.</summary>
[PublicAPI]
public sealed class ReleaseGroupCollectionModification : CollectionModificationBase<ReleaseGroupCollectionModification, IReleaseGroup> {

  internal ReleaseGroupCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.ReleaseGroup) {
  }

}

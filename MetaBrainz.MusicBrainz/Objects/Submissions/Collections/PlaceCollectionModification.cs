using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing places to/from a collection.</summary>
[PublicAPI]
public sealed class PlaceCollectionModification : CollectionModificationBase<PlaceCollectionModification, IPlace> {

  internal PlaceCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Place) {
  }

}

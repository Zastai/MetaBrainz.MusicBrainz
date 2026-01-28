using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing series to/from a collection.</summary>
[PublicAPI]
public sealed class SeriesCollectionModification : CollectionModification<SeriesCollectionModification, ISeries> {

  internal SeriesCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Series) {
  }

}

using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing labels to/from a collection.</summary>
[PublicAPI]
public sealed class LabelCollectionModification : CollectionModificationBase<LabelCollectionModification, ILabel> {

  internal LabelCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Label) {
  }

}

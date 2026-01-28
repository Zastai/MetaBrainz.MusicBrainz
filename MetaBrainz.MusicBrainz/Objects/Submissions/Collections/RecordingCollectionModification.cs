using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing recordings to/from a collection.</summary>
[PublicAPI]
public sealed class RecordingCollectionModification : CollectionModification<RecordingCollectionModification, IRecording> {

  internal RecordingCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Recording) {
  }

}

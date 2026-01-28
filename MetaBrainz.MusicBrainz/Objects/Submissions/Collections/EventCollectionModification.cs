using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing events to/from a collection.</summary>
[PublicAPI]
public sealed class EventCollectionModification : CollectionModification<EventCollectionModification, IEvent> {

  internal EventCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Event) {
  }

}

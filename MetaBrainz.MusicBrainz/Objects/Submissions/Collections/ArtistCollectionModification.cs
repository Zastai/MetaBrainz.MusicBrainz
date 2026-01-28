using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing artists to/from a collection.</summary>
[PublicAPI]
public sealed class ArtistCollectionModification : CollectionModification<ArtistCollectionModification, IArtist> {

  internal ArtistCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Artist) {
  }

}

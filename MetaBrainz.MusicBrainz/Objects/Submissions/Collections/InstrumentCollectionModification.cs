using System;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing instruments to/from a collection.</summary>
[PublicAPI]
public sealed class InstrumentCollectionModification : CollectionModification<InstrumentCollectionModification, IInstrument> {

  internal InstrumentCollectionModification(Query query, string client, Guid id) : base(query, client, id, EntityType.Instrument) {
  }

}

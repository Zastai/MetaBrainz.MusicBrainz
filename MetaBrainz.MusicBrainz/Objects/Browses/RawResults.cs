using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class RawResults(int count, int offset) : JsonBasedObject {

  public IReadOnlyList<IArea>? Areas;

  public IReadOnlyList<IArtist>? Artists;

  public IReadOnlyList<ICollection>? Collections;

  public readonly int Count = count;

  public IReadOnlyList<IEvent>? Events;

  public IReadOnlyList<IGenre>? Genres;

  public IReadOnlyList<IInstrument>? Instruments;

  public IReadOnlyList<ILabel>? Labels;

  public readonly int Offset = offset;

  public IReadOnlyList<IPlace>? Places;

  public IReadOnlyList<IRecording>? Recordings;

  public IReadOnlyList<IReleaseGroup>? ReleaseGroups;

  public IReadOnlyList<IRelease>? Releases;

  public IReadOnlyList<ISeries>? Series;

  public IReadOnlyList<IWork>? Works;

}

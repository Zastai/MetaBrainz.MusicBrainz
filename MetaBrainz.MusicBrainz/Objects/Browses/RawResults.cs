using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class RawResults : JsonBasedObject {

  public IReadOnlyList<IArea>? Areas { get; init; }

  public IReadOnlyList<IArtist>? Artists { get; init; }

  public IReadOnlyList<ICollection>? Collections { get; init; }

  public required int Count { get; init; }

  public IReadOnlyList<IEvent>? Events { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public IReadOnlyList<IInstrument>? Instruments { get; init; }

  public IReadOnlyList<ILabel>? Labels { get; init; }

  public required int Offset { get; init; }

  public IReadOnlyList<IPlace>? Places { get; init; }

  public IReadOnlyList<IRecording>? Recordings { get; init; }

  public IReadOnlyList<IReleaseGroup>? ReleaseGroups { get; init; }

  public IReadOnlyList<IRelease>? Releases { get; init; }

  public IReadOnlyList<ISeries>? Series { get; init; }

  public IReadOnlyList<IWork>? Works { get; init; }

}

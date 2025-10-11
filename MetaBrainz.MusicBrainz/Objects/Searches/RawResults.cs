using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class RawResults : JsonBasedObject {

  public IReadOnlyList<ISearchResult<IAnnotation>>? Annotations { get; init; }

  public IReadOnlyList<ISearchResult<IArea>>? Areas { get; init; }

  public IReadOnlyList<ISearchResult<IArtist>>? Artists { get; init; }

  public IReadOnlyList<ISearchResult<ICdStub>>? CdStubs { get; init; }

  public required int Count { get; init; }

  public required DateTimeOffset Created { get; init; }

  public IReadOnlyList<ISearchResult<IEvent>>? Events { get; init; }

  public IReadOnlyList<ISearchResult<IInstrument>>? Instruments { get; init; }

  public IReadOnlyList<ISearchResult<ILabel>>? Labels { get; init; }

  public required int Offset { get; init; }

  public IReadOnlyList<ISearchResult<IPlace>>? Places { get; init; }

  public IReadOnlyList<ISearchResult<IRecording>>? Recordings { get; init; }

  public IReadOnlyList<ISearchResult<IReleaseGroup>>? ReleaseGroups { get; init; }

  public IReadOnlyList<ISearchResult<IRelease>>? Releases { get; init; }

  public IReadOnlyList<ISearchResult<ISeries>>? Series { get; init; }

  public IReadOnlyList<ISearchResult<ITag>>? Tags { get; init; }

  public IReadOnlyList<ISearchResult<IUrl>>? Urls { get; init; }

  public IReadOnlyList<ISearchResult<IWork>>? Works { get; init; }

}

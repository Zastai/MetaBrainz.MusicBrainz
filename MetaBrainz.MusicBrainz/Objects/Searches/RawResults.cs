using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class RawResults(int count, int offset, DateTimeOffset created) : JsonBasedObject {

  public IReadOnlyList<ISearchResult<IAnnotation>>? Annotations;

  public IReadOnlyList<ISearchResult<IArea>>? Areas;

  public IReadOnlyList<ISearchResult<IArtist>>? Artists;

  public IReadOnlyList<ISearchResult<ICdStub>>? CdStubs;

  public readonly int Count = count;

  public readonly DateTimeOffset Created = created;

  public IReadOnlyList<ISearchResult<IEvent>>? Events;

  public IReadOnlyList<ISearchResult<IInstrument>>? Instruments;

  public IReadOnlyList<ISearchResult<ILabel>>? Labels;

  public readonly int Offset = offset;

  public IReadOnlyList<ISearchResult<IPlace>>? Places;

  public IReadOnlyList<ISearchResult<IRecording>>? Recordings;

  public IReadOnlyList<ISearchResult<IReleaseGroup>>? ReleaseGroups;

  public IReadOnlyList<ISearchResult<IRelease>>? Releases;

  public IReadOnlyList<ISearchResult<ISeries>>? Series;

  public IReadOnlyList<ISearchResult<ITag>>? Tags;

  public IReadOnlyList<ISearchResult<IUrl>>? Urls;

  public IReadOnlyList<ISearchResult<IWork>>? Works;

}

using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseResult : JsonBasedObject {

  public BrowseResult(int count, int offset) {
    this.Count = count;
    this.Offset = offset;
  }

  public IReadOnlyList<IArea>? Areas;

  public IReadOnlyList<IArtist>? Artists;

  public IReadOnlyList<ICollection>? Collections;

  public readonly int Count;

  public IReadOnlyList<IEvent>? Events;

  public IReadOnlyList<IInstrument>? Instruments;

  public IReadOnlyList<ILabel>? Labels;

  public readonly int Offset;

  public IReadOnlyList<IPlace>? Places;

  public IReadOnlyList<IRecording>? Recordings;

  public IReadOnlyList<IReleaseGroup>? ReleaseGroups;

  public IReadOnlyList<IRelease>? Releases;

  public IReadOnlyList<ISeries>? Series;

  public IReadOnlyList<IWork>? Works;

}
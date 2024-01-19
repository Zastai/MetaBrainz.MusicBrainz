using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseRecordings : BrowseResults<IRecording> {

  public BrowseRecordings(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                          int? offset) : base(query, "recording", null, options, limit, offset) {
  }

  public override IReadOnlyList<IRecording> Results => this.CurrentResult?.Recordings ?? Array.Empty<IRecording>();

}

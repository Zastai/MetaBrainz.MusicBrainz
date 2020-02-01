using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A recording returned by a search request.</summary>
  [PublicAPI]
  public interface IFoundRecording : IRecording, ISearchResult { }

}

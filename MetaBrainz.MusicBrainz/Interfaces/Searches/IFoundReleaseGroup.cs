using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A release group returned by a search request.</summary>
  [PublicAPI]
  public interface IFoundReleaseGroup : IReleaseGroup, ISearchResult { }

}

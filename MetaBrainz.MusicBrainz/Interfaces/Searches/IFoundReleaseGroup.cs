using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A release group returned by a search request.</summary>
  public interface IFoundReleaseGroup : IReleaseGroup, ISearchResult { }

}

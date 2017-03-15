using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A release returned by a search request.</summary>
  public interface IFoundRelease : IRelease, ISearchResult { }

}

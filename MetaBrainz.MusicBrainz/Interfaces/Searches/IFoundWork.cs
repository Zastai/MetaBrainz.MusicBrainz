using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A work returned by a search request.</summary>
  public interface IFoundWork : IWork, ISearchResult { }

}

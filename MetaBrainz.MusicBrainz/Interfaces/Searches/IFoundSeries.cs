using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A series returned by a search request.</summary>
  public interface IFoundSeries : ISeries, ISearchResult { }

}

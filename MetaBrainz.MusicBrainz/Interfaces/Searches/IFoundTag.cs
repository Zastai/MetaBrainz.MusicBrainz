using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A tag returned by a search request.</summary>
  public interface IFoundTag : ITag, ISearchResult { }

}

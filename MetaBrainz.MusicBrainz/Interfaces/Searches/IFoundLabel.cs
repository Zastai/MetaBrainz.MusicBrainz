using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A label returned by a search request.</summary>
  public interface IFoundLabel : ILabel, ISearchResult { }

}

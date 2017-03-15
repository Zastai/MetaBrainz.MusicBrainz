using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>An event returned by a search request.</summary>
  public interface IFoundEvent : IEvent, ISearchResult { }

}

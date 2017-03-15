using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>An instrument returned by a search request.</summary>
  public interface IFoundInstrument : IInstrument, ISearchResult { }

}

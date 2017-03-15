using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A CD stub returned by a search request.</summary>
  public interface IFoundCdStub : ICdStub, ISearchResult { }

}

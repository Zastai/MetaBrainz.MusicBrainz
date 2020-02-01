using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A place returned by a search request.</summary>
  [PublicAPI]
  public interface IFoundPlace : IPlace, ISearchResult { }

}

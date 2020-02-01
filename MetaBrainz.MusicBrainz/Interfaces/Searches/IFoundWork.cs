using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>A work returned by a search request.</summary>
  [PublicAPI]
  public interface IFoundWork : IWork, ISearchResult { }

}

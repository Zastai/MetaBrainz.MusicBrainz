using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>An annotation returned by a search request.</summary>
  [PublicAPI]
  public interface IFoundAnnotation : IAnnotation, ISearchResult { }

}

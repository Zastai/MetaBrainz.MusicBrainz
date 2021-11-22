using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches; 

/// <summary>A result returned by a search request.</summary>
[PublicAPI]
public interface ISearchResult {

  /// <summary>The score (0-100) indicating how well the item matches the search request.</summary>
  byte Score { get; }

}

/// <summary>A result returned by a search request.</summary>
/// <typeparam name="T">The type of item returned.</typeparam>
[PublicAPI]
public interface ISearchResult<out T> : ISearchResult {

  /// <summary>The result item.</summary>
  T Item { get; }

}
using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>The result of a search request for a specific entity type.</summary>
  /// <typeparam name="T">The type of entity that was searched for.</typeparam>
  [PublicAPI]
  public interface ISearchResults<T> : IPagedQueryResults<ISearchResults<T>, T> where T : ISearchResult {

    /// <summary>The date and time at which this search result was created, if available.</summary>
    DateTime? Created { get; }

  }

}

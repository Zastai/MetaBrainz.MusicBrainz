using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>The extra properties available on an item returned by a search request.</summary>
  [PublicAPI]
  public interface ISearchResult : IJsonBasedObject {

    /// <summary>The score (0-100) indicating how well the item matches the search request.</summary>
    byte Score { get; }

  }

}

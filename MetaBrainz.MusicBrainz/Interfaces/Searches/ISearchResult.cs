using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>The extra properties available on an item returned by a search request.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ISearchResult {

    /// <summary>The score (0-100) indicating how well the item matches the search request.</summary>
    byte Score { get; }

  }

}

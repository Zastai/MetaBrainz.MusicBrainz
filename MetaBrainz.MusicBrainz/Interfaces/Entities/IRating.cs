using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information about an entity's rating.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IRating {

    /// <summary>The rating value, if any, expressed as a fractional number of stars (0-5).</summary>
    decimal? Value { get; }

    /// <summary>The number of ratings submitted for the entity.</summary>
    int VoteCount { get; }

  }

}

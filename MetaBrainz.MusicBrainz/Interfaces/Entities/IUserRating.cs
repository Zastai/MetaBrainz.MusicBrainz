using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information about the authenticated user's rating for an entity.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IUserRating {

    /// <summary>The rating value, if any, expressed as a fractional number of stars (0-5).</summary>
    decimal? Value { get; }

  }

}

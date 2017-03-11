using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A entity that can be rated.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IRatableEntity : IEntity {

    /// <summary>The rating for the entity.</summary>
    IRating Rating { get; }

    /// <summary>The rating given to the entity by the authenticated user.</summary>
    IUserRating UserRating { get; }

  }

}

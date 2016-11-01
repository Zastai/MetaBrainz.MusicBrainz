namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A entity that can be rated.</summary>
  public interface IRatableEntity : IMbEntity {

    /// <summary>The rating for the entity.</summary>
    IRating Rating { get; }

    /// <summary>The rating given to the entity by the authenticated user.</summary>
    IUserRating UserRating { get; }

  }

}

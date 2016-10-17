namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A rated entity.</summary>
  public interface IRatedEntity {

    /// <summary>The rating for the entity.</summary>
    IRating Rating { get; }

    /// <summary>The rating given to the entity by the authenticated user.</summary>
    IUserRating UserRating { get; }

  }

}

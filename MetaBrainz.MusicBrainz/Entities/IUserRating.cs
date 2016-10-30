namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Information about the authenticated user's rating for an entity.</summary>
  public interface IUserRating {

    /// <summary>The rating value, expressed as a fractional number of stars (0-5).</summary>
    decimal Value { get; }

  }

}

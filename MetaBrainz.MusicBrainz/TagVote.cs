namespace MetaBrainz.MusicBrainz {

  /// <summary>A vote for a tag.</summary>
  public enum TagVote {

    /// <summary>
    ///   Vote up the tag. This adds 1 to the tag's vote count.
    /// </summary>
    Up,

    /// <summary>
    ///   Vote down the tag. This subtracts 1 from the tag's vote count and hides it from you in the UI.
    ///   If this brings a tag's vote count to 0 or below, the tag will be hidden for everyone.
    /// </summary>
    Down,

    /// <summary>Withdraws any vote you may have previously submitted for the tag.</summary>
    Withdraw,

  }

}

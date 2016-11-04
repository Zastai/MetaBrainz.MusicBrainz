namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A tag attached to an entity.</summary>
  public interface ITag {

    /// <summary>The name of the tag.</summary>
    string Name { get; }

    /// <summary>The number of votes that have been registered for this tag.</summary>
    int VoteCount { get; }

  }

}

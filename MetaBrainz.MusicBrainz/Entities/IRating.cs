namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRating {

    byte? Value { get; }

    int VoteCount { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRating : IResource {

    string Text { get; }

    uint VoteCount { get; }

  }

}

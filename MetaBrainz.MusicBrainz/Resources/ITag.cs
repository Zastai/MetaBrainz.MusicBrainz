namespace MetaBrainz.MusicBrainz.Resources {

  public interface ITag : IResource {

    uint VoteCount { get; }

    string Name { get; }

  }

}

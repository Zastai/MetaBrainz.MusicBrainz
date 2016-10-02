namespace MetaBrainz.MusicBrainz.Resources {

  public interface INameCredit : IResource {

    IArtist Artist { get; }

    string JoinPhrase { get; }

    string Name { get; }

  }

}

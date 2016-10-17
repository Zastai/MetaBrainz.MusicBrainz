namespace MetaBrainz.MusicBrainz.Entities {

  public interface INameCredit {

    IArtist Artist { get; }

    string JoinPhrase { get; }

    string Name { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Resources {

  public interface ITrackInfo : IEntity {

    IArtistCredit ArtistCredit { get; }

    uint? Length { get; }

    string Number { get; }

    uint? Position { get; }

    IRecording Recording { get; }

    string Title { get; }

  }

}

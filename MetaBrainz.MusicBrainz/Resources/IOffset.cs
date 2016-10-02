namespace MetaBrainz.MusicBrainz.Resources {

  public interface IOffset : IResource {

    uint Position { get; }

    string Value { get; }

  }

}

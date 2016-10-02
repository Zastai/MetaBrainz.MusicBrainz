namespace MetaBrainz.MusicBrainz.Resources {

  public interface ISpokenLanguage : IResource {

    string Fluency { get; }

    string Name { get; }

  }

}

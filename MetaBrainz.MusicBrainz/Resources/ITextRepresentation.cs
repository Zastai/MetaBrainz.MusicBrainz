namespace MetaBrainz.MusicBrainz.Resources {

  public interface ITextRepresentation : IResource {

    string Language { get; }

    string Script { get; }

  }

}

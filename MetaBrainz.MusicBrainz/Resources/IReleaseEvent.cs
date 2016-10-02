namespace MetaBrainz.MusicBrainz.Resources {

  public interface IReleaseEvent : IResource {

    IArea Area { get; }

    string Date { get; }

  }

}

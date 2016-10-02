namespace MetaBrainz.MusicBrainz.Resources {

  public interface IFreeDbDisc : IResource {

    string Artist { get; }

    string Category { get; }

    string Comment { get; }

    string Title { get; }

    IResourceList<ISimpleTrackInfo> TrackList { get; }

    string Year { get; }

  }

}

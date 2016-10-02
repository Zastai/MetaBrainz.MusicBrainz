namespace MetaBrainz.MusicBrainz.Resources {

  public interface IMedium : IResource {

    IResourceList<ITrackInfo> DataTrackList { get; }

    IResourceList<IDisc> DiscList { get; }

    ITextResource Format { get; }

    uint Position { get; }

    ITrackInfo Pregap { get; }

    string Title { get; }

    IResourceList<ITrackInfo> TrackList { get; }

  }

}

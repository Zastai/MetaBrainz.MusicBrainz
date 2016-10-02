namespace MetaBrainz.MusicBrainz.Resources {

  public interface IDisc : IEntity {

    IResourceList<IOffset> OffsetList { get; }

    IResourceList<IRelease> ReleaseList { get; }

    uint Sectors { get; }

  }

}

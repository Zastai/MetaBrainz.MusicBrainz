using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRecording : IMbEntity, IAnnotatedResource, IRatedResource, IRelatableResource, ITaggedResource, ITitledResource {

    IArtistCredit ArtistCredit { get; }

    IResourceList<IIsrc> IsrcList { get; }

    uint? Length { get; }

    IResourceList<IRelease> ReleaseList { get; }

    bool? Video { get; }

    [Obsolete] IResourceList<IPuid> PuidList { get; }

  }

}

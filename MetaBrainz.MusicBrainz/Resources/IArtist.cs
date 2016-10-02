namespace MetaBrainz.MusicBrainz.Resources {

  public interface IArtist : IMbEntity, IAnnotatedResource, INamedResource, IRatedResource, IRelatableResource, ITypedResource, ITaggedResource {

    IArea Area { get; }

    IArea BeginArea { get; }

    string Country { get; }

    IArea EndArea { get; }

    ITextResource Gender { get; }

    string Ipi { get; }

    IResourceList<ITextResource> IpiList { get; }

    IResourceList<ITextResource> IsniList { get; }

    IResourceList<ILabel> LabelList { get; }

    ILifeSpan Lifespan { get; }

    IResourceList<IRecording> RecordingList { get; }

    IResourceList<IRelease> ReleaseList { get; }

    IResourceList<IReleaseGroup> ReleaseGroupList { get; }

    IResourceList<IWork> WorkList { get; }

  }

}

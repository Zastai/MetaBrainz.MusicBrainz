namespace MetaBrainz.MusicBrainz.Resources {

  public interface IArtist : IMbEntity, IAnnotatedResource, INamedResource, IRatedResource, IRelatableResource, ITaggedResource, ITypedResource {

    IArea Area { get; }

    IArea BeginArea { get; }

    string Country { get; }

    IArea EndArea { get; }

    ITextResource Gender { get; }

    string Ipi { get; }

    IStringList IpiList { get; }

    IStringList IsniList { get; }

    IResourceList<ILabel> LabelList { get; }

    ILifeSpan Lifespan { get; }

    IResourceList<IRecording> RecordingList { get; }

    IResourceList<IRelease> ReleaseList { get; }

    IResourceList<IReleaseGroup> ReleaseGroupList { get; }

    IResourceList<IWork> WorkList { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IArea : IMbEntity, IAnnotatedResource, INamedResource, IRelatableResource, ITaggedResource, ITypedResource {

    IStringList Iso31661CodeList { get; }

    IStringList Iso31662CodeList { get; }

    IStringList Iso31663CodeList { get; }

    ILifeSpan LifeSpan { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A musical instrument.</summary>
  public interface IInstrument : IMbEntity, IAnnotatedResource, INamedResource, IRelatableResource, ITaggedResource, ITypedResource {

    /// <summary>The instrument's description.</summary>
    string Description { get; }

  }

}

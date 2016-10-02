namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource that is can have an associated annotation.</summary>
  public interface IAnnotatedResource : IResource {

    /// <summary>The annotation for this resource.</summary>
    IAnnotation Annotation { get; }

  }

}

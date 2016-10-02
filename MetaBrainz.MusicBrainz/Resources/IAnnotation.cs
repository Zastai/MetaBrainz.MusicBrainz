namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>An annotation.</summary>
  public interface IAnnotation : ITextResource {

    // FIXME: Should this be a URI? Is this ever returned?
    /// <summary>The entity the annotation belongs to.</summary>
    string Entity { get; }

    // FIXME: What exactly is this? The editor's name? Is this ever returned?
    /// <summary>The annotation's name.</summary>
    string Name { get; }

    // FIXME: What exactly is this? Is this ever returned?
    /// <summary>The annotation's type.</summary>
    string Type { get; }

  }

}

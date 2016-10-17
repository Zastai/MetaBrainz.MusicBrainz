namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An entity that can have an associated annotation.</summary>
  public interface IAnnotatedEntity {

    /// <summary>The annotation for this entity.</summary>
    string Annotation { get; }

  }

}

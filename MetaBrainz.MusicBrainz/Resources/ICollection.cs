namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A collection.</summary>
  public interface ICollection<out T> : IMbEntity, ITypedResource where T : IMbEntity {

    /// <summary>The name of the editor who created the collection.</summary>
    string Editor { get; }

    /// <summary>The type of entity stored in the collection.</summary>
    string EntityType { get; }

    /// <summary>The name of the collection.</summary>
    string Name { get; }

    /// <summary>The contents of the collection.</summary>
    IResourceList<T> Contents { get; }

  }

}

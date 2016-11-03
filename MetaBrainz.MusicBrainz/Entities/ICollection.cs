namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A collection.</summary>
  public interface ICollection : IEntity, ITypedEntity {

    /// <summary>The name of the editor who created the collection.</summary>
    string Editor { get; }

    /// <summary>The type of entity stored in the collection (as an enumeration value).</summary>
    EntityType ContentType { get; }

    /// <summary>The type of entity stored in the collection (as text).</summary>
    string ContentTypeText { get; }

    /// <summary>The number of items in in the collection.</summary>
    int ItemCount { get; }

    /// <summary>The name of the collection.</summary>
    string Name { get; }

  }

}

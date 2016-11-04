namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz instrument.</summary>
  public interface IInstrument : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The instrument's description.</summary>
    string Description { get; }

  }

}

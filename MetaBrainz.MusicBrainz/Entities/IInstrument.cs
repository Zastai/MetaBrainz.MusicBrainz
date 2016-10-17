namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A musical instrument.</summary>
  public interface IInstrument : IMbEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggedEntity, ITypedEntity {

    /// <summary>The instrument's description.</summary>
    string Description { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Entities {

  public interface ISeries : IMbEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    string OrderingAttribute { get; }

  }

}

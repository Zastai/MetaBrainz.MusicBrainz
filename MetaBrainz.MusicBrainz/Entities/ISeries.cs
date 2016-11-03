namespace MetaBrainz.MusicBrainz.Entities {

  public interface ISeries : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    string OrderingAttribute { get; }

  }

}

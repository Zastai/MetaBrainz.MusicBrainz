namespace MetaBrainz.MusicBrainz.Entities {

  public interface ISeries : IMbEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggedEntity, ITypedEntity {

    string OrderingAttribute { get; }

  }

}

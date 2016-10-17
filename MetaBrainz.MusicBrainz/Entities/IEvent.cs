namespace MetaBrainz.MusicBrainz.Entities {

  public interface IEvent : IMbEntity, IAnnotatedEntity, INamedEntity, IRatedEntity, IRelatableEntity, ITaggedEntity, ITypedEntity {

    bool? Cancelled { get; }

    ILifeSpan LifeSpan { get; }

    string Setlist { get; }

    string Time { get; }

  }

}

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IEvent : IEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    bool? Cancelled { get; }

    ILifeSpan LifeSpan { get; }

    string Setlist { get; }

    string Time { get; }

  }

}

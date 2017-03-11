using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz event.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IEvent : IEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>A flag indicating whether or not the event was cancelled.</summary>
    bool Cancelled { get; }

    /// <summary>The event's lifespan.</summary>
    ILifeSpan LifeSpan { get; }

    /// <summary>The setlist for the event.</summary>
    string Setlist { get; }

    /// <summary>The starting time for the event, in HH:MM format.</summary>
    string Time { get; }

  }

}

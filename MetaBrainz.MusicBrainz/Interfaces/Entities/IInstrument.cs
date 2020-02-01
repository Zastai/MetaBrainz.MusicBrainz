using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz instrument.</summary>
  [PublicAPI]
  public interface IInstrument : IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The instrument's description.</summary>
    string Description { get; }

  }

}

using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz series.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  public interface ISeries : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

  }

}

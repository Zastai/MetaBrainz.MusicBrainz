using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A MusicBrainz series.</summary>
[PublicAPI]
public interface ISeries : IAliasedEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity;

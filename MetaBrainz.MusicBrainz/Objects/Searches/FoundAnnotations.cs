using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundAnnotations(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IAnnotation>(query, "annotation", queryString, limit, offset, simple, static r => r?.Annotations);

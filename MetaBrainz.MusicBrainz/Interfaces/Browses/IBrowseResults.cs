using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces.Browses {

  /// <summary>The result of a browse request for a specific entity type.</summary>
  /// <typeparam name="T">The type of entity being browsed.</typeparam>
  [PublicAPI]
  public interface IBrowseResults<T> : IPagedQueryResults<IBrowseResults<T>, T> where T : IEntity { }

}

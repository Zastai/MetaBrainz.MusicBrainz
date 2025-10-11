using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>
/// The results for a query that supports paging (i.e. search or browse), returned as a stream of results that can be enumerated
/// synchronously or asynchronously.
/// </summary>
/// <typeparam name="TItem">The type of item being returned.</typeparam>
[PublicAPI]
public interface IStreamingQueryResults<out TItem> : IAsyncEnumerable<TItem> {

}

using System.Collections.Generic;
using System.Threading;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class StreamingQueryResults<TResult, TItem, TResultObject> : IStreamingQueryResults<TItem>
where TResult : IPagedQueryResults<TResult, TItem>
where TResultObject : class {

  public StreamingQueryResults(PagedQueryResults<TResult, TItem, TResultObject> pagedResults) {
    this._pagedResults = pagedResults;
  }

  private readonly PagedQueryResults<TResult, TItem, TResultObject> _pagedResults;

  #region IAsyncEnumerable

  public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = default) {
    IPagedQueryResults<TResult, TItem> currentPage = this._pagedResults;
    if (!currentPage.IsActive) {
      currentPage = await currentPage.NextAsync(cancellationToken).ConfigureAwait(false);
      if (cancellationToken.IsCancellationRequested) {
        yield break;
      }
    }
    while (currentPage.Results.Count > 0) {
      foreach (var item in currentPage.Results) {
        if (cancellationToken.IsCancellationRequested) {
          break;
        }
        yield return item;
      }
      if (currentPage.Offset + currentPage.Results.Count >= currentPage.TotalResults || cancellationToken.IsCancellationRequested) {
        break;
      }
      currentPage = await currentPage.NextAsync(cancellationToken).ConfigureAwait(false);
    }
  }

  #endregion

}

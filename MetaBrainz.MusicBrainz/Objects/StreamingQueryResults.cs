using System.Collections;
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

  public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = new()) {
    IPagedQueryResults<TResult, TItem> currentPage = this._pagedResults;
    if (!currentPage.IsActive) {
      currentPage = await currentPage.NextAsync();
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
      currentPage = await currentPage.NextAsync();
    }
  }

  #endregion

  #region IEnumerable

  IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

  public IEnumerator<TItem> GetEnumerator() {
    IPagedQueryResults<TResult, TItem> currentPage = this._pagedResults;
    if (!currentPage.IsActive) {
      currentPage = currentPage.Next();
    }
    while (currentPage.Results.Count > 0) {
      foreach (var item in currentPage.Results) {
        yield return item;
      }
      if (currentPage.Offset + currentPage.Results.Count >= currentPage.TotalResults) {
        break;
      }
      currentPage = currentPage.Next();
    }
  }

  #endregion

}

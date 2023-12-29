using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects;

internal abstract class PagedQueryResults<TResults, TItem, TResultObject> : IPagedQueryResults<TResults, TItem>
where TResults : IPagedQueryResults<TResults, TItem>
where TResultObject : class {

  protected PagedQueryResults(Query query, string endpoint, string? value, int? limit, int? offset) {
    this._endpoint = endpoint;
    this.Limit = limit;
    this.NextOffset = offset;
    this.Offset = 0;
    this._query = query;
    this._value = value;
  }

  #region IPagedQueryResults

  public IStreamingQueryResults<TItem> AsStream() => new StreamingQueryResults<TResults, TItem, TResultObject>(this);

  bool IPagedQueryResults<TResults, TItem>.IsActive => this.CurrentResult is not null;

  public int? Limit { get; set; }

  public abstract IReadOnlyDictionary<string, object?>? UnhandledProperties { get; }

  public TResults Next() => AsyncUtils.ResultOf(this.NextAsync());

  public async Task<TResults> NextAsync(CancellationToken cancellationToken = default) {
    this.UpdateOffset(this.Results.Count);
    return await this.PerformRequestAsync(cancellationToken).ConfigureAwait(false);
  }

  public int? NextOffset { get; set; }

  public int Offset { get; private set; }

  public TResults Previous() => AsyncUtils.ResultOf(this.PreviousAsync());

  public async Task<TResults> PreviousAsync(CancellationToken cancellationToken = default) {
    this.UpdateOffset();
    return await this.PerformRequestAsync(cancellationToken).ConfigureAwait(false);
  }

  public abstract IReadOnlyList<TItem> Results { get; }

  public abstract int TotalResults { get; }

  #endregion

  #region Protected Elements

  protected TResultObject? CurrentResult;

  protected abstract Task<TResults> DeserializeAsync(HttpResponseMessage response, CancellationToken cancellationToken);

  protected abstract string FullExtraText();

  #endregion

  #region Internals

  private readonly string _endpoint;

  private readonly Query _query;

  private readonly string? _value;

  private async Task<TResults> PerformRequestAsync(CancellationToken cancellationToken) {
    var task = this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText(), cancellationToken);
    var response = await task.ConfigureAwait(false);
    return await this.DeserializeAsync(response, cancellationToken).ConfigureAwait(false);
  }

  private void UpdateOffset() {
    if (this.NextOffset is not null) {
      this.Offset = this.NextOffset.Value;
      this.NextOffset = null;
    }
    else {
      var limit = Math.Min(this.Limit ?? Query.DefaultPageSize, Query.MaximumPageSize);
      if (limit < 1) {
        limit = Query.DefaultPageSize;
      }
      this.Offset -= limit;
    }
    if (this.Offset < 0) {
      this.Offset = 0;
    }
  }

  private void UpdateOffset(int lastResultCount) {
    if (this.NextOffset is not null) {
      this.Offset = this.NextOffset.Value;
      this.NextOffset = null;
    }
    else {
      this.Offset += lastResultCount;
    }
    if (this.Offset < 0) {
      this.Offset = 0;
    }
  }

  #endregion

}

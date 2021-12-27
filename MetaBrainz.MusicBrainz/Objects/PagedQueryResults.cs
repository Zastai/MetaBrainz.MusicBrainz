using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

  public int? Limit { get; set; }

  public abstract IReadOnlyDictionary<string, object?>? UnhandledProperties { get; }

  public TResults Next() => Utils.ResultOf(this.NextAsync());

  public async Task<TResults> NextAsync() {
    this.UpdateOffset(this.Results.Count);
    return await this.PerformRequestAsync().ConfigureAwait(false);
  }

  public int? NextOffset { get; set; }

  public int Offset { get; private set; }

  public TResults Previous() => Utils.ResultOf(this.PreviousAsync());

  public async Task<TResults> PreviousAsync() {
    this.UpdateOffset();
    return await this.PerformRequestAsync().ConfigureAwait(false);
  }

  public abstract IReadOnlyList<TItem> Results { get; }

  public abstract int TotalResults { get; }

  #endregion

  #region Protected Elements

  protected TResultObject? CurrentResult;

  protected abstract Task<TResults> Deserialize(HttpResponseMessage response);

  protected abstract string FullExtraText();

  #endregion

  #region Internals

  private readonly string _endpoint;

  private readonly Query _query;

  private readonly string? _value;

  private async Task<TResults> PerformRequestAsync() {
    var response = await this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText()).ConfigureAwait(false);
    return await this.Deserialize(response).ConfigureAwait(false);
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

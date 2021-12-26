using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects;

internal abstract class PagedQueryResults<TInterface, TItem, TResultObject> : IPagedQueryResults<TInterface, TItem>
where TInterface : IPagedQueryResults<TInterface, TItem>
where TResultObject : class {

  protected PagedQueryResults(Query query, string endpoint, string? value, int? limit, int? offset) {
    this._endpoint = endpoint;
    this.Limit = limit;
    this.NextOffset = offset;
    this.Offset = 0;
    this._query = query;
    this._value = value;
  }

  protected TResultObject? CurrentResult;

  private int CurrentResultCount => this.Results.Count;

  protected abstract Task<TInterface> Deserialize(HttpResponseMessage response);

  private readonly string _endpoint;

  protected abstract string FullExtraText();

  public int? Limit { get; set; }

  public TInterface Next() => Utils.ResultOf(this.NextAsync());

  public async Task<TInterface> NextAsync() {
    this.UpdateOffset(this.CurrentResultCount);
    return await this.PerformRequestAsync().ConfigureAwait(false);
  }

  public int? NextOffset { get; set; }

  public int Offset { get; private set; }

  public TInterface Previous() => Utils.ResultOf(this.PreviousAsync());

  public async Task<TInterface> PreviousAsync() {
    this.UpdateOffset();
    return await this.PerformRequestAsync().ConfigureAwait(false);
  }

  private async Task<TInterface> PerformRequestAsync() {
    var response = await this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText()).ConfigureAwait(false);
    return await this.Deserialize(response).ConfigureAwait(false);
  }

  private readonly Query _query;

  public abstract IReadOnlyList<TItem> Results { get; }

  public abstract int TotalResults { get; }

  public abstract IReadOnlyDictionary<string, object?>? UnhandledProperties { get; }

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

  private readonly string? _value;

  public async IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken cancellationToken = new ()) {
    foreach (var item in this.Results) {
      yield return item;
    }
    var current = await this.NextAsync();
    while (current.Results.Count > 0 && !cancellationToken.IsCancellationRequested) {
      foreach (var item in current.Results) {
        yield return item;
      }
      current = await current.NextAsync();
    }
  }
  

}

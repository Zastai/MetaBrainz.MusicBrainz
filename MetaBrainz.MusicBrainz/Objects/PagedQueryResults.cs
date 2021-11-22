using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects; 

internal abstract class PagedQueryResults<TInterface, TItem, TResultObject>
  : IPagedQueryResults<TInterface, TItem>
where TInterface : IPagedQueryResults<TInterface, TItem>
where TResultObject : class {

  protected PagedQueryResults(Query query, string endpoint, string? value, int? limit, int? offset) {
    this.Endpoint = endpoint;
    this.Limit = limit;
    this.NextOffset = offset;
    this.Offset = 0;
    this.Query = query;
    this.Value = value;
  }

  protected TResultObject? CurrentResult;

  private int CurrentResultCount => this.Results?.Count ?? 0;

  protected abstract TInterface Deserialize(string json);

  private readonly string Endpoint;

  protected abstract string FullExtraText();

  public int? Limit { get; set; }

  public TInterface Next() => this.Deserialize(this.NextResponse(this.CurrentResultCount));

  public async Task<TInterface> NextAsync() => this.Deserialize(await this.NextResponseAsync(this.CurrentResultCount).ConfigureAwait(false));

  public int? NextOffset { get; set; }

  private string NextResponse(int lastResultCount) {
    this.UpdateOffset(lastResultCount);
    return this.Query.PerformRequest(this.Endpoint, this.Value, this.FullExtraText());
  }

  private Task<string> NextResponseAsync(int lastResultCount) {
    this.UpdateOffset(lastResultCount);
    return this.Query.PerformRequestAsync(this.Endpoint, this.Value, this.FullExtraText());
  }

  public int Offset { get; private set; }

  public TInterface Previous() => this.Deserialize(this.PreviousResponse());

  public async Task<TInterface> PreviousAsync() => this.Deserialize(await this.PreviousResponseAsync().ConfigureAwait(false));

  private string PreviousResponse() {
    this.UpdateOffset();
    return this.Query.PerformRequest(this.Endpoint, this.Value, this.FullExtraText());
  }

  private Task<string> PreviousResponseAsync() {
    this.UpdateOffset();
    return this.Query.PerformRequestAsync(this.Endpoint, this.Value, this.FullExtraText());
  }

  private readonly Query Query;

  public abstract IReadOnlyList<TItem> Results { get; }

  public abstract int TotalResults { get; }

  public abstract IReadOnlyDictionary<string, object?>? UnhandledProperties { get; }

  private void UpdateOffset() {
    if (this.NextOffset.HasValue) {
      this.Offset = this.NextOffset.Value;
      this.NextOffset = null;
    }
    else {
      var limit = Math.Min(this.Limit.GetValueOrDefault(Query.DefaultPageSize), Query.MaximumPageSize);
      if (limit < 1)
        limit = Query.DefaultPageSize;
      this.Offset -= limit;
    }
    if (this.Offset < 0)
      this.Offset = 0;
  }

  private void UpdateOffset(int lastResultCount) {
    if (this.NextOffset.HasValue) {
      this.Offset = this.NextOffset.Value;
      this.NextOffset = null;
    }
    else
      this.Offset += lastResultCount;
    if (this.Offset < 0)
      this.Offset = 0;
  }

  private readonly string? Value;

}
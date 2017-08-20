using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects {

  internal abstract class PagedQueryResults<TInterface, TItem> : IPagedQueryResults<TInterface, TItem> where TInterface : IPagedQueryResults<TInterface, TItem> {

    protected PagedQueryResults(Query query, string endpoint, string value, int? limit = null, int? offset = null) {
      this._query     = query    ?? throw new ArgumentNullException(nameof(query));
      this._endpoint  = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
      this._value     = value;
      this.Limit      = limit;
      this.Offset     = 0;
      this.NextOffset = offset;
    }

    private readonly Query  _query;
    private readonly string _endpoint;
    private readonly string _value;

    protected abstract int CurrentResultCount { get; }

    protected abstract TInterface Deserialize(string json);

    protected abstract string FullExtraText();

    public int? Limit { get; set; }

    public TInterface Next() => this.Deserialize(this.NextResponse(this.CurrentResultCount));

    public async Task<TInterface> NextAsync() => this.Deserialize(await this.NextResponseAsync(this.CurrentResultCount).ConfigureAwait(false));

    public int? NextOffset { get; set; }

    private string NextResponse(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    private Task<string> NextResponseAsync(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

    public int Offset { get; private set; }

    public TInterface Previous() => this.Deserialize(this.PreviousResponse());

    public async Task<TInterface> PreviousAsync() => this.Deserialize(await this.PreviousResponseAsync().ConfigureAwait(false));

    private string PreviousResponse() {
      this.UpdateOffset();
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    private Task<string> PreviousResponseAsync() {
      this.UpdateOffset();
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

    public abstract IReadOnlyList<TItem> Results { get; }

    public abstract int TotalResults { get; }

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

  }

}

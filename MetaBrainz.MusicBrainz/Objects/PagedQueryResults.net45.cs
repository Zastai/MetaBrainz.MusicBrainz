using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MetaBrainz.MusicBrainz.Entities {

  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  internal abstract partial class PagedQueryResults<TInterface, TItem> : IPagedQueryResults<TInterface, TItem> {

    public abstract Task<TInterface> NextAsync();

    public abstract Task<TInterface> PreviousAsync();

    protected Task<string> NextResponseAsync(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

    protected Task<string> PreviousResponseAsync() {
      this.UpdateOffset();
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

  }

}

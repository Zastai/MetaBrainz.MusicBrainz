using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Browses;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  using Interface = IBrowseResults<IWork>;

  internal abstract partial class BrowseWorksBase {

    public sealed override async Task<Interface> NextAsync() {
      var json = await base.NextResponseAsync(this._currentResult?.results.Length ?? 0).ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public sealed override async Task<Interface> PreviousAsync() {
      var json = await base.PreviousResponseAsync().ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

  }

}

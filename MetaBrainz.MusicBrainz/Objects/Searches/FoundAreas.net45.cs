using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  using Interface = ISearchResults<IFoundArea>;

  internal sealed partial class FoundAreas {

    public override async Task<Interface> NextAsync() {
      var json = await base.NextResponseAsync(this._currentResult?.results.Length ?? 0).ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override async Task<Interface> PreviousAsync() {
      var json = await base.PreviousResponseAsync().ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

  }

}

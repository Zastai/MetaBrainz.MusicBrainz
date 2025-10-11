using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the genres known to MusicBrainz.</summary>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  public Task<IBrowseResults<IGenre>> BrowseAllGenresAsync(int? limit = null, int? offset = null,
                                                           Include inc = Include.None,
                                                           CancellationToken cancellationToken = default)
    => new BrowseGenres(this, Query.CreateOptions(inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Gets the names of all genres known to MusicBrainz.</summary>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>All genre names, in alphabetical order.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public async Task<string[]> GetAllGenreNamesAsync(CancellationToken cancellationToken = default) {
    var result = await this.PerformRequestAsync("genre", "all", null, cancellationToken, "txt");
    var text = await result.GetStringContentAsync(cancellationToken);
    return text.Split('\n');
  }

}

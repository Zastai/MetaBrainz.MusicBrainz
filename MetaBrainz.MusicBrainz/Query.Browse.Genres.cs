using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Gets the names of all genres known to MusicBrainz.</summary>
  /// <returns>All genre names, in alphabetical order.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string[] GetAllGenreNames() => AsyncUtils.ResultOf(this.GetAllGenreNamesAsync());

  /// <summary>Gets the names of all genres known to MusicBrainz.</summary>
  /// <returns>All genre names, in alphabetical order.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public async Task<string[]> GetAllGenreNamesAsync(CancellationToken cancellationToken = default) {
    var result = await this.PerformRequestAsync("genre", "all", null, cancellationToken, "txt");
    var text = await result.GetStringContentAsync(cancellationToken);
    return text.Split('\n');
  }

}

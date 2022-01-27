using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for artists using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>
  /// All results of the search request.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// <para>
  /// When <paramref name="simple"/> is specified as <see langword="true"/>, certain special query characters are escaped
  /// automatically, for ease of use. This corresponds to using "Indexed Search" on MusicBrainz.
  /// </para>
  /// <para>
  /// Otherwise, the full Lucene query syntax applies. This corresponds to using "Indexed Search with Advanced Query Syntax" on
  /// MusicBrainz. The following fields are available for the Lucene query:
  /// <list type="table">
  /// <listheader><term>Field</term><description>Description</description></listheader>
  /// <item><term>alias</term><description>an alias attached to the artist</description></item>
  /// <item><term>area</term><description>the artist's main associated area</description></item>
  /// <item><term>arid</term><description>the artist's MBID</description></item>
  /// <item><term>artist</term><description>the artist's name (without accented characters)</description></item>
  /// <item><term>artistaccent</term><description>the artist's name (with accented characters)</description></item>
  /// <item><term>begin</term><description>the artist's begin date</description></item>
  /// <item><term>beginarea</term><description>the artist's begin area</description></item>
  /// <item><term>comment</term><description>the artist's disambiguation comment</description></item>
  /// <item>
  /// <term>country</term>
  /// <description>the 2-character code (ISO 3166-1 alpha-2) for the artist's main associated country</description>
  /// </item>
  /// <item><term>end</term><description>the artist's end date</description></item>
  /// <item><term>endarea</term><description>the artist's end area</description></item>
  /// <item><term>ended</term><description>a flag indicating whether or not the artist has ended</description></item>
  /// <item><term>gender</term><description>the artist's gender ("male", "female", or "other")</description></item>
  /// <item><term>ipi</term><description>an IPI code associated with the artist</description></item>
  /// <item><term>sortname</term><description>the artist's sort name</description></item>
  /// <item><term>tag</term><description>a tag attached to the artist</description></item>
  /// <item><term>type</term><description>the artist's type ("person", "group", ...)</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>alias</em>, <em>artist</em> and <em>sortname</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Artist">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<IArtist>> FindAllArtists(string query, int? pageSize = null, int? offset = null,
                                                                       bool simple = false)
    => new FoundArtists(this, query, pageSize, offset, simple).AsStream();

  /// <summary>Searches for artists using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllArtists"/></remarks>
  public ISearchResults<ISearchResult<IArtist>> FindArtists(string query, int? limit = null, int? offset = null,
                                                            bool simple = false)
    => Utils.ResultOf(this.FindArtistsAsync(query, limit, offset, simple));

  /// <summary>Searches for artists using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllArtists"/></remarks>
  public Task<ISearchResults<ISearchResult<IArtist>>> FindArtistsAsync(string query, int? limit = null, int? offset = null,
                                                                       bool simple = false,
                                                                       CancellationToken cancellationToken = default)
    => new FoundArtists(this, query, limit, offset, simple).NextAsync(cancellationToken);

}

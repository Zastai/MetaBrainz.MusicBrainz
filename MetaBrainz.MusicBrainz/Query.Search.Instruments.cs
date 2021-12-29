using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for instruments using the given query.</summary>
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
  /// <item><term>alias</term><description>an alias attached to the instrument</description></item>
  /// <item><term>comment</term><description>the disambiguation comment for the instrument</description></item>
  /// <item><term>description</term><description>the description of the instrument</description></item>
  /// <item><term>iid</term><description>the MBID of the instrument</description></item>
  /// <item><term>instrument</term><description>the name of the instrument</description></item>
  /// <item><term>type</term><description>the instrument's type</description></item>
  /// <item><term>tag</term><description>a tag attached to the instrument</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>alias</em>, <em>description</em> and <em>instrument</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Instrument">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<IInstrument>> FindAllInstruments(string query, int? pageSize = null,
                                                                               int? offset = null, bool simple = false)
    => new FoundInstruments(this, query, pageSize, offset, simple).AsStream();

  /// <summary>Searches for instruments using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllInstruments"/></remarks>
  public ISearchResults<ISearchResult<IInstrument>> FindInstruments(string query, int? limit = null, int? offset = null,
                                                                    bool simple = false)
    => Utils.ResultOf(this.FindInstrumentsAsync(query, limit, offset, simple));

  /// <summary>Searches for instruments using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllInstruments"/></remarks>
  public Task<ISearchResults<ISearchResult<IInstrument>>> FindInstrumentsAsync(string query, int? limit = null, int? offset = null,
                                                                               bool simple = false,
                                                                               CancellationToken cancellationToken = new())
    => new FoundInstruments(this, query, limit, offset, simple).NextAsync(cancellationToken);

}

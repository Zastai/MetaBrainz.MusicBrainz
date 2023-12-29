using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for labels using the given query.</summary>
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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
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
  /// <item><term>alias</term><description>an alias attached to the label</description></item>
  /// <item><term>area</term><description>the name of the main area associated with the label</description></item>
  /// <item><term>begin</term><description>the label's founding date</description></item>
  /// <item><term>code</term><description>the label's label code (only the number, no "LC" prefix)</description></item>
  /// <item><term>comment</term><description>the disambiguation comment for the label</description></item>
  /// <item>
  /// <term>country</term>
  /// <description>the 2-character code (ISO 3166-1 alpha-2) for the label's main associated country</description>
  /// </item>
  /// <item><term>end</term><description>the label's dissolution date</description></item>
  /// <item><term>ended</term><description>a flag indicating whether or not the label has been dissolved</description></item>
  /// <item><term>ipi</term><description>an IPI code associated with the label</description></item>
  /// <item><term>label</term><description>the label's name (without accented characters)</description></item>
  /// <item><term>labelaccent</term><description>the label's name (with accented characters)</description></item>
  /// <item><term>laid</term><description>the label's MBID</description></item>
  /// <item><term>sortname</term><description>the label's sort name</description></item>
  /// <item><term>tag</term><description>a tag attached to the label</description></item>
  /// <item><term>type</term><description>the label's type</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>alias</em>, <em>label</em> and <em>sortname</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Label">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<ILabel>> FindAllLabels(string query, int? pageSize = null, int? offset = null,
                                                                     bool simple = false)
    => new FoundLabels(this, query, pageSize, offset, simple).AsStream();

  /// <summary>Searches for labels using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks><inheritdoc cref="FindAllLabels"/></remarks>
  public ISearchResults<ISearchResult<ILabel>> FindLabels(string query, int? limit = null, int? offset = null, bool simple = false)
    => AsyncUtils.ResultOf(this.FindLabelsAsync(query, limit, offset, simple));

  /// <summary>Searches for labels using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks><inheritdoc cref="FindAllLabels"/></remarks>
  public Task<ISearchResults<ISearchResult<ILabel>>> FindLabelsAsync(string query, int? limit = null, int? offset = null,
                                                                     bool simple = false,
                                                                     CancellationToken cancellationToken = default)
    => new FoundLabels(this, query, limit, offset, simple).NextAsync(cancellationToken);

}

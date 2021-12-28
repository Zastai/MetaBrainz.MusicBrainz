using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for releases using the given query.</summary>
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
  /// <item><term>arid</term><description>the MBID of an artist credited for the release</description></item>
  /// <item><term>artist</term><description>the full artist credit for the release</description></item>
  /// <item><term>artistname</term><description>the name of an artist credited for the release</description></item>
  /// <item><term>asin</term><description>an Amazon ASIN associated with the release</description></item>
  /// <item><term>barcode</term><description>the release's barcode</description></item>
  /// <item><term>catno</term><description>a catalog number associated with the release</description></item>
  /// <item><term>comment</term><description>the disambiguation comment for the release</description></item>
  /// <item>
  /// <term>country</term>
  /// <description>the 2-character code (ISO 3166-1 alpha-2) for the release's main associated country</description>
  /// </item>
  /// <item><term>creditname</term><description>the name of an artist credited for the release, as credited</description></item>
  /// <item><term>date</term><description>the release date (YYYY-MM-DD)</description></item>
  /// <item>
  /// <term>discids</term>
  /// <description>the total number of Disc IDs (across all mediums) linked to the release</description>
  /// </item>
  /// <item>
  /// <term>discidsmedium</term><description>the number of DiscIDs linked to a single medium in the release</description>
  /// </item>
  /// <item><term>format</term><description>the release's format</description></item>
  /// <item><term>label</term><description>the name of a label associated with the release</description></item>
  /// <item><term>laid</term><description>the MBID of a label associated with the release</description></item>
  /// <item><term>lang</term><description>the three-character language code (ISO 639) for the release</description></item>
  /// <item><term>mediums</term><description>the number of mediums in the release</description></item>
  /// <item><term>primarytype</term><description>the primary type of the release's release group</description></item>
  /// <item><term>quality</term><description>the release's data quality (low/normal/high)</description></item>
  /// <item><term>reid</term><description>the release's MBID</description></item>
  /// <item><term>release</term><description>the release's title (without accented characters)</description></item>
  /// <item><term>releaseaccent</term><description>the release's title (with accented characters)</description></item>
  /// <item><term>rgid</term><description>the MBID of the release's release group</description></item>
  /// <item><term>script</term><description>the 4-character code (ISO 15924) for the release's script</description></item>
  /// <item><term>secondarytype</term><description>the secondary type of the release's release group</description></item>
  /// <item><term>status</term><description>the release's status</description></item>
  /// <item><term>tag</term><description>a tag attached to the release</description></item>
  /// <item><term>tracks</term><description>the total number of tracks (across all mediums) on the release</description></item>
  /// <item><term>tracksmedium</term><description>the number of tracks on a single medium in the release</description></item>
  /// <item><term>type</term><description>the primary or secondary type of the release's release group</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>release</em> field only.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Release">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<IRelease>> FindAllReleases(string query, int? pageSize = null, int? offset = null,
                                                                         bool simple = false)
    => new FoundReleases(this, query, pageSize, offset, simple).AsStream();

  /// <inheritdoc cref="FindReleasesAsync"/>
  public ISearchResults<ISearchResult<IRelease>> FindReleases(string query, int? limit = null, int? offset = null,
                                                              bool simple = false)
    => Utils.ResultOf(this.FindReleasesAsync(query, limit, offset, simple));

  /// <summary>Searches for releases using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllReleases"/></remarks>
  public Task<ISearchResults<ISearchResult<IRelease>>> FindReleasesAsync(string query, int? limit = null, int? offset = null,
                                                                         bool simple = false)
    => new FoundReleases(this, query, limit, offset, simple).NextAsync();

}

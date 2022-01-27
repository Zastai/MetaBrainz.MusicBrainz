using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for recordings using the given query.</summary>
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
  /// <item><term>arid</term><description>the MBID of an artist credited for the recording</description></item>
  /// <item><term>artist</term><description>the full artist credit for the recording</description></item>
  /// <item><term>artistname</term><description>the name of an artist credited for the recording</description></item>
  /// <item><term>comment</term><description>the disambiguation comment for the recording</description></item>
  /// <item>
  /// <term>country</term>
  /// <description>
  /// the 2-character code (ISO 3166-1 alpha-2) of the main associated country for a release containing the recording
  /// </description>
  /// </item>
  /// <item>
  /// <term>creditname</term><description>the name of an artist credited for the recording, as credited</description>
  /// </item>
  /// <item><term>date</term><description>the recording's (earliest) release date</description></item>
  /// <item><term>dur</term><description>the recording's (average) duration, in milliseconds</description></item>
  /// <item><term>format</term><description>the format of a release containing the recording</description></item>
  /// <item><term>isrc</term><description>an ISRC associated with the recording</description></item>
  /// <item><term>number</term><description>the track number set for the recording on a release</description></item>
  /// <item>
  /// <term>position</term><description>the (1-based) medium number containing the recording on a release</description>
  /// </item>
  /// <item>
  /// <term>primarytype</term>
  /// <description>
  /// the primary type (album, single, ...) of a release group including a release containing the recording
  /// </description>
  /// </item>
  /// <item><term>qdur</term><description>the recording's quantized duration (duration / 2000)</description></item>
  /// <item><term>recording</term><description>the recording's title (without accented characters)</description></item>
  /// <item><term>recordingaccent</term><description>the recording's title (with accented characters)</description></item>
  /// <item><term>reid</term><description>the MBID of a release containing the recording</description></item>
  /// <item><term>release</term><description>the name of a release containing the recording</description></item>
  /// <item>
  /// <term>rgid</term><description>the MBID of a release group including a release containing the recording</description>
  /// </item>
  /// <item><term>rid</term><description>the recording's MBID</description></item>
  /// <item>
  /// <term>secondarytype</term>
  /// <description>
  /// a secondary type (compilation, live, ...) of a release group including a release containing the recording
  /// </description>
  /// </item>
  /// <item>
  /// <term>status</term>
  /// <description>the status (official, promotion, ...) of a release containing the recording</description>
  /// </item>
  /// <item><term>tag</term><description>a tag attached to the recording</description></item>
  /// <item><term>tid</term><description>the MBID of a track linked to the recording</description></item>
  /// <item><term>tnum</term><description>the recording's (1-based) track number on a medium</description></item>
  /// <item><term>tracks</term><description>the number of tracks on a medium containing the recording</description></item>
  /// <item>
  /// <term>tracksrelease</term><description>the total number of tracks on a release containing the recording</description>
  /// </item>
  /// <item>
  /// <term>type</term>
  /// <description>
  /// a primary or secondary type (compilation, live, ...) of a release group including a release containing the recording, or
  /// "standalone" for standalone recordings
  /// </description>
  /// </item>
  /// <item><term>video</term><description>a flag indicating whether or not the recording includes video</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>recording</em> field only.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Recording">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<IRecording>> FindAllRecordings(string query, int? pageSize = null, int? offset = null,
                                                                             bool simple = false)
    => new FoundRecordings(this, query, pageSize, offset, simple).AsStream();

  /// <summary>Searches for recordings using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllRecordings"/></remarks>
  public ISearchResults<ISearchResult<IRecording>> FindRecordings(string query, int? limit = null, int? offset = null,
                                                                  bool simple = false)
    => AsyncUtils.ResultOf(this.FindRecordingsAsync(query, limit, offset, simple));

  /// <summary>Searches for recordings using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllRecordings"/></remarks>
  public Task<ISearchResults<ISearchResult<IRecording>>> FindRecordingsAsync(string query, int? limit = null, int? offset = null,
                                                                             bool simple = false,
                                                                             CancellationToken cancellationToken = default)
    => new FoundRecordings(this, query, limit, offset, simple).NextAsync(cancellationToken);

}

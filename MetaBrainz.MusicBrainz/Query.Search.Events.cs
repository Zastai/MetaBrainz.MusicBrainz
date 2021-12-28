using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for events using the given query.</summary>
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
  /// <item><term>alias</term><description>an alias attached to the event</description></item>
  /// <item><term>aid</term><description>the MBID of an area related to the event</description></item>
  /// <item><term>area</term><description>the name of an area related to the event</description></item>
  /// <item><term>arid</term><description>the MBID of an artist related to the event</description></item>
  /// <item><term>artist</term><description>the name of an artist related to the event</description></item>
  /// <item><term>comment</term><description>the disambiguation comment for the event</description></item>
  /// <item><term>eid</term><description>the MBID of the event</description></item>
  /// <item><term>event</term><description>the name of the event</description></item>
  /// <item><term>pid</term><description>the MBID of a place related to the event</description></item>
  /// <item><term>place</term><description>the name of a place related to the event</description></item>
  /// <item><term>type</term><description>the event's type</description></item>
  /// <item><term>tag</term><description>a tag attached to the event</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>alias</em>, <em>artist</em> and <em>name</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Event">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public IStreamingQueryResults<ISearchResult<IEvent>> FindAllEvents(string query, int? pageSize = null, int? offset = null,
                                                                     bool simple = false)
    => new FoundEvents(this, query, pageSize, offset, simple).AsStream();

  /// <inheritdoc cref="FindEventsAsync"/>
  public ISearchResults<ISearchResult<IEvent>> FindEvents(string query, int? limit = null, int? offset = null, bool simple = false)
    => Utils.ResultOf(this.FindEventsAsync(query, limit, offset, simple));

  /// <summary>Searches for events using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks><inheritdoc cref="FindAllEvents"/></remarks>
  public Task<ISearchResults<ISearchResult<IEvent>>> FindEventsAsync(string query, int? limit = null, int? offset = null,
                                                                     bool simple = false)
    => new FoundEvents(this, query, limit, offset, simple).NextAsync();

}

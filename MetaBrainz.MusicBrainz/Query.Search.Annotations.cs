using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Searches for annotations using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
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
  /// <item><term>entity</term><description>the MBID of the annotated entity</description></item>
  /// <item><term>name</term><description>the name of the annotated entity</description></item>
  /// <item><term>text</term><description>the content of the annotation (includes wiki formatting)</description></item>
  /// <item><term>type</term><description>the type of the annotated entity</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>name</em>, <em>text</em> and <em>type</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Annotation">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public ISearchResults<ISearchResult<IAnnotation>> FindAnnotations(string query, int? limit = null, int? offset = null,
                                                                    bool simple = false)
    => Utils.ResultOf(this.FindAnnotationsAsync(query, limit, offset, simple));

  /// <summary>Searches for annotations using the given query.</summary>
  /// <param name="query">The search query to use.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="simple">If set to <see langword="true"/>, this disables advanced query syntax.</param>
  /// <returns>The search request, including the initial results.</returns>
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
  /// <item><term>entity</term><description>the MBID of the annotated entity</description></item>
  /// <item><term>name</term><description>the name of the annotated entity</description></item>
  /// <item><term>text</term><description>the content of the annotation (includes wiki formatting)</description></item>
  /// <item><term>type</term><description>the type of the annotated entity</description></item>
  /// </list>
  /// Query terms without a field specifier will search the <em>name</em>, <em>text</em> and <em>type</em> fields.
  /// </para>
  /// <para>
  /// See <a href="http://www.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Annotation">the MusicBrainz
  /// Search API Docs</a> for more details.
  /// </para>
  /// </remarks>
  public Task<ISearchResults<ISearchResult<IAnnotation>>> FindAnnotationsAsync(string query, int? limit = null, int? offset = null,
                                                                               bool simple = false)
    => new FoundAnnotations(this, query, limit, offset, simple).NextAsync();

}

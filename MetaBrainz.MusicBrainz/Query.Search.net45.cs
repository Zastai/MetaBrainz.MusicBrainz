using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    /// <summary>Searches for annotations using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>entity:</term><description>the MBID of the annotated entity</description></item>
    ///     <item><term>name:</term><description>the name of the annotated entity</description></item>
    ///     <item><term>text:</term><description>the content of the annotation</description></item>
    ///     <item><term>type:</term><description>the type of the annotated entity</description></item>
    ///   </list>
    ///   When no fields are specified, the <em>name</em>, <em>text</em> and <em>type</em> fields will be searched.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Annotation">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundAnnotation>> FindAnnotationsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundAnnotations(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for areas using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>aid:</term><description>the area's MBID</description></item>
    ///     <item><term>alias:</term><description>an alias attached to the area</description></item>
    ///     <item><term>area:</term><description>the area's name</description></item>
    ///     <item><term>begin:</term><description>the area's begin date</description></item>
    ///     <item><term>comment:</term><description>the area's disambiguation comment</description></item>
    ///     <item><term>end:</term><description>the area's end date</description></item>
    ///     <item><term>ended:</term><description>a flag indicating whether or not the area has ended</description></item>
    ///     <item><term>iso1:</term><description>an ISO 3166-1 code attached to the area</description></item>
    ///     <item><term>iso2:</term><description>an ISO 3166-2 code attached to the area</description></item>
    ///     <item><term>iso3:</term><description>an ISO 3166-3 code attached to the area</description></item>
    ///     <item><term>iso:</term><description>an ISO 3166-1/2/3 code attached to the area</description></item>
    ///     <item><term>sortname:</term><description>the area's sort name</description></item>
    ///     <item><term>type:</term><description>the area's type</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>alias</em>, <em>area</em> and <em>sortname</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Area">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundArea>> FindAreasAsync(string query, int? limit = null, int? offset = null) {
      return new FoundAreas(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for artists using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the artist</description></item>
    ///     <item><term>area:</term><description>the artist's main associated area</description></item>
    ///     <item><term>arid:</term><description>the artist's MBID</description></item>
    ///     <item><term>artist:</term><description>the artist's name (without accented characters)</description></item>
    ///     <item><term>artistaccent:</term><description>the artist's name (with accented characters)</description></item>
    ///     <item><term>begin:</term><description>the artist's begin date</description></item>
    ///     <item><term>beginarea:</term><description>the artist's begin area</description></item>
    ///     <item><term>comment:</term><description>the artist's disambiguation comment</description></item>
    ///     <item><term>country:</term><description>the 2-character code (ISO 3166-1 alpha-2) for the artist's main associated country</description></item>
    ///     <item><term>end:</term><description>the artist's end date</description></item>
    ///     <item><term>endarea:</term><description>the artist's end area</description></item>
    ///     <item><term>ended:</term><description>a flag indicating whether or not the artist has ended</description></item>
    ///     <item><term>gender:</term><description>the artist's gender ("male", "female", or "other")</description></item>
    ///     <item><term>ipi:</term><description>an IPI code associated with the artist</description></item>
    ///     <item><term>sortname:</term><description>the artist's sort name</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the artist</description></item>
    ///     <item><term>type:</term><description>the artist's type ("person", "group", ...)</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>alias</em>, <em>artist</em> and <em>sortname</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Artist">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundArtist>> FindArtistsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundArtists(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for CD stubs using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>artist:</term><description>the artist name set on the CD stub</description></item>
    ///     <item><term>barcode:</term><description>the barcode set on the CD stub</description></item>
    ///     <item><term>comment:</term><description>the comment set on the CD stub</description></item>
    ///     <item><term>discid:</term><description>the CD stub's Disc ID</description></item>
    ///     <item><term>title:</term><description>the release title set on the CD stub</description></item>
    ///     <item><term>tracks:</term><description>the CD stub's number of tracks</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>artist</em> and <em>title</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#CdStubs">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundCdStub>> FindCdStubsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundCdStubs(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for events using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the event</description></item>
    ///     <item><term>aid:</term><description>the MBID of an area related to the event</description></item>
    ///     <item><term>area:</term><description>the name of an area related to the event</description></item>
    ///     <item><term>arid:</term><description>the MBID of an artist related to the event</description></item>
    ///     <item><term>artist:</term><description>the name of an artist related to the event</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the event</description></item>
    ///     <item><term>eid:</term><description>the MBID of the event</description></item>
    ///     <item><term>event:</term><description>the name of the event</description></item>
    ///     <item><term>pid:</term><description>the MBID of a place related to the event</description></item>
    ///     <item><term>place:</term><description>the name of a place related to the event</description></item>
    ///     <item><term>type:</term><description>the event's type</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the event</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>alias</em>, <em>artist</em> and <em>name</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Event">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundEvent>> FindEventsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundEvents(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for instruments using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the instrument</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the instrument</description></item>
    ///     <item><term>description:</term><description>the description of the instrument</description></item>
    ///     <item><term>iid:</term><description>the MBID of the instrument</description></item>
    ///     <item><term>instrument:</term><description>the name of the instrument</description></item>
    ///     <item><term>type:</term><description>the instrument's type</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the instrument</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>alias</em>, <em>description</em> and <em>instrument</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Instrument">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundInstrument>> FindInstrumentsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundInstruments(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for labels using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the label</description></item>
    ///     <item><term>area:</term><description>the name of the main area associated with the label</description></item>
    ///     <item><term>begin:</term><description>the label's founding date</description></item>
    ///     <item><term>code:</term><description>the label's label code (only the number, no "LC" prefix)</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the label</description></item>
    ///     <item><term>country:</term><description>the 2-character code (ISO 3166-1 alpha-2) for the label's main associated country</description></item>
    ///     <item><term>end:</term><description>the label's dissolution date</description></item>
    ///     <item><term>ended:</term><description>a flag indicating whether or not the label has been dissolved</description></item>
    ///     <item><term>ipi:</term><description>an IPI code associated with the label</description></item>
    ///     <item><term>label:</term><description>the label's name (without accented characters)</description></item>
    ///     <item><term>labelaccent:</term><description>the label's name (with accented characters)</description></item>
    ///     <item><term>laid:</term><description>the label's MBID</description></item>
    ///     <item><term>sortname:</term><description>the label's sort name</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the label</description></item>
    ///     <item><term>type:</term><description>the label's type</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Label">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundLabel>> FindLabelsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundLabels(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for places using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the place</description></item>
    ///     <item><term>area:</term><description>the name of the main area associated with the place</description></item>
    ///     <item><term>begin:</term><description>the place's begin date</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the place</description></item>
    ///     <item><term>end:</term><description>the place's end date</description></item>
    ///     <item><term>ended:</term><description>a flag indicating whether or not the place has ended</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the place</description></item>
    ///     <item><term>type:</term><description>the place's type</description></item>
    ///     <item><term>address:</term><description>the place's address</description></item>
    ///     <item><term>lat:</term><description>the place's latitude</description></item>
    ///     <item><term>long:</term><description>the place's longitude</description></item>
    ///     <item><term>pid:</term><description>the place's MBID</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Place">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundPlace>> FindPlacesAsync(string query, int? limit = null, int? offset = null) {
      return new FoundPlaces(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for recordings using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>arid:</term><description>the MBID of an artist credited for the recording</description></item>
    ///     <item><term>artist:</term><description>the full artist credit for the recording</description></item>
    ///     <item><term>artistname:</term><description>the name of an artist credited for the recording</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the recording</description></item>
    ///     <item><term>country:</term><description>the 2-character code (ISO 3166-1 alpha-2) of the main associated country for a release containing the recording</description></item>
    ///     <item><term>creditname:</term><description>the name of an artist credited for the recording, as credited</description></item>
    ///     <item><term>date:</term><description>the recording's (earliest) release date</description></item>
    ///     <item><term>dur:</term><description>the recording's (average) duration, in milliseconds</description></item>
    ///     <item><term>format:</term><description>the format of a release containing the recording</description></item>
    ///     <item><term>isrc:</term><description>an ISRC associated with the recording</description></item>
    ///     <item><term>number:</term><description>the track number set for the recording on a release</description></item>
    ///     <item><term>position:</term><description>the (1-based) medium number containing the recording on a release</description></item>
    ///     <item><term>primarytype:</term><description>the primary type (album, single, ...) of a release group including a release containing the recording</description></item>
    ///     <item><term>qdur:</term><description>the recording's quantized duration (duration / 2000)</description></item>
    ///     <item><term>recording:</term><description>the recording's title (without accented characters)</description></item>
    ///     <item><term>recordingaccent:</term><description>the recording's title (with accented characters)</description></item>
    ///     <item><term>reid:</term><description>the MBID of a release containing the recording</description></item>
    ///     <item><term>release:</term><description>the name of a release containing the recording</description></item>
    ///     <item><term>rgid:</term><description>the MBID of a release group including a release containing the recording</description></item>
    ///     <item><term>rid:</term><description>the recording's MBID</description></item>
    ///     <item><term>secondarytype:</term><description>a secondary type (compilation, live, ...) of a release group including a release containing the recording</description></item>
    ///     <item><term>status:</term><description>the status (official, promotion, ...) of a release containing the recording</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the recording</description></item>
    ///     <item><term>tid:</term><description>the MBID of a track linked to the recording</description></item>
    ///     <item><term>tnum:</term><description>the recording's (1-based) track number on a medium</description></item>
    ///     <item><term>tracks:</term><description>the number of tracks on a medium containing the recording</description></item>
    ///     <item><term>tracksrelease:</term><description>the total number of tracks on a release containing the recording</description></item>
    ///     <item><term>type:</term><description>a primary or secondary type (compilation, live, ...) of a release group including a release containing the recording, or "standalone" for standalone recordings</description></item>
    ///     <item><term>video:</term><description>a flag indicating whether or not the recording includes video</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Recording">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundRecording>> FindRecordingsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundRecordings(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for release groups using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>arid:</term><description>the MBID of an artist credited for the release group</description></item>
    ///     <item><term>artist:</term><description>the full artist credit for the release group</description></item>
    ///     <item><term>artistname:</term><description>the name of an artist credited for the release group</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the release group</description></item>
    ///     <item><term>creditname:</term><description>the name of an artist credited for the release group, as credited</description></item>
    ///     <item><term>primarytype:</term><description>the primary type (album, single, ...) of the release group</description></item>
    ///     <item><term>reid:</term><description>the MBID of a release in the release group</description></item>
    ///     <item><term>release:</term><description>the name of a release in the release group</description></item>
    ///     <item><term>releasegroup:</term><description>the release group's title (without accented characters)</description></item>
    ///     <item><term>releasegroupaccent:</term><description>the release group's title (with accented characters)</description></item>
    ///     <item><term>releases:</term><description>the number of releases in the release group</description></item>
    ///     <item><term>rgid:</term><description>the release group's MBID</description></item>
    ///     <item><term>secondarytype:</term><description>a secondary type (compilation, live, ...) of the release group</description></item>
    ///     <item><term>status:</term><description>the status (official, promotion, ...) of a release in the release group</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the release group</description></item>
    ///     <item><term>type:</term><description>a primary or secondary type of the release group</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Release_Group">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundReleaseGroup>> FindReleaseGroupsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundReleaseGroups(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for releases using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>arid:</term><description>the MBID of an artist credited for the release</description></item>
    ///     <item><term>artist:</term><description>the full artist credit for the release</description></item>
    ///     <item><term>artistname:</term><description>the name of an artist credited for the release</description></item>
    ///     <item><term>asin:</term><description>an Amazon ASIN associated with the release</description></item>
    ///     <item><term>barcode:</term><description>the release's barcode</description></item>
    ///     <item><term>catno:</term><description>a catalog number associated with the release</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the release</description></item>
    ///     <item><term>country:</term><description>the 2-character code (ISO 3166-1 alpha-2) for the release's main associated country</description></item>
    ///     <item><term>creditname:</term><description>the name of an artist credited for the release, as credited</description></item>
    ///     <item><term>date:</term><description>the release date (YYYY-MM-DD)</description></item>
    ///     <item><term>discids:</term><description>the total number of Disc IDs (across all mediums) linked to the release</description></item>
    ///     <item><term>discidsmedium:</term><description>the number of DiscIDs linked to a single medium in the release</description></item>
    ///     <item><term>format:</term><description>the release's format</description></item>
    ///     <item><term>label:</term><description>the name of a label associated with the release</description></item>
    ///     <item><term>laid:</term><description>the MBID of a label associated with the release</description></item>
    ///     <item><term>lang:</term><description>the three-character language code (ISO 639) for the release</description></item>
    ///     <item><term>mediums:</term><description>the number of mediums in the release</description></item>
    ///     <item><term>primarytype:</term><description>the primary type of the release's release group</description></item>
    ///     <item><term>quality:</term><description>the release's data quality (low/normal/high)</description></item>
    ///     <item><term>reid:</term><description>the release's MBID</description></item>
    ///     <item><term>release:</term><description>the release's title (without accented characters)</description></item>
    ///     <item><term>releaseaccent:</term><description>the release's title (with accented characters)</description></item>
    ///     <item><term>rgid:</term><description>the MBID of the release's release group</description></item>
    ///     <item><term>script:</term><description>the 4-character code (ISO 15924) for the release's script</description></item>
    ///     <item><term>secondarytype:</term><description>the secondary type of the release's release group</description></item>
    ///     <item><term>status:</term><description>the release's status</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the release</description></item>
    ///     <item><term>tracks:</term><description>the total number of tracks (across all mediums) on the release</description></item>
    ///     <item><term>tracksmedium:</term><description>the number of tracks on a single medium in the release</description></item>
    ///     <item><term>type:</term><description>the primary or secondary type of the release's release group</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Release">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundRelease>> FindReleasesAsync(string query, int? limit = null, int? offset = null) {
      return new FoundReleases(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for series using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the series</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the series</description></item>
    ///     <item><term>series:</term><description>the name of the series</description></item>
    ///     <item><term>sid:</term><description>the MBID of the series</description></item>
    ///     <item><term>type:</term><description>the series' type</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the series</description></item>
    ///   </list>
    ///   Query terms without a field specifier will search the <em>alias</em> and <em>series</em> fields.
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Series">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundSeries>> FindSeriesAsync(string query, int? limit = null, int? offset = null) {
      return new FoundSeries(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for tags using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>tag:</term><description>the tag</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Tag">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundTag>> FindTagsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundTags(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for URLs using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>relationtype:</term><description>the type of a relationship attached to the URL</description></item>
    ///     <item><term>targetid:</term><description>the MBID of an entity related to the URL</description></item>
    ///     <item><term>targettype:</term><description>an entity type related to the URL</description></item>
    ///     <item><term>uid:</term><description>the URL's MBID</description></item>
    ///     <item><term>url:</term><description>the URL itself</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#URL">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundUrl>> FindUrlsAsync(string query, int? limit = null, int? offset = null) {
      return new FoundUrls(this, query, limit, offset).NextAsync();
    }

    /// <summary>Searches for works using the given query.</summary>
    /// <param name="query">The Lucene search query to use.</param>
    /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
    /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
    /// <returns>An asynchronous operation returning the search request, including the initial results.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    /// <remarks>
    ///   The following fields are available for the Lucene query:
    ///   <list type="bullet">
    ///     <item><term>alias:</term><description>an alias attached to the work</description></item>
    ///     <item><term>arid:</term><description>the MBID of an artist related to the work</description></item>
    ///     <item><term>artist:</term><description>the name of an artist related to the work</description></item>
    ///     <item><term>comment:</term><description>the disambiguation comment for the work</description></item>
    ///     <item><term>iswc:</term><description>an ISWC attached to the work</description></item>
    ///     <item><term>lang:</term><description>the lyrics language for the work</description></item>
    ///     <item><term>tag:</term><description>a tag attached to the work</description></item>
    ///     <item><term>type:</term><description>the work's type</description></item>
    ///     <item><term>wid:</term><description>the MBID of the work</description></item>
    ///     <item><term>work:</term><description>the name of the work (without accented characters)</description></item>
    ///     <item><term>workaccent:</term><description>the name of the work (with accented characters)</description></item>
    ///   </list>
    /// </remarks>
    /// <seealso cref="!:http://beta.musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search#Work">MusicBrainz Search API Docs</seealso>
    public Task<ISearchResults<IFoundWork>> FindWorksAsync(string query, int? limit = null, int? offset = null) {
      return new FoundWorks(this, query, limit, offset).NextAsync();
    }

  }

}

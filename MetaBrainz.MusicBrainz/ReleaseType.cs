using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Enumeration of the release type values (combines primary and secondary types).</summary>
  /// <remarks>For more detailed descriptions, see <a href="https://musicbrainz.org/doc/Release_Group/Type">the MusicBrainz documentation website</a>.</remarks>
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public enum ReleaseType : byte {

    #region Primary Types

    /// <summary>
    /// An album, perhaps better defined as a "Long Play" (LP) release, generally consists of previously unreleased material (unless this type is combined with secondary
    /// types which change that, such as <see cref="Compilation"/>).
    /// </summary>
    Album,

    /// <summary>
    /// A single has different definitions depending on the market it is released for.
    /// <list type="bullet">
    /// <item><description>
    /// In the US market, a single typically has one main song and possibly a handful of additional tracks or remixes of the main track; the single is usually named after
    /// its main song; the single is primarily released to get radio play and to promote release sales.
    /// </description></item>
    /// <item><description>
    /// The U.K. market (also Australia and Europe) is similar to the US market, however singles are often released as a two disc set, with each disc sold separately.
    /// They also sometimes have a longer version of the single (often combining the tracks from the two disc version) which is very similar to the US style single, and
    /// this is referred to as a "maxi-single". (In some cases the maxi-single is longer than the release the single comes from!)
    /// </description></item>
    /// <item><description>
    /// The Japanese market is much more single driven. The defining factor is typically the length of the single and the price it is sold at. Up until 1995 it was common
    /// that these singles would be released using a mini-cd format, which is basically a much smaller CD typically 8 cm in diameter. Around 1995 the 8cm single was phased
    /// out, and the standard 12cm CD single is more common now; generally re-releases of singles from pre-1995 will be released on the 12cm format, even if they were
    /// originally released on the 8cm format. Japanese singles often come with instrumental versions of the songs and also have maxi-singles like the UK with remixed
    /// versions of the songs. Sometimes a maxi-single will have more tracks than an EP but as it's all alternate versions of the same 2-3 songs it is still classified
    /// as a single.
    /// </description></item>
    /// </list>
    /// There are other variations of the single called a "split single" where songs by two different artists are released on the one disc, typically vinyl. The term
    /// "B-Side" comes from the era when singles were released on 7 inch (or sometimes 12 inch) vinyl with a song on each side, and so side A is the track that the single
    /// is named for, and the other side -side B- would contain a bonus song, or sometimes even the same song. 
    /// </summary>
    Single,

    /// <summary>
    /// An EP is a so-called "Extended Play" release and often contains the letters EP in the title. Generally an EP will be shorter than a full length release
    /// (an LP or "Long Play") and the tracks are usually exclusive to the EP, in other words the tracks don't come from a previously issued release. EP is fairly
    /// difficult to define; usually it should only be assumed that a release is an EP if the artist defines it as such.
    /// </summary>
    EP,

    /// <summary>An episodic release that was originally broadcast via radio, television, or the Internet, including podcasts.</summary>
    Broadcast,

    /// <summary>Any release that does not fit or can't decisively be placed in any of the categories above.</summary>
    Other,

    #endregion

    #region Secondary Types

    /// <summary></summary>
    Audiobook,

    /// <summary>
    /// A compilation, for the purposes of the MusicBrainz database, covers the following types of releases:
    /// <list type="bullet">
    /// <item><description>
    /// a collection of recordings from various old sources (not necessarily released) combined together. For example a "best of", retrospective or rarities type release.
    /// </description></item>
    /// <item><description>
    /// a various artists song collection, usually based on a general theme ("Songs for Lovers"), a particular time period ("Hits of 1998"), or some other kind of grouping
    /// ("Songs From the Movies", the "Café del Mar" series, etc).
    /// </description></item>
    /// </list>
    /// The MusicBrainz project does not generally consider the following to be compilations:
    /// <list type="bullet">
    /// <item><description>a reissue of an album, even if it includes bonus tracks.</description></item>
    /// <item><description>a tribute release containing covers of music by another artist.</description></item>
    /// <item><description>a classical release containing new recordings of works by a classical artist.</description></item>
    /// <item><description>
    /// a release containing two albums and/or EPs (these should be tagged as either an 'album' or an 'EP' depending on the circumstances).
    /// </description></item>
    /// </list>
    /// </summary>
    Compilation,

    /// <summary>
    /// A DJ-mix is a sequence of several recordings played one after the other, each one modified so that they blend together into a continuous flow of music. A DJ mix
    /// release requires that the recordings be modified in some manner, and the DJ who does this modification is usually (although not always) credited in a fairly
    /// prominent way.
    /// </summary>
    DJMix,

    /// <summary>An interview release contains an interview, generally with an artist.</summary>
    Interview,

    /// <summary>A release that was recorded live.</summary>
    Live,

    /// <summary>
    /// Promotional in nature (but not necessarily free), mixtapes and street albums are often released by artists to promote new artists, or upcoming studio albums
    /// by prominent artists. They are also sometimes used to keep fans' attention between studio releases and are most common in rap &amp; hip hop genres. They are often
    /// not sanctioned by the artist's label, may lack proper sample or song clearances and vary widely in production and recording quality. While mixtapes are
    /// generally DJ-mixed, they are distinct from commercial DJ mixes (which are usually deemed compilations) and are defined by having a significant proportion of
    /// new material, including original production or original vocals over top of other artists' instrumentals. They are distinct from demos in that they are designed
    /// for release directly to the public and fans; not to labels.
    /// </summary>
    MixTape,

    /// <summary>A release that primarily contains remixed material.</summary>
    Remix,

    /// <summary>
    /// A soundtrack is the musical score to a movie, TV series, stage show, computer game etc. In the specific cases of computer games, a game CD with audio tracks
    /// should be classified as a soundtrack: the musical properties of the CD are more interesting to MusicBrainz than the data properties.
    /// </summary>
    Soundtrack,

    /// <summary>Non-music spoken word releases.</summary>
    SpokenWord,

    /// <summary>
    /// Promotional in nature (but not necessarily free), mixtapes and street albums are often released by artists to promote new artists, or upcoming studio albums
    /// by prominent artists. They are also sometimes used to keep fans' attention between studio releases and are most common in rap &amp; hip hop genres. They are often
    /// not sanctioned by the artist's label, may lack proper sample or song clearances and vary widely in production and recording quality. While mixtapes are
    /// generally DJ-mixed, they are distinct from commercial DJ mixes (which are usually deemed compilations) and are defined by having a significant proportion of
    /// new material, including original production or original vocals over top of other artists' instrumentals. They are distinct from demos in that they are designed
    /// for release directly to the public and fans; not to labels.
    /// </summary>
    StreetAlbum = ReleaseType.MixTape,

    #endregion

  }

}

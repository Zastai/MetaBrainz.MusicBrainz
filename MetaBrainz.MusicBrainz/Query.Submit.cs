using System;
using MetaBrainz.MusicBrainz.Objects.Submissions;

namespace MetaBrainz.MusicBrainz {

  public sealed partial class Query {

    /// <summary>Creates a submission request for setting a barcode on one or more releases.</summary>
    /// <param name="client">
    /// The ID of the client software submitting data.<br/>
    /// This has to be the application's name and version number.
    /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
    /// It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new barcode submission request.</returns>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public BarcodeSubmission SubmitBarcodes(string client) => new BarcodeSubmission(this, client);

    /// <summary>Creates a submission request for adding one or more ISRCs to one or more recordings.</summary>
    /// <param name="client">
    /// The ID of the client software submitting data.<br/>
    /// This has to be the application's name and version number.
    /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
    /// It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new ISRC submission request.</returns>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public IsrcSubmission SubmitIsrcs(string client) => new IsrcSubmission(this, client);

    /// <summary>Creates a submission request for rating one or more entities.</summary>
    /// <param name="client">
    /// The ID of the client software submitting data.<br/>
    /// This has to be the application's name and version number.
    /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.
    /// </param>
    /// <returns>A new rating submission request.</returns>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public RatingSubmission SubmitRatings(string client) => new RatingSubmission(this, client);

    /// <summary>Creates a submission request for modifying tags on one or more entities.</summary>
    /// <param name="client">
    /// The ID of the client software submitting data.<br/>
    /// This has to be the application's name and version number.
    /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.
    /// </param>
    /// <returns>A new tag submission request.</returns>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public TagSubmission SubmitTags(string client) => new TagSubmission(this, client);

  }

}

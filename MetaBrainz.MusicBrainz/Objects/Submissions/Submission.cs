using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Submissions;

namespace MetaBrainz.MusicBrainz.Objects.Submissions {

  /// <summary>Base class for the submission request classes.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public abstract class Submission : ISubmission {

    #region Public API

    /// <summary>Submits the request.</summary>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="System.Net.WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string Submit() => this._query.PerformSubmission(this);

    /// <summary>Submits the request asynchronously.</summary>
    /// <returns>An asynchronous operation returning a message describing the result (usually "OK").</returns>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="System.Net.WebException">When the MusicBrainz web service could not be contacted.</exception>
    public async Task<string> SubmitAsync() => await this._query.PerformSubmissionAsync(this).ConfigureAwait(false);

    #endregion

    #region Internals
    
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    internal abstract string RequestBody { get; }

    internal Submission(Query query, string client, string entity, Method method) {
      this._query  = query;
      this._client = client;
      this._entity = entity;
      this._method = method;
    }

    private readonly Query  _query;
    private readonly string _client;
    private readonly string _entity;
    private readonly Method _method;

    string ISubmission.Client      => this._client;
    string ISubmission.Entity      => this._entity;
    Method ISubmission.Method      => this._method;
    string ISubmission.RequestBody => this.RequestBody;

    string ISubmission.ContentType { get; } = "application/xml; charset=utf-8";

    // A StringWriter using UTF-8 as encoding (so that XmlWriter writes "utf-8" as encoding instead of "utf-16").
    internal sealed class U8StringWriter : StringWriter {

      public override Encoding Encoding => Encoding.UTF8;

    }

    #endregion

  }

}

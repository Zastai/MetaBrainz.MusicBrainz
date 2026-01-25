using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>Base class for the submission request classes that use XML bodies.</summary>
[PublicAPI]
public abstract class XmlSubmission : ISubmission {

  #region Public API

  /// <summary>Submits the request asynchronously.</summary>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public async Task<string> SubmitAsync(CancellationToken cancellationToken = default)
    => await this._query.PerformSubmissionAsync(this, cancellationToken).ConfigureAwait(false);

  #endregion

  #region Internals

  // A StringWriter using UTF-8 as encoding (so that XmlWriter writes "utf-8" as encoding instead of "utf-16").
  private sealed class U8StringWriter : StringWriter {

    public override Encoding Encoding => Encoding.UTF8;

  }

  private protected XmlSubmission(Query query, string client, string entity, HttpMethod method) {
    if (string.IsNullOrWhiteSpace(client)) {
      throw new ArgumentException("The client ID must not be blank.", nameof(client));
    }
    this._query = query;
    this._client = client;
    this._entity = entity;
    this._method = method;
  }

  internal string RequestBody {
    get {
      using var sw = new U8StringWriter();
      using (var xml = XmlWriter.Create(sw)) {
        xml.WriteStartDocument();
        xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
        this.WriteBodyContents(xml);
        xml.WriteEndElement();
      }
      return sw.ToString();
    }
  }

  private protected abstract void WriteBodyContents(XmlWriter xml);

  private readonly string _client;

  private readonly string _entity;

  private readonly HttpMethod _method;

  private readonly Query _query;

  string ISubmission.Client => this._client;

  string ISubmission.ContentType => "application/xml";

  string ISubmission.Entity => this._entity;

  HttpMethod ISubmission.Method => this._method;

  string ISubmission.RequestBody => this.RequestBody;

  #endregion

}

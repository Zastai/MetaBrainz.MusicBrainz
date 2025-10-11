using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>A submission request for adding ISRCs to recordings.</summary>
[PublicAPI]
public sealed class IsrcSubmission : Submission {

  #region Public API

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(Guid mbid, params string[] isrcs) {
    if (isrcs.Length == 0) {
      return this;
    }
    if (this._isrcs.TryGetValue(mbid, out var current)) {
      current.AddRange(isrcs);
    }
    else {
      this._isrcs.Add(mbid, [..isrcs]);
    }
    return this;
  }

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(IRecording recording, params string[] isrcs) => this.Add(recording.Id, isrcs);

  #endregion

  #region Internals

  internal IsrcSubmission(Query query, string client) : base(query, client, "recording", HttpMethod.Post) { }

  private readonly Dictionary<Guid, List<string>> _isrcs = new();

  internal override string RequestBody {
    get {
      using var sw = new U8StringWriter();
      using (var xml = XmlWriter.Create(sw)) {
        xml.WriteStartDocument();
        xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
        xml.WriteStartElement("recording-list");
        foreach (var entry in this._isrcs) {
          var isrcs = entry.Value;
          if (isrcs.Count == 0) {
            continue;
          }
          xml.WriteStartElement("recording");
          xml.WriteAttributeString("id", entry.Key.ToString("D"));
          xml.WriteStartElement("isrc-list");
          xml.WriteAttributeString("count", isrcs.Count.ToString());
          foreach (var isrc in isrcs) {
            xml.WriteStartElement("isrc");
            xml.WriteAttributeString("id", isrc);
            xml.WriteEndElement();
          }
          xml.WriteEndElement();
          xml.WriteEndElement();
        }
        xml.WriteEndElement();
        xml.WriteEndElement();
      }
      return sw.ToString();
    }
  }

  #endregion

}

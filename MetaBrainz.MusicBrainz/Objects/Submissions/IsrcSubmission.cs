using System;
using System.Collections.Generic;
using System.Xml;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions {

  /// <summary>A submission request for adding ISRCs to recordings.</summary>
  public sealed class IsrcSubmission : Submission {

    #region Public API

    /// <summary>Adds one or more ISRCs to the request.</summary>
    /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
    /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
    /// <returns>This submission request.</returns>
    public IsrcSubmission Add(Guid mbid, params string[] isrcs) {
      if (isrcs == null || isrcs.Length == 0)
        return this;
      if (this._isrcs.TryGetValue(mbid, out var current))
        current.AddRange(isrcs);
      else
        this._isrcs.Add(mbid, new List<string>(isrcs));
      return this;
    }

    /// <summary>Adds a barcode to the request.</summary>
    /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
    /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
    /// <returns>This submission request.</returns>
    public IsrcSubmission Add(IRecording recording, params string[] isrcs) {
      if (recording == null || isrcs == null || isrcs.Length == 0)
        return this;
      return this.Add(recording.MbId, isrcs);
    }

    #endregion

    #region Internals

    internal IsrcSubmission(Query query, string client) : base(query, client, "recording", Method.POST) { }

    private readonly Dictionary<Guid, List<string>> _isrcs = new Dictionary<Guid, List<string>>();

    internal override string RequestBody {
      get {
        using (var sw = new U8StringWriter()) {
          using (var xml = XmlWriter.Create(sw)) {
            xml.WriteStartDocument();
            xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
            xml.WriteStartElement("recording-list");
            foreach (var entry in this._isrcs) {
              if (entry.Value == null || entry.Value.Count == 0)
                continue;
              xml.WriteStartElement("recording");
              xml.WriteAttributeString("id", entry.Key.ToString("D"));
              xml.WriteStartElement("isrc-list");
              xml.WriteAttributeString("count", entry.Value.Count.ToString());
              foreach (var isrc in entry.Value) {
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
    }

    #endregion

  }

}

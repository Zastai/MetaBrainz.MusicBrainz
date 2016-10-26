using System;
using System.Collections.Generic;
using System.Xml;

using MetaBrainz.MusicBrainz.Entities;

namespace MetaBrainz.MusicBrainz.Submissions {

  /// <summary>A submission request for adding barcodes to releases.</summary>
  public sealed class BarcodeSubmission : Submission {

    #region Public API

    /// <summary>Adds a barcode to the request.</summary>
    /// <param name="mbid">The MBID of the release to which <paramref name="barcode"/> should be added.</param>
    /// <param name="barcode">The barcode to add to the release. This must be a valid EAN.</param>
    /// <returns>This submission request.</returns>
    public BarcodeSubmission Add(Guid mbid, string barcode) {
      if (barcode != null)
        this._barcodes[mbid] = barcode;
      return this;
    }

    /// <summary>Adds a barcode to the request.</summary>
    /// <param name="release">The release to which <paramref name="barcode"/> should be added.</param>
    /// <param name="barcode">The barcode to add. This must be a valid EAN.</param>
    /// <returns>This submission request.</returns>
    public BarcodeSubmission Add(IRelease release, string barcode) {
      if (release == null)
        return this;
      return this.Add(release.MbId, barcode);
    }

    #endregion

    #region Internals

    internal BarcodeSubmission(Query query, string client) : base(query, client, "release", "POST") { }

    private readonly Dictionary<Guid, string> _barcodes = new Dictionary<Guid, string>();

    internal override string RequestBody {
      get {
        using (var sw = new U8StringWriter()) {
          using (var xml = XmlWriter.Create(sw)) {
            xml.WriteStartDocument();
            xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
            xml.WriteStartElement("release-list");
            foreach (var entry in this._barcodes) {
              xml.WriteStartElement("release");
              xml.WriteAttributeString("id", entry.Key.ToString("D"));
              xml.WriteElementString("barcode", entry.Value);
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

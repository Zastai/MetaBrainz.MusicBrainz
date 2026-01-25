using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>A submission request for adding barcodes to releases.</summary>
[PublicAPI]
public sealed class BarcodeSubmission : XmlSubmission {

  #region Public API

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="mbid">The MBID of the release to which <paramref name="barcode"/> should be added.</param>
  /// <param name="barcode">The barcode to add to the release. This must be a valid EAN.</param>
  /// <returns>This submission request.</returns>
  public BarcodeSubmission Add(Guid mbid, string barcode) {
    this._barcodes[mbid] = barcode;
    return this;
  }

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="release">The release to which <paramref name="barcode"/> should be added.</param>
  /// <param name="barcode">The barcode to add. This must be a valid EAN.</param>
  /// <returns>This submission request.</returns>
  public BarcodeSubmission Add(IRelease release, string barcode) => this.Add(release.Id, barcode);

  #endregion

  #region Internals

  internal BarcodeSubmission(Query query, string client) : base(query, client, "release", HttpMethod.Post) { }

  private readonly Dictionary<Guid, string> _barcodes = new();

  private protected override void WriteBodyContents(XmlWriter xml) {
    xml.WriteStartElement("release-list");
    foreach (var entry in this._barcodes) {
      xml.WriteStartElement("release");
      xml.WriteAttributeString("id", entry.Key.ToString("D"));
      xml.WriteElementString("barcode", entry.Value);
      xml.WriteEndElement();
    }
    xml.WriteEndElement();
  }

  #endregion

}

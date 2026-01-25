using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>A submission request for adding ISRCs to recordings.</summary>
[PublicAPI]
public sealed class IsrcSubmission : XmlSubmission {

  #region Public API

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(Guid mbid, params IEnumerable<string> isrcs) {
    this.GetIsrcList(mbid).AddRange(isrcs);
    return this;
  }

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(Guid mbid, params ReadOnlySpan<string> isrcs) {
    if (isrcs.Length == 0) {
      return this;
    }
    this.GetIsrcList(mbid).AddRange(isrcs);
    return this;
  }

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(Guid mbid, params string[] isrcs) => this.Add(mbid, isrcs.AsSpan());

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(IRecording recording, params IEnumerable<string> isrcs) => this.Add(recording.Id, isrcs);

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(IRecording recording, params ReadOnlySpan<string> isrcs) => this.Add(recording.Id, isrcs);

  /// <summary>Adds a barcode to the request.</summary>
  /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public IsrcSubmission Add(IRecording recording, params string[] isrcs) => this.Add(recording.Id, isrcs.AsSpan());

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="mbid">The MBID of the recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public async Task<IsrcSubmission> AddAsync(Guid mbid, IAsyncEnumerable<string> isrcs) {
    var list = this.GetIsrcList(mbid);
    await foreach (var isrc in isrcs) {
      list.Add(isrc);
    }
    return this;
  }

  /// <summary>Adds one or more ISRCs to the request.</summary>
  /// <param name="recording">The recording to which <paramref name="isrcs"/> should be added.</param>
  /// <param name="isrcs">One or more (valid) ISRCs to add to the recording.</param>
  /// <returns>This submission request.</returns>
  public Task<IsrcSubmission> AddAsync(IRecording recording, IAsyncEnumerable<string> isrcs)
    => this.AddAsync(recording.Id, isrcs);

  #endregion

  #region Internals

  internal IsrcSubmission(Query query, string client) : base(query, client, "recording", HttpMethod.Post) { }

  private readonly Dictionary<Guid, List<string>> _isrcs = new();

  private List<string> GetIsrcList(Guid mbid) {
    List<string>? current;
    while (!this._isrcs.TryGetValue(mbid, out current)) {
      if (this._isrcs.TryAdd(mbid, current = [])) {
        break;
      }
    }
    return current;
  }

  private protected override void WriteBodyContents(XmlWriter xml) {
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
  }

  #endregion

}

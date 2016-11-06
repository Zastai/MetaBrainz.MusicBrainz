using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using MetaBrainz.MusicBrainz.Entities;
using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing the result of a lookup for a MusicBrainz disc ID: a disc or cd stub for direct ID matches, or a release list for a fuzzy lookup.</summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "NotAccessedField.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public sealed class DiscIdLookupResult {

    internal DiscIdLookupResult(string discid, string json, JsonSerializerSettings jss) {
      // Currently this can return:
      // - a serialized Disc (id + releases)
      // - a serialized CD stub (id + tracks)
      // - a list of releases (as a serialized object containing only a "releases" property)
      // It would be nicer if the first two had a wrapper object with a disc and stub property, respectively.
      var jobj = JsonConvert.DeserializeObject(json, jss) as JObject;
      if (jobj == null)
        throw new ArgumentException($"Disc ID lookup for '{discid}' did not return a usable JSON result.\nContents: {json}");
      var jid = jobj["id"];
      if (jid != null && jobj["releases"] != null)
        this.Disc = JsonConvert.DeserializeObject<Disc>(json, jss);
      else if (jid != null && jobj["tracks"] != null)
        this.Stub = JsonConvert.DeserializeObject<CdStub>(json, jss);
      else {
        var jreleases = jobj["releases"];
        if (jreleases == null)
          throw new ArgumentException($"Disc ID lookup for '{discid}' returned a JSON result that could not be identified as a disc, stub or release list.\nContents: {json}");
        this.Releases = JsonConvert.DeserializeObject<Release[]>(jreleases.ToString(), jss);
      }
      this.Id = discid;
    }

    /// <summary>The MusicBrainz disc ID that was looked up (or "-" for a fuzzy lookup).</summary>
    public string Id { get; }

    /// <summary>The disc returned by the lookup (if any was found).</summary>
    public IDisc Disc { get; }

    /// <summary>The list of matching releases, if a fuzzy TOC lookup was done.</summary>
    public IEnumerable<IRelease> Releases { get; }

    /// <summary>The CD stub returned by the lookup (if any was found).</summary>
    public ICdStub Stub { get; }

    /// <summary>Gets the textual representation of the disc ID lookup result.</summary>
    /// <returns>A string describing the lookup results.</returns>
    public override string ToString() {
      if (this.Disc != null)
        return "Disc: " + this.Disc;
      if (this.Stub != null)
        return "CD Stub: " + this.Stub;
      if (this.Releases != null)
        return $"{this.Releases.Count()} Release(s)";
      return string.Empty; // should be impossible
    }

  }

}

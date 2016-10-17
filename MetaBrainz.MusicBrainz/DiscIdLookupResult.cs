using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Entities;
using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing the result of a disc-id lookup: a disc or cd stub for direct ID matches, or a release list for a fuzzy lookup.</summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "NotAccessedField.Global")]
  public sealed class DiscIdLookupResult {

    internal DiscIdLookupResult(string json, JsonSerializerSettings jss) {
      // Currently this can return:
      // - a serialized Disc (id + releases)
      // - a serialized CD stub (id + tracks)
      // - a list of releases (as a serialized object containing only a "releases" property)
      // It would be nicer if the first two had a wrapper object with a disc and stub property, respectively.
      var jobj = JsonConvert.DeserializeObject(json, jss) as JObject;
      if (jobj == null)
        throw new ArgumentException($"Disc ID lookup did not return a usable JSON result.\nContents: {json}");
      var jid = jobj["id"];
      if (jid != null && jobj["releases"] != null)
        this.Disc = new Disc(JsonConvert.DeserializeObject<Disc.JSON>(json, jss));
      else if (jid != null && jobj["tracks"] != null)
        this.Stub = new CdStub(JsonConvert.DeserializeObject<CdStub.JSON>(json, jss));
      else {
        var jreleases = jobj["releases"];
        if (jreleases == null)
          throw new ArgumentException($"Disc ID lookup returned a JSON result that could not be identified as a disc, stub or release list.\nContents: {json}");
        var releases = JsonConvert.DeserializeObject<Release.JSON[]>(jreleases.ToString(), jss);
        Release[] arr = null;
        releases.WrapArray(ref arr, j => new Release(j));
        this.Releases = arr;
      }
    }

    /// <summary>The disc returned by the lookup (if any was found).</summary>
    public IDisc Disc { get; }

    /// <summary>The list of matching releases, if a fuzzy TOC lookup was done.</summary>
    public IEnumerable<IRelease> Releases { get; }

    /// <summary>The CD stub returned by the lookup (if any was found).</summary>
    public ICdStub Stub { get; }

  }

}

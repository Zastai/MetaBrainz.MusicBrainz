using System;
using System.Collections.Generic;
using System.Text.Json;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class DiscIdLookupResult : IDiscIdLookupResult {

    internal DiscIdLookupResult(string discid, string json) {
      this.Id = discid;
      // Currently this can return:
      // - a serialized Disc (id + releases)
      // - a serialized CD stub (id + tracks)
      // - a list of releases (as a serialized object containing only a "releases" property)
      // It would be nicer if the first two had a wrapper object with a disc and stub property, respectively.
      JsonElement result;
      try {
        result = JsonDocument.Parse(json).RootElement;
      }
      catch (Exception e) {
        throw new ArgumentException($"Disc ID lookup for '{discid}' did not return a usable JSON result.\nContents: {json}", e);
      }
      if (result.TryGetProperty("id", out var id)) {
        if (result.TryGetProperty("releases", out var releases)) {
          this.Disc = JsonUtils.Deserialize<Disc>(json);
          return;
        }
        else if (result.TryGetProperty("tracks", out var tracks)) {
          this.Stub = JsonUtils.Deserialize<CdStub>(json);
          return;
        }
      }
      else if (result.TryGetProperty("releases", out var releases) && releases.ValueKind == JsonValueKind.Array) {
        this.Releases = JsonUtils.Deserialize<Release[]>(releases.ToString());
        return;
      }
      throw new ArgumentException($"Disc ID lookup for '{discid}' returned a JSON result that could not be identified as a disc, stub or release list.\nContents: {json}");
    }

    public string Id { get; }

    public IDisc Disc { get; }

    public IReadOnlyList<IRelease> Releases { get; }

    public ICdStub Stub { get; }

    /// <summary>Gets the textual representation of the disc ID lookup result.</summary>
    /// <returns>A string describing the lookup results.</returns>
    public override string ToString() {
      if (this.Disc != null)
        return "Disc: " + this.Disc;
      if (this.Stub != null)
        return "CD Stub: " + this.Stub;
      if (this.Releases != null)
        return $"{this.Releases.Count} Release(s)";
      return string.Empty; // should be impossible
    }

  }

}

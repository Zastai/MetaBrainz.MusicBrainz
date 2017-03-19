using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_LT_4_5
  using SimpleTrackList = IEnumerable<ISimpleTrack>;
  #else
  using SimpleTrackList = IReadOnlyList<ISimpleTrack>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [SuppressMessage("ReSharper", "UnusedMember.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class CdStub : SearchResult, IFoundCdStub {

    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; private set; }

    [JsonProperty("artist", Required = Required.Always)]
    public string Artist { get; private set; }

    [JsonProperty("barcode", Required = Required.Default)]
    public string Barcode { get; private set; }

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    [JsonProperty("track-count", Required = Required.Default)]
    public int? TrackCount { get; private set; }

    public SimpleTrackList Tracks => this._tracks;

    [JsonProperty("tracks", Required = Required.DisallowNull)]
    private SimpleTrack[] _tracks = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - barcode is omitted when not set, instead of being serialized as null
    // - use of 'comment' instead of 'disambiguation'
    // - use of 'count' instead of 'track-count'
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // => Added setter-only properties for the search server's names.

    [JsonProperty("count", Required = Required.DisallowNull)]
    private int SearchTrackCount {
      set { this.TrackCount = value; }
    }

    [JsonProperty("comment", Required = Required.DisallowNull)]
    private string SearchComment {
      set { this.Disambiguation = value; }
    }

    #endregion

    public override string ToString() {
      var text = this.SearchScore.HasValue ? $"[Score: {this.SearchScore.Value}] " : "";
      text += this.Artist + " / " + this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      return text;
    }

  }

}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  #if NETFX_LT_4_5
  using RecordingList = IEnumerable<IRecording>;
  #else
  using RecordingList = IReadOnlyList<IRecording>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Isrc : IIsrc {

    public RecordingList Recordings => this._recordings;

    [JsonProperty("recordings", Required = Required.Always)]
    private Recording[] _recordings = null;

    [JsonProperty("isrc", Required = Required.Always)]
    public string Value { get; private set; }

    public override string ToString() => this.Value;

  }

}

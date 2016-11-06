using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Isrc : IIsrc {

    public IEnumerable<IRecording> Recordings => this._recordings;

    [JsonProperty("recordings")]
    private Recording[] _recordings = null;

    [JsonProperty("isrc")]
    public string Value { get; private set; }

    public override string ToString() => this.Value;

  }

}

using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class SimpleTrack : ISimpleTrack {

    [JsonProperty("artist")]
    public string Artist { get; private set; }

    [JsonProperty("length")]
    public int Length { get; private set; }

    [JsonProperty("title")]
    public string Title { get; private set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Artist != null)
        text += this.Artist + " / ";
      text += this.Title + " (" + new TimeSpan(0, 0, 0, 0, this.Length) + ")";
      return text;
    }

  }

}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Track : ITrack {

    [JsonProperty("id", Required = Required.Always)]
    public Guid Id { get; private set; }

    public IEnumerable<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.DisallowNull)] 
    private NameCredit[] _artistCredit = null;

    [JsonProperty("length", Required = Required.AllowNull)] 
    public int? Length { get; private set; }

    [JsonProperty("number", Required = Required.AllowNull)] 
    public string Number { get; private set; }

    public IRecording Recording => this._recording;

    [JsonProperty("recording", Required = Required.DisallowNull)]
    private Recording _recording = null;

    [JsonProperty("title", Required = Required.Always)] 
    public string Title { get; private set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Number != null)
        text += $"{this.Number}. ";
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (this.Length.HasValue)
        text += $" ({new TimeSpan(0, 0, 0, 0, this.Length.Value)})";
      return text;
    }

  }

}

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class NameCredit : JsonBasedObject, INameCredit {

    [JsonConverter(typeof(JsonInterfaceConverter<IArtist, Artist>))]
    [JsonPropertyName("artist")]
    public IArtist? Artist { get; set; }

    [JsonPropertyName("joinphrase")]
    public string? JoinPhrase { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Artist != null)
        text += $"[{this.Artist} as “{this.Name}”]";
      else if (this.Name != null)
        text += this.Name;
      if (this.JoinPhrase != null)
        text += this.JoinPhrase;
      return text;
    }

  }

}

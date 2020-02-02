using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class NameCredit : JsonBasedObject, INameCredit {

    public IArtist Artist => this.TheArtist;

    [JsonPropertyName("artist")]
    public Artist TheArtist { get; set; }

    [JsonPropertyName("joinphrase")]
    public string JoinPhrase { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public override string ToString() => this.Name + this.JoinPhrase;

  }

}

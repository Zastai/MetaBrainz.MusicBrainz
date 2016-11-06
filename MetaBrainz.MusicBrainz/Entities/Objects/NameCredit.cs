using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class NameCredit : INameCredit {

    public IArtist Artist => this._artist;

    [JsonProperty("artist", Required = Required.Always)]
    private Artist _artist = null;

    [JsonProperty("joinphrase", Required = Required.Always)] 
    public string JoinPhrase { get; private set; }

    [JsonProperty("name", Required = Required.Always)] 
    public string Name { get; private set; }

    public override string ToString() => this.Name + this.JoinPhrase;

  }

}

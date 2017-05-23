using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class NameCredit : INameCredit {

    public IArtist Artist => this._artist;

    [JsonProperty("artist", Required = Required.Always)]
    private Artist _artist = null;

    [JsonProperty("joinphrase", Required = Required.DisallowNull)] 
    public string JoinPhrase { get; private set; }

    [JsonProperty("name", Required = Required.DisallowNull)] 
    public string Name { get; private set; }

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the join phrase is not serialized when empty (instead of being serialized as an empty string)
    // - the name is not always serialized (instead of being serialized as an empty string)
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // => Use a nullable boolean for the video flag.

    #endregion

    public override string ToString() => this.Name + this.JoinPhrase;

  }

}

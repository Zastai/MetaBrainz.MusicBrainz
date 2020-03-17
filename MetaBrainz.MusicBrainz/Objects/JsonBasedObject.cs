using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal abstract class JsonBasedObject : IJsonBasedObject {

    IReadOnlyDictionary<string, object?>? IJsonBasedObject.UnhandledProperties => this.UnhandledProperties;

    [JsonExtensionData]
    public Dictionary<string, object?>? UnhandledProperties { get; set; }

  }

}

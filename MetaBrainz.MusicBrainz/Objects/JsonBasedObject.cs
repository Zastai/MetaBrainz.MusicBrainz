#nullable enable

using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal abstract class JsonBasedObject : IJsonBasedObject {

    public IReadOnlyDictionary<string, object?>? UnhandledProperties
      => this._unwrapped ??= JsonUtils.Unwrap(this.TheUnhandledProperties);

    private Dictionary<string, object?>? _unwrapped;

    [JsonExtensionData]
    public Dictionary<string, object?>? TheUnhandledProperties { get; set; }

  }

}

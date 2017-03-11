using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Alias : IAlias {

    [JsonProperty("begin", Required = Required.AllowNull)]
    public PartialDate Begin { get; private set; }

    [JsonProperty("end", Required = Required.AllowNull)]
    public PartialDate End { get; private set; }

    [JsonProperty("ended", Required = Required.Always)]
    public bool Ended { get; private set; }

    [JsonProperty("locale", Required = Required.AllowNull)]
    public string Locale { get; private set; }

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("primary", Required = Required.AllowNull)]
    public bool? Primary { get; private set; }

    [JsonProperty("sort-name", Required = Required.AllowNull)]
    public string SortName { get; private set; }

    [JsonProperty("type", Required = Required.AllowNull)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.AllowNull)]
    public Guid? TypeId { get; private set; }

    public override string ToString() {
      var text = this.Name;
      if (!string.IsNullOrEmpty(this.Type))
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

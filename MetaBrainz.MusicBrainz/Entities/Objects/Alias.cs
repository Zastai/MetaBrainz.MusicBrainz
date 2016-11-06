using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Alias : IAlias {

    [JsonProperty("begin")]
    public PartialDate Begin { get; private set; }

    [JsonProperty("end")]
    public PartialDate End { get; private set; }

    [JsonProperty("ended")]
    public bool? Ended { get; private set; }

    [JsonProperty("locale")]
    public string Locale { get; private set; }

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("primary")]
    public bool? Primary { get; private set; }

    [JsonProperty("sort-name")]
    public string SortName { get; private set; }

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("type-id")]
    public Guid? TypeId { get; private set; }

    public override string ToString() {
      var text = this.Name;
      if (!string.IsNullOrEmpty(this.Type))
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

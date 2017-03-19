using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [SuppressMessage("ReSharper", "UnusedMember.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Alias : IAlias {

    [JsonProperty("begin", Required = Required.Default)]
    public PartialDate Begin { get; private set; }

    [JsonProperty("end", Required = Required.Default)]
    public PartialDate End { get; private set; }

    [JsonProperty("ended", Required = Required.DisallowNull)]
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

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - use of 'begin-date' instead of 'begin'
    // - use of 'end-date' instead of 'end'
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // => Added setter-only properties for the search server's names.

    [JsonProperty("begin-date", Required = Required.Default)]
    private PartialDate SearchBeginDate {
      set { this.Begin = value; }
    }

    [JsonProperty("end-date", Required = Required.Default)]
    private PartialDate SearchEndDate {
      set { this.Begin = value; }
    }

    #endregion

    public override string ToString() {
      var text = this.Name;
      if (!string.IsNullOrEmpty(this.Type))
        text += " (" + this.Type + ")";
      return text;
    }

  }

}

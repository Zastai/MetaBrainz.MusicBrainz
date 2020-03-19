using System;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Alias : JsonBasedObject, IAlias {

    [JsonPropertyName("begin")]
    public PartialDate? Begin { get; set; }

    // The search server uses 'begin-date' instead of 'begin' => forward the value.
    [JsonPropertyName("begin-date")]
    public PartialDate? SearchBeginDate { set => this.Begin = value; }

    [JsonPropertyName("end")]
    public PartialDate? End { get; set; }

    // The search server uses 'end-date' instead of 'end' => forward the value.
    [JsonPropertyName("end-date")]
    public PartialDate? SearchEndDate { set => this.End = value; }

    [JsonPropertyName("ended")]
    public bool Ended { get; set; }

    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("primary")]
    public bool? Primary { get; set; }

    [JsonPropertyName("sort-name")]
    public string? SortName { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Type))
        text += $" ({this.Type})";
      return text;
    }

  }

}

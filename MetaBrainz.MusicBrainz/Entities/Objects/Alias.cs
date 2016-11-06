using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Alias : IAlias {

    // TODO: Should become Required.AllowNull once https://github.com/metabrainz/musicbrainz-server/pull/373 is merged.
    [JsonProperty("begin", Required = Required.Default)]
    public PartialDate Begin { get; private set; }

    // TODO: Should become Required.AllowNull once https://github.com/metabrainz/musicbrainz-server/pull/373 is merged.
    [JsonProperty("end", Required = Required.Default)]
    public PartialDate End { get; private set; }

    // TODO: Make this a normal bool (and Required.Always) once https://github.com/metabrainz/musicbrainz-server/pull/373 is merged.
    public bool Ended => this._ended.GetValueOrDefault();

    [JsonProperty("ended", Required = Required.DisallowNull)]
    private bool? _ended = null;

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

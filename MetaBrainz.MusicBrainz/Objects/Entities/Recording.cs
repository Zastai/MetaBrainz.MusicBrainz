using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Recording : SearchResult, IFoundRecording {

    public EntityType EntityType => EntityType.Recording;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    public IReadOnlyList<INameCredit> ArtistCredit => this._artistCredit;

    [JsonProperty("artist-credit", Required = Required.DisallowNull)]
    private NameCredit[] _artistCredit = null;

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    public IReadOnlyList<string> Isrcs { get; private set; }

    [JsonProperty("length", Required = Required.Default)]
    public int? Length { get; private set; }

    public IRating Rating => this._rating;

    [JsonProperty("rating", Required = Required.DisallowNull)]
    private Rating _rating = null;

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public IReadOnlyList<IRelease> Releases => this._releases;

    [JsonProperty("releases", Required = Required.DisallowNull)]
    private Release[] _releases = null;

    public IReadOnlyList<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    public IUserRating UserRating => this._userRating;

    [JsonProperty("user-rating", Required = Required.DisallowNull)]
    private UserRating _userRating = null;

    public IReadOnlyList<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    public bool Video => this._video.GetValueOrDefault();

    [JsonProperty("video", Required = Required.AllowNull)]
    private bool? _video = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // - the length is not serialized when not set (instead of being serialized as null)
    // - the video flag is serialized as null when not set (instead of false)
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // => Use a nullable boolean for the video flag.
    // - the ISRCs are not serialized as an array of strings, but as an array of objects with only an ID property
    // => added a setter-only property for those, which extract the IDs

    [JsonProperty("isrcs", Required = Required.DisallowNull)]
    private JArray RawIsrcs {
      set {
        if (value == null || !value.HasValues)
          return;
        var isrcs = new string[value.Count];
        var i = 0;
        foreach (var child in value.Children()) {
          var jval = child as JValue;
          if (jval == null && child is JObject jobj) {
            if (jobj.TryGetValue("id", out JToken jtok))
              jval = jtok as JValue;
          }
          if (jval != null)
            isrcs[i++] = jval.Value<string>();
          else
            throw new JsonException($"Found invalid contents for the 'isrcs' property; need either an array of strings or an array of objects with 'id' properties.\nContents found: {value}");
        }
        this.Isrcs = isrcs;
      }
    }

    #endregion

    public override string ToString() {
      var text = string.Empty;
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Length.HasValue)
        text += $" ({new TimeSpan(0, 0, 0, 0, this.Length.Value)})";
      return text;
    }

  }

}

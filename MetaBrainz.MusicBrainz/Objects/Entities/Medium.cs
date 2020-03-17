using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Medium : JsonBasedObject, IMedium {

    [JsonConverter(typeof(JsonInterfaceListConverter<ITrack, Track>))]
    [JsonPropertyName("data-tracks")]
    public IReadOnlyList<ITrack>? DataTracks { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IDisc, Disc>))]
    [JsonPropertyName("discs")]
    public IReadOnlyList<IDisc>? Discs { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("format-id")]
    public Guid? FormatId { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<ITrack, Track>))]
    [JsonPropertyName("pregap")]
    public ITrack? Pregap { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("track-count")]
    public int TrackCount { get; set; }

    [JsonPropertyName("track-offset")]
    public int? TrackOffset { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITrack, Track>))]
    [JsonPropertyName("tracks")]
    public IReadOnlyList<ITrack>? Tracks { get; set; }

    // SEARCH-604: A medium can have either 'track' or 'tracks' depending on how it was included in the search.
    [JsonConverter(typeof(JsonInterfaceListConverter<ITrack, Track>))]
    [JsonPropertyName("track")]
    public IReadOnlyList<ITrack>? Track { set => this.Tracks = value; }

    public override string ToString() {
      var text = this.Format ?? "Medium";
      if (!string.IsNullOrEmpty(this.Title))
        text += " “" + this.Title + "”";
      text += $" ({this.TrackCount} track(s))";
      return text;
    }

  }

}

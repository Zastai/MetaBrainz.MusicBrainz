using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Collection : Entity, ICollection {

    public override EntityType EntityType => EntityType.Collection;

    [JsonPropertyName("editor")]
    public string Editor { get; set; }

    public EntityType ContentType => this._entityType ??= HelperMethods.ParseEntityType(this.ContentTypeText);

    private EntityType? _entityType;

    [JsonPropertyName("entity-type")]
    public string ContentTypeText { get; set; }

    public int ItemCount => this.AreaCount
                          + this.ArtistCount
                          + this.EventCount
                          + this.InstrumentCount
                          + this.LabelCount
                          + this.PlaceCount
                          + this.RecordingCount
                          + this.ReleaseCount
                          + this.ReleaseGroupCount
                          + this.SeriesCount
                          + this.WorkCount;

    [JsonPropertyName("area-count")]
    public int AreaCount { get; set; }

    [JsonPropertyName("artist-count")]
    public int ArtistCount { get; set; }

    [JsonPropertyName("event-count")]
    public int EventCount { get; set; }

    [JsonPropertyName("instrument-count")]
    public int InstrumentCount { get; set; }

    [JsonPropertyName("label-count")]
    public int LabelCount { get; set; }

    [JsonPropertyName("place-count")]
    public int PlaceCount { get; set; }

    [JsonPropertyName("recording-count")]
    public int RecordingCount { get; set; }

    [JsonPropertyName("release-count")]
    public int ReleaseCount { get; set; }

    [JsonPropertyName("release-group-count")]
    public int ReleaseGroupCount { get; set; }

    [JsonPropertyName("series-count")]
    public int SeriesCount { get; set; }

    [JsonPropertyName("work-count")]
    public int WorkCount { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

  }

}

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class TextRepresentation : JsonBasedObject, ITextRepresentation {

  public string? Language { get; init; }

  public string? Script { get; init; }

  public override string ToString() => $"{this.Language ?? "???"} / {this.Script ?? "???"}";

}

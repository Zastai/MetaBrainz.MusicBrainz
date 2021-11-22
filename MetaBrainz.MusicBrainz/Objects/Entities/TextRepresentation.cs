using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class TextRepresentation : JsonBasedObject, ITextRepresentation {

  public string? Language { get; set; }

  public string? Script { get; set; }

  public override string ToString() => $"{this.Language ?? "???"} / {this.Script ?? "???"}";

}
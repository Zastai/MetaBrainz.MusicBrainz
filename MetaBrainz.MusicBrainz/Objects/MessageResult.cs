using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class MessageResult : JsonBasedObject {

  public string? Message { get; init; }

}

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class ErrorResult : JsonBasedObject {

  public required string Error { get; init; }

  public required string Help { get; init; }

}

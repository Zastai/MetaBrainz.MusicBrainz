using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class ErrorResult : JsonBasedObject {

  public string? Error { get; init; }

  public string? Help { get; init; }

}

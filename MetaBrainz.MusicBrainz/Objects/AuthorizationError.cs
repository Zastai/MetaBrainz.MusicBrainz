using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class AuthorizationError : JsonBasedObject {

  public string? Error { get; init; }

  public string? Description { get; init; }

}

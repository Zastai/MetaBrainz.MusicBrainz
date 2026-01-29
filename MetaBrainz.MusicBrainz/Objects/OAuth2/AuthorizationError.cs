using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects.OAuth2;

internal sealed class AuthorizationError : JsonBasedObject {

  public required string Error { get; init; }

  public required string Description { get; init; }

}

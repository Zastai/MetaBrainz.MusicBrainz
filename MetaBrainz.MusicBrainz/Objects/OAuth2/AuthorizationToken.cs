using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects.OAuth2;

/// <summary>Class representing an OAuth2 authorization token.</summary>
internal sealed class AuthorizationToken : JsonBasedObject, IAuthorizationToken {

  /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
  public required string AccessToken { get; init; }

  /// <summary>The lifetime of the token, in seconds (typically one hour).</summary>
  public required int Lifetime { get; init; }

  /// <summary>The refresh token (i.e. the one you use to get a new access token).</summary>
  public required string RefreshToken { get; init; }

  /// <summary>The type of this authorization token.</summary>
  public required string TokenType { get; init; }

  /// <summary>Gets the textual representation of this authorization token.</summary>
  /// <returns><see cref="AccessToken"/>.</returns>
  public override string ToString() => this.AccessToken;

}

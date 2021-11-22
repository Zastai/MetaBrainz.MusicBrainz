using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects; 

/// <summary>Class representing an OAuth2 authorization token.</summary>
internal sealed class AuthorizationToken : JsonBasedObject, IAuthorizationToken {

  public AuthorizationToken(string accessToken, int lifetime, string refreshToken, string tokenType) {
    this.AccessToken = accessToken;
    this.Lifetime = lifetime;
    this.RefreshToken = refreshToken;
    this.TokenType = tokenType;
  }

  /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
  public string AccessToken { get; }

  /// <summary>The lifetime of the token, in seconds (typically one hour).</summary>
  public int Lifetime { get; }

  /// <summary>The refresh token (i.e. the one you use to get a new access token).</summary>
  public string RefreshToken { get; }

  /// <summary>The type of this authorization token.</summary>
  public string TokenType { get; }

  /// <summary>Gets the textual representation of this authorization token.</summary>
  /// <returns><see cref="AccessToken"/>.</returns>
  public override string? ToString() => this.AccessToken;

}
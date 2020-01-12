using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects {

  /// <summary>Class representing an OAuth2 authorization token.</summary>
  [UsedImplicitly]
  public sealed class AuthorizationToken : IAuthorizationToken {

    /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
    [JsonPropertyName("access_token")]
    [UsedImplicitly]
    public string AccessToken { get; set; }

    /// <summary>The lifetime of the token, in seconds (typically one hour).</summary>
    [JsonPropertyName("expires_in")]
    [UsedImplicitly]
    public int Lifetime { get; set; }

    /// <summary>The refresh token (i.e. the one you use to get a new access token).</summary>
    [JsonPropertyName("refresh_token")]
    [UsedImplicitly]
    public string RefreshToken { get; set; }

    /// <summary>The type of this authorization token.</summary>
    [JsonPropertyName("token_type")]
    [UsedImplicitly]
    public string TokenType { get; set; }

    /// <summary>Gets the textual representation of this authorization token.</summary>
    /// <returns><see cref="AccessToken"/>.</returns>
    public override string ToString() => this.AccessToken;

  }

}

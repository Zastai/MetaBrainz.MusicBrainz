using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces {

  /// <summary>An OAuth2 authorization token.</summary>
  [PublicAPI]
  public interface IAuthorizationToken {

    /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
    string AccessToken { get; }

    /// <summary>The lifetime of the token, in seconds (typically one hour).</summary>
    int Lifetime { get; }

    /// <summary>The refresh token (i.e. the one you use to get a new access token).</summary>
    string RefreshToken { get; }

    /// <summary>The type of this authorization token.</summary>
    string TokenType { get; }

  }

}

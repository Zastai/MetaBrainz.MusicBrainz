using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing an OAuth2 authorization token.</summary>
  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [JsonObject(MemberSerialization.OptIn)]
  public sealed class AuthorizationToken {

    /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
    [JsonProperty("access_token", Required = Required.Always)]
    public string AccessToken { get; private set; }

    /// <summary>The lifetime of the token, in seconds (typically one hour).</summary>
    [JsonProperty("expires_in", Required = Required.Always)]
    public int Lifetime { get; private set; }

    /// <summary>The refresh token (i.e. the one you use to get a new access token).</summary>
    [JsonProperty("refresh_token", Required = Required.Always)]
    public string RefreshToken { get; private set; }

    /// <summary>The type of this authorization token.</summary>
    [JsonProperty("token_type", Required = Required.Always)]
    public string TokenType { get; private set; }

    /// <summary>Gets the textual representation of this authorization token.</summary>
    /// <returns><see cref="AccessToken"/>.</returns>
    public override string ToString() => this.AccessToken;

  }

}

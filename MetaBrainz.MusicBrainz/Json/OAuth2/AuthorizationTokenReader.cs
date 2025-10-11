using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.OAuth2;

namespace MetaBrainz.MusicBrainz.Json.OAuth2;

internal sealed class AuthorizationTokenReader : ObjectReader<AuthorizationToken> {

  public static readonly AuthorizationTokenReader Instance = new();

  protected override AuthorizationToken ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? accessToken = null;
    int? lifetime = null;
    string? refreshToken = null;
    string? tokenType = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "access_token":
            accessToken = reader.GetString();
            break;
          case "expires_in":
            lifetime = reader.GetInt32();
            break;
          case "refresh_token":
            refreshToken = reader.GetString();
            break;
          case "token_type":
            tokenType = reader.GetString();
            break;
          default:
            rest ??= new Dictionary<string, object?>();
            rest[prop] = reader.GetOptionalObject(options);
            break;
        }
      }
      catch (Exception e) {
        throw new JsonException($"Failed to deserialize the '{prop}' property.", e);
      }
      reader.Read();
    }
    return new AuthorizationToken {
      AccessToken = accessToken ?? throw new MissingPropertyException("access_token"),
      Lifetime = lifetime ?? throw new MissingPropertyException("lifetime"),
      RefreshToken = refreshToken ?? throw new MissingPropertyException("refresh_token"),
      TokenType = tokenType ?? throw new MissingPropertyException("token_type"),
      UnhandledProperties = rest,
    };
  }

}

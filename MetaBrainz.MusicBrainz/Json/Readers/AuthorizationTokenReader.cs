using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class AuthorizationTokenReader : ObjectReader<AuthorizationToken> {

    public static readonly AuthorizationTokenReader Instance = new AuthorizationTokenReader();

    protected override AuthorizationToken ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      string? accessToken = null;
      int? lifetime = null;
      string? refreshToken = null;
      string? tokenType = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
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
              rest[prop] = AnyObjectReader.Instance.Read(ref reader, typeof(object), options);
              break;
          }
        }
        catch (Exception e) {
          throw new JsonException($"Failed to deserialize the '{prop}' property.", e);
        }
        reader.Read();
      }
      if (accessToken == null)
        throw new JsonException("Expected access token not found or null.");
      if (!lifetime.HasValue)
        throw new JsonException("Expected token lifetime not found or null.");
      if (refreshToken == null)
        throw new JsonException("Expected refresh token not found or null.");
      if (tokenType == null)
        throw new JsonException("Expected token type not found or null.");
      return new AuthorizationToken(accessToken, lifetime.Value, refreshToken, tokenType) {
        UnhandledProperties = rest
      };
    }

  }

}

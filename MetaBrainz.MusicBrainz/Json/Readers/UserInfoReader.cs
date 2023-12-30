using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class UserInfoReader : ObjectReader<UserInfo> {

  public static readonly UserInfoReader Instance = new();

  protected override UserInfo ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? email = null;
    string? gender = null;
    int? id = null;
    string? name = null;
    Dictionary<string, object?>? rest = null;
    Uri? profile = null;
    var verifiedEmail = false;
    string? timeZone = null;
    Uri? webSite = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "email":
            email = reader.GetString();
            break;
          case "email_verified":
            verifiedEmail = reader.GetBoolean();
            break;
          case "gender":
            gender = reader.GetString();
            break;
          case "metabrainz_user_id":
            id = reader.GetInt32();
            break;
          case "profile":
            profile = reader.GetUri();
            break;
          case "sub":
            name = reader.GetString();
            break;
          case "website":
            webSite = reader.GetOptionalUri();
            break;
          case "zoneinfo":
            timeZone = reader.GetString();
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
    if (id is null) {
      throw new JsonException("Expected MusicBrainz user ID not found or null.");
    }
    if (name is null) {
      throw new JsonException("Expected user name not found or null.");
    }
    if (email is null) {
      throw new JsonException("Expected email address not found or null.");
    }
    if (profile is null) {
      throw new JsonException("Expected profile URI not found or null.");
    }
    return new UserInfo(id.Value, name, email, profile) {
      Gender = gender,
      TimeZone = timeZone,
      UnhandledProperties = rest,
      VerifiedEmail = verifiedEmail,
      Website = webSite,
    };
  }

}

using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.OAuth2;

namespace MetaBrainz.MusicBrainz.Json.OAuth2;

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
    return new UserInfo {
      Email = email,
      Gender = gender,
      Name = name ?? throw new MissingPropertyException("sub"),
      Profile = profile ?? throw new MissingPropertyException("profile"),
      TimeZone = timeZone,
      UnhandledProperties = rest,
      UserId = id ?? throw new MissingPropertyException("metabrainz_user_id"),
      VerifiedEmail = verifiedEmail,
      Website = webSite,
    };
  }

}

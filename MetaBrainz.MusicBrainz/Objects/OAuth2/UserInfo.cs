using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects.OAuth2;

internal sealed class UserInfo : JsonBasedObject, IUserInfo {

  public string? Email { get; init; }

  public string? Gender { get; init; }

  public required string Name { get; init; }

  public required Uri Profile { get; init; }

  public string? TimeZone { get; init; }

  public required int UserId { get; init; }

  public bool VerifiedEmail { get; init; }

  public Uri? Website { get; init; }

}

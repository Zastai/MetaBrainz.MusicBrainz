using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class UserInfo : JsonBasedObject, IUserInfo {

  public UserInfo(int userId, string name, Uri profile) {
    this.Name = name;
    this.Profile = profile;
    this.UserId = userId;
  }

  public string? Email { get; init; }

  public string? Gender { get; init; }

  public string Name { get; }

  public Uri Profile { get; }

  public string? TimeZone { get; init; }

  public int UserId { get; }

  public bool VerifiedEmail { get; init; }

  public Uri? Website { get; init; }

}

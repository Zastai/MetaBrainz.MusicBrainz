using System;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>Information about a user as returned by OAuth2.</summary>
public interface IUserInfo : IJsonBasedObject {

  /// <summary>The user's email address.</summary>
  string Email { get; }

  /// <summary>The user's gender.</summary>
  string? Gender { get; }

  /// <summary>The user's name.</summary>
  string Name { get; }

  /// <summary>The user's MusicBrainz profile page.</summary>
  Uri Profile { get; }

  /// <summary>The user's time zone.</summary>
  string? TimeZone { get; }

  /// <summary>The user's internal MetaBrainz user id.</summary>
  int UserId { get; }

  /// <summary>Indicates whether the user's email address is verified.</summary>
  bool VerifiedEmail { get; }

  /// <summary>The user's website.</summary>
  Uri? Website { get; }

}

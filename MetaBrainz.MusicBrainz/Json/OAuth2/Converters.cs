using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetaBrainz.MusicBrainz.Json.OAuth2;

internal static class Converters {

  public static IEnumerable<JsonConverter> Readers {
    get {
      yield return AuthorizationTokenReader.Instance;
      yield return AuthorizationErrorReader.Instance;
      yield return UserInfoReader.Instance;
    }
  }

}

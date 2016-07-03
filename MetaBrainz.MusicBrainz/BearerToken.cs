using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing an OAuth2 bearer token.</summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public sealed class BearerToken {

    /// <summary>The access token (i.e. the one you use for authenticated requests).</summary>
    public string AccessToken { get; }

    /// <summary>The lifetime of the token, in seconds.</summary>
    public int Lifetime { get; }

    /// <summary>The refresh token (i.e. the one you use to get a new access token, via <see cref="OAuth2.RefreshBearerToken"/>).</summary>
    public string RefreshToken { get; }

    #region JSON-Based Construction

    internal BearerToken(string responseText) {
      var json = new JavaScriptSerializer().Deserialize<JSON>(responseText);
      if (json?.token_type != "bearer")
        throw new InvalidOperationException("No bearer token data found in the response text.");
      this.AccessToken  = json.access_token;
      this.Lifetime     = json.expires_in;
      this.RefreshToken = json.refresh_token;
    }

    // The following class is created by a deserializer, so there's no point in complaining that its fields are uninitialized.
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    private sealed class JSON {
      public string access_token;
      public int    expires_in;
      public string refresh_token;
      public string token_type;
    }

    #endregion

  }

}

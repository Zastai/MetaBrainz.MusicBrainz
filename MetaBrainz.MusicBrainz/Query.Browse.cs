using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Entities;
using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    #region Browse

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class BrowseWorksResult {
      [JsonProperty("works",       Required = Required.Always)] public Work[] works;
      [JsonProperty("work-count",  Required = Required.Always)] public int    work_count;
      [JsonProperty("work-offset", Required = Required.Always)] public int    work_offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    #endregion

  }

}

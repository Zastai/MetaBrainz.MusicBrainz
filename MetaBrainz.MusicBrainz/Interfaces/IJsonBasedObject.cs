using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces {

  /// <summary>An object based on a JSON string (as returned by web services).</summary>
  [PublicAPI]
  public interface IJsonBasedObject {

    /// <summary>
    /// A dictionary containing all properties not handled by this library.<br/>
    /// This should be <see langword="null"/>; if it's not, please file a ticket, listing its contents.
    /// </summary>
    IReadOnlyDictionary<string, object?>? UnhandledProperties { get; }

  }

}

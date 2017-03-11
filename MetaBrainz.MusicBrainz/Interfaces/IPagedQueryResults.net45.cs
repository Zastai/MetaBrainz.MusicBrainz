using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;

namespace MetaBrainz.MusicBrainz.Entities {

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  [SuppressMessage("ReSharper", "UnusedTypeParameter")]
  public partial interface IPagedQueryResults<TInterface, TItem> {

    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the next set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>An asynchronous task returning this result set (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    Task<TInterface> NextAsync();

    /// <summary>
    ///   Queries the MusicBrainz server (the same one used for the original request) for the previous set of results, based on <see cref="Offset"/> and <see cref="Limit"/>.
    /// </summary>
    /// <returns>An asynchronous task returning this result set (with updated values).</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    Task<TInterface> PreviousAsync();

  }

}

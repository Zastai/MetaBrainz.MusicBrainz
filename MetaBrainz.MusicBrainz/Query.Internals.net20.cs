using System.Threading;

namespace MetaBrainz.MusicBrainz {

  public sealed partial class Query {

    /// <summary>A function taking no arguments.</summary>
    /// <typeparam name="TResult">The type for the function's result.</typeparam>
    /// <returns>The result of the function.</returns>
    private delegate TResult Func<out TResult>();

    private static readonly ReaderWriterLock RequestLock = new ReaderWriterLock();

    private static void Lock() {
      Query.RequestLock.AcquireWriterLock(-1);
    }

    private static void Unlock() {
      Query.RequestLock.ReleaseWriterLock();
    }

  }

}

using System.Threading;

namespace MetaBrainz.MusicBrainz {

  public sealed partial class Query {

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static void Lock() {
      Query.RequestLock.EnterWriteLock();
    }

    private static void Unlock() {
      Query.RequestLock.ExitWriteLock();
    }

  }

}

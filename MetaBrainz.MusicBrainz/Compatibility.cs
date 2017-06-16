// Compatibility for features missing from some frameworks.

#if NETFX_EQ_2_0

namespace System {

  /// <summary>A function taking no arguments.</summary>
  /// <typeparam name="TResult">The type for the function's result.</typeparam>
  /// <returns>The result of the function.</returns>
  delegate TResult Func<out TResult>();

}

#endif

#if (NETFX_TARGET && !NETFX_GE_3_5)

namespace System.Threading {

  internal enum LockRecursionPolicy { NoRecursion }

  internal class ReaderWriterLockSlim {

    private readonly ReaderWriterLock _lock = new ReaderWriterLock();

    public ReaderWriterLockSlim(LockRecursionPolicy policy) { }

    public void EnterWriteLock() => this._lock.AcquireWriterLock(-1);

    public void ExitWriteLock() => this._lock.ReleaseWriterLock();

  }

}

#endif

using System;
using System.Threading;

namespace Photo.Utility.Synchronization
{
	public class SynchronizationHandler : IDisposable
	{
		#region Private members

		ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

		#endregion


		#region IDisposable

		public void Dispose()
		{
			Mode = SynchronizationMode.None;
		}

		#endregion


		#region Public properties

		public SynchronizationMode Mode
		{
			get
			{
				if (_rwLock.IsWriteLockHeld)
					return SynchronizationMode.Write;

				if (_rwLock.IsUpgradeableReadLockHeld)
					return SynchronizationMode.UpgradableRead;

				if (_rwLock.IsReadLockHeld)
					return SynchronizationMode.Read;

				return SynchronizationMode.None;
			}

			set
			{
				if(value == SynchronizationMode.None)
				{
					if (_rwLock.IsWriteLockHeld)
					{
						_rwLock.ExitWriteLock();
					}

					if (_rwLock.IsUpgradeableReadLockHeld)
					{
						_rwLock.ExitUpgradeableReadLock();
					}
					else if (_rwLock.IsReadLockHeld)
					{
						_rwLock.ExitReadLock();
					}
				}
				else if(value == SynchronizationMode.Read)
				{
					if (_rwLock.IsWriteLockHeld)
					{
						_rwLock.ExitWriteLock();
					}

					if (_rwLock.IsUpgradeableReadLockHeld)
					{
						//This is good enough
						//Nothing to do
					}
					else if (_rwLock.IsReadLockHeld)
					{
						//We're already in this mode!
						//Nothing to do here!
					}
					else
					{
						_rwLock.EnterReadLock();
					}
				}
				else if (value == SynchronizationMode.UpgradableRead)
				{
					if (_rwLock.IsWriteLockHeld)
					{
						_rwLock.ExitWriteLock();
					}

					if (_rwLock.IsUpgradeableReadLockHeld)
					{
						//We're already in this mode!
						//Nothing to do here!
					}
					else if (_rwLock.IsReadLockHeld)
					{
						_rwLock.ExitReadLock();
						_rwLock.EnterUpgradeableReadLock();
					}
					else
					{
						_rwLock.EnterUpgradeableReadLock();
					}
				}
				else if (value == SynchronizationMode.Write)
				{
					if (_rwLock.IsReadLockHeld)
						_rwLock.ExitReadLock();

					if (!_rwLock.IsWriteLockHeld)
					{
						_rwLock.EnterWriteLock();
					}
				}
				else
				{
					throw new Exception(string.Format("Invalid value for SynchronizationMode: {0}", value));
				}
			}
		}

		#endregion


		#region Constructor

		public SynchronizationHandler(ReaderWriterLockSlim rwLock, SynchronizationMode initialMode)
		{
			_rwLock = rwLock;
			Mode = initialMode;
		}

		#endregion
	}
}

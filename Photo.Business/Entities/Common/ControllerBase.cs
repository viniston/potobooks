using System;
using System.Collections.Generic;
using System.Threading;
using Photo.Utility.Synchronization;

namespace Photo.Business.Entities.Common
{
	public abstract class ControllerBase<TCBO, TIdentity> where TCBO : class, ICBO<TIdentity> where TIdentity : IComparable, IComparable<TIdentity>
	{
		#region Private

		#region Memebrs

		//Syncronization stuff
		private readonly ReaderWriterLockSlim _rwLock;

		//Caching control
		private TimeSpan? _maxAge = null;
		private DateTime? _lastUpdated;
		
		//Main list and dictionary
		private List<TCBO> _allItems;
		private Dictionary<TIdentity, TCBO> _allItemsDictionary;

		#endregion

		#endregion


		#region Protected

		#region Properties

		protected SynchronizationHandler ReadSyncHandler => new SynchronizationHandler(_rwLock, SynchronizationMode.Read);

		protected SynchronizationHandler UpgradableSyncHandler => new SynchronizationHandler(_rwLock, SynchronizationMode.UpgradableRead);

		protected SynchronizationHandler WriteSyncHandler => new SynchronizationHandler(_rwLock, SynchronizationMode.Write);

		#endregion


		#region Virtual

		protected virtual bool IsOutdated => !_lastUpdated.HasValue || (_maxAge.HasValue && _lastUpdated + _maxAge > DateTime.UtcNow);

		protected virtual IEqualityComparer<TIdentity> IdentityComparer => EqualityComparer<TIdentity>.Default;

		#endregion


		#region Abstract

		protected abstract Func<DateTime?, IEnumerable<TCBO>> GetAllFunc { get; }
       

        #endregion


        #region Methods

        protected void SynchronizeContent(bool force)
		{
			if (!IsOutdated && !force)
				return;

			using (WriteSyncHandler)
			{
				if (IsOutdated || force)
				{
					IEnumerable<TCBO> newContent = GetAllFunc(_lastUpdated);
					if (newContent != null)
					{
						OnNewContent(newContent);
					}

					_lastUpdated = DateTime.UtcNow;
				}
			}
		}

		protected virtual void OnNewContent(IEnumerable<TCBO> newContent)
		{
			IEnumerator<TCBO> e = newContent.GetEnumerator();
			while (e.MoveNext())
			{
				TCBO cbo = e.Current;
				if (!_allItemsDictionary.ContainsKey(cbo.Identity))
				{
					_allItems.RemoveAll(c => c.Identity.CompareTo(cbo.Identity) == 0);
					_allItems.Add(cbo);
				}

				_allItemsDictionary[cbo.Identity] = cbo;
			}
		}

		#endregion

		#endregion


		#region Public

		#region Properties

		public bool IncludeInActive { get; set; } = false;

		public bool Reload
		{
			set
			{
				if (value)
				{
					_lastUpdated = null;
					using (WriteSyncHandler)
					{
						_allItems = new List<TCBO>();
						_allItemsDictionary = new Dictionary<TIdentity, TCBO>();
					}
				}
			}
		}

		public List<TCBO> All
		{
			get
			{
				using (ReadSyncHandler)
				{
					SynchronizeContent(false);

					return _allItems;
				}
			}
		}

		public TCBO this[TIdentity identity]
		{
			get
			{
				TCBO cbo;
				using (UpgradableSyncHandler)
				{
					SynchronizeContent(false);
					_allItemsDictionary.TryGetValue(identity, out cbo);

					if (cbo == null)
					{
						SynchronizeContent(true);
						_allItemsDictionary.TryGetValue(identity, out cbo);
					}
				}

				return cbo;
			}
		}

		#endregion

		#endregion


		#region Constructor

		protected ControllerBase()
		{
			_rwLock = new ReaderWriterLockSlim();
			_allItems = new List<TCBO>();
			_allItemsDictionary = new Dictionary<TIdentity, TCBO>(IdentityComparer);
		}

		#endregion
	}
}

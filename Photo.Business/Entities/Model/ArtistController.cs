using System;
using System.Linq;
using System.Collections.Generic;
using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Model
{
    public class ArtistController : ControllerBase<ArtistInfo, int>
    {
        #region Singleton

        public static ArtistController Instance { get; private set; }

		#endregion


		#region Constructor

		static ArtistController()
        {
            Instance = new ArtistController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<ArtistInfo>> GetAllFunc => DataProviderManager.Provider.GetAllArtists;

        public ArtistInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }

		public ArtistInfo GetArtistByToken(string token)
		{
			return DataProviderManager.Provider.GetArtistByToken(token).FirstOrDefault();
		}

		public static long Update(ArtistInfo artist)
		{
			return DataProviderManager.Provider.UpdateArtist(artist);
		}		
	}
}

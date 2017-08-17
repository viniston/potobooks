using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class ArtistFareController : ControllerBase<ArtistFareInfo, int>
    {
        #region Singleton

        public static ArtistFareController Instance { get; private set; }

        #endregion


        #region Constructor

        static ArtistFareController()
        {
            Instance = new ArtistFareController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<ArtistFareInfo>> GetAllFunc => DataProviderManager.Provider.GetAllArtistFares;

        public ArtistFareInfo GetByID(int id)
        {
            return All.Find(city => city.ID == id);
        }
    }
}

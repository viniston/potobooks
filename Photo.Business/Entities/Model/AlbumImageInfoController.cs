using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Photo.Business.Entities.Model {
    public class AlbumImageInfoController : ControllerBase<AlbumImageInfo, long> {
        #region Singleton

        public static AlbumImageInfoController Instance { get; private set; }

        #endregion


        #region Constructor

        static AlbumImageInfoController() {
            Instance = new AlbumImageInfoController();
        }

        #endregion

        protected override Func<DateTime?, IEnumerable<AlbumImageInfo>> GetAllFunc
            => null;


        public static IEnumerable<AlbumImageInfo> GetAllAlbumImages(long albumId) {
            return DataProviderManager.Provider.GetAllAlbumImages(albumId);
        }

        public static AlbumImageInfo GetByID(long id) {
            return DataProviderManager.Provider.GetAlbumImageByID(id);
        }

        public static IEnumerable<AlbumImageInfo> GetByAlbumID(long id) {
            return DataProviderManager.Provider.GetIAlbumImageByAlbumID(id);
        }

        public static long Save(AlbumImageInfo image, IDbTransaction transaction) {
            return DataProviderManager.Provider.SaveAlbumImage(image, transaction);
        }

    }
}

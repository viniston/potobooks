using Photo.Business.DataProvider;
using Photo.Business.Entities.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace Photo.Business.Entities.Model
{
	public class ImageController : ControllerBase<ImageInfo, long>
	{
		#region Singleton

		public static ImageController Instance { get; private set; }

		#endregion


		#region Constructor

		static ImageController()
		{
			Instance = new ImageController();
		}

		#endregion

		protected override Func<DateTime?, IEnumerable<ImageInfo>> GetAllFunc => DataProviderManager.Provider.GetAllImages;

		public ImageInfo GetByID(long id)
		{
			return All.Find(city => city.ID == id);
		}

        public IEnumerable<ImageInfo> GetByBookingID(long id)
        {
            return DataProviderManager.Provider.GetImageByBookingID(id);
        }

        public static long Save(ImageInfo image, IDbTransaction transaction)
		{
            return DataProviderManager.Provider.SaveImage(image, transaction);		
		}

        public bool DeleteImageByID(long imageId)
        {
            return DataProviderManager.Provider.DeleteImageByID(imageId);
        }
    }
}

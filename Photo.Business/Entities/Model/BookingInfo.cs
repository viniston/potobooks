using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;
using System;
using System.Collections.Generic;

namespace Photo.Business.Entities.Model
{
    public class BookingInfo : ICBO<long>
    {
        private List<PaymentInfo> _paymentList = null;

		private List<ImageInfo> _imageList = null;

		public long ID { get; set; }

        public Guid PurchaseID { get; set; }

		public string SystemReference { get; set; }

        public string ImagePath { get; set; }

        public int? ProductCategoryID { get; set; }

        public ProductCategoryInfo ProductCategory { get { return ProductCategoryID.HasValue ? ProductCategoryController.Instance.GetByID(ProductCategoryID.Value) : null; } }

        public List<PaymentInfo> PaymentList
        {
            get
            {
                if (_paymentList == null)
                    _paymentList = PaymentController.Instance.All.FindAll(p => p.BookingID == ID);

                return _paymentList;
            }
            set { _paymentList = value; }
        }

		public List<ImageInfo> ImageList
		{
			get
			{
				if (_imageList == null || _imageList.Count == 0)
					_imageList = ImageController.Instance.All.FindAll(p => p.BookingID == ID && p.IsActive == true);

				return _imageList;
			}
			set { _imageList = value; }
		}

		public string Email { get; set; }

        public string Remarks { get; set; }

        public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

		public bool? IsPaidToArtist { get; set; }

		public DateTime Updated { get; set; }

		public int? DealerID { get; set; }

        public DealerInfo Dealer { get { return DealerID.HasValue ? DealerController.Instance.GetByID(DealerID.Value) : null; } }

        public string VoucherCode { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public short StatusID { get; set; }

		public int? ArtistID { get; set; }

        public ArtistInfo Artist { get { return ArtistID.HasValue ? ArtistController.Instance.GetByID(ArtistID.Value) : null; } }

        public long Identity => ID;		
	}
}

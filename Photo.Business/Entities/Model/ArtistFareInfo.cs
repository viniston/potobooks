using System;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
	public class ArtistFareInfo : ICBO<int>
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        public ProductInfo Product
		{
            get { return ProductController.Instance.GetByID(ProductID); }
        }

		public int ArtistID { get; set; }

        public ArtistInfo Artist
        {
            get { return ArtistController.Instance.GetByID(ArtistID); }
        }

        public decimal Cost { get; set; }

		public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public int Identity => ID;        
    }
}

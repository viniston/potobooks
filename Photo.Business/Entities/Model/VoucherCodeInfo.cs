using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Business.Entities.Model
{
    public class VoucherCodeInfo : ICBO<long>
    {
        public long ID { get; set; }

		public int DealerID { get; set; }

		public DealerInfo Dealer
		{
			get
			{
				return DealerController.Instance[DealerID];
			}
		}

		public string VoucherCode { get; set; }

		public int ProductCategoryID { get; set; }

		public ProductCategoryInfo ProductCategory
		{
			get { return ProductCategoryController.Instance[ProductCategoryID]; }
		}

		public bool Redeemed { get; set; }

		public DateTime Expiry { get; set; }

        public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public long Identity => ID;        
    }
}

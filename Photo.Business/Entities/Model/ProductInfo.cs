using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;
using System;

namespace Photo.Business.Entities.Model
{
    public class ProductInfo : ICBO<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Features { get; set; }

        public string ImagePath { get; set; }

        public decimal Amount { get; set; }

		public decimal GrossAmount { get; set; }

		public int CategoryID { get; set; }

        public CategoryInfo Category
        {
            get
            {
                return CategoryController.Instance.GetByID(CategoryID);
            }
        }

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

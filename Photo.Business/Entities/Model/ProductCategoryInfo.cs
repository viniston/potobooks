using System;
using Photo.Business.Entities.Common;

namespace Photo.Business.Entities.Model
{
	public class ProductCategoryInfo : ICBO<int>
    {
        public int ID { get; set; }

		public int ProductID { get; set; }

		public int CategoryID { get; set; }

		public decimal Weightage { get; set; }

		public string Name { get { return Category.Name + " " + Product.Name; } }

		public string Title { get { return Category.Name + " " + Product.Title; } }

		public decimal Amount { get { return Product.Amount + Weightage; } }

		public string ImagePath { get; set; }

		public decimal GrossAmount { get { return Product.GrossAmount + Weightage; } }

		public ProductInfo Product { get { return ProductController.Instance.GetByID(ProductID); } }

		public CategoryInfo Category { get { return CategoryController.Instance.GetByID(CategoryID); } }

		public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public int Identity => ID;
        
    }
}

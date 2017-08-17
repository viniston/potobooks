using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Business.Entities.Model
{
    public class CategoryInfo : ICBO<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public int Identity => ID;

        private List<CategoryInfo> GetCategoryList(string csvList)
        {
            List<CategoryInfo> categoryList = new List<CategoryInfo>();
            if (csvList == null)
                return categoryList;

            string[] csvModules = csvList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            categoryList = csvModules.Select(mod => CategoryController.Instance.GetByID(int.Parse(mod.Trim()))).Where(category => category != null).ToList();

            return categoryList;
        }
    }
}

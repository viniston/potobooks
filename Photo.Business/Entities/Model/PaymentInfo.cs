using Photo.Business.Entities.Common;
using Photo.Business.Entities.Model.Common.Money;
using Photo.Business.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Business.Entities.Model
{
    public class PaymentInfo : ICBO<long>
    {
        public long ID { get; set; }

        public long BookingID { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public MoneyInfo AmountObject
        {
            get
            {
                return new MoneyInfo(Amount, 3);
            }
        }

        public decimal ProcessingCost { get; set; }

        public MoneyInfo ProcessingCostObject
        {
            get
            {
                return new MoneyInfo(ProcessingCost, 3);
            }
        }

        public short? PaymentMethodID { get; set; }

        public PaymentMethod PaymentMethod { get { return (PaymentMethod)PaymentMethodID; } }

        public short? PaymentStatusID { get; set; }

        public PaymentStatus PaymentStatus { get { return (PaymentStatus)PaymentStatusID; } }

        public string ProviderReference { get; set; }

        public string ProviderReply { get; set; }

        public string ResponseData { get; set; }

        public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public long Identity => ID;

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

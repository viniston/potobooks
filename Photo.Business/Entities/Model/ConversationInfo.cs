using System;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
	public class ConversationInfo : ICBO<long>
    {
        public long ID { get; set; }

        public long BookingID { get; set; }

		public bool IsCustomerQuery { get; set; }

        public bool Resolved { get; set; }

        public string Text { get; set; }

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

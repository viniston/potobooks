using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Business.Entities.Model
{
    public class DealerInfo : ICBO<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

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

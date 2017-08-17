using System;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
    public class SearchBookingInfo
    {
        public long ID { get; set; }

        public string SystemReference { get; set; }

        public string Product { get; set; }

        public string Email { get; set; }

        public string CustomerName { get; set; }

        public string DealerName { get; set; }

        public bool? IsActive { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string VoucherCode { get; set; }

        public string Status { get; set; }

        public short StatusID { get; set; }

        public string Artist { get; set; }

        public int TotalRecords { get; set; }

        public string ImagePath { get; set; }
    }
}

using System;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
    public class SearchConversationInfo
    {
        public long ID { get; set; }

        public long BookingID { get; set; }

        public BookingInfo Booking { get { return BookingController.Instance.GetByID(BookingID); } }

        public string SystemReference { get; set; }

        public DateTime Updated { get; set; }

        public int DurationInMinutes { get; set; }

        public string Email { get; set; }

        public string Product { get; set; }

        public int TotalRecords { get; set; }
    }
}

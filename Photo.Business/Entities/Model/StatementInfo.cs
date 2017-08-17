using System;
using System.Collections.Generic;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
	public class StatementInfo : ICBO<int>
    {
		private List<BookingInfo> _bookings = null;

		private string _systemReferences = null;

		public int ID { get; set; }

        public DateTime Date { get; set; }

		public int? ArtistID { get; set; }

		public string BookingIDS { get; set; }

		public string Artist
		{
			get
			{
				return ArtistID.HasValue ? ArtistController.Instance.GetByID(ArtistID.Value).Name : null;
			}
		}

		public List<BookingInfo> Bookings
		{
			get
			{
				if (_bookings != null || string.IsNullOrEmpty(BookingIDS))
					return _bookings;

				_bookings = new List<BookingInfo>();
				foreach(string strBookingID in BookingIDS.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
					_bookings.Add(BookingController.Instance.GetByID(long.Parse(strBookingID)));

				return _bookings;
			}
		}

		public string SystemReferences
		{
			get
			{
				if (_systemReferences != null || string.IsNullOrEmpty(BookingIDS))
					return _systemReferences;

				_systemReferences = string.Empty;
				foreach (BookingInfo booking in Bookings)
				{
					if (!string.IsNullOrEmpty(_systemReferences))
						_systemReferences += ", ";

					_systemReferences += booking.SystemReference;
				}

				return _systemReferences;
			}
		}

		public decimal Amount { get; set; }

		public string Remark { get; set; }

        public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public int Identity => ID;        

		public DateTime Updated { get; set; }
    }
}

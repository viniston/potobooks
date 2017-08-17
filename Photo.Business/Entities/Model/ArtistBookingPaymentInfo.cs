namespace Photo.Business.Entities.Model
{
	public class ArtistBookingPaymentInfo 
    {
		public long ID { get; set; }

		public string SystemReference { get; set; }

        public decimal Cost { get; set; }

		public string Code { get; set; }

		public string CategoryName { get; set; }

		public bool? IsPaidToArtist { get; set; }        
    }
}

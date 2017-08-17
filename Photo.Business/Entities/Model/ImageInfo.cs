using System;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
	public class ImageInfo : ICBO<long>
    {
        public long ID { get; set; }

        public string Path { get; set; }

		public int TypeID { get; set; }

		public ImageType Type { get { return (ImageType)TypeID; } }

		public long BookingID { get; set; }
		
        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public long Identity => ID;        
    }
}

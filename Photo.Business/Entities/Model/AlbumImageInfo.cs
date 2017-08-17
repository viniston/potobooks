using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model {
    public class AlbumImageInfo : ICBO<long> {

        public long AlbumImageID { get; set; }

        public int ImageTypeId { get; set; }

        public ImageType Type => (ImageType)ImageTypeId;

        public string AlbumImagePath { get; set; }

        public long AlbumID { get; set; }

        public bool? IsActive { get; set; }

        public string AlbumImageName { get; set; }

        public string AlbumImageDescription { get; set; }

        public short StatusId { get; set; }

        public DateTime Updated { get; set; }

        public long Identity => AlbumImageID;

        public Guid UploadedBy { get; set; }

        public UserInfo UploadedByUser {
            get { return UserController.GetByID(UploadedBy); }
            set { if (value != null) UploadedBy = value.ID; }
        }
    }
}

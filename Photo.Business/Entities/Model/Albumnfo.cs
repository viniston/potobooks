using System;
using System.Collections.Generic;
using System.Linq;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model {
    [Serializable]
    public class AlbumInfo : ICBO<long> {

        private IEnumerable<AlbumImageInfo> _imageList;

        public long AlbumId { get; set; }

        public string AlbumImagePath { get; set; }
        
        public IEnumerable<AlbumImageInfo> ImageList {
            get {
                if (_imageList == null || _imageList.ToList().Count == 0)
                    _imageList = AlbumImageInfoController.GetAllAlbumImages(AlbumId);

                return _imageList;
            }
            set { _imageList = value; }
        }


        public Guid UploadedBy { get; set; }

        public UserInfo UploadedByUser {
            get { return UserController.GetByID(UploadedBy); }
            set { if (value != null) UploadedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

        public string AlbumName { get; set; }

        public string AlbumDescription { get; set; }

        public short StatusId { get; set; }

        public int AlbumType { get; set; }

        public AlbumType Type => (AlbumType)AlbumType;

        public long Identity => AlbumId;
    }
}

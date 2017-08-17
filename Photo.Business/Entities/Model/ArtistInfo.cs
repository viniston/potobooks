using System;
using Photo.Business.Entities.Common;
using Photo.Business.Entities.Security;

namespace Photo.Business.Entities.Model
{
	public class ArtistInfo : ICBO<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

		public string Password { get; set; }

		public Guid Token { get; set; }

		public string Email { get; set; }

		public Guid UpdatedBy { get; set; }

        public UserInfo UpdatedByUser
        {
            get { return UserController.GetByID(UpdatedBy); }
            set { if (value != null) UpdatedBy = value.ID; }
        }

        public bool? IsActive { get; set; }

        public DateTime Updated { get; set; }

		public bool IsTokenExpired
		{
			get
			{
				return (DateTime.UtcNow - Updated).Minutes > 45; 
			}
		}

        public int Identity => ID;        
    }
}

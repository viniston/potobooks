using System;
using System.Xml.Serialization;

namespace Photo.Business.Entities.Common
{
	public interface ICBO<TIdentity>
	{
		[XmlIgnore]
		TIdentity Identity { get; }

		[XmlIgnore]
		DateTime Updated { get; set; }

		[XmlIgnore]
		bool? IsActive { get; set; }
	}
}

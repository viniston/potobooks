using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.IO;

using Photo.Business.Utilities.Storage;

public partial class UploadFile : CorePage.CorePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			if (User != null)
			{
                if (Request.Files["file"] == null)
					return;
                
                var path = RepositoryHelper.UploadImagePath() + Path.GetFileName(Request.Files["file"].FileName.Replace(" ","_").Replace("&", ""));
				Request.Files["file"].SaveAs(path);
				Response.Write(JsonConvert.SerializeObject(new { success = true, file = path },
								new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
				
			}
		}
	}
}
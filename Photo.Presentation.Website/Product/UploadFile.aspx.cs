using Photo.Business.Utilities.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.IO;

public partial class UploadFile : CorePage.CorePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			if (User != null)
			{
				long dirID = DateTime.Now.Ticks;
				string workfileRuleFileDir = RepositoryHelper.GetWorkFileStaticContentStoragePath(DocumentType.WorkFileUpload, dirID);

				string staticDirectoryPath = ConfigurationManager.AppSettings["DocumentStorageLocation"];
				if (!Directory.Exists(workfileRuleFileDir))
					Directory.CreateDirectory(workfileRuleFileDir);

				if (Request.Files["file"] == null)
					return;
								
				var path = workfileRuleFileDir + Path.DirectorySeparatorChar + Path.GetFileName(Request.Files["file"].FileName);
				Request.Files["file"].SaveAs(path);
				Response.Write(JsonConvert.SerializeObject(new { success = true, file = path },
												new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
				
			}
		}
	}
}
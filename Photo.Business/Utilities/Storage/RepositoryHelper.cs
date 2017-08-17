using System;
using System.Configuration;
using System.IO;
using System.Web;
using Photo.Resources.Repository;

namespace Photo.Business.Utilities.Storage
{
	/// <summary>
	/// Helper class for File Storage Repository
	/// </summary>
	public static class RepositoryHelper
	{
		#region Private Members

		private static byte _workfileStaticContentFolderLevels = Convert.ToByte(Repository.FareRuleContentFolderLevels);
		private static byte _workfileStaticContentMaximumIDLeftPadding = Convert.ToByte(Repository.FareRuleStaticContentMaximumIDLeftPadding);

		public static byte WorkfileStaticContentMaximumIDLeftPadding
		{
			get
			{
				return _workfileStaticContentMaximumIDLeftPadding;
			}

			set
			{
				_workfileStaticContentMaximumIDLeftPadding = value;
			}
		}

		#endregion


		#region Private Methods

		/// <summary>
		/// Get the application root path
		/// </summary>
		/// <returns>string</returns>
		private static string GetApplicationRoot()
		{
			if (HttpContext.Current != null)
				return HttpContext.Current.Server.MapPath("/");
			else
				return AppDomain.CurrentDomain.BaseDirectory;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Get the Document Storage Path based upon DocumentType
		/// </summary>
		/// <param name="documentType"></param>
		/// <returns>string</returns>
		public static string GetRespositoryPath(DocumentType documentType)
		{
			string repositoryPath = string.Empty;

			switch (documentType)
			{
				case DocumentType.WorkFileUpload:
					repositoryPath = Repository.FareRuleContentStoragePath;
					break;
				default:
					throw new Exception("Document type: " + documentType.ToString() + " is not supported");
			}

			return repositoryPath.Replace("[DocumentStorageLocation]", ConfigurationManager.AppSettings["DocumentStorageLocation"])
				.Replace("[ApplicationPath]", GetApplicationRoot());
		}
		
		/// <summary>
		/// Returns static content storage path for the provided FareRuleDetails
		/// </summary>
		/// <param name="documentType"></param>
		/// <param name="id"></param>
		/// <returns>string</returns>
		public static string GetWorkFileStaticContentStoragePath(DocumentType documentType, long id)
		{
			if (documentType != DocumentType.WorkFileUpload)
				throw new Exception("Document type: " + documentType.ToString() + " is not supported");

			string storagePath = "EN";
			string paddedID = id.ToString().PadLeft(_workfileStaticContentMaximumIDLeftPadding, '0');
			
			for (byte i = _workfileStaticContentFolderLevels; i > 0; i--)
				if (i == 1)
					storagePath = Path.Combine(paddedID, storagePath);
				else
				{
					storagePath = Path.Combine(paddedID.Substring(paddedID.Length - _workfileStaticContentFolderLevels), storagePath);
					paddedID = paddedID.Remove(paddedID.Length - _workfileStaticContentFolderLevels, _workfileStaticContentFolderLevels);
				}
			
			return Path.Combine(GetRespositoryPath(documentType), storagePath);
		}

        public static string UploadImagePath()
        {
            long dirID = DateTime.Now.Ticks;
            string workfileRuleFileDir = RepositoryHelper.GetWorkFileStaticContentStoragePath(DocumentType.WorkFileUpload, dirID);

            string staticDirectoryPath = ConfigurationManager.AppSettings["StaticDataStoragePath"];
            if (!Directory.Exists(workfileRuleFileDir))
                Directory.CreateDirectory(workfileRuleFileDir);

            return workfileRuleFileDir + Path.DirectorySeparatorChar;
        }

        #endregion
    }
}
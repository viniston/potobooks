using System;
using System.Web.UI;
using Utility;

public partial class Trip_Resource_Pages_SEM_Default : Page
{
	#region Event Handlers

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			string hotelLocation = string.Empty;

			byte mode;
			if (!string.IsNullOrEmpty(Request.QueryString["args"]) &&
				!string.IsNullOrEmpty(Request.QueryString["mode"]) &&
				byte.TryParse(Request.QueryString["mode"].ToString(), out mode))
			{
				string args = Request.QueryString["args"].ToString();

				if (mode == (byte)SEMMode.Hotels)
				{
					ltlContentHeading.Text = GetLocalResourceObject("Hotels").ToString()
						.Replace("[location]", "COK");

					hotelLocation = "Kozhikode";
				}
				else
					Utility.Utilities.RedirectToErrorPage();

				
			}
			else
				Utility.Utilities.RedirectToErrorPage();
		}
	}

	#endregion	
}

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Photo.Business.Entities.Model;
using Photo.Utility.LogHelper;

public partial class Resource_Pages_Preview_Default : System.Web.UI.Page
{
    private static string _staticDirectoryPath = ConfigurationManager.AppSettings["DocumentStorageLocation"];

    private long _bookingId = 0;
    private string _previewPNGImagePath;
    private string _previewJPGImagePath;
    private bool _isPreview = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["BookingID"]) && long.TryParse(Request.QueryString["BookingID"], out _bookingId))
        {
            string purchaseID = Request.QueryString["PurchaseID"].ToString();
            BookingInfo booking = BookingController.Instance.GetByID(_bookingId);
            _isPreview = booking.StatusID == (int)BookingStatus.Completed;
            if (booking.PurchaseID.ToString() == purchaseID.ToString())
            {
                foreach (ImageInfo image in ImageController.Instance.GetByBookingID(_bookingId).OrderByDescending(itr => itr.Updated).ToList())
                {
                    if (_isPreview && image.Type != ImageType.Final)
                        continue;

                    if (!_isPreview && image.Type != ImageType.Draft)
                        continue;

                    if (!string.IsNullOrEmpty(_previewJPGImagePath) && !string.IsNullOrEmpty(_previewPNGImagePath))
                        break;

                    if (!string.IsNullOrEmpty(image.Path) && image.Path.ToLower().EndsWith(".png"))
                        _previewPNGImagePath = image.Path;

                    if (!string.IsNullOrEmpty(image.Path) && image.Path.ToLower().EndsWith(".jpg"))
                        _previewJPGImagePath = image.Path;
                }

                if (_isPreview)
                {
                    if (string.IsNullOrEmpty(_previewPNGImagePath) || string.IsNullOrEmpty(_previewJPGImagePath))
                    {
                        LogHelper.Log(Logger.Application, LogLevel.Error, "Missing preview images for " + booking.SystemReference);
                        Utility.Utilities.RedirectToErrorPage();
                    }
                    ltlPreviewImage.Text = ltlPreviewImage.Text.Replace("[PreviewImagePath]", _previewPNGImagePath.Replace(_staticDirectoryPath, "").Replace("\\", "/"));
                }
                else
                {
                    ltlPreviewImage.Visible = false;
                    ltlTitle.Text = ltlTitle.Text.Replace("Gift", "Draft");
                    lnkDownloadPNG.Visible = false;
                }
            }
            else
                Utility.Utilities.RedirectToErrorPage();
        }
        else
            Utility.Utilities.RedirectToErrorPage();
    }

    protected void lnkDownloadJPG_Click(object sender, EventArgs e)
    {
        DownLoad(_previewJPGImagePath);
    }

    protected void lnkDownloadPNG_Click(object sender, EventArgs e)
    {
        DownLoad(_previewPNGImagePath);
    }

    private void DownLoad(string filename)
    {
        try
        {
            string path = filename;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }
        catch(Exception ex)
        {
            LogHelper.Log(Logger.Application, LogLevel.Error, ex);
        }
    }
}
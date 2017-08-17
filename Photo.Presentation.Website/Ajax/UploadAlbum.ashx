<%@ WebHandler Language="C#" Class="UploadAlbum" %>

using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using Helper;
using Photo.Business.Entities.Album;
using Photo.Business.Entities.Model;
using Photo.Business.Entities.Security;
using Photo.Business.Utilities.ImagePreview;

public class UploadAlbum : IHttpHandler {

    #region Private Members

    private UserInfo _user;

    #endregion


    public UserInfo User {
        get { return _user ?? (_user = SecurityHelper.GetCurrentUser()); }
    }

    public void ProcessRequest(HttpContext context) {


        // get variables first
        var nvc = HttpContext.Current.Request.Form;
        var request = new AlbumDto();
        // iterate through and map to strongly typed model
        foreach (var kvp in nvc.AllKeys) {
            var pi = request.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
            int i;
            if (int.TryParse(nvc[kvp], out i)) {
                pi.SetValue(request, i, null);
            } else {
                pi.SetValue(request, nvc[kvp], null);
            }

        }
        request.SourceFile = HttpContext.Current.Request.Files["SourceFile"];
        if (request.SourceFile != null) {
            var album = new AlbumInfo {
                AlbumDescription = request.Description,
                AlbumName = request.Name,
                AlbumType = request.AlbumType,
                AlbumImagePath = request.SourceFile.FileName,
                StatusId = 1,
                IsActive = true,
                UploadedBy = User.ID,
            };

            var albumId = AlbumController.Save(album);

            var baseBath = ConfigurationManager.AppSettings["DocumentStorageLocation"];
            var filePath = baseBath + @"AlbumTemplates\" + albumId + "_" + request.SourceFile.FileName.Trim();
            request.SourceFile.SaveAs(filePath);
            //Generate the thumnail
            ImageThumbnailGenerator.ResizeToFixedSize(filePath, albumId);
        }

       context.Response.ContentType = "text/json";
        context.Response.Write("Album got created Successfully!");
    }

    public bool IsReusable {
        get { return false; }
    }

}

public class AlbumDto {
    public string Name { get; set; }
    public string Description { get; set; }
    public int AlbumType { get; set; }
    public HttpPostedFile SourceFile { get; set; }
}
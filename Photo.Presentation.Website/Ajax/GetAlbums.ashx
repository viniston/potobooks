<%@ WebHandler Language="C#" Class="GetAlbums" %>

using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Photo.Business.Entities.Album;

public class GetAlbums : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string jsonResult = "";
        var list = AlbumController.GetAllAlbums();
        if (list != null && list.ToList().Any())
        {
            jsonResult = (new JavaScriptSerializer().Serialize(list));
        }
        context.Response.ContentType = "text/json";
        context.Response.Write(jsonResult);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}
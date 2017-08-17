using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Photo.Business.Utilities.Formatting;
using Photo.Business.Utilities.URL;
using Photo.Resources.PageLink;
using Photo.Resources.RegEx;
using Utility;

public class SEMHandler : IHttpHandler, IRequiresSessionState
{
    #region Private Members

    private string queryString;
    private bool isForHotelDetails = false;
    private string defaultPage = "default";

    #endregion


    #region Private Members

    public void ProcessRequest(HttpContext context)
    {
        queryString = PageLink.SEMPageQuerystring;
        string hotelIDString = "";

        if (!context.Request.Path.Contains("/") && !context.Request.Path.Contains("."))
            Utility.Utilities.RedirectToErrorPage();

        string[] urlPathArray = context.Request.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //UrlPathArray[0] == flights-from
        //UrlPathArray[1] == abu-dhabi-flights.auh.aspx

        string[] urlPageArray = urlPathArray[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        //UrlPageArray[0] = abu-dhabi-flights
        //UrlPageArray[1] = auh
        //UrlPageArray[2] = aspx

        string mode = urlPathArray[0].ToLower().Replace("-", "");
        queryString = queryString.Replace("[args]", urlPageArray[urlPageArray.Length - 2]);

        if (mode == SEMMode.Products.ToString().ToLower())
        {
            isForHotelDetails = true;
            queryString = context.Request.Url.Query.Trim('?');
            hotelIDString = FormatHelper.CleanUpNonNumericCharacters(
                Regex.Match(context.Request.Path, RegEx.ProductPropertyID, RegexOptions.IgnoreCase).Value);
            
        }
        else if (mode == SEMMode.Product.ToString().ToLower())
        {
            isForHotelDetails = true;
            queryString = context.Request.Url.Query.Trim('?');
            hotelIDString = FormatHelper.CleanUpNonNumericCharacters(
                Regex.Match(context.Request.Path, RegEx.ProductPropertyID, RegexOptions.IgnoreCase).Value);
        }
        else
        {
            Utility.Utilities.RedirectToErrorPage();
        }

        Page aspxHandler = new Page();

        #region Hotels

        if (isForHotelDetails)
        {
            int hotelId;
            if (int.TryParse(hotelIDString, out hotelId))
            {
                context.RewritePath(context.Request.Path, string.Empty, queryString);

                string hotelPropertyFriendlyURL = URLHelper.GetResolvedProductDetailsLink(hotelId);
                if (string.IsNullOrEmpty(hotelPropertyFriendlyURL))
                    Utility.Utilities.RedirectToErrorPage();

                if (context.Request.Url.AbsolutePath.ToLower() != hotelPropertyFriendlyURL.ToLower())
                {
                    context.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;

                    if (!string.IsNullOrEmpty(queryString))
                        hotelPropertyFriendlyURL += ("?" + queryString);

                    context.Response.AddHeader("Location", hotelPropertyFriendlyURL);
                }
                else
                {
                    aspxHandler = (Page)PageParser.GetCompiledPageInstance(
                        PageLink.ProductDetailsPage, context.Server.MapPath(PageLink.ProductDetailsPage), context);
                    aspxHandler.PreRenderComplete += new EventHandler(AspxPage_PreRenderComplete);
                    aspxHandler.ProcessRequest(context);
                }
            }
            else
                Utility.Utilities.RedirectToErrorPage();
        }

        #endregion


        else
        {
            context.RewritePath(context.Request.Path, string.Empty, queryString);

            string resolvedUrlPath = string.Empty;

            if (mode == SEMMode.Products.ToString().ToLower())
            {
                if (urlPageArray[0].ToLower() == defaultPage)
                {
                    resolvedUrlPath = context.Request.Path;
                }                
            }
            else
                Utility.Utilities.RedirectToErrorPage();

            if (string.IsNullOrEmpty(resolvedUrlPath))
                Utility.Utilities.RedirectToErrorPage();

            if (context.Request.Url.AbsolutePath.ToLower() != resolvedUrlPath.ToLower())
            {
                context.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                context.Response.AddHeader("Location", resolvedUrlPath);
            }
            else
            {
                aspxHandler = (Page)PageParser.GetCompiledPageInstance(PageLink.SEMPage, context.Server.MapPath(PageLink.SEMPage), context);
                aspxHandler.PreRenderComplete += new EventHandler(AspxPage_PreRenderComplete);
                aspxHandler.ProcessRequest(context);
            }
        }
    }

    #endregion


    #region Event Handlers

    void AspxPage_PreRenderComplete(object sender, EventArgs e)
    {
        if (isForHotelDetails)
            HttpContext.Current.RewritePath(HttpContext.Current.Request.Path, string.Empty, queryString);
        else
            HttpContext.Current.RewritePath(HttpContext.Current.Request.Path);
    }

    #endregion


    #region IHttpHandler property override

    public bool IsReusable { get { return false; } }

    #endregion
}
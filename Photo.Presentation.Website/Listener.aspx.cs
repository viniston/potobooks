using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Utility;

partial class Listener : System.Web.UI.Page
{
    private Target _targetEnvironment = (Target)Enum.Parse(typeof(Target), ConfigurationManager.AppSettings["Target"].ToString());

    protected void Page_Load(object sender, System.EventArgs e)
    {
        IPNVerification();

        PDTVerification();

    }

    private void PDTVerification()
    {
        // CUSTOMIZE THIS: This is the seller's Payment Data Transfer authorization token.
        // Replace this with the PDT token in "Website Payment Preferences" under your account.
        string authToken = "Dc7P6f0ZadXW-U1X8oxf8_vUK09EHBMD7_53IiTT-CfTpfzkN0nipFKUPYy";
        string txToken = Request.QueryString["tx"];
        string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

        //Post back to either sandbox or live
        string strUrl = _targetEnvironment == Target.Development ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);

        //Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = query.Length;


        //Send the request to PayPal and get the response
        StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        streamOut.Write(query);
        streamOut.Close();
        StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
        string strResponse = streamIn.ReadToEnd();
        streamIn.Close();

        Dictionary<string, string> results = new Dictionary<string, string>();
        if (strResponse != "")
        {
            StringReader reader = new StringReader(strResponse);
            string line = reader.ReadLine();

            if (line == "SUCCESS")
            {

                while ((line = reader.ReadLine()) != null)
                {
                    results.Add(line.Split('=')[0], line.Split('=')[1]);

                }
                Response.Write("<p><h3>Your order has been received.</h3></p>");
                Response.Write("<b>Details</b><br>");
                Response.Write("<li>Name: " + results["first_name"] + " " + results["last_name"] + "</li>");
                Response.Write("<li>Item: " + results["item_name"] + "</li>");
                Response.Write("<li>Amount: " + results["payment_gross"] + "</li>");
                Response.Write("<hr>");
            }
            else if (line == "FAIL")
            {
                // Log for manual investigation
                Response.Write("Unable to retrive transaction detail");
            }
        }
        else
        {
            //unknown error
            Response.Write("ERROR");
        }
    }

    private void IPNVerification()
    {
        //Post back to either sandbox or live
        string strUrl = _targetEnvironment == Target.Development ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strUrl);

        //Set values for the request back
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        byte[] Param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
        string strRequest = Encoding.ASCII.GetString(Param);
        strRequest = strRequest + "&cmd=_notify-validate";
        req.ContentLength = strRequest.Length;

        //for proxy
        //Dim proxy As New WebProxy(New System.Uri("http://url:port#"))
        //req.Proxy = proxy

        //Send the request to PayPal and get the response
        StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
        streamOut.Write(strRequest);
        streamOut.Close();
        StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
        string strResponse = streamIn.ReadToEnd();
        streamIn.Close();

        if (strResponse == "VERIFIED")
        {
            //check the payment_status is Completed
            //check that txn_id has not been previously processed
            //check that receiver_email is your Primary PayPal email
            //check that payment_amount/payment_currency are correct
            //process payment
        }
        else if (strResponse == "INVALID")
        {
            //log for manual investigation
        }
        else
        {
            //Response wasn't VERIFIED or INVALID, log for manual investigation
        }
    }
}


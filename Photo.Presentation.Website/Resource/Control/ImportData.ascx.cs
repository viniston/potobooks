using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

public partial class Core_Resource_Control_ImportData : UserControl
{
	public object _dataList;

	public void InitControl<T>(List<T> dataList)
	{
		_dataList = (List<T>)dataList;
	}

	protected void btnExport_Click(object sender, EventArgs e)
	{
		//DataTable dtData = Utilities.ConvertToDatatable(_dataList);
		DataTable dtData = new DataTable();
		try
		{
			if (ddlFile.SelectedValue == ".pdf")
			{
				PDFform pdfForm = new PDFform(dtData, "Dbo. Program", "Many");
				Document document = pdfForm.CreateDocument();
				PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
				renderer.Document = document;
				renderer.RenderDocument();

				MemoryStream stream = new MemoryStream();
				renderer.PdfDocument.Save(stream, false);

				Response.Clear();
				Response.ContentType = "application/" + ddlFile.SelectedItem.Text + ddlFile.SelectedValue;
				Response.AddHeader("content-disposition", "attachment;filename=" + "Report" + ddlFile.SelectedValue);
				Response.BinaryWrite(stream.ToArray());
				Response.Flush();
				Response.End();
			}
			else
			{
				Response.Clear();
				Response.Charset = "";
				Response.ContentEncoding = System.Text.Encoding.UTF8;
				Response.Cache.SetCacheability(HttpCacheability.NoCache);
				Response.ContentType = "application/" + ddlFile.SelectedItem.Text + ddlFile.SelectedValue;
				Response.AddHeader("content-disposition", "attachment;filename=" + "Report" + ddlFile.SelectedValue);

				System.IO.StringWriter sw = new System.IO.StringWriter();
				HtmlTextWriter htw = new HtmlTextWriter(sw);
				GridView GVExport = new GridView();
				GVExport.DataSource = Session["dvProgram"];
				GVExport.DataBind();
				GVExport.RenderControl(htw);

				Response.Write(sw);
				sw = null;
				htw = null;
				Response.Flush();
				Response.End();
			}
		}
		catch
		{
		}
	}
}
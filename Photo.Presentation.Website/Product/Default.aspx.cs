using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Photo.Business.Entities.Model;
using Photo.Business.Utilities.Formatting;
using Photo.Business.Utilities.Storage;
using Photo.Resources.RegEx;
using Photo.Utility.LogHelper;

namespace Photo.Presentation.Website.Core
{
    public partial class Default : CorePage.CorePage
    {
        public ProductInfo Product
        {
            get
            {
                string productIDString = FormatHelper.CleanUpNonNumericCharacters(Regex.Match(Request.Path, RegEx.ProductPropertyID, RegexOptions.IgnoreCase).Value);
                int productId;
                if (int.TryParse(productIDString, out productId))
                {
                    return ProductController.Instance.GetByID(productId);
                }
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string productIDString = FormatHelper.CleanUpNonNumericCharacters(
                Regex.Match(Request.Path, RegEx.ProductPropertyID, RegexOptions.IgnoreCase).Value);

            if (!IsPostBack)
            {
                ltlProductName.Text = Product != null ? Product.Name : "No product found";
                ltlHeadArea.Text = GetLocalResourceObject("HeaderHolder").ToString()
                                                    .Replace("[ImageName]", Product.ImagePath)
                                                    .Replace("[ImageToolTip]", Product.Name);
                lnkProductTitle.InnerText = ltlProductName.Text;
                ltlFeatures.Text = Product.Features;
                ltlDescription.Text = Product.Description;
                ltlAmount.Text = Product.Amount.ToString();

                repMainProductList.DataSource = ProductController.Instance.All.FindAll(item => item.IsActive == true).OrderBy(i => i.Amount);
                repMainProductList.DataBind();

            }
            else
            {
                HttpPostedFile file = Request.Files["uploadedImage"];
                bool hasValidUploadFile = file != null && file.ContentLength > 0;
                if (!hasValidUploadFile)
                    Response.Write("Please select a valid file to upload");

                bool hasValidEmail = !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtConfirmEmail.Text) &&
                    txtConfirmEmail.Text == txtEmail.Text && Photo.Utility.Validation.ValidationHelper.IsValidEmailAddress(txtEmail.Text.ToLower());

                if (!hasValidEmail)
                    Response.Write("Invalid EMail address");

                if (!hasValidUploadFile || !hasValidEmail)
                    return;

                Guid purchaseID = Guid.NewGuid();
                long dirID = DateTime.Now.Ticks;

                long productTxnId = 0;
                string filePath = string.Empty;
                try
                {
                    filePath = RepositoryHelper.GetWorkFileStaticContentStoragePath(DocumentType.WorkFileUpload, dirID);
                    string staticDirectoryPath = ConfigurationManager.AppSettings["DocumentStorageLocation"];
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    filePath += Path.DirectorySeparatorChar + Path.GetFileName(file.FileName);
                    file.SaveAs(filePath);
                }
                catch (Exception ex)
                {
                    LogHelper.Log(Logger.Application, LogLevel.Error, ex);
                    Response.Write("Message : " + ex.Message + "<br> Callstack : " + ex.StackTrace);
                }

                BookingInfo booking = new BookingInfo
                {
                    PurchaseID = purchaseID,
                    PaymentList = new List<PaymentInfo>
                    {
                        new PaymentInfo
                        {
                            PaymentMethodID = (short)PaymentMethod.PayPal,
                            PaymentStatusID = (short)PaymentStatus.Requested,
                            Amount = Product.Amount
                        }
                    },
                    Email = txtEmail.Text.Trim(),
                    Remarks = txtRemarks.InnerText,
                    IsActive = true,
                    ImageList = new List<ImageInfo>
                    {
                        new ImageInfo
                        {
                            BookingID = productTxnId,
                            Path = filePath,
                            TypeID = (int)ImageType.Original,
                            IsActive = true
                        }
                    }
                };

                long paymentId = 0;

                List<long> imageIds;
                productTxnId = BookingController.Save(booking, out paymentId, out imageIds);
                PaymentInfo p = PaymentController.Instance[paymentId];
                foreach (long imageId in imageIds)
                {
                    ImageInfo image = ImageController.Instance[imageId];
                    LogHelper.Log(Logger.Application, LogLevel.Info, "Image = " + image.Path);
                }
                Response.Redirect("~/Purchase.aspx?sid=" + purchaseID + "&ptid=" + productTxnId + "&p=" + p.ID);
            }
        }

        protected void repMainProductList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item ||
                e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                ProductInfo product = e.Item.DataItem as ProductInfo;
                Literal literal = (Literal)e.Item.FindControl("ltlProductGrid");
                literal.Text = GetLocalResourceObject("ProductGridHtml").ToString().
                                    Replace("[ProductUrlFriendlyName]", product.Name.Replace(" ", "-")).
                                    Replace("[ID]", product.ID.ToString()).
                                    Replace("[ProductImage]", product.ImagePath).
                                    Replace("[ProductTitle]", product.Title).
                                    Replace("[Amount]", product.Amount.ToString()).
                                    Replace("[GrossAmount]", product.GrossAmount.ToString());
            }
        }
    }
}
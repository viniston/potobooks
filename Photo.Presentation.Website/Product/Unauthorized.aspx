<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unauthorized.aspx.cs" MasterPageFile="~/Resource/Master/Home.master" Inherits="Photo.Presentation.Website.Core.Unauthorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
  <div class="row">
    <div>
      <div class="error-container">
        <br />
        <br />
        <br />
        <br />
        <h1>NOT AUTHORIZED!</h1>
        <h2>Sorry! You do not have permission to access this page.</h2>
        <div class="error-actions">
          <a class="btn btn-large btn-primary" href="../Business/Default.aspx">
            <i class="icon-chevron-left"></i>
            &nbsp;
						Back to home page						
          </a>
        </div>
      </div>
    </div>
  </div>
</asp:Content>

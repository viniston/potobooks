<%@ Page Title="" Language="C#" MasterPageFile="~/Resource/Master/Home.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
  <div class="row">
    <div class="span12">
      <div class="error-container">
        <br />
        <br />
        <br />
        <br />
        <h1>Opps! 404/500 :(</h1>
        <h2>Either there is an unexpected error or this page is not found.</h2>
        <div class="error-actions">
          <a class="btn btn-large btn-primary backButton">
            <i class="icon-chevron-left"></i>
            &nbsp;
						Back to previous page						
          </a>
        </div>
      </div>
    </div>
  </div>
  <script type="text/javascript">
    $('.backButton').click(function () { location.href = history.back(0); });
  </script>
</asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Resource/Master/Business.master" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<%@ Register Src="~/Resource/Control/SignUp.ascx" TagPrefix="uc1" TagName="SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <link href="Resource/Pages/Preview/css/login/login.css" rel="stylesheet" />
    <uc1:SignUp runat="server" ID="ucSignIn" />
</asp:Content>

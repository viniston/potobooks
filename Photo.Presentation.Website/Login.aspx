<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Resource/Master/Business.master" CodeFile="Login.aspx.cs" Inherits="SignIn" %>

<%@ Register Src="~/Resource/Control/SignIn.ascx" TagPrefix="uc1" TagName="SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <link href="Resource/Pages/Preview/css/login/login.css" rel="stylesheet" />
    <uc1:SignIn runat="server" ID="ucSignIn" />
</asp:Content>

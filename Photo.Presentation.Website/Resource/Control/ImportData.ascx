<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImportData.ascx.cs" Inherits="Core_Resource_Control_ImportData" %>
<div>
    <asp:DropDownList ID="ddlFile" Width="100px" runat="server" CssClass="input">
        <asp:ListItem Value=".pdf">Pdf</asp:ListItem>
        <asp:ListItem Value=".xls">Excel</asp:ListItem>
        <asp:ListItem Value=".doc">Word</asp:ListItem>
    </asp:DropDownList>
    <asp:Button Visible="True" ID="btnExport" Width="70px" runat="server" Text="Export" OnClick="btnExport_Click" CausesValidation="False"></asp:Button>
</div>

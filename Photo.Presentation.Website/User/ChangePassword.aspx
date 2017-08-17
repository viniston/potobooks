<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Core.User.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="subContent fullSize">
            <div id="contentPanel">
                <asp:Literal ID="ltlMessage" EnableViewState="false" runat="server"></asp:Literal>
                <ul class="form1" runat="server" id="ulChangePassword">
                    <li class="first">
                        <label>
                            <asp:Literal ID="litUserName" meta:resourcekey="litUserName" runat="server"></asp:Literal>
                        </label>
                        <fieldset>
                            <asp:Literal ID="litUser" runat="server"></asp:Literal>
                        </fieldset>
                    </li>
                    <li class='<asp:Literal ID="litCurrentPasswordClass" runat="server"/>'>
                        <label class="mandatory">
                            <asp:Literal ID="litCurrentPassword" meta:resourcekey="litCurrentPassword" runat="server"></asp:Literal>
                        </label>
                        <fieldset>
                            <asp:TextBox ID="txtCurrentPassword" TextMode="Password" class="text" runat="server"></asp:TextBox>
                        </fieldset>
                        <p>
                            <asp:Literal ID="litCurrentPasswordError" EnableViewState="false" runat="server"></asp:Literal>
                        </p>
                    </li>
                    <li class='<asp:Literal ID="litNewPasswordClass" runat="server"/>'>
                        <label class="mandatory">
                            <asp:Literal ID="litNewPassword" meta:resourcekey="litNewPassword" runat="server"></asp:Literal>
                        </label>
                        <fieldset>
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" class="text" runat="server"></asp:TextBox>
                        </fieldset>
                        <p>
                            <asp:Literal ID="litNewPasswordError" EnableViewState="false" runat="server"></asp:Literal>
                        </p>
                    </li>
                    <li class='<asp:Literal ID="litConfirmPasswordClass" runat="server"/>'>
                        <label class="mandatory">
                            <asp:Literal ID="litConfirmPassword" meta:resourcekey="litConfirmPassword" runat="server"></asp:Literal></label>
                        <fieldset>
                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" class="text" runat="server"></asp:TextBox>
                        </fieldset>
                        <p>
                            <asp:Literal ID="litConfirmPasswordError" EnableViewState="false" runat="server"></asp:Literal>
                        </p>
                    </li>
                    <li class="action">
                        <asp:Button ID="btnChangePassword" meta:resourcekey="btnChangePassword" runat="server" class="submit button" OnClick="btnChangePassword_Click" />
                    </li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>

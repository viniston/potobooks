<%@ Page Title="" Language="C#" MasterPageFile="~/Resource/Master/Admin.master" AutoEventWireup="true" CodeFile="AddEdit.aspx.cs" Inherits="User.Core_User_AddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="Server">
    <script>
        var code = '<%=UserName%>';
        if (code) {
            $(".page-title h2").append(' (<%=UserName %>)');
    }
    </script>
    <div class="widget-content">
        <div class="widget-action">
            <div class="backToPage">
                <asp:Literal runat="server" ID="ltlBackLink" meta:resourcekey="ltlBackLink"></asp:Literal>
            </div>
            <div class="hidden" runat="server" id="divMessage">
            </div>
        </div>
        <div class="form-horizontal">
            <fieldset>
                <div class="control-group">
                    <div>
                        Email address
                    </div>
                    <div>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="red" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="red" SetFocusOnError="True" ControlToValidate="txtEmail" runat="server" ErrorMessage="The email is not valid." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="control-group empty">
                </div>
                <div class="control-group empty">
                </div>
                <div class="control-group">
                    <div>
                        First name 
                    </div>
                    <div>
                        <asp:TextBox ID="txtFirstNameEN" runat="server" CssClass="input"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator2" SetFocusOnError="True" ControlToValidate="txtFirstNameEN" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="control-group">
                    <div>
                        Last name 
                    </div>
                    <div>
                        <asp:TextBox ID="txtLastNameEN" runat="server" CssClass="input"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ForeColor="red" ID="RequiredFieldValidator4" SetFocusOnError="True" ControlToValidate="txtLastNameEN" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="control-group empty">
                </div>
                <div class="control-group">
                    <div>
                        User role
                    </div>
                    <div  class="radio">
                        <asp:CheckBoxList ID="chkRoleList" runat="server" RepeatLayout="Flow">
                        </asp:CheckBoxList>
                        <asp:CustomValidator ID="RequiredFieldValidator6" ForeColor="red" ClientValidationFunction="ValidateCheckBoxList" runat="server" ErrorMessage="Please select at least one role" Display="Dynamic"></asp:CustomValidator>
                    </div>
                </div>
                <div class="form-actions">
                    <asp:Button ID="btnSave" Text="Save Changes" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-secondary" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button>

                </div>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkRoleList.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
    }
    </script>
</asp:Content>



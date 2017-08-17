<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" MasterPageFile="~/Resource/Master/Admin.master" Inherits="Core.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
   <script>
         $(".page-title h2");
  </script>
  <div class="widget-content">
    <div class="widget-action">
    </div>
    <div class="hidden" runat="server" id="divMessage"></div>
    <div class="form-horizontal" runat="server" id="divForm">
      <fieldset>
        <div class="control-group">
          <label class="control-label">
            User name
          </label>
          <div class="controls">
            <label class="control-label">
              <asp:Label ID="lblUserName" runat="server" CssClass="input"></asp:Label></label>
          </div>
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group">
          <div>
            Current password
          </div>
          <div>
            <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="red" ID="rfvNameEN" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic" ControlToValidate="txtCurrentPassword"></asp:RequiredFieldValidator>
          </div>
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group">
          <div>
            New password
          </div>
          <div>
            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="red" ID="RequiredFieldValidator1" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
          </div>
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group empty">
        </div>
        <div class="control-group">
          <div>
            Confirm password
          </div>
          <div>
            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator SetFocusOnError="True" ForeColor="red" ID="RequiredFieldValidator2" runat="server" ErrorMessage="You can't leave this empty." Display="Dynamic" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
            <asp:CompareValidator ControlToCompare="txtNewPassword" ForeColor="red" ControlToValidate="txtConfirmPassword" ID="CompareValidator2" runat="server" Display="Dynamic" ErrorMessage="The passwords do not match."></asp:CompareValidator>
          </div>
        </div>
        <div class="form-actions">
          <asp:Button ID="btnSave" Text="Save Changes" runat="server" CssClass="btn btn-primary" OnClick="btnChangePassword_Click"></asp:Button>
          <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-secondary" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button>
        </div>
      </fieldset>
    </div>
  </div>
</asp:Content>

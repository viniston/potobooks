<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subscription.aspx.cs" Inherits="Core_Subscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <ul>
        <li>
          <div>First name</div>
          <div><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></div>
        </li>
        <li>
          <div>Last name</div>
          <div><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></div>
        </li>        
        <li>
          <div>Email</div>
          <div><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></div>
        </li>        
        <li>
          <div>
            <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe" OnClick="btnSubscribe_Click" />
          </div>
        </li>
      </ul>
    </div>
    </form>
</body>
</html>

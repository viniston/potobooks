<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoreMenu.ascx.cs" Inherits="CoreMenu" %>

<div id="sidebar-wrapper">
  <div class="subnavbar">
    <div class="subnavbar-inner">
      <div class="container">
        <ul class="mainnav">
          <li class="dropdown subnavbar-open-right" runat="server" id="liSubscriptionMgmnt">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="icon-list-alt"></i>
              <span runat="server" id="spanSubscription" class="spanSubscription">Subscription Management</span>
              <b class="caret"></b>
            </a>
            <ul class="dropdown-menu">
              <li><a href="../../../Core/Subscription/Default.aspx" class="subscription">Subscription</a></li>              
            </ul>
          </li>
          <li class="dropdown subnavbar-open-right">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              <i class="icon-user"></i>
              <span class="spanAccnt">Account</span>
              <b class="caret"></b>
            </a>
            <ul class="dropdown-menu">
              <li runat="server" id="liUser"><a href="../../../Core/User/Default.aspx" class="users">Users</a></li>
              <li runat="server" id="liChangePassword"><a href="../../Core/ChangePassword.aspx" class="changepassword">Change password</a></li>
              <li><a href="../../../Core/SignOut.aspx" class="logout">Logout</a></li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </div>
</div>

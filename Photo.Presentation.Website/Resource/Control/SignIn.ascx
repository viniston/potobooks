<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SignIn.ascx.cs" Inherits="Resource.Control.SignIn" %>
<div class="home-login-container">
    <h2 class="title1 home-login-title">Sign In</h2>
    <div id="login-form-container">
        <form id="login-form" class="form" role="form" method="POST">
            <div class="login-body-content">
                <div class="bad-cred-alert alert alert-warning" runat="server" id="divMessage" visible="False">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Literal ID="ltlMessage" EnableViewState="false" runat="server" />
                </div>
                <div class="login-view">
                    <div class="default-view">
                        <div class="form-group login-email-section">
                            <label class="login-form-label legacy-form-label-block" for="txtEmailAddress">User Name</label>
                            <input type="text" id="txtEmailAddress" name="username" value="" placeholder="Username" class="form-control login-input-field" autofocus="" runat="server" />
                        </div>
                        <div class="form-group login-password-section">
                            <label class="login-form-label legacy-form-label-block" for="txtCKPassword">Password</label>
                            <input type="password" id="txtCKPassword" name="password" value="" placeholder="Password" class="form-control login-input-field" runat="server" />
                        </div>
                        <div class="remember-me checkbox">
                            <label class="login-form-label" for="chkRememberMe">
                                <input id="chkRememberMe" name="Field" type="checkbox" value="remember" class="on-click-remember-me" style="float: none;" runat="server" />
                                Remember me</label>
                        </div>
                    </div>
                </div>
            </div>
            <div style="margin-top: 15px;">
                <div class="login-view">
                    <div class="default-view">
                        <asp:Button type="submit" class="btn btn-primary btn-login" runat="server" ID="btnSignIn" OnClick="btnSignIn_Click" Text="Sign In" />
                        <asp:HyperLink class="de-link read-terms-and-conditions-link" ID="CreateUserLink" runat="server"
                            NavigateUrl="~/SignUp.aspx">Not registered yet? 
                                        Create an account!</asp:HyperLink>
                        <a href="#" class="de-link forgot-password-link">Forgot your password?</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>

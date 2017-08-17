<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SignUp.ascx.cs" Inherits="Resource.Control.SignUp" %>
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
                            <label class="login-form-label legacy-form-label-block" for="txtFirstName">First Name</label>
                            <input type="text" id="txtFirstName" name="username" value="" placeholder="First Name" class="form-control login-input-field" autofocus="" runat="server" />
                        </div>
                        <div class="form-group login-email-section">
                            <label class="login-form-label legacy-form-label-block" for="txtLastName">Last Name</label>
                            <input type="text" id="txtLastName" name="username" value="" placeholder="Last Name" class="form-control login-input-field" autofocus="" runat="server" />
                        </div>
                        <div class="form-group login-email-section">
                            <label class="login-form-label legacy-form-label-block" for="txtUserName">User Name</label>
                            <input type="email" id="txtUserName" name="username" value="" placeholder="User Name" class="form-control login-input-field" autofocus="" runat="server" />
                        </div>
                        <div class="form-group login-password-section">
                            <label class="login-form-label legacy-form-label-block" for="txtCKPassword">Password</label>
                            <input type="password" id="txtCKPassword" name="password" value="" placeholder="Password" class="form-control login-input-field" runat="server" />
                        </div>
                        <div class="form-group login-password-section">
                            <label class="login-form-label legacy-form-label-block" for="txtCKConfirmPassword">Confirm Password</label>
                            <input type="password" id="txtCKConfirmPassword" name="password" value="" placeholder="Password" class="form-control login-input-field" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="margin-top: 15px;">
                <div class="login-view">
                    <div class="default-view">
                        <asp:Button type="submit" class="btn btn-primary btn-login" runat="server" ID="btnSignUp" OnClick="btnSignUp_Click" Text="Sign Up" />
                        <asp:HyperLink class="de-link read-terms-and-conditions-link" ID="CreateUserLink" runat="server"
                            NavigateUrl="~/Login.aspx">Already having an account? 
                                        Sign In!</asp:HyperLink>
                        <a href="#" class="de-link forgot-password-link">Forgot your password?</a>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>

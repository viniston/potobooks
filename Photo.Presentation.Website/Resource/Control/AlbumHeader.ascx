<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlbumHeader.ascx.cs" Inherits="Resource.Control.AlbumHeader" %>
<header class="marketplaceHeader" role="banner">
    <div class="row">
        <div class="marketplaceHeader__wrapper">
            <a href="#" class="marketplaceHeader__logoWrapper"></a>
            <nav class="navbar" role="navigation">
                <ul class="navigationMenu__items">
                    <li class="navigationMenuDropdown navigationMenu__item">
                        <a class="navigationMenuDropdown__label navigationMenu__label" href="#">Menu 1</a>
                        <div class="navigationMenuDropdown__content">
                            <ul class="navigationMenuDropdown__scrollInner">
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 1</a></li>
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 2</a></li>
                            </ul>
                        </div>
                    </li>
                    <li class="navigationMenuDropdown navigationMenu__item">
                        <a class="navigationMenuDropdown__label navigationMenu__label" href="#">Menu 2</a>
                        <div class="navigationMenuDropdown__content">
                            <ul class="navigationMenuDropdown__scrollInner">
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 1</a></li>
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 2</a></li>
                            </ul>
                        </div>
                    </li>
                    <li class="navigationMenuDropdown navigationMenu__item">
                        <a class="navigationMenuDropdown__label navigationMenu__label" href="#">Menu 3</a>
                        <div class="navigationMenuDropdown__content">
                            <ul class="navigationMenuDropdown__scrollInner">
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 1</a></li>
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Sub Menu 2</a></li>
                            </ul>
                        </div>
                    </li>
                    <li class="navigationMenuDropdown navigationMenu__item">
                        <asp:LinkButton class="navigationMenuDropdown__label navigationMenu__label" ID="userName" Style="font-weight: bold;" href="#" runat="server"></asp:LinkButton>
                        <div class="navigationMenuDropdown__content">
                            <ul class="navigationMenuDropdown__scrollInner">
                                <li class="navigationMenuDropdown__item"><a class="navigationMenuDropdown__button" href="#">Settings</a></li>
                                <li class="navigationMenuDropdown__item">
                                    <asp:LinkButton ID="btnSignOut" OnClick="btnSignOut_Click" class="navigationMenuDropdown__button" runat="server">Logout</asp:LinkButton></li>
                            </ul>
                        </div>
                    </li>
                </ul>
                <div class="navigationMenuMobile__button">Menu</div>
                <div class="navigationMenuMobile__items">
                    <div class="navigationMenuMobile__wrapper">
                        <ul class="navigationMenuMobile__content navigationMenuMobile__content--topLevel">
                            <li class="navigationMenuMobile__contentItem">
                                <a class="navigationMenuMobile__label navigationMenuMobile__label--hasChildren" href="#">Menu 1</a>
                                <ul class="navigationMenuMobile__content">
                                    <li class="navigationMenuMobile__contentItem"><a href="#" class="navigationMenuMobile__label navigationMenuMobile__label--goUp">Tools</a></li>
                                    <li class="navigationMenuMobile__contentItem"><a class="navigationMenuMobile__label" href="#">Sub Menu 1</a></li>
                                    <li class="navigationMenuMobile__contentItem"><a class="navigationMenuMobile__label" href="#">Sub Menu 2</a></li>
                                </ul>
                            </li>
                            <li class="navigationMenuMobile__contentItem">
                                <a class="navigationMenuMobile__label navigationMenuMobile__label--hasChildren" href="#">Menu 2</a>
                                <ul class="navigationMenuMobile__content">
                                    <li class="navigationMenuMobile__contentItem"><a href="#" class="navigationMenuMobile__label navigationMenuMobile__label--goUp">Sub Menu 1</a></li>
                                    <li class="navigationMenuMobile__contentItem"><a class="navigationMenuMobile__label" href="#">Sub Menu 2</a></li>
                                </ul>
                            </li>
                            <li class="navigationMenuMobile__contentItem">
                                <a class="navigationMenuMobile__label navigationMenuMobile__label--hasChildren" href="#">John Doe</a>
                                <ul class="navigationMenuMobile__content">
                                    <li class="navigationMenuMobile__contentItem"><a href="#" class="navigationMenuMobile__label navigationMenuMobile__label--goUp">Settings</a></li>
                                    <li class="navigationMenuMobile__contentItem"><a class="navigationMenuMobile__label" href="#">Logout</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="navigationMenuMobile__overlay"></div>
            </nav>
        </div>
    </div>
</header>

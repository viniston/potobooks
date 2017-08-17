<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Resource_Pages_Preview_Default" %>

<!DOCTYPE HTML>
<html>

<head>
  <title>I have been Yellowed&hellip;!!</title>
  <link rel="icon" href="images/favicon.png" type="image/png" sizes="16x16">
  <meta http-equiv="content-type" content="text/html; charset=utf-8" />
  <meta name="keywords" content="caricature, caricatures, gift, order, photo, yellow me, yellows avatar, yellows character generator, yellow avatar, make a yellows character, make me a yellows character, yellow character generator, create a yellow character, yellows pic, make a yellow character, make me a yellow character, make yellows character,cartoon gift, search results, search photos, download photos, community, stock photography community, download images, stock photos, stock images, stock photography, free images, royalty free images, cheap images, free stock images, free stock photos, photos, photography, dreamstime, photo search,CartoonKart - Create your personalised caricature, Caricatures from photos, Order your caricature today, create caricature, create cartoon from photo,  caricature body templates,gift cartoon, stock photos, stock pictures, stock images, stock photography, royalty free images,Shop for the perfect Cartoon gift from our wide selection of designs, or create personalized Cartoon gifts that impress. 24hr shipping on most orders!">
  <meta name="description" content="Cartoonkart is no.1 online best printing store in USA. You can buy cheapest t shirt printing and personalised mug printing online etc. CartoonKart - Create your personalised caricature, Caricatures from photos, Order your caricature today, create caricature, create cartoon from photo,  caricature body templates,gift cartoon, stock photos, stock pictures, stock images, stock photography, royalty free images,Shop for the perfect Cartoon gift from our wide selection of designs, or create personalized Cartoon gifts that impress. 24hr shipping on most orders!">
  <meta name="author" content="CartoonKart">
  <link href="http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600" rel="stylesheet" type="text/css" />
  <!--[if lte IE 8]><script src="js/html5shiv.js"></script><![endif]-->
  <script src="js/jquery.min.js"></script>
  <script src="js/jquery.dropotron.min.js"></script>
  <script src="js/skel.min.js"></script>
  <script src="js/init.js"></script>
</head>
<body class="homepage">
  <form runat="server" id="form1">
    <div id="logoWrap">
      <a href="http://www.cartoonkart.com">
        <img src="images/logo.png" alt="" /></a>
    </div>
    <!-- Header -->
    <asp:Literal ID="ltlPreviewImage" runat="server" meta:resourcekey="ltlPreviewImage"></asp:Literal>
    <!-- Main -->
    <div class="wrapper style2">
      <!-- Go to www.addthis.com/dashboard to customize your tools -->
      <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-52fe21b324037d55"></script>
      <article id="main" class="container special">
        <header>
          <asp:Literal ID="ltlTitle" runat="server" meta:resourcekey="ltlTitle"></asp:Literal>
          <span class="byline">IM avatars and social networking sites’ profile photos, Birthday gift, Wedding gift, Valentines day Gift, Anniversary gift for you
          </span>
        </header>
        <p>
          Personally for me, caricatures should bring a smile to a person’s face. If I am able to achieve that, I will consider my job done. I don’t believe in automated caricatures because they do not do any justice to the art. These images lack the depth and personal touch of an artist. We, at CartoonKart, believe in providing caricatures that are drawn by hand and you will definitely be able to tell the difference!
        </p>
        <footer>
          <asp:LinkButton ID="lnkDownloadJPG" runat="server" CssClass="button" OnClick="lnkDownloadJPG_Click">Download JPG</asp:LinkButton>
          <asp:LinkButton ID="lnkDownloadPNG" runat="server" CssClass="button" OnClick="lnkDownloadPNG_Click">Download PNG</asp:LinkButton>          
        </footer>
      </article>
    </div>

    <div id="banner">
      <h2>Purchase more..!! <strong>Save up to 55%..</strong></h2>
      <span class="byline">Personalised Caricature (from £12.99) via <a href="https://www.groupon.co.uk/deals/cartoonkart-8" style="color: #53a318">@Groupon</a>
      </span>
    </div>
    <!-- Carousel -->
    <div class="carousel">
      <div class="reel">
        <article>
          <a href="http://www.cartoonkart.com" class="image featured">
            <img src="images/pic01.jpg" alt="" /></a>
          <header>
            <h3><a href="#">Head and shoulders</a></h3>
          </header>
          <p>This caricature is for a single person which will include your face up to the shoulders&hellip;</p>
        </article>
        <article>
          <a href="http://www.cartoonkart.com" class="image featured">
            <img src="images/pic02.jpg" alt="" /></a>
          <header>
            <h3><a href="#">Group caricature – Head and shoulders</a></h3>
          </header>
          <p>This caricature includes two or more people in a single image&hellip;</p>
        </article>
        <article>
          <a href="http://www.cartoonkart.com" class="image featured">
            <img src="images/pic04.jpg" alt="" /></a>
          <header>
            <h3><a href="#">Full body</a></h3>
          </header>
          <p>This caricature is for a single person which will include the full body&hellip;</p>
        </article>
        <article>
          <a href="http://www.cartoonkart.com" class="image featured">
            <img src="images/pic07.jpg" alt="" /></a>
          <header>
            <h3><a href="#">Group caricature – Full body with couch</a></h3>
          </header>
          <p>This caricature includes two or more people in a single image&hellip;</p>
        </article>
        <article>
          <a href="http://www.cartoonkart.com" class="image featured">
            <img src="images/pic06.jpg" alt="" /></a>
          <header>
            <h3><a href="#">Posters, photo frames, desktop screensaver and much more</a></h3>
          </header>
          <p>From personal diaries and scrapbooks to posters and photo frames, etc&hellip;</p>
        </article>
      </div>
    </div>
    <!-- Footer -->
    <div id="footer">
      <div class="container">
        <div class="row">
          <div class="12u">
            <!-- Contact -->
            <section class="contact">
              <header>
                <h3>Share with your friend&hellip;</h3>
              </header>
              <p>Facebook, Twitter, Instagram, Pinterest, Google Plus, Linked…ermm maybe not that one! &#9786; Add your cute avatar on all your social networking sites and beam with joy as you see your friends getting green with jealousy!</p>
              <ul class="icons">
                <li><a href="https://twitter.com/Cartooonkart" class="fa fa-twitter solo"><span>Twitter</span></a></li>
                <li><a href="https://www.facebook.com/pages/Cartoonkart/638472049533120?ref=br_tf" class="fa fa-facebook solo"><span>Facebook</span></a></li>
                <li><a href="https://plus.google.com/110939723127054857510/posts" class="fa fa-google-plus solo"><span>Google+</span></a></li>
                <li><a href="http://www.pinterest.com/cartoonkart/" class="fa fa-pinterest solo"><span>Pinterest</span></a></li>
                <li><a href="https://www.instagram.com/caricature_from_photo/" class="fa fa-instagram solo"><span>Instagram</span></a></li>
              </ul>
            </section>
            <!-- Copyright -->
            <div class="copyright">
              <ul class="menu">
                <li>&copy; CartoonKart. All rights reserved.</li>
                <li>Contact: <a href="mailto:support@cartoonkart.com">support@cartoonkart.com</a></li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  </form>
</body>

</html>

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/Resource/Master/Album.master" CodeFile="Default.aspx.cs" Inherits="Business.BusinessDefault" %>

<%@ Register Src="~/Resource/Control/AlbumHeader.ascx" TagPrefix="uc1" TagName="SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <uc1:SignIn runat="server" ID="ucSignIn" />
    <input type="hidden" id="apiUrl" runat="server" />
    <div id="app" class="row">
        <main id="main" role="main">
            <div id="grid" class="gridHomepage loaded" style="position: relative; height: 4127.9px;">

                <figure class="mediaCard gridItem" style="position: absolute; left: 298px; top: 0px;">
                    <div class="gridItemInner" style="background-color: lightgrey !important;">
                        <a href="#" class="thumbnailGridItem cardThumbnail addAlbum" style="padding-top: 77.0909090909091%">
                            <div class="articleListWrapper__imageCaption">
                                <i class="icon-plus-sign icon-6" style="font-size: 3em;"></i>
                            </div>
                        </a>
                        <div class="infoGridItem">
                            <h2 class="titleGridItem">
                                <a href="#" class="cardTitle addAlbum">Add new</a>
                            </h2>
                            <p class="categoryGridItem">
                                <a href="#" class="cardCategory">Album</a>
                            </p>
                        </div>
                    </div>
                </figure>

                <div class="dynamic-grid">
                </div>

            </div>
        </main>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="vertical-alignment-helper">
            <div class="modal-dialog vertical-align-center">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myModalLabel">Add new album</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-5 col-md-offset-3">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="sharedDesignsEmptyMessage">
                                            <div class="sharedDesignsEmptyMessage__inner">
                                                <img src="../Resource/Images/shared-empty-graphic.svg" class="sharedDesignsEmptyMessage__imageContainer selected-template" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="text-center">
                                            <form id="album-creation-form" role="form" autocomplete="off" class="form" method="post">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <label for="file_import">Select Preview Image :</label>
                                                        <input type="file" style="width: 95%;" accept="image/gif, image/jpg, image/jpeg, image/png" class="form-control" name="file_import" id="file_import" />
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="input-group">
                                                        <label for="albumtitle">Album Title:</label>
                                                        <input style="width: 95%;" type="text"
                                                            id="albumtitle" name="displaydata" placeholder="album title" class="form-control">
                                                    </div>
                                                    <div class="input-group">
                                                        <label for="albumdescription">Album Description:</label>
                                                        <input style="width: 95%;" type="text"
                                                            id="albumdescription" placeholder="Album description" name="albumdescription" class="form-control">
                                                    </div>
                                                    <div class="input-group">
                                                        <label for="softcover" class="checkboxlabel">Soft Cover Book</label>
                                                        <input type="radio" class="checkbox checkbboxinput" id="softcover" name="covertype" value="1">
                                                        <div class="clear"></div>
                                                    </div>
                                                    <div class="input-group">
                                                        <label for="hardcover" class="checkboxlabel">Hard Cover Book</label>
                                                        <input type="radio" class="checkbox checkbboxinput" id="hardcover" name="covertype" value="2">
                                                        <div class="clear"></div>
                                                    </div>
                                                </div>

                                            </form>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="upload-album" class="btn btn-primary">Save</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        $("#file_import").change(function () {
            readURL(this);
        });

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('.selected-template').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }

        $(".addAlbum").click(function () {
            $("#myModal").modal("show");
        });

        $('#upload-album')
            .click(function () {
                var model = new FormData();
                model.append('Name', $("#albumtitle").val());
                model.append('Description', $("#albumdescription").val());
                model.append('AlbumType', parseInt($('input[name=covertype]:checked').val(), 9));
                model.append('SourceFile', $('#file_import')[0].files[0]);

                $.ajax({
                			url: "http://127.0.0.50/Ajax/UploadAlbum.ashx",
                    type: 'POST',
                    dataType: 'json',
                    data: model,
                    processData: false,
                    contentType: false,
                    success: function (result) {
                        $("#myModal").modal("hide");
                        $("form#album-creation-form")[0].reset();
                        $.notify("Album created successfully", "success");
                        window.location = "Business/Default.aspx";
                    },
                    error: function (err) {
                        $("#myModal").modal("hide");
                        $("form")[0].reset();
                        $.notify("Album not created", "warn");
                        window.location = "Default.aspx";
                    }
                });
            });

        function getAlbums() {
            var self = this;


            var albumTemplate = '{{#albums}}<figure class="mediaCard gridItem">' +
            '            <div class="gridItemInner">' +
            '                <a href="#" class="thumbnailGridItem cardThumbnail" data-label="Edit this template" style="padding-top: 77.0909090909091%">' +
            '                    <img src="http://127.0.0.50/uploads/data/ImageThumbnails/{{AlbumId}}_100x100.png" alt="image path">' +
            '                </a>' +
            '                <div class="infoGridItem">' +
            '                    <h2 class="titleGridItem">' +
            '                        <a href="#" class="cardTitle">{{AlbumName}}</a>' +
            '                    </h2>' +
            '                    <p class="categoryGridItem">' +
            '                        <a href="#" class="cardCategory">Album</a>' +
            '                    </p>' +
            '                </div>' +
            '            </div>' +
            '        </figure>{{/albums}}';



            $.ajax({
            				url: "http://127.0.0.50/Ajax/GetAlbums.ashx",
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                    if (data != null && data.length > 0) {
                        var albums = { 'albums': data };
                        var htm = Mustache.render(albumTemplate, albums);
                        $('.dynamic-grid').html(htm);
                        setTimeout(responsiveManager.makeResponsiveLayout, 200);
                    }
                },
                error: function () {
                    setTimeout(responsiveManager.makeResponsiveLayout, 200);
                }
            });
        }

        getAlbums();

    </script>
</asp:Content>

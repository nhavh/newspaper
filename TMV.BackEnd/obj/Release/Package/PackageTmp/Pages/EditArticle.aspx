<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="TMV.BackEnd.Pages.EditArticle" %>
<%@ Register src="../Controls/TextEditor.ascx" tagname="TextEditor" tagprefix="uc1" %>

<asp:content id="Content1" contentplaceholderid="placeHolderContent" runat="server">
    <div id="toolbar-box">
		<div class="t">
			<div class="t">
				<div class="t"></div>
			</div>
		</div>
		<div class="m">
		    <div id="toolbar" class="toolbar">
			    <table class="toolbar">
			        <tbody>
					    <tr>    
					        <td id="qa" class="button" runat="server">
							    <a href="/Pages/ListCustomerReview.aspx?xml=CustomerReview&ArticleId=<%=Request.QueryString["ArticleId"] %>" class="modal">
								    <span title="Preview" class="icon-32-preview"></span>ĐÁNH GIÁ
							    </a>
						    </td>
                            <td id="review" class="button" runat="server">
							    <a href="/Pages/ListQuestionAnswer.aspx?xml=QuestionAnswer&ArticleId=<%=Request.QueryString["ArticleId"] %>" class="modal">
								    <span title="Preview" class="icon-32-preview"></span>HỎI ĐÁP
							    </a>
						    </td>
						    <td id="toolbar-save" class="button">
						        <asp:LinkButton ID="lbtSave" runat="server" Text="" onclick="lbtSave_Click" CssClass="toolbar">
					                <span id="Span1" title="Save" class="icon-32-save" runat="server"></span>Lưu
					            </asp:LinkButton>
						    </td>
						    <td id="toolbar-cancel" class="button">
						        <asp:LinkButton ID="lbtClose" runat="server" Text="" onclick="lbtClose_Click" 
                                    CssClass="toolbar" CausesValidation="False">
					                <span id="Span2" title="Close" class="icon-32-cancel" runat="server"></span>Thoát
					            </asp:LinkButton>
						    </td>
					    </tr>
				    </tbody>
			    </table>
		    </div>
			<div class="header">Thêm mới/chỉnh sửa tin tức</div>
			<div class="clr"></div>
		</div>
		<div class="b">
			<div class="b">
				<div class="b"></div>
			</div>
		</div>
	</div>
	<div id="element-box">
		<div class="t">
			<div class="t">
				<div class="t"></div>
			</div>
		</div>
		<div class="m" style="float:left; width:100%">
			<div class="admin-left">
                <table class="adminform">
                    <tr>
						<td colspan="3">
                            <div id="ErorMessage" style="color: Red;">
                            </div>
                        </td>
					</tr>
                    
                    <tr>
                        <td style="width:20%;">Tiêu đề</td>
                        <td colspan="2"><asp:TextBox id="txtTitle" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:20%;">Tiêu đề</td>
                        <td colspan="2"><asp:TextBox id="txtSlug" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
						<td>Hình&nbsp;đại&nbsp;diện(230x143)</td>
						<td>
                            <div id="divThumbnailButtonAvatarPlaceholder"></div>
                            <div id="divThumbnailProgressAvatarContainer"></div>
                            <div id="thumbnailPreviewAvatar"><%=ThumbnailPreviewAvatar%></div>                                      
                            <input type="hidden" name="thumbnailSrcAvatar" id="thumbnailSrcAvatar" value='<%= ThumbnailSrcAvatar %>' />    											
						</td>
						<td><input type="button" onclick="javascript: RemoveThumbnail(this, 'thumbnailPreviewAvatar', 'thumbnailSrcAvatar');" value="Remove" id="btnRemoveThumbnailAvatar" style='visibility:<% = (ThumbnailPreviewAvatar == "" ? "hidden" : "visible") %>' /></td>
					</tr>
                    <tr>
                        <td>Mô tả: </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="400px" Height="100px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>Tags: </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtTags" runat="server" TextMode="MultiLine" Width="400px" Height="100px"/>
                        </td>
                    </tr>                                                 
                </table>  
            </div>  
            <div class="admin-right">
                <table>
                    <tr>
                        <td style="width:35%;">Chỉ&nbsp;hiển&nbsp;thị&nbsp;bài&nbsp;giới&nbsp;thiệu</td>
                        <td><asp:CheckBox id="cbIsTab" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>Chuyên mục</td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="200px"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Ngày hiển thị: </td>
                        <td>
                            <input type="text" id="dtePublishDate" readonly="readonly" runat="server" />
                            <select id="ddlPublishHours" runat="server"></select> : <select id="ddlPublishMinute" runat="server"></select>
                        </td>
                    </tr>
                    <tr id="transferto" runat="server">
                        <td>Chuyển đến: </td>
                        <td valign="middle">
                            <table>
						        <tr>
						            <td style="width:10%"><asp:DropDownList ID="ddlRole" runat="server" DataTextField="RoleName" DataValueField="RoleId" /></td>
						            <td style="text-align:left"><asp:ImageButton ID="btnTransferTo" runat="server"  Text="Chuyển" ImageUrl="~/images/j_button1_next.png" onclick="btnTransferTo_Click" /></td>
                                    <asp:HiddenField ID="hdCurrentRole" runat="server" Value="-1" />
						        </tr>
						    </table>
                        </td>
                    </tr>
                    <tr id="returnto" style="display:none;" runat="server">
                        <td>Thông báo: </td>
                        <td>
                            <table>
						        <tr>
						            <td valign="middle">
						                <asp:ImageButton ID="btnReturn" runat="server"  Text="Chuyển" ImageUrl="~/images/j_button1_prev.png" onclick="btnReturn_Click"/>
						            </td>
						            <td style="width:90%">
						                <asp:TextBox ID="txtAdminNote" runat="server" TextMode="MultiLine" Width="350px" Height="100px"></asp:TextBox>
						            </td>
						        </tr>
						    </table>
                        </td>
                    </tr>
                    <tr>
                        <td>Tiêu đề SEO: </td>
                        <td>
                            <asp:TextBox ID="txtSeoTitle" runat="server" Width="400px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>Mô tả SEO: </td>
                        <td>
                            <asp:TextBox ID="txtSeoDescription" runat="server" TextMode="MultiLine" Width="400px" Height="100px"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clr"></div>
            <div style="width:90%;">
                <table class="adminform">
                    <tr>
                        <td colspan="3">
                            Giới thiệu
                        </td>
                    </tr>
                    <tr>
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtInfo" runat="server" UploadImage="true" 
                                Toolbar="Full" Width="100%" Height="400px"/>
                        </td>
					</tr>  
                    <tr class="tab">
                        <td colspan="3">Ưu điểm</td>
                    </tr>
                    <tr class="tab">
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtAdvantage" runat="server" UploadImage="true" Toolbar="Full" Width="100%" Height="500px"/>
                        </td>
					</tr>
                    <tr class="tab">
                        <td colspan="3">Kết quả</td>
                    </tr>
                    <tr class="tab">
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtResult" runat="server" UploadImage="true" Toolbar="Full" Width="100%" Height="500px"/>
                        </td>
					</tr>
                    <tr class="tab">
                        <td colspan="3">Quy trình</td>
                    </tr>
                    <tr class="tab">
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtProcess" runat="server" UploadImage="true" Toolbar="Full" Width="100%" Height="500px"/>
                        </td>
					</tr>
                    <tr class="tab">
                        <td colspan="3">Video</td>
                    </tr>
                    <tr class="tab">
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtVideo" runat="server" UploadImage="true" Toolbar="Full" Width="100%" Height="500px"/>
                        </td>
					</tr>
                    <tr class="tab">
                        <td colspan="3">Hình ảnh</td>
                    </tr>
                    <tr class="tab">
						<td colspan="3" align="center">
							<uc1:TextEditor ID="txtFrontBack" runat="server" UploadImage="true" Toolbar="Full" Width="100%" Height="500px"/>
                        </td>
					</tr>  
                </table>
            </div>
		</div>
		<div class="b">
			<div class="b">
				<div class="b"></div>
			</div>
		</div>
	</div>  
    
    <!-- Load Country, City, District -->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= lbtSave.ClientID %>").click(function () {
                var rtnValue = true;
                $("#ErorMessage").html('');
                if (jQuery.trim($("#<%= txtTitle.ClientID %>").val()) == "") {
                    $("#ErorMessage").append("<br\>Mời nhập tiêu đề");
                    rtnValue = false;
                }
                return rtnValue;
            });

            $('#<%=cbIsTab.ClientID %>').click(function () {
                if ($(this).is(':checked')) {
                    $('.tab').hide();
                } else {
                    $('.tab').show();
                }
            });
        });
    </script>
    <link rel="stylesheet" href="/css/datepicker/jquery.ui.all.css" />
	<script type="text/javascript" src="/js/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/js/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#<%= dtePublishDate.ClientID %>").datepicker({
	            changeMonth: false,
	            changeYear: false,
	            dateFormat: "dd/mm/yy"
	        });
	    });
	</script>

    <script type="text/javascript" src="/js/flashupload/fu_checkflash.js" ></script>
    <script type="text/javascript" src="/js/flashupload/swfupload.js"></script>
    <script type="text/javascript" src="/js/flashupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="/js/flashupload/fileprogress.js"></script>
    <script type="text/javascript" src="/js/flashupload/tinyhandler.js"></script> 
    <script type="text/javascript">
        var swfuThumbnail;
        window.onload = function() {
            if (flashinstalled != 2) {
                var adobe_download = "http://fpdownload.macromedia.com/get/flashplayer/current/install_flash_player.exe";
                if (MSDetect == "true") {
                    adobe_download = "http://fpdownload.macromedia.com/get/flashplayer/current/install_flash_player_ax.exe";
                }
                var html = '<div><p align="center">Your computer is not installed Flash Player or your version of Flash Player is not compatible spending. <br /> Please download and reinstall:<br /><br /><a href="' + adobe_download + '"><img src="/images/down_btn.gif" border="0"/></a></p></div>';
                document.getElementById("divThumbnailProgressAvatarContainer").innerHTML = html;
                return;
            }
            //Image Avatar
            swfuThumbnail = new SWFUpload({
                upload_url: "/SaveFile.aspx", // Relative to the SWF file                    
                file_size_limit: "250 KB", // 100KB
                file_types: "*.jpg;*.JPG;*.jpeg;*.JPEG;*.gif;*.GIF;*.png;*.PNG",
                file_types_description: "Image",
                //                   
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_start_handler: uploadStart,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadThumbnailAvatarSuccess,
                //
                button_image_url: "/images/blankButton.png",
                button_placeholder_id: "divThumbnailButtonAvatarPlaceholder",
                button_width: 110,
                button_height: 22,
                button_text: '<span class="button">Chọn ảnh</span>',
                button_text_style: '.button {text-align:center; font-family: Helvetica, Arial, sans-serif; font-size: 14pt; }',
                button_action: SWFUpload.BUTTON_ACTION.SELECT_FILE,
                // Flash Settings
                flash_url: "/js/flashupload/swfupload.swf",
                custom_settings: {
                    progressTarget: "divThumbnailProgressAvatarContainer"
                },
                // Debug Settings
                debug: false
            });
        };
        //Upload Avatar
        function uploadThumbnailAvatarSuccess(file, serverData) {
            try {
                var obj = eval('(' + serverData + ')');
                var filename = obj.filename.substring(obj.filename.lastIndexOf("/") + 1);
                var s = obj.image_handler + obj.filename.replace("~/Upload", "/image").replace(filename, "230_" + filename);
                document.getElementById("thumbnailSrcAvatar").value = obj.filename;
                PreviewThumbnailAvatar(s);
                document.getElementById("btnRemoveThumbnailAvatar").style.visibility = "visible";

                var progress = new FileProgress(file, this.customSettings.progressTarget);
                progress.setComplete();
                progress.setStatus("Complete.");
                progress.toggleCancel(false);
            } catch (ex) {
                this.debug(ex);
            }
        }
        //Preview Avatar
        function PreviewThumbnailAvatar(src) {
            var newImg = document.createElement("img");
            newImg.style.margin = "5px";
            newImg.style.border = "1px solid #7F7F7F";
            newImg.style.padding = "2px";
            document.getElementById("thumbnailPreviewAvatar").innerHTML = ""; //remove all child
            document.getElementById("thumbnailPreviewAvatar").appendChild(newImg);
            if (newImg.filters) {
                try {
                    newImg.filters.item("DXImageTransform.Microsoft.Alpha").opacity = 0;
                } catch (e) {
                    newImg.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + 0 + ')';
                }
            } else {
                newImg.style.opacity = 0;
            }

            newImg.onload = function () {
                fadeIn(newImg, 0);
            };
            newImg.src = src;
        }

        //remove thumbnail
        function RemoveThumbnail(obj, idPreview, idSrc) {
            document.getElementById(idPreview).innerHTML = ""; //remove all child
            document.getElementById(idSrc).value = "";
            obj.style.visibility = "hidden";
        }
        
        function fadeIn(element, opacity) {
            var reduceOpacityBy = 5;
            var rate = 30; // 15 fps
            if (opacity < 100) {
                opacity += reduceOpacityBy;
                if (opacity > 100) {
                    opacity = 100;
                }
                if (element.filters) {
                    try {
                        element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
                    } catch (e) {
                        // If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
                        element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
                    }
                } else {
                    element.style.opacity = opacity / 100;
                }
            }
            if (opacity < 100) {
                setTimeout(function () {
                    fadeIn(element, opacity);
                }, rate);
            }
        }
    </script>
</asp:content>


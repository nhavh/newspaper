<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBanner.aspx.cs" Inherits="TMV.BackEnd.Pages.EditBanner" %>

<asp:content id="Content1" contentplaceholderid="placeHolderContent" runat="server">
    <style>
        div.toolbar { margin-right: 0;}
        div.m { width: 700px;}
    </style>
    <div id="toolbar-box" style="width:770px;">
		<div class="t">
			<div class="t">
				<div class="t"></div>
			</div>
		</div>
		<div class="m">
		    <div id="toolbar" class="toolbar">
			    <table class="toolbar"><tbody>
					    <tr>    
						<td id="toolbar-save" class="button">
						    <asp:LinkButton ID="lbtSave" runat="server" Text="" onclick="lbtSave_Click" CssClass="toolbar">
					            <span id="Span1" title="Save" class="icon-32-save" runat="server"></span>Save
					        </asp:LinkButton>
						</td>

						<td id="toolbar-cancel" class="button">
						    <asp:LinkButton ID="lbtClose" runat="server" Text="" onclick="lbtClose_Click" 
                                CssClass="toolbar" CausesValidation="False">
					            <span id="Span2" title="Close" class="icon-32-cancel" runat="server"></span>Close
					        </asp:LinkButton>
						</td>
					    </tr>
				    </tbody>
			    </table>
		    </div>
			<div class="header">Thêm mới/chỉnh sửa banner
			</div>
			<div class="clr"></div>
		</div>
		<div class="b">
			<div class="b">
				<div class="b"></div>
			</div>
		</div>
	</div>
	<div id="element-box" style="width:700px;">
			<div class="t">
				<div class="t">
					<div class="t"></div>
				</div>
			</div>
			<div class="m">
                <table class="adminform">
                    <tr>
						<td colspan="3" >
                            <div id="ErorMessage" style="color: Red;">
                            </div>
                        </td>
					</tr>
                    <tr>
                        <td>Tiêu đề</td>
                        <td><asp:TextBox id="txtTitle" runat="server" Width="100%" MaxLength="256"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Đường dẫn</td>
                        <td><asp:TextBox id="txtUrl" runat="server" Width="100%" MaxLength="256"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Thứ tự</td>
                        <td><asp:TextBox id="txtPosition" runat="server" Width="100%" MaxLength="256"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr>
						<td style="width:20%">Hình&nbsp;khuyến&nbsp;mãi</td>
						<td>
                            <div id="divThumbnailButtonAvatarPlaceholder"></div>
                            <div id="divThumbnailProgressAvatarContainer"></div>
                            <div id="thumbnailPreviewAvatar"><%=ThumbnailPreviewAvatar %></div>                                      
                            <input type="hidden" name="thumbnailSrcAvatar" id="thumbnailSrcAvatar" value='<%= ThumbnailSrcAvatar %>' />    											
						</td>
						<td><input type="button" onclick="javascript: RemoveThumbnail(this, 'thumbnailPreviewAvatar', 'thumbnailSrcAvatar');" value="Remove" id="btnRemoveThumbnailAvatar" style='visibility:<% = (ThumbnailPreviewAvatar == "" ? "hidden" : "visible") %>' /></td>
					</tr>
                    <tr>
                        <td>Vị trí</td>
                        <td align="center"><asp:DropDownList id="ddlPriority" runat="server"></asp:DropDownList></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Ngày hiển thị: </td>
                        <td>
                            <input type="text" id="dteStartDate" readonly="readonly" runat="server" />
                            <select id="ddlStartHours" runat="server"></select> : <select id="ddlStartMinute" runat="server"></select>
                        </td>
                    </tr>
                    <tr>
                        <td>Ngày kết thúc: </td>
                        <td>
                            <input type="text" id="dteEndDate" readonly="readonly" runat="server" />
                            <select id="ddlEndHours" runat="server"></select> : <select id="ddlEndMinute" runat="server"></select>
                        </td>
                    </tr>
                </table> 
             <div class="clr"></div>
		    </div>
			<div class="b">
				<div class="b">
					<div class="b"></div>
				</div>
			</div>
		</div>  
    <link rel="stylesheet" href="/css/datepicker/jquery.ui.all.css" />
	<script type="text/javascript" src="/js/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/js/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#<%= dteStartDate.ClientID %>").datepicker({
	            changeMonth: true,
	            changeYear: true,
	            dateFormat: "dd/mm/yy"
	        });
	        $("#<%= dteEndDate.ClientID %>").datepicker({
	            changeMonth: true,
	            changeYear: true,
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
        window.onload = function () {
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
                file_size_limit: "500 KB", // 100KB
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
                var s = obj.image_handler + obj.filename.replace("~/Upload", "/image").replace(filename, "430_" + filename);
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

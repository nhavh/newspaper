<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditVideo.aspx.cs" Inherits="TMV.BackEnd.Pages.EditVideo" %>

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
			<div class="header">Thêm mới/chỉnh sửa video</div>
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
                        <td style="width:30%;">Tiêu đề</td>
                        <td colspan="2"><asp:TextBox id="txtTitle" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:30%;">Đường dẫn Youtube</td>
                        <td colspan="2"><asp:TextBox id="txtUrl" runat="server" Width="400px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Hiển&nbsp;thị&nbsp;trang&nbsp;chủ: </td>
                        <td>
                            <asp:CheckBox ID="cbIsHome" runat="server" />
                        </td>
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
                    <tr>
                        <td>Tags: </td>
                        <td>
                            <asp:TextBox ID="txtTags" runat="server" TextMode="MultiLine" Width="400px" Height="100px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>Mô tả: </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="400px" Height="100px"/>
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
        });
    </script>
    <link rel="stylesheet" href="/css/datepicker/jquery.ui.all.css" />
	<script type="text/javascript" src="/js/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/js/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#<%= dteStartDate.ClientID %>").datepicker({
	            changeMonth: false,
	            changeYear: false,
	            dateFormat: "dd/mm/yy"
	        });
	        $("#<%= dteEndDate.ClientID %>").datepicker({
	            changeMonth: false,
	            changeYear: false,
	            dateFormat: "dd/mm/yy"
	        });
	    });
	</script>
</asp:content>

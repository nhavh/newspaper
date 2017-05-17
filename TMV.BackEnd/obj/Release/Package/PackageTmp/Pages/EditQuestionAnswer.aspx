<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQuestionAnswer.aspx.cs" Inherits="TMV.BackEnd.Pages.EditQuestionAnswer" %>
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
			<div class="header">Thêm mới/chỉnh sửa HỎI ĐÁP</div>
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
            <table class="adminform">
                <tr>
					<td colspan="3">
                        <div id="ErorMessage" style="color: Red;">
                        </div>
                    </td>
				</tr>
                <tr>
                    <td style="width:10%;">Tiêu đề</td>
                    <td colspan="2"><asp:TextBox id="txtTitle" runat="server" Width="400px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Ngày hiển thị: </td>
                    <td>
                        <input type="text" id="dtePublishDate" readonly="readonly" runat="server" />
                        <select id="ddlPublishHours" runat="server"></select> : <select id="ddlPublishMinute" runat="server"></select>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Nội dung câu hỏi
                    </td>
                </tr>
                <tr>
					<td colspan="3" align="center">
						<uc1:TextEditor ID="txtQuestion" runat="server" UploadImage="true" 
                            Toolbar="Full" Width="99%" Height="400px"/>
                    </td>
				</tr>
                <tr>
                    <td colspan="3">
                        Nội dung trả lời
                    </td>
                </tr>
                <tr>
					<td colspan="3" align="center">
						<uc1:TextEditor ID="txtContent" runat="server" UploadImage="true" 
                            Toolbar="Full" Width="99%" Height="400px"/>
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
	        $("#<%= dtePublishDate.ClientID %>").datepicker({
	            changeMonth: false,
	            changeYear: false,
	            dateFormat: "dd/mm/yy"
	        });
	    });
	</script>

</asp:content>
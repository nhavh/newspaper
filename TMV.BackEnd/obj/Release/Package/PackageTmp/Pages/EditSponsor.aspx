<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSponsor.aspx.cs" Inherits="TMV.BackEnd.Pages.EditSponsor" %>

<asp:content id="Content1" contentplaceholderid="placeHolderContent" runat="server">
   	<link rel="stylesheet" href="/css/datepicker/jquery.ui.all.css" />
    <div id="toolbar-box" style="width:770px;">
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
			<div class="header">Thêm mới/chỉnh sửa bài viết nổi bật
			</div>
			<div class="clr"></div>
		</div>
		<div class="b">
			<div class="b">
				<div class="b"></div>
			</div>
		</div>
	</div>
	<div id="element-box" style="width:770px;">
		<div class="t">
			<div class="t">
				<div class="t"></div>
			</div>
		</div>
		<div class="m" style="width: 100%;">		
            <table class="adminform">
                <tr>
		            <td colspan="2">
                        <div id="ErorMessage" style="color: Red;" runat="server"></div>
                    </td>
				</tr>
			    <tr>
				    <td>Tiêu đề:</td>
				    <td>
				        <input id="txtItemId" style="width:98%" runat="server" type="text"/>
                        <input id="hdItemId" type="hidden" runat="server" />
                    </td>
			    </tr>
                <tr>
	                <td>Chuyên mục:</td>
	                <td><asp:DropDownList ID="ddlItemType" runat="server"></asp:DropDownList></td>
                </tr>
				<tr>
                    <td>Ngày hiển thị:</td>
			        <td>
                        <input type="text" id="dpStartDate" readonly="readonly" runat="server" />
                        <select id="ddlStartDateHour" runat="server"></select> : <select id="ddlStartDateMinute" runat="server"></select>
                    </td>
                </tr>	
			    <tr>
			        <td>Ngày kết thúc:</td>
			        <td>
                        <input type="text" id="dpEndDate" readonly="readonly" runat="server" />
                        <select id="ddlEndDateHour" runat="server"></select> : <select id="ddlEndDateMinute" runat="server"></select>
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
	<script type="text/javascript">
	    $(document).ready(function () {
	        //Save
	        $("#<%= lbtSave.ClientID %>").click(function () {
		            var rtnValue = true;
		            var errorMessage = "";
		            $("#<%= ErorMessage.ClientID %>").html('');
		            if ($("#<%= hdItemId.ClientID %>").val() == "") {
		                errorMessage += "<br/>+Vui lòng chọn bài viết";
		                rtnValue = false;
		            }
		            if (rtnValue == false)
		                $("#<%= ErorMessage.ClientID %>").append(errorMessage.substring(5, errorMessage.length));
		            return rtnValue;
		        });
		    });
	</script>	   
    <script type="text/javascript" src="/js/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" src="/js/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $("#<%= dpStartDate.ClientID %>").datepicker({
	            changeMonth: true,
	            changeYear: true,
	            dateFormat: "dd/mm/yy"
	        });
	    });
	</script>  
	<script type="text/javascript">
	    $(function () {
	        $("#<%= dpEndDate.ClientID %>").datepicker({
	            changeMonth: true,
	            changeYear: true,
	            dateFormat: "dd/mm/yy"
	        });
	    });
	</script>   
	
    <link type="text/css" href="/css/jquery_autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="/js/jquery.autocomplete.js"></script>     
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%= txtItemId.ClientID %>").autocomplete("/AjaxAction.aspx?t=ListArticleByTitle", {
                width: 260,
                selectFirst: false
            });

            $("#<%= txtItemId.ClientID %>").result(function (event, data, formatted) {
                if (data)
                    $("#<%= hdItemId.ClientID %>").val(data[1]);
            });
        });
    </script>
</asp:content>
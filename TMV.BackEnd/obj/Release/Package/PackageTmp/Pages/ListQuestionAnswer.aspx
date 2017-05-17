<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListQuestionAnswer.aspx.cs" Inherits="TMV.BackEnd.Pages.ListQuestionAnswer" %>
<%@ Register src="../Controls/GridViewManager.ascx" tagname="GridViewManager" tagprefix="uc1" %>

<asp:content id="Content1" contentplaceholderid="placeHolderContent" runat="server">
    <%if (!string.IsNullOrEmpty(Title)) { %>
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
						    <td id="toolbarAddRoom" class="button" runat="server">
							    <a href="<%=UrlAdd %>" class="modal">
								    <span title="Thêm đánh giá" class="icon-32-new"></span>THÊM HỎI ĐÁP
							    </a>
						    </td>
                            <td id="Td1">
							    <a href="<%=UrlArticles %>" class="modal">
								    <span title="Thoát" class="icon-32-cancel"></span>Thoát
							    </a>
						    </td>
					    </tr>
				    </tbody>
			    </table>
		    </div>
			<div class="header"> HỎI & ĐÁP của <%=Title %>
			</div>
			<div class="clr"></div>
		</div>
		<div class="b">
			<div class="b">
				<div class="b"></div>
			</div>
		</div>
	</div>
    <% } %>
    <uc1:GridViewManager ID="GridViewManager1" runat="server" />
</asp:content>


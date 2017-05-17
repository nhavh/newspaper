<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewManager.ascx.cs" Inherits="TMV.BackEnd.Controls.GridViewManager" %>
<table cellpadding="0" cellspacing="0" border="0">
    <link href="/css/screen.css" rel="stylesheet" type="text/css" />
    <tr>
        <td>
            <table bgcolor="#5669B0" cellpadding="4" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" CssClass="Title" Text="Tiêu đề GridManager" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        <asp:GridView ID="gvManager" runat="server" CellPadding="7" CellSpacing="1" GridLines="none"
            AllowPaging="True" AllowSorting="True" ShowFooter="true" AutoGenerateColumns="False"
            OnRowCommand="gvManager_RowCommand" OnInit="gvManager_Init" PageSize="20" OnRowCreated="gvManager_RowCreated"
            OnPreRender="gvManager_PreRender" BackColor="White">
            <RowStyle BackColor="#FFFFFF" VerticalAlign="Top" />
            <PagerStyle BackColor="#FEF8DC" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#FEF8DC" Font-Bold="True" HorizontalAlign="Left" />
            <FooterStyle BackColor="#FEF8DC" Font-Bold="True" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="#F5F7FC" VerticalAlign="Top" />
            <PagerSettings FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" Mode="NumericFirstLast"
                PageButtonCount="20" />
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    <HeaderTemplate>
                        <asp:LinkButton ID="cmdSID" runat="server" CommandArgument="" CommandName="Sort"
                            Text="ID" Width="100%" ToolTip="Reset sort expression"></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server" Width="100%" Visible="<%# ShowID %>" Text='<%# Eval(PrimaryKeyName) %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Center"></FooterStyle>
                    <FooterTemplate>
                        <asp:Label ID="lblANo" runat="server" Text="*" Width="100%"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Width="120px" VerticalAlign="Bottom"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text="Chỉnh sửa" Visible="<%# ShowUpdateCommand %>" CommandName="Edit" CausesValidation="false"
                            ID="cmdEdit" CommandArgument='<% # Eval(PrimaryKeyName) %>' CssClass="CommandButton">
                        </asp:LinkButton>                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" Text="Cập nhật" CommandName="Update" ID="cmdUpdate" CssClass="CommandButton"></asp:LinkButton>&nbsp;
                        <asp:LinkButton runat="server" Text="Bỏ qua" CommandName="Cancel" ID="cmdCancel" CssClass="CommandButton"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderStyle Width="75px" VerticalAlign="Top"></HeaderStyle>
                    <HeaderTemplate>
                        <a class="CommandButton" href="javascript:ClearFilter()">Bỏ lọc</a>
                        <asp:LinkButton runat="server" CommandName="Filter" Text="Lọc" Width="100%" ID="cmdFilter" CssClass="CommandButton"></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton Width="100%" Text="Xóa" Visible="<%# ShowDeleteCommand %>" runat="server" CommandName="Delete" ID="cmdDelete" 
                            CommandArgument='<% # Eval(PrimaryKeyName) %>' CssClass="CommandButton" OnClientClick="return confirm('Bạn có chắc muốn xóa?');">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton runat="server" Width="100%" Text="Thêm mới" CommandName="Add" ID="cmdAdd" CssClass="CommandButton"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="objDataSource" runat="server"></asp:ObjectDataSource>
    </td>
    </tr>
</table>

<script type="text/javascript">
    function ClearFilter() {
        prefix = '<%=gvManager.ClientID%>';
        row = document.getElementById(prefix).rows[0];
        for (i = 0; i < row.cells.length; i++) {
            field = row.cells[i];
            for (j = 0; j < field.childNodes.length; j++) {
                id = field.childNodes[j].id;
                if (id != null) {
                    if (id.indexOf("cbo") != -1) {
                        field.childNodes[j].selectedIndex = 0;
                        continue;
                    }

                    if (id.indexOf("txt") != -1) {
                        field.childNodes[j].value = "";
                        continue;
                    }

                    if (id.indexOf("cmdFilter") != -1) {
                        location.href = field.childNodes[j].href;
                        break;
                    }
                }
            }
        }
    }
</script>


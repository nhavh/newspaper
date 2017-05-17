<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCustomer.aspx.cs" Inherits="TMV.BackEnd.Pages.ListCustomer" %>
<%@ Register src="../Controls/GridViewManager.ascx" tagname="GridViewManager" tagprefix="uc1" %>

<asp:content id="Content1" contentplaceholderid="placeHolderContent" runat="server">
    <uc1:GridViewManager ID="GridViewManager1" runat="server" />
</asp:content>

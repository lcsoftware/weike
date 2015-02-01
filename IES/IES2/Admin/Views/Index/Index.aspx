<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Admin.Views.Index.Index" %>
<%@ Register Src="~/Views/Index/DiskSize.ascx" TagPrefix="uc1" TagName="DiskSize" %>
<%@ Register Src="~/Views/Index/OnlineUser.ascx" TagPrefix="uc1" TagName="OnlineUser" %>
<%@ Register Src="~/Views/Index/Todo.ascx" TagPrefix="uc1" TagName="Todo" %>
<%@ Register Src="~/Views/Index/Notice.ascx" TagPrefix="uc1" TagName="Notice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="core_content">
        <uc1:Todo runat="server" id="Todo" />
        <uc1:Notice runat="server" id="Notice" />
    </div>
    <div class="right_side">
        <uc1:OnlineUser runat="server" ID="OnlineUser" />
        <uc1:DiskSize runat="server" ID="DiskSize" />         
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="Admin.Views.Resource.Video.Teacher" %>
<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li><a href="#">管理员上传</a></li>
            <li><a href="#">教师上传</a></li>
            <li><a href="#">学生上传</a></li>
            <li class="active"><a href="#">推荐视频</a></li>
        </ul>
    </div>
    <div class="sousuo_box">



    </div>

    <div class="search_result_box">

    </div>

</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Admin.Views.Resource.Video.Admin" %>
<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Js/admin.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="data_nav_box" style="background:#fff">
        <ul class="data_nav_list">
            <li class="active"><a href="#">管理员上传</a></li>
            <li><a href="#">教师上传</a></li>
            <li><a href="#">学生上传</a></li>
            <li><a href="#">推荐视频</a></li>
        </ul>
    </div>
    <div class="sousuo_box">
        <div class="filter_item">
            <a class="add_student" href="SpecialtyType.aspx?PID=A112" style="margin-left:10px"><i class="icon_admin add_btn"></i>上传视频</a>
            <a class="add_student" href="javascript:ADD();" style="margin-left:0px"><i class="icon_admin add_btn"></i>新建文件夹</a>         
            <div class="class_operation">
                <a href="javascript:NoOpen();"><i class="icon_admin daoru"></i>批量操作</a>
                <ul>
                    <a href="Specialty.xls"><i class="icon_admin download_btn"></i>批量权限</a>
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>批量移动</a>
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>批量删除</a>
                </ul>
            </div>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="请输入您想要搜索的关键字"><a runat="server" style="float:right;position:relative;margin-right:3px; margin-top:-23px;" onclick="ParmSelect()" class="icon_admin search_icon"></a>
            </p>
        </div>
        <div class="select_requirement_box">
            <!--数据绑定-->
            
            <asp:Repeater runat="server" ID="rptorg" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>所属机构</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="orgParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a  href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)"  id="<%# Eval("OrganizationID")%>"><a href="#"><%# Eval("OrganizationName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                    <a class="fold_btn" href="javascript:;">[更多]</a>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ID="rptschlen" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>学制</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="schlenParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a  href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("SchoolingLength")%>"><a href="#"><%# Eval("SchoolingLength")%>年制</a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <!--数据绑定-->
        </div>
    </div>

    <div class="search_result_box">

    </div>

</asp:Content>
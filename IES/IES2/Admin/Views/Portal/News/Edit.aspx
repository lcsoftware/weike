<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.Portal.News.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li><a href="#">新闻公告信息</a></li>
        </ul>
    </div>
    <div class="data_box">
        <div class="info_box">
            <h5>新闻公告信息</h5>
            <div class="info_detail_box">
                <div class="info_detail">
                    <div class="info_detail_list">
                        <label>新闻标题:</label>
                        <div class="fill_box">
                            <input type="text" runat="server" id="txtTitle" /><label style="color: red; width: 5px">*</label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                    <div style="float: left; width: 363px; height: 32px; margin-top: 15px; line-height: 32px">
                        <asp:CheckBox ID="IsImp" Width="150px" Height="30px" runat="server" Text="是否重要新闻" />
                        <asp:CheckBox ID="IsTop" Width="150px" Height="30px" runat="server" Text="是否置顶" />
                    </div>
                    <div class="info_detail_list">
                        <label>开始时间:</label>
                        <div class="fill_box">
                            <input id="BeginTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true})" runat="server" value="2015-01-01" type="text">
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>结束时间:</label>
                        <div class="fill_box">
                            <input id="EndTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true})" runat="server" value="2015-01-31" type="text">
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>所属板块:</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Section" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div><label style="color: red; width: 5px">*</label>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Section" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                        </div>
                    <div class="info_detail_list">
                        <label>子系统:</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Sys" Width="200px" runat="server">
                                <asp:ListItem Value="-1">所有系统</asp:ListItem>
                                <asp:ListItem Value="0">课程中心</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        *</div>
                    <div class="info_detail_list" style="width: 670px; height: 295px;">
                        <label>新闻内容:</label>
                        <div class="fill_box" style="top: 0px; left: 0px; height: 295px;">
                            <input type="hidden" name="oEditor1" id="oEditor1" runat="server" />
                            <iframe runat="server" id="frmoEditor1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=coolblue" frameborder="0" scrolling="no" width="559" height="290" style="display: block;"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 940px;float:left">
            <a style="float:left" class="update_btn" runat="server"  onserverclick="update_Click">提交</a>
            <a style="float:left" class="update_btn" runat="server" onserverclick="cancel_Click" >取消</a>
        </div>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.Portal.Notice.Edit" %>
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
            <h5>通知信息</h5>
            <div class="info_detail_box">
                <div class="info_detail">
                    <div class="info_detail_list">
                        <label>通知标题：</label>
                        <div class="fill_box">
                            <input type="text" runat="server" id="txtTitle" /><label style="color: red; width: 5px">*</label>
                        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                    <div style="float: left; width: 363px; height: 32px; margin-top: 15px; line-height: 32px">
                        <asp:CheckBox ID="IsTop" Width="150px" Height="30px" runat="server" Text="是否置顶" />
                    </div>
                    <div class="info_detail_list" style="width: 640px; height: 295px;">
                        <label>通知内容：</label>
                        <div class="fill_box" style="top: 0px; left: 0px; height: 295px;">
                            <input type="hidden" name="oEditor1" id="oEditor1" runat="server" />
                            <iframe runat="server" id="frmoEditor1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=coolblue" frameborder="0" scrolling="no" width="559" height="290" style="display: block;"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="info_box">
            <h5>接收对象</h5>
            <div class="info_detail_box">
                <div class="info_detail">
                    
                    
                </div>
            </div>
        </div>
        <div style="width: 940px;float:left">
            <a style="float:left" class="update_btn" runat="server"  onserverclick="update_Click">提交</a>
            <a style="float:left" class="update_btn" runat="server" onserverclick="cancel_Click" >取消</a>
        </div>
    </div>
</asp:Content>

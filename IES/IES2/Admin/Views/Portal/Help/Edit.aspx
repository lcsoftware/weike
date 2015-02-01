<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.Portal.Help.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li><a href="#">使用指南信息</a></li>
        </ul>
    </div>
    <div class="data_box">
        <div class="info_box">
            <h5>使用指南信息</h5>
            <div class="info_detail_box">
                <div class="info_detail">
                    <div class="info_detail_list">
                        <label>问题：</label>
                        <div class="fill_box">
                            <input type="text" runat="server" id="HelpTitle" /><label style="color: red; width: 5px">*</label>
                        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="HelpTitle" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                    <div class="info_detail_list" style="width: 670px; height: 295px;">
                        <label>内容：</label>
                        <div class="fill_box" style="top: 0px; left: 0px; height: 190px;">
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


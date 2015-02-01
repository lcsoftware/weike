<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Specialty.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Portal/Edit.js"></script>
    
    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_box">
        <div class="info_box">
            <h5>基本信息</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="width:920px">
                    <div class="info_detail_list">
                        <label>专业编号：</label>
                        <div class="fill_box">
                            <input id="SpecialtyNo" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SpecialtyNo" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                    </div>
                    <div class="info_detail_list" style="width:520px">
                        <label>学  科：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="SpecialtyType" Width="200px" Height="32px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SpecialtyType_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="fill_box" style="margin-left:10px">
                            <asp:DropDownList ID="SpecialtyType2" Width="150px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                        <label style="color: red; width: 5px">*</label>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="SpecialtyType" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>专业名称：</label>
                        <div class="fill_box">
                            <input id="SpecialtyName" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>                     
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SpecialtyName" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>英文名称：</label>
                        <div class="fill_box">
                            <input id="SpecialtyNameEn" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>所属机构：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Organization" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                        <label style="color:red;width:5px">*</label>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Organization" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>学制：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="SchoolingLength" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 680px; height: 295px;">
                        <label style="text-align:left">专业简介:</label>
                        <div class="fill_box" style="top: 0px; left: 0px; height: 295px;">
                            <input type="hidden" name="oEditor1" id="oEditor1" runat="server" />
                            <iframe runat="server" id="frmoEditor1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=coolblue" frameborder="0" scrolling="no" width="559" height="290" style="display: block;"></iframe>
                        </div>
                    </div>
                    <div style="float: left; width: 559px; height: 30px; margin-left: 75px; line-height: 30px">
                        <asp:CheckBox ID="IsShow" runat="server" Text="在门户中展示" />
                    </div>
                </div>
            </div>
        </div>

        <!--<div class="info_box">
            <h5>推荐教师:</h5>
            <div id="OmGrid1" style="width: 960px; z-index: 999">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table border="1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr style="width: 460px">
                            <td rowspan="2" style="width: 135px;">
                                <img src="" style="width: 120px; height: 160px; margin: 5px 7px 5px 7px" />                   
                            </td>
                            <td style="width: 95px; height: 60px"><%# Eval("UserName")%></td>
                            <td style="width: 95px; height: 60px"><%# Eval("UserNo")%></td>
                            <td style="width: 135px; height: 60px">
                                <p class='operation_box'>
                                    <i title='编辑' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='删除' onclick='Del(this.id)' id='<%#Eval("UserID")%>' class='icon_admin delete_btn'></i>
                                </p>
                            </td>
                        </tr>
                        <tr style="width: 460px;">
                            <td colspan="3" style="height: 140px; width: 340px"><%# Eval("Brief")%></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <a id="ts" style="color: #000; line-height: 18px; font-size: 13px" href="javascript:AddTeacher();"><i class="icon_admin addss_btn" style="vertical-align: middle; margin: 2px 5px 5px 0px"></i>新增推荐教师</a>
        </div>-->

        <div style="width: 940px">
            <a class="update_btn" runat="server" onserverclick="update_Click">更新资料</a>
        </div>      
    </div>
</asp:Content>

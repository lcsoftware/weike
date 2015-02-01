<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.Portal.Link.Edit" %>
<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Nav runat="server" id="Nav" />
            <div class="data_nav_box">
            	<ul class="data_nav_list">
            		<li><a href="#">友情链接信息</a></li>
                </ul>
            </div>
            <div class="data_box">
            	<div class="info_box">
                	<h5>友情链接信息</h5>	
                    <div class="info_detail_box">
                		<div class="info_detail">
                        	<div class="info_detail_list">
                            	<label>链接名称：</label>
                                <div class="fill_box">
                                	<input type="text" runat="server" id="LinkTitle" /><label style="color: red; width: 5px">*</label>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LinkTitle" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                                </div>                    
                                                                
                		</div>
                    <div class="info_detail" style="margin-top:10px">
                        <div class="info_detail_list">
                            	<label>链接地址：</label>
                                <div class="fill_box">
                                	<input type="text" runat="server" id="LinkUrl" /><label style="color: red; width: 5px">*</label>
                                </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LinkUrl" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                                </div>   
                           </div>
                    </div>
                </div>
               
            </div>
            <div style="width: 940px;float:left">
            <a style="float:left" class="update_btn" runat="server"  onserverclick="update_Click">提交</a>
            <a style="float:left" class="update_btn" runat="server" onserverclick="cancel_Click" >取消</a>
        </div>
</asp:Content>

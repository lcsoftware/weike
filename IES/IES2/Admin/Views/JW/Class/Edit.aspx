<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Class.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:Nav runat="server" id="Nav" />


            <div class="data_nav_box">
            	<ul class="data_nav_list">
            		<li><a href="#">个人资料</a></li>
                    <li class="active"><a href="#">教职信息</a></li>
                </ul>
            </div>
            <div class="data_box">
            	<div class="tip_box">
            		<p><i class="icon_admin tip_icon"></i><b>提示信息：</b>请尽量完善以下个人资料，并保证其真实性。*为必填项。</p>
                    <span class="close_tip">×</span>
            	</div>
            	<div class="info_box">
                	<h5>教职信息</h5>	
                    <div class="info_detail_box">
                		<div class="info_detail">
                        	<div class="info_detail_list">
                            	<label>所属机构：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option></option>
                                        <option></option>
                                        <option></option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>岗位类别：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>专技岗位</option>
                                        <option>专技岗位</option>
                                        <option>专技岗位</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>专业类别：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>行政专员</option>
                                        <option>行政专员</option>
                                        <option>行政专员</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>专技职务：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>讲师</option>
                                        <option>讲师</option>
                                        <option>讲师</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>学科：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>大学英语</option>
                                        <option>大学英语</option>
                                        <option>大学英语</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>身份：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>校内</option>
                                        <option>校内</option>
                                        <option>校内</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>聘任日期：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>2014年1月1日</option>
                                        <option>2014年1月1日</option>
                                        <option>2014年1月1日</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>是否助教：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>助教</option>
                                        <option>助教</option>
                                        <option>助教</option>
                                    </select>
                                </div>
                            </div>
                            <div class="info_detail_list">
                            	<label>门户展示：</label>
                                <div class="fill_box">
                                	<select>
                                    	<option>展示</option>
                                        <option>不展示</option>
                                    </select>
                                </div>
                            </div>
                		</div>
                    </div>
                </div>
                <a class="update_btn" href="javascript:;">更新资料</a>
            </div>
            <!--右侧主题内容结束-->  
</asp:Content>

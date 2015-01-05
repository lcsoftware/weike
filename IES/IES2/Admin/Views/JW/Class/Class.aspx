<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Class.aspx.cs" Inherits="Admin.Views.JW.Class.Class" %>
<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <script src="Class.js"></script>
   <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
         <div class="sousuo_box">
            	<div class="filter_item">
                	<a class="filter_btn" href="javascript:;">筛选</a>
                    <div class="class_operation">
                    	<a href="javascript:;"><i class="icon_admin daoru"></i>导入行政班</a>
                        <ul>
                            <a href="javascript:;"><i class="icon_admin download_btn"></i>模板下载</a>
                            <a href="javascript:;"><i class="icon_admin daochu"></i>导出行政班</a>
                        </ul>
                    </div>
                    <a class="add_student" href="javascript:;"><i class="icon_admin add_btn"></i>新增学生</a>
                    <p class="search_btn"><input type="text" placeholder="输入姓名、工号搜索学员"><i class="icon_admin search_icon"></i></p>
                </div>
                <div class="select_requirement_box">
                    <dl class="requirement_box">
                        <dt>所属机构</dt>
                        <dd>
                        	<div class="all_requirement">
                                <div class="select_require">
                                    <span class="active"><a href="#">不限</a></span>
                                    <span><a href="#">校长办公室</a></span>
                                    <span><a href="#">校领导</a></span>
                                    <span><a href="#">教务处</a></span>
                                    <span><a href="#">校医院</a></span>
                                    <span><a href="#">国际商务与管理学院</a></span>
                                    <span><a href="#">音乐艺术学院</a></span>
                                    <span><a href="#">经济信息学院</a></span>
                                    <span><a href="#">地球科学学院</a></span>
                                    <span><a href="#">地球科学学院</a></span>
                                    <span><a href="#">国际商务与管理学院</a></span>
                                    <span><a href="#">音乐艺术学院</a></span>
                                </div>
                                <a class="fold_btn" href="javascript:;">[更多]</a>
                            </div>
                            <div class="select_require second_require">
                            	<span class="active"><a href="#">不限</a></span>
                            	<span><a href="#">金融管理</a></span>
                                <span><a href="#">商务管理</a></span>
                                <span><a href="#">社会学</a></span>
                                <span><a href="#">计算机</a></span>
                                <span><a href="#">市场营销</a></span>
                                <span><a href="#">经济学</a></span>
                            </div>
                        </dd>
                    </dl>
                    <dl class="requirement_box">
                        <dt>创建时间</dt>
                        <dd>
                        	<div class="date_time">
                            	<p><input type="text"><i class="icon_admin date_icon"></i></p>
                            	<span>~</span>
                                <p><input type="text"><i class="icon_admin date_icon"></i></p>
                            </div>
                        </dd>
                    </dl>
                    <a class="search_button" href="javascript:;">搜索</a>
                    <span class="close_box"><i class="icon_admin shouqi_icon"></i></span>
                </div>
            </div>
            <div class="search_result_box">
             <div class="result_tit">
                        <h4>学生列表<span>（共18条）</span></h4>
                        <p class="show_num">每页显示<select><option>20</option><option>50</option><option>100</option><option>200</option></select></p>
                        <a href="javascript:;">删除</a>
                        <a href="javascript:;">设置入学年份</a>
                        <a href="javascript:;">批量设置专业</a>
                        <a href="javascript:;">设置为助教</a>
                    </div>
        
          <table class="result_table">
                 <asp:Repeater ID="Repeater1" runat="server">
                     <HeaderTemplate>
                        <tr>
                            <th width="10"></th>
                            <th width="30"><input type="checkbox"></th>
                            <th width="100">班级编号</th>
                            <th width="100">班级名称</th>
                            <th width="100">所属机构</th>
                            <th width="150">专业</th>
                            <th width="100">辅导员</th>
                            <th width="100">入学日期</th>
                            <th width="100" >操作</th>
                        </tr> 
                     </HeaderTemplate>
                     <ItemTemplate>
                        <tr>
                            <td></td>
                            <td><input id='<%# Eval("ClassID")%>' type="checkbox"></td>
                            <td><%# Eval("ClassNo")%></i></td>
                            <td><%# Eval("ClassName")%></td>
                            <td><%# Eval("OrganizationName")%></td>
                            <td></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("EntryDate")%></td>
                            <td> 
                                <p >
                                     <a  title='编辑' href ='Edit.aspx?id<%# Eval("ClassID")%>'   class='icon_admin edit_btn'  ></a>
                                     <img  title='删除'  pid='<%# Eval("ClassID")%>' onclick='classModule.del(this)'  class='icon_admin delete_btn' ></img>  
                                </p>
                            </td>

                        </tr>
                     </ItemTemplate>
                 </asp:Repeater>
           </table>
            <div class="page_wrap">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                    ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                    HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true" 
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box" >
                </webdiyer:AspNetPager>
           </div>
   </div>
</asp:Content>
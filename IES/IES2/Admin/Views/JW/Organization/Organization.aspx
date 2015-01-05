<%@ Page  Title="组织机构" MasterPageFile="~/Site.Master"         Language="C#"       AutoEventWireup="true"            CodeBehind="Organization.aspx.cs" Inherits="Admin.Views.JW.Organization.Organization" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

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
                    <tr>
                        <th width="10"></th>
                        <th width="30"><input type="checkbox"></th>
                        <th width="100">用户名<i class="icon_admin rank_btn"></i></th>
                        <th width="80">姓名</th>
                        <th width="100">学号<i class="icon_admin rank_btn"></i></th>
                        <th width="50">性别</th>
                        <th width="200">所属机构</th>
                        <th width="80">专业</th>
                        <th width="70">是否助教</th>
                        <th width="80">入学年份</th>
                        <th width="90">行政班</th>
                        <th>操作</th>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="checkbox"></td>
                        <td><p class="user_id">20130142</p></td>
                        <td>蒋晓军</td>
                        <td>10101201</td>
                        <td>女</td>
                        <td>校长办公室</td>
                        <td>金融管理</td>
                        <td>助教</td>
                        <td>2013</td>
                        <td>班级0701</td>
                        <td><p class="operation_box"><i class="icon_admin edit_btn"></i><i class="icon_admin delete_btn"></i></p></td>
                    </tr>
                </table>
                <div class="page_wrap">
                    <div class="page_box">
                        <a class="prev" href="javascript:;">前一页</a>
                        <a href="#">1</a>
                        <a href="#">2</a>
                        <a href="#">3</a>
                        <a href="#">4</a>
                        <span class="more">...</span>
                        <a href="#">8</a>
                        <a class="prev" href="javascript:;">后一页</a>
                        <span>共229条，到第<input type="text">页</span>
                        <a class="confirm" href="#">确认</a>
                    </div>
                </div>
            </div>
</asp:Content>

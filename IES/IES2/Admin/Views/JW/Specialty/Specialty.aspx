<%@ Page Title="专业" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Specialty.aspx.cs" Inherits="Admin.Views.JW.Specialty.Specialty" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         function GetListChange(obj) {
             var sValue = obj.options[obj.options.selectedIndex].value; //这是取值
             var sText = obj.options[obj.options.selectedIndex].innerHTML; //取文本内容
             //$('#<%=Change.ClientID%>').click();
         }
     </script>
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Js/admin.js"></script>
    <script src="Course.js"></script>
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
            <p class="search_btn">
                <input type="text" placeholder="输入姓名、工号搜索学员"><i class="icon_admin search_icon"></i></p>
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
                        <p>
                            <input type="text"><i class="icon_admin date_icon"></i></p>
                        <span>~</span>
                        <p>
                            <input type="text"><i class="icon_admin date_icon"></i></p>
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
            <p class="show_num">每页显示<select id="sel_pageSize"  onchange="GetListChange(this)"><option value="1">20</option>
                <option value="2">50</option>
                <option value="3">100</option>
                <option value="4">200</option>
            </select></p>
            <a href="javascript:;">删除</a>
            <a href="javascript:;">设置入学年份</a>
            <a href="javascript:;">批量设置专业</a>
            <a href="javascript:;">设置为助教</a>
        </div>
        <asp:Button ID="Change" runat="server" Text="" OnClick="Look_Click" Visible="false" />
        <%--<div id="div_List"></div>
                <div class="page_wrap" id="div_page_wrap">
                    
                </div>--%>
        <table class="result_table">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <tr>
                        <th width="10"></th>
                        <th width="30">
                            <input type="checkbox"></th>
                        <th width="90">专业编号</th>
                        <th width="200">专业名称</th>
                        <th width="200">学科</th>
                        <th width="200">开设机构</th>
                        <th width="90">学制</th>
                        <th width="90">专业方向</th>
                        <th>操作</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td></td>
                        <td>
                            <input id='<%# Eval("SpecialtyID")%>' type="checkbox"></td>
                        <td><%# Eval("SpecialtyNo")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td></td>
                        <td><%#Eval("OrganizationName") %></td>
                        <td><%#Eval("SchoolingLength") %></td>
                        <td><%#Eval("ParentID") %></td>
                        <td>
                            <asp:Button ID="btnSetBestAnswer" runat="server" OnCommand="Edit" CommandArgument='<%#Eval("SpecialtyID") %>' Text="编辑" />
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="page_wrap">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
            </webdiyer:AspNetPager>
        </div>
    </div>

</asp:Content>

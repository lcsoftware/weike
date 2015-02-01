<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="Admin.Views.JW.User.Teacher" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
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
                <input type="text" placeholder="输入姓名、工号搜索学员"><i class="icon_admin search_icon"></i>
            </p>
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
                <dt>身份</dt>
                <dd>
                    <div class="all_requirement">
                        <div class="select_require">
                            <span><a href="#">不限</a></span>
                            <span class="active"><a href="#">校内</a></span>
                            <span><a href="#">校外</a></span>
                        </div>
                    </div>
                </dd>
            </dl>
            <dl class="requirement_box">
                <dt>是否助教</dt>
                <dd>
                    <div class="all_requirement">
                        <div class="select_require">
                            <span class="active"><a href="#">不限</a></span>
                            <span><a href="#">助教</a></span>
                            <span><a href="#">非助教</a></span>
                        </div>
                    </div>
                </dd>
            </dl>
            <dl class="requirement_box">
                <dt>门户展示</dt>
                <dd>
                    <div class="all_requirement">
                        <div class="select_require">
                            <span class="active"><a href="#">不限</a></span>
                            <span><a href="#">展示</a></span>
                            <span><a href="#">不展示</a></span>
                        </div>
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
            <p class="show_num">
                每页显示
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
            </p>
            <a href="javascript:;">删除</a>
            <a href="javascript:;">设置入学年份</a>
            <a href="javascript:;">批量设置专业</a>
            <a href="javascript:;">设置为助教</a>
        </div>
        <div id="OmGrid1" style="width: 960px; z-index: 999">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 10px" />
                            <col style="width: 30px" />
                            <col style="width: 70px" />
                            <col style="width: 70px" />
                            <col style="width: 70px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 70px" />
                            <col style="width: 75px" />
                            <col style="width: 75px" />
                        </colgroup>
                        <tr>
                            <td></td>
                            <td><input type="checkbox" name="ckbAll" onclick="selectAll()" /></td>
                            <td>教工号</td>
                            <td>姓名</td>
                            <td>用户名</td>
                            <td>教学组织</td>
                            <td>专业</td>
                            <td>是否锁定</td>
                            <td>是否注册用户</td>
                            <td>操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td></td>
                        <td>
                            <input id='<%# Eval("UserID")%>' type="checkbox"></td>
                        <td><%# Eval("UserNo")%></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("LoginName")%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td><%# Eval("IsLocked")%></td>
                        <td><%# Eval("IsRegister")%></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' pid='<%#Eval("UserID")%>' onclick='Del(this)' class='icon_admin delete_btn'></i>
                            </p>
                        </td>

                    </tr>
                </ItemTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="page_wrap">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
            </webdiyer:AspNetPager>
        </div>
    </div>
</asp:Content>

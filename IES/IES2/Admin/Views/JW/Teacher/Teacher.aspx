<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Teacher.aspx.cs" Inherits="Admin.Views.JW.Teacher.Teacher" %>

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
            <!--<a class="filter_btn" href="javascript:;">筛选</a>-->
            <div class="class_operation">
                <a href="javascript:NoOpen();"><i class="icon_admin daoru"></i>导入行政班</a>
                <ul>
                    <a  href="teacher.xls"><i class="icon_admin download_btn"></i>模板下载</a>
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>导出行政班</a>
                </ul>
            </div>
            <a class="add_student" href="javascript:ADD();" style="margin-left: 10px"><i class="icon_admin add_btn"></i>新增教师</a>
            <p class="search_btn">
                <input type="text" id="txtKey" runat="server" placeholder="输入姓名、工号搜索教师"><a runat="server" style="float:right;position:relative;margin-right:3px; margin-top:-23px;z-index:999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>
        <div class="select_requirement_box">
            <asp:Repeater runat="server" ID="rptorg" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>所属机构</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="orgParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a  href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("OrganizationID")%>"><a  href="#"><%# Eval("OrganizationName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <dl class="requirement_box">
                <dt>身份</dt>
                <dd>
                    <div class="all_requirement">
                        <div id="IsInSchool" class="select_require">
                            <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                            <span onclick="Gbclass(this)" id="1"><a href="#">校内</a></span>
                            <span onclick="Gbclass(this)" id="0"><a href="#">校外</a></span>
                        </div>
                    </div>
                </dd>
            </dl>
            <dl class="requirement_box">
                <dt>是否助教</dt>
                <dd>
                    <div class="all_requirement">
                        <div id="IsAssistant" class="select_require">
                            <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                            <span onclick="Gbclass(this)" id="1"><a href="#">助教</a></span>
                            <span onclick="Gbclass(this)" id="0"><a href="#">非助教</a></span>
                        </div>
                    </div>
                </dd>
            </dl>
            <dl class="requirement_box">
                <dt>门户展示</dt>
                <dd>
                    <div class="all_requirement">
                        <div id="IsShow" class="select_require">
                            <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                            <span onclick="Gbclass(this)" id="1"><a href="#">展示</a></span>
                            <span onclick="Gbclass(this)" id="0"><a href="#">不展示</a></span>
                        </div>
                    </div>
                </dd>
            </dl>
            <a class="search_button" runat="server" onclick="ParmSelect()" onserverclick="btnSelect_Click">搜索</a>
            <%--<span class="close_box"><i class="icon_admin shouqi_icon"></i></span>--%>
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>教师列表</h4>
            <span runat="server" id="number" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
            <p class="show_num">
                每页显示
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
            </p>
            <a href="javascript:DelInfo();">删除</a>
            <%--<a href="javascript:;">设置入学年份</a>
            <a href="javascript:;">批量设置专业</a>
            <a href="javascript:;">设置为助教</a>--%>
        </div>
         <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="BatchDel" Style="display: none;" runat="server" Text="Button" OnClick="BatchDel_Click" />
        <asp:HiddenField ID="hfIDS" runat="server" />
        <asp:HiddenField ID="Parms" runat="server" />
        <!--end-->
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
                            <col style="width: 100px"/>
                            <col style="width: 75px" />
                        </colgroup>
                        <tr>
                            <th></th>
                            <th><input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>教工号</th>
                            <th>姓名</th>
                            <th>性别</th>
                            <th>所属机构</th>
                            <th>是否助教</th>
                            <th>身份</th>
                            <th>门户展示</th>
                            <th>可用空间(M)</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id='<%# Eval("UserID")%>'>
                        <td></td>
                        <td><input name="ckbItem"  type="checkbox"></td>
                        <td><%# Eval("UserNo")%></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("Gender").ToString().Trim()=="0"?"女":"男"%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("IsAssistant").ToString().Trim()=="0"?"非助教":"助教"%></td>
                        <td><%# Eval("IsInSchool").ToString().Trim()=="0"?"校外":"校内" %></td>
                        <td><%# Eval("IsShow").ToString().Trim()=="0"?"不展示":"展示"%></td>
                        <td><%# Eval("DiskSize")%></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("UserID")%>' onclick='Del(this.id)' class='icon_admin delete_btn'></i>
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
    <script>
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }

        }
        function DelBatch(ids) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfIDS.ClientID %>").val(ids);
                $("#<%=BatchDel.ClientID %>").click();
            }
        }
        function ParmSelect() {
            var parm = ParmInfo();
            $("#<%=Parms.ClientID %>").val(parm);
        }
        window.onload = function () {
            var s = '<%=Getclass()%>';
        }
    </script>
</asp:Content>

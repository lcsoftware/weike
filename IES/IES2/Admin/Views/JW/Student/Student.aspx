﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Admin.Views.JW.Student.Student" %>

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
            <div class="class_operation">
                <a href="javascript:NoOpen();"><i class="icon_admin daoru"></i>导入学生</a>
                <ul>
                    <a href="Student.xls"><i class="icon_admin download_btn"></i>模板下载</a>
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>导出学生</a>
                </ul>
            </div>
            <a class="add_student" href="javascript:ADD();" style="margin-left:10px"><i class="icon_admin add_btn"></i>新增学生</a>
            <p class="search_btn">
                <input type="text" id="txtKey"  runat="server" placeholder="输入学号、姓名搜索学生"><a runat="server" style="float:right;position:relative;margin-right:3px; margin-top:-23px;" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>
        <div class="select_requirement_box">
            <!--数据绑定-->
            <asp:Repeater runat="server" ID="rptorg" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>所属机构</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="orgParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("OrganizationID")%>"><a href="#"><%# Eval("OrganizationName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                    <a class="fold_btn" href="javascript:;">[更多]</a>
                    </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ID="rptsp" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>专业</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="spParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("SpecialtyID")%>"><a href="#"><%# Eval("SpecialtyName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                    <a class="fold_btn" href="javascript:;">[更多]</a>
                    </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>

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

            <asp:Repeater runat="server" ID="rptety" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>入学年份</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="etyParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("EntryDate")%>"><a href="#"><%# Eval("EntryDate")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <!--数据绑定-->
            <a class="search_button" runat="server" onclick="ParmSelect()" onserverclick="btnSelect_Click">搜索</a>
            <!--<span class="close_box"><i class="icon_admin shouqi_icon"></i></span>-->
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>学生列表</h4>
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
                            <col style="width: 50px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 110px" />
                            <col style="width: 120px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>
                                <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>学号</th>
                            <th>姓名</th>
                            <th>性别</th>
                            <th>所属机构</th>
                            <th>专业</th>
                            <th>是否助教</th>
                            <th>入学年份</th>
                            <th>行政班</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("UserID")%>'>
                        <td></td>
                        <td>
                            <input type="checkbox" name="ckbItem"></td>
                        <td><%# Eval("UserNo")%></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("Gender").ToString().Trim()=="0"?"女":"男"%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td><%# Eval("IsAssistant").ToString().Trim()=="0"?"非助教":"助教" %></td>
                        <td><%# Eval("EntryDate") %></td>
                        <td><%# Eval("ClassName")%></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' onclick='Del(this.id)' id='<%#Eval("UserID")%>' class='icon_admin delete_btn'></i>
                            </p>
                        </td>

                    </tr>
                </ItemTemplate>
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
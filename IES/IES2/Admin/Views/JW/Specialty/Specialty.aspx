<%@ Page Title="专业" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Specialty.aspx.cs" Inherits="Admin.Views.JW.Specialty.Specialty" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="sousuo_box">
        <div class="filter_item">
            <div class="class_operation">
                <a href="Specialty.xls"><i class="icon_admin download_btn"></i>模板下载</a>
                <!--<a href="javascript:NoOpen();"><i class="icon_admin daoru"></i>导入专业</a>
                <ul>
                    
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>导出专业</a>
                </ul>-->
            </div>
            <a class="add_student" href="javascript:ADD();" style="margin-left: 10px"><i class="icon_admin add_btn"></i>新增专业</a>
            <a class="add_student" href="SpecialtyType.aspx?PID=A112" style="margin-left: 10px"><i class="icon_u425"></i>管理学科</a>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="输入编号、名称搜索专业"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -23px;" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
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
            <asp:Repeater runat="server" ID="rptschlen" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>学制</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="schlenParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("SchoolingLength")%>"><a href="#"><%# Eval("SchoolingLength")%>年制</a></span>
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
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>专业列表</h4>
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
                            <col style="width: 10px"></col>
                            <col style="width: 50px"></col>
                            <col style="width: 150px"></col>
                            <col style="width: 220px"></col>
                            <col style="width: 180px"></col>
                            <col style="width: 180px"></col>
                            <col style="width: 100px"></col>
                            <col style="width: 70px"></col>
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>
                                <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>专业编号</th>
                            <th>专业名称</th>
                            <th>学科</th>
                            <th>开设机构</th>
                            <th>学制</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("SpecialtyID")%>'>
                        <td></td>
                        <td>
                            <input name="ckbItem" type="checkbox"></td>
                        <td><%# Eval("SpecialtyNo")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td><%# Eval("SpecialtyTypeName") %></td>
                        <td><%#Eval("OrganizationName") %></td>
                        <td><%#Eval("SchoolingLength") %></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("SpecialtyID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("SpecialtyID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
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

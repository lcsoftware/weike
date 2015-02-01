<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Link.aspx.cs" Inherits="Admin.Views.Portal.Link.Link" %>
<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <script src="../../../Js/admin.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../Edit.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="sousuo_box">
        <div class="filter_item">
            <a class="add_student" href="javascript:ADD();" style="margin-right: 15px"><i class="icon_admin add_btn"></i>新增链接</a>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="输入名称搜索友情链接"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -29px;" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>友情链接</h4>
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
        <!--end-->
        <div id="OmGrid1" style="width: 960px; z-index: 999">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 10px" />
                            <col style="width: 50px" />
                            <col style="width: 290px" />
                            <col style="width: 420px" />
                            <col style="width: 120px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr style="font-size: 14px; font-weight: bold">
                            <th></th>
                            <th>
                                <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>友情链接名称</th>
                            <th>地址</th>
                            <th>点击次数</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id="<%#Eval("LinkID")%>">
                        <td></td>
                        <td>
                            <input type="checkbox" name="ckbItem" /></td>
                        <td>
                            <a style="color: #000000;" href="http://<%#Eval("URL")%>"><%#Eval("Title")%></a></td>
                        <td>
                            <label><%#Eval("URL")%></label></td>
                        <td>
                            <label><%#Eval("Clicks")%></label></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("LinkID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("LinkID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
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
            if (confirm("是否确定删除!") == true)
                $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnInfo.ClientID %>").click();
        }
        function DelBatch(ids) {
            $("#<%=hfIDS.ClientID %>").val(ids);
            $("#<%=BatchDel.ClientID %>").click();
        }
    </script>
</asp:Content>

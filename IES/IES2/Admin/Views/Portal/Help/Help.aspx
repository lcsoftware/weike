<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="Admin.Views.Portal.Help.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <script src="../../../Js/admin.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../Edit.js"></script>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>使用指南</h4>
            <p class="show_num">每页显示<select>
                <option>10</option>
                <option>15</option>
                <option>20</option>
            </select></p>
            <a href="javascript:;">删除</a>
            <a href="javascript:;">编辑</a>
            <a href="javascript:;">添加</a>
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        </div>
   <div id="OmGrid1" style="width:960px;z-index:999">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table class="result_table">
                    <colgroup>
                        <col style="width:50px" />
                        <col style="width:300px" />
                        <col style="width:420px" />
                        <col style="width:120px" />
                        <col style="width:70px" />
                    </colgroup>
                    <tr>
                        <td><input type="checkbox" name="ckbAll" /></td>
                        <td>主题</td>
                        <td>发布时间</td>
                        <td>点击次数</td>
                        <td></td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="item item_row" id="<%#Eval("HelpID")%>">
                    <td>
                        <input type="checkbox" name="ckbItem" /></td>
                    <td>
                        <a href="#"><%#Eval("Title")%></a></td>
                    <td>
                        <label><%#Eval("UpdateTime")%></label></td>
                    <td>
                        <label><%#Eval("Clicks")%></label></td>
                    <td>
                        <p class='operation_box'>
                            <i title='编辑' pid='<%#Eval("HelpID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                            <i title='删除' id='<%#Eval("HelpID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i></p></td>
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
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box" >
                </webdiyer:AspNetPager>
    </div>
    </div>
    <script>
        function Del(id) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnInfo.ClientID %>").click();
        }
    </script>
</asp:Content>

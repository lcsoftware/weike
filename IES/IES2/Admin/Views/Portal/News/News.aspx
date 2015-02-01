<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Admin.Views.Portal.News.News" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <script src="../../../Js/admin.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../Edit.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="sousuo_box">
        <div class="filter_item">
            <a class="add_student" href="javascript:ADD();" style="margin-right:15px"><i class="icon_admin add_btn"></i>新增新闻</a>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="输入主题、内容搜索新闻"><a runat="server" style="float:right;position:relative;margin-right:3px; margin-top:-23px;"  onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>
        <div class="select_requirement_box">
            <dl class="requirement_box">
                <dt>创建时间</dt>
                <dd>
                    <div class="date_time">
                        <p>
                            <input id="BeginTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2015-01-01" type="text">
                        </p>
                        <span>~</span>
                        <p>
                            <input id="EndTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2015-01-31" type="text">
                        </p>
                                            
                    </div>
                </dd>
            </dl>
            <dl class="requirement_box">
            <dt>所属板块</dt>
                <dd>
                        <p>
                            <asp:DropDownList ID="Section" Width="150px" Height="32px" runat="server"></asp:DropDownList>
                        </p>  
                    </dd>
            </dl>  
            <a class="search_button" runat="server" onserverclick="btnSelect_Click">搜索</a>
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>新闻公告</h4>
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
                            <col style="width: 300px" />
                            <col style="width: 270px" />
                            <col style="width: 150px" />
                            <col style="width: 110px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr style="font-size: 14px; font-weight: bold">
                            <th></th>
                            <th>
                                <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>主题</th>
                            <th>创建时间</th>
                            <th>所属板块</th>
                            <th>点击次数</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id="<%#Eval("NewsID")%>">
                        <td></td>
                        <td>
                            <input type="checkbox" name="ckbItem" /></td>
                        <td>
                            <label><%#Eval("Title")%></label></td>
                        <td>
                            <label><%#Eval("CreateDate")%></label></td>
                        <td>
                            <label><%#Eval("SectionName")%></label></td>
                        <td>
                            <label><%#Eval("Clicks")%></label></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("NewsID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("NewsID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
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

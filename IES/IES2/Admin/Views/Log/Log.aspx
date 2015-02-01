<%@ Page Title="操作日志" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="Admin.Views.Log.Log" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Js/G2S.js"></script>
    <script src="../../Js/admin.js"></script>
    <link href="../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../Portal/Edit.js"></script>
    <script src="../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="data_nav_box" style="background:#fff">
        <ul class="data_nav_list">
            <li class="active"><a href="#">用户日志</a></li>
        </ul>
    </div>
    <div class="sousuo_box">
        <div class="filter_item">
            <span style="float: right; font-family: '微软雅黑 Regular', '微软雅黑'; font-weight: 400; font-style: normal; width: 80px; text-align: center; line-height: 40px">高级搜索</span>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="输入用户名、编号搜索日志"><a runat="server" style="float:right;position:relative;margin-right:3px; margin-top:-23px;"  onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>
        <div class="select_requirement_box">
            <dl class="requirement_box">
                <dt>操作时间</dt>
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
            <a class="search_button" runat="server" onserverclick="btnSelect_Click">搜索</a>
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>用户日志</h4>
            <span id="number" runat="server" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
            <p class="show_num">
                每页显示                  
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                        </asp:DropDownList>
            </p>
        </div>
        <div id="OmGrid1" style="width: 970px; z-index: 999">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px" />
                            <col style="width: 120px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                            <col style="width: 200px" />
                            <col style="width: 100px" />
                            <col style="width: 120px" />
                            <col style="width: 100px" />
                            <col style="width: 100px" />
                        </colgroup>
                        <tr style="font-size: 14px; font-weight: bold">
                            <th></th>
                            <th>身份</th>
                            <th>操作帐号</th>
                            <th>姓名</th>
                            <th>操作时间</th>
                            <th>功能模块</th>
                            <th>操作类型</th>
                            <th>用户IP</th>
                            <th>联系电话</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id="<%#Eval("ID")%>">
                        <td></td>
                        <td style="line-height: 18px">
                            <%# GetUser(Eval("UserType").ToString()) %></td>
                        <td>
                            <label><%#Eval("LoginName")%></label></td>
                        <td>
                            <%#Eval("UserName")%></td>
                        <td>
                            <label><%#Eval("Date")%></label></td>
                        <td>
                            <label><%#Eval("ModName")%></label></td>
                        <td>
                            <%#Eval("ActName")%></td>
                        <td>
                            <label><%#Eval("IP")%></label></td>
                        <td>
                            <label><%#Eval("Mobile")%></label></td>
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
</asp:Content>

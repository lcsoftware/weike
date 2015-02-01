<%@ Page Title="操作日志" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="EditLog.aspx.cs" Inherits="Admin.Views.Log.EditLog" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Js/G2S.js"></script>
    <script src="../../Js/admin.js"></script>
    <link href="../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../Portal/Edit.js"></script>
    <script src="../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li><a href="Log.aspx">登录日志</a></li>
            <li class="active"><a href="#">操作日志</a></li>
        </ul>
    </div>
    <div id="editlog" style="width:998px">
    <div class="search_result_box">
        <div class="result_tit">
            <h4>用户操作日志</h4>
            <span id="number" runat="server" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
            <span style="float: right; font-family: '微软雅黑 Regular', '微软雅黑'; font-weight: 400; font-style: normal; width: 80px; text-align: center; line-height: 40px">高级搜索</span>
            <p class="search_btn">
                <input type="text" placeholder="输入帐号"><i class="icon_admin search_icon"></i>
            </p>
        </div>
        <div style="padding: 25px 0px 0px 0px; overflow: hidden; clear: both; position: relative;">
            
                <div>    
            <span style="float: left; line-height: 30px">操作对象:</span>
                <p style="float: left; margin-left: 10px">
                    <input style="width: 180px; height: 30px; line-height: 30px\9;" id="OperationObj" runat="server" type="text" />
                </p>
            
                <span style="float: left; line-height: 30px;margin-left:40px;">功能模块:</span>  
                <p style="float: left; margin-left: 10px">
                    <input style="width: 180px; height: 30px; line-height: 30px\9;" id="Module" runat="server" type="text" />
                </p>  

                <span style="float: left; line-height: 30px;margin-left:40px;">操作时间:</span>
                <div class="date_time">
                    <p style="margin-left: 10px">
                        <input id="BeginTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2015-01-01" type="text">
                    </p>
                    <span>-</span>
                    <p>
                        <input id="EndTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2015-01-31" type="text">
                    </p>
                </div>
                </div>
                
                <div style="margin-top:25px">
                <span style="float: left; line-height: 30px">操作帐号:</span>
                <p style="float: left; margin-left: 10px">
                    <asp:DropDownList ID="OperationNum" Width="185px" Height="34px" runat="server"></asp:DropDownList>
                </p>

                <span style="float: left; line-height: 30px;margin-left:40px;">操作类型:</span>
                <p style="float: left; margin-left: 10px">
                    <asp:DropDownList ID="OperationType" Width="185px" Height="34px" runat="server"></asp:DropDownList>
                </p>
                    </div>
           
            
                
            </div>
        <a class="select_btn" runat="server" onserverclick="btnSelect_Click">搜索</a>
        </div>
        <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="hfID" runat="server" />
        <!--end-->
        <div id="OmGrid1" style="width: 970px; z-index: 999">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px" />
                            <col style="width: 130px" />
                            <col style="width: 190px" />
                            <col style="width: 140px" />
                            <col style="width: 130px" />
                            <col style="width: 130px" />
                            <col style="width: 230px" />
                        </colgroup>
                        <tr style="font-size:14px;font-weight:bold">
                            <td></td>
                            <td>操作帐号</td>
                            <td>操作时间<i class="icon_admin rank_btn"/></td>
                            <td>操作对象</td>
                            <td>功能模块</td>
                            <td>操作类型</td>
                            <td>操作内容</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id="<%#Eval("ID")%>">
                        <td></td>
                        <td>
                            <a href="#"><%#Eval("LoginName")%></a></td>
                        <td>
                            <label><%#Eval("Date")%></label></td>
                        <td>
                            <label><%#Eval("LoginName")%></label></td>
                        <td>
                            <a href="#"><%#Eval("ModName")%></a></td>
                        <td>
                            <label><%#Eval("ActName")%></label></td>
                        <td>
                            <label><%#Eval("Date")%></label></td>
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

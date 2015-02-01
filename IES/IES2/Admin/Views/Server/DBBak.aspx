<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DBBak.aspx.cs" Inherits="Admin.Views.Server.DBBak" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">

        <div class="tip_box" style="margin: 10px 20px 10px 20px">
            <p>数据库的备份数据默认存储路径为：【数据库服务器】D:/CC Backup，建议定期将最新的备份文件拷贝到其他存储设备上，以防止设备故障造成备份文件丢失！</p>
        </div>
        <div class="result_tit">
            <h4 style="margin-left: 25px">数据库备份</h4>
        </div>
    </div>

    <div class="search_result_box" style="padding: 0;">
        
        <div style="height: 40px; width: 100%; padding-top: 5px; background: #ccc">
            <div style="margin-left: 25px; width: 600px;line-height:40px;float:left">

                <input id="BeginTime" style="height:30px" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" type="text">

                <span style="line-height:40px;height:40px">~</span>

                <input id="EndTime" style="height:30px" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" type="text">
            </div>
            <div style="float: left;width:350px;margin-top:-3px">
                <a class="add_student" style="background: #185e87; padding-left: 5px; width: 100px; color: #fff; margin-right: 0px" href="javascript:PageTeacher();"><i class="icon_admin add_btn"></i>设置自动备份</a>
                <a class="add_student" style="background: #185e87; padding-left: 5px; width: 100px; color: #fff;" href="javascript:PageTeacher();"><i class="icon_admin add_btn"></i>手动备份</a>            
            </div>


        </div>

        <div id="OmGrid1" style="width: 1000px; z-index: 999;">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px" />
                            <col style="width: 190px" />
                            <col style="width: 260px" />
                            <col style="width: 90px" />
                            <col style="width: 90px" />
                            <col style="width: 90px" />
                            <col style="width: 90px" />
                            <col style="width: 110px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>备份文件</th>
                            <th>数据库</th>
                            <th>备份内容</th>
                            <th>数据量</th>
                            <th>文件大小</th>
                            <th>任务</th>
                            <th>备份日期</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("UserID")%>'>
                        <td></td>
                        <td><%# Eval("UserNo")%></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("Gender")%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
                        <td>
                            <p class='operation_box'>
                                <i title='下载' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin xia_btn'></i>
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
        <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" />
        <asp:HiddenField ID="hfID" runat="server" />

        <!--end-->

    </div>
</asp:Content>

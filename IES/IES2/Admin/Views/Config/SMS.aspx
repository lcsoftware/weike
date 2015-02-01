<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="Admin.Views.Config.SMS" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <script src="Config.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">
    </div>

    <div class="search_result_box" style="padding:0;margin-top:5px">
        <div class="result_tit" >
            <h4 style="margin-left:25px">短信权限用户</h4>
            <a class="add_student" style="background: #185e87;padding-left:5px;width:100px; color: #fff;margin-right:20px" href="javascript:PageTeacher();"><i class="icon_admin add_btn"></i>新增权限用户</a>
        </div>
        <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="hfID" runat="server" />

        <!--end-->
        <div id="OmGrid1" style="width: 1000px; z-index: 999;">
            <div style="height: 40px; width: 100%; background: #ccc">
                <span style="line-height: 40px; float: left;margin-left:25px">总计已发送短信数：
                    <label id="znum" runat="server" style="color: red"></label>
                    ，剩余短信：<label id="snum" style="color: green" runat="server"></label>，剩余数为0时，短信功能将被封闭，请及时找客服增加短信条数</span>
                <input id="Key" style="width: 300px; height: 30px; position: absolute; left: 678px; top: 146px;" runat="server" type="text" placeholder="请输入“姓名”关键字">
                <a runat="server" style="float: right; position: absolute; left: 958px; top: 153px" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </div>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px" />
                            <col style="width: 120px" />
                            <col style="width: 120px" />
                            <col style="width: 200px" />
                            <col style="width: 120px" />
                            <col style="width: 310px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>工号</th>
                            <th>姓名</th>
                            <th>授权日期</th>
                            <th>已发送短信数</th>
                            <th>状态</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("UserID")%>'>
                        <td></td>
                        <td><%# Eval("UserNo")%></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("Gender").ToString().Trim()=="0"?"女":"男"%></td>
                        <td><%# Eval("OrganizationName")%></td>
                        <td><%# Eval("SpecialtyName")%></td>
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

        
    </div>
    <script>
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }
        }
    </script>
</asp:Content>

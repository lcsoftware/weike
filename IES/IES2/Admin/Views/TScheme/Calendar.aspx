<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="Admin.Views.TScheme.Calendar" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <script src="../Portal/Edit.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="sousuo_box">
        <div class="filter_item">
            <a class="add_student" href="javascript:CalendarADD();" style="margin-left: 10px"><i class="icon_admin add_btn"></i>新增校历</a>
            <p class="search_btn">
                <input id="Key" runat="server" type="text" placeholder="输入学年、学期进行搜索"><a runat="server" onserverclick="btnSelect_Click" style="float: right; position: relative; margin-right: 3px; margin-top: -23px;" class="icon_admin search_icon"></a>
            </p>
        </div>
    </div>
    <div class="search_result_box">
        <div class="result_tit">
            <h4>校历列表</h4>
            <span runat="server" id="number" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
        </div>
        <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="hfID" runat="server" />
        <!--end-->

        <div id="OmGrid1" style="width: 960px; z-index: 999">
            <asp:Repeater ID="rptypelist" runat="server" OnItemDataBound="rptypelist_ItemDataBound">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px"></col>
                            <col style="width: 200px"></col>
                            <col style="width: 200px"></col>
                            <col style="width: 200px"></col>
                            <col style="width: 200px"></col>
                            <col style="width: 140px"></col>
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>学年</th>
                            <th>学期</th>
                            <th>开始日期</th>
                            <th>结束日期</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("TermID")%>'>
                        <td></td>
                        <td><%# Eval("TermYear")%>学年</td>
                        <td><%# Eval("TermTypeName")%></td>
                        <td><%# Eval("StartDate","{0:yyyy-MM-dd}") %></td>
                        <td><%#Eval("EndDate","{0:yyyy-MM-dd}") %></td>
                        <td>
                            <p class='operation_box'>
                                <i title='详细' id='<%#Eval("TermID")%>' onclick="showhide(this.id,this)" class='icon_admin open_btn'></i>
                                <i title='编辑' pid='<%#Eval("TermID")%>' onclick='CalendarEdit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("TermID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                            </p>
                        </td>
                    </tr>
                    <tr id="son<%#Eval("TermID")%>" style="display: none;border-bottom:1px solid #ccc">
                        <td></td>
                        <td colspan="5">
                            <asp:Repeater runat="server" ID="rpquestionlist">
                                <ItemTemplate>
                                    <div style="width: 110px; float: left; text-align: center">
                                        <p>
                                            <label><%# Eval("LessonName")%></label></p>

                                        <p>
                                            <label><%# Eval("StartTime")%> -<%# Eval("StartTime")%></label></p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr style="display:none"><td colspan="6"></td></tr>
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
        function showhide(id, obj) {
            var jq = $("#son" + id);
            var temp = jq.is(":hidden");//是否隐藏
            if (temp == true) {
                jq.show();
                obj.className = 'icon_admin clses_btn';
            }
            else {
                jq.hide();
                obj.className = 'icon_admin open_btn';
            }
        }
    </script>
</asp:Content>

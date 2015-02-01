<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Term.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_box">
        <div class="info_box">
            <h5>校历信息</h5>
            <div class="info_detail_box">
                <div class="info_detail">
                    <div class="info_detail_list">
                        <label>学年：</label>
                        <div class="fill_box">
                            <input id="TermYear" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TermYear" ErrorMessage="RequiredFieldValidator" ForeColor="Red">不能为空</asp:RequiredFieldValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>学期：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Termn" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Termn" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>开始日期：</label>
                        <div class="fill_box">
                            <input id="BeginTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2014-01-01" type="text">
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>结束日期：</label>
                        <div class="fill_box">
                            <input id="EndTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="2015-01-31" type="text">
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 728px; height: 460px">
                        <label>课节列表：</label>
                        <div class="fill_box" style="width: 620px">
                            <table id="TermTable" class="result_table">
                                <colgroup>
                                    <col style="width: 20px"/>
                                    <col style="width: 150px"/>
                                    <col style="width: 150px"/>
                                    <col style="width: 150px"/>
                                    <col style="width: 170px"/>
                                </colgroup>
                                <tr>
                                    <th></th>
                                    <th>课节名称</th>
                                    <th>开始时间</th>
                                    <th>结束时间</th>
                                    <th></th>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr id="<%#Eval("LessonID")%>" class="item item_row">
                                            <td></td>
                                            <td><%# Eval("LessonName")%></i></td>
                                            <td><%# Eval("StartTime")%></td>
                                            <td><%# Eval("EndTime")%></td>
                                            <td>
                                                <p class='operation_box'>
                                                    <i title='编辑' pid='<%#Eval("LessonID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                                    <i title='删除' id='<%#Eval("LessonID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                                                </p>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr class="item item_row" style="display: none" id="shuru">
                                    <td></td>
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0 0 0 0" runat="server" id="termname" type="text" /></td>
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0 0 0 0" runat="server" id="stime" type="text" /></td>
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0 0 0 0" runat="server" id="etime" type="text" /></td>
                                    <td><a class="wod_btn" href="javascript:DataAdd();">确定</a>
                                        <a class="wrt_btn" href="javascript:ItemDel();">取消</a></td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: 108px"><a id="ts" style="color: #000; line-height: 18px; font-size: 13px" href="javascript:ItemAdd();"><i class="icon_construction add_contdt" style="vertical-align: middle; margin: 2px 5px 5px 5px"></i></a></div>
                    </div>

                </div>
            </div>
        </div>
        <div style="width: 940px">
            <a class="wod_btn" runat="server" onserverclick="update_Click">确定</a>
            <a class="wrt_btn" runat="server" onserverclick="cancel_Click">取消</a>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:Button ID="btnAdd" Style="display: none;" runat="server" Text="Button" OnClick="btnAdd_Click" />
    </div>
    <script>
        function Del(id) {
            if (confirm("是否确定删除!") == true)
                $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnInfo.ClientID %>").click();
        }
        function ItemAdd() {
            var tr = $("#shuru");
            var temp = tr.is(":hidden");
            if (temp == true) {
                tr.show();
            }

        }
        function ItemDel() {
            $("#shuru").hide();
        }
        function DataAdd() {
            $("#<%=btnAdd.ClientID %>").click();
        }
    </script>
</asp:Content>

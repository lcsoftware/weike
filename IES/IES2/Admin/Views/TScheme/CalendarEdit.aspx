<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalendarEdit.aspx.cs" Inherits="Admin.Views.TScheme.CalendarEdit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Js/G2S.js"></script>
    <link href="../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../Js/admin.js"></script>
    <script src="../Portal/Edit.js"></script>
    <script src="../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <style>
        .wod_btn {
            padding: 2px 10px 2px 10px;
            height: 20px;
            width: 60px;
            background: #284a51;
            line-height: 30px;
            color: #fff;
            text-align: center;
            border: 1px solid #ccc;
        }

            .wod_btn:hover {
                background: #233f45;
            }

        .wrt_btn {
            padding: 2px 10px 2px 10px;
            height: 20px;
            width: 60px;
            background: #f2f2f2;
            line-height: 30px;
            color: #333;
            text-align: center;
            border: 1px solid #ccc;
        }

            .wrt_btn:hover {
                background: #f8f8f8;
            }
    </style>
    <uc1:Nav runat="server" ID="Nav" />
    <div class="sousuo_box"></div>
    <div class="data_box">
        <div class="info_box">
            <h5>校历信息</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="height:600px;">
                    <div class="info_detail_list">
                        <label>学年：</label>
                        <div class="fill_box">
                            <input id="TermYear" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TermYear" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
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
                    <div class="info_detail_list" style="width: 728px; height: 380px">
                        <label>课节列表：</label>
                        <div class="fill_box" style="width: 562px; height: 330px; overflow-y: scroll; background: #fff">
                            <table id="TermTable" style='border: 1px solid #ccc; line-height: 30px; width: 544px'>
                                <colgroup>
                                    <col style="height: 30px; width: 160px" />
                                    <col style="height: 30px; width: 140px" />
                                    <col style="height: 30px; width: 140px" />
                                    <col style="height: 30px; width: 102px" />
                                </colgroup>
                                <tr style='background: #ccc'>
                                    <th style="text-align: left; text-indent: 1em; border-right: 1px solid #fff">课节名称</th>
                                    <th style="text-align: left; text-indent: 1em; border-right: 1px solid #fff">开始时间</th>
                                    <th style='text-align: left; text-indent: 1em'>结束时间</th>
                                    <th></th>
                                </tr>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <tr id="<%#Eval("LessonID")%>" style='border-top: 1px solid #ccc'>
                                            <td style='text-indent: 1em'>
                                                <input id="ln<%# Eval("LessonID")%>" name="ln<%#Eval("LessonID")%>" type="text" style="border: 0; width: 110px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%# Eval("LessonName")%>"></td>
                                            <td style='text-indent: 1em'>
                                                <input id="st<%# Eval("LessonID")%>" name="st<%#Eval("LessonID")%>" type="text" style="border: 0; width: 110px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%# Eval("StartTime")%>"></td>
                                            <td style='text-indent: 1em'>
                                                <input id="et<%# Eval("LessonID")%>" name="et<%#Eval("LessonID")%>" type="text" style="border: 0; width: 110px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%# Eval("EndTime")%>"></td>
                                            <td>
                                                <p id="ee<%#Eval("LessonID")%>" class='operation1_box'>
                                                    <i title='编辑' id='<%#Eval("LessonID")%>' onclick='EditLes(this.id)' class='icon_admin edit_btn'></i>
                                                    <i title='删除' id='<%#Eval("LessonID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                                                </p>
                                                <p id="ss<%#Eval("LessonID")%>" class='operation2_box' style="display: none">
                                                    <a class="wod_btn" id="<%#Eval("LessonID")%>" onclick="LessUpd(this.id)" href="#">确定</a>
                                                    <a class="wrt_btn" id="<%#Eval("LessonID")%>" onclick="CzShowHide(this.id)" href="#">取消</a>
                                                </p>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr class="item item_row" style="display: none" id="shuru">
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0" placeholder="例:第一节" runat="server" id="termname" type="text" /></td>
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0" placeholder="例:08:08:08" onkeyup="this.value=this.value.replace(/[^0-9-+:]+/,'');" runat="server" id="stime" type="text" /></td>
                                    <td>
                                        <input style="width: 100px; height: 30px; margin: 0" placeholder="例:08:08:08" onkeyup="this.value=this.value.replace(/[^0-9-+:]+/,'');" runat="server" id="etime" type="text" /></td>
                                    <td><a class="wod_btn" href="javascript:DataAdd();">确定</a>
                                        <a class="wrt_btn" href="javascript:ItemDel();">取消</a></td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-left: 70px; float: left"><a id="ts" style="color: #000; line-height: 18px; font-size: 13px" href="javascript:ItemAdd();"><i class="icon_admin addss_btn" style="vertical-align: middle; margin: 2px 5px 5px 5px"></i></a></div>
                        
                    </div>
                    <div style="width: 440px; margin-left: 250px;">
                            <a class="wod_btn" style="padding: 7px 30px 6px 30px" runat="server" onserverclick="update_Click">确定</a>
                            <a class="wrt_btn" style="padding: 7px 30px 6px 30px; margin-left: 10px" href="Calendar.aspx?PID=A135">取消</a>
                        </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:Button ID="btnAdd" Style="display: none;" runat="server" Text="Button" OnClick="btnAdd_Click" />
        <asp:Button ID="btnUpd" Style="display: none;" runat="server" Text="Button" OnClick="btnUpd_Click" />
    </div>
    <script>
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }
        }
        function LessUpd(id) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnUpd.ClientID %>").click();
        }
        function EditLes(id) {
            CzShowHide(id);
            var les = document.getElementById("ln" + id);
            var stm = document.getElementById("st" + id);
            var etm = document.getElementById("et" + id);
            les.readOnly = false;
            les.style.borderStyle = "solid";
            les.style.borderColor = "#3794ed";
            les.style.borderWidth = "1px";
            les.style.background = "#fff";
            stm.readOnly = false;
            stm.style.borderStyle = "solid";
            stm.style.borderColor = "#3794ed";
            stm.style.borderWidth = "1px";
            stm.style.background = "#fff";
            etm.readOnly = false;
            etm.style.borderStyle = "solid";
            etm.style.borderColor = "#3794ed";
            etm.style.borderWidth = "1px";
            etm.style.background = "#fff";
        }
        function CzShowHide(i) {
            var ee = $("#ee" + i);
            var ss = $("#ss" + i);
            var temp = ee.is(":hidden");
            if (temp == true) {
                ee.show();
                ss.hide();
                var les = document.getElementById("ln" + i);
                var stm = document.getElementById("st" + i);
                var etm = document.getElementById("et" + i);
                les.readOnly = true;
                les.style.borderWidth = "0px";
                les.style.background = "transparent";
                stm.readOnly = true;
                stm.style.borderWidth = "0px";
                stm.style.background = "transparent";
                etm.readOnly = true;
                etm.style.borderWidth = "0px";
                etm.style.background = "transparent";
            }
            else {
                ee.hide();
                ss.show();
            }
        }
        function ItemAdd() {
            var tr = $("#shuru");
            var temp = tr.is(":hidden");
            if (temp == true) {
                $("#MainContent_termname").val("");
                $("#MainContent_stime").val("");
                $("#MainContent_etime").val("");
                tr.show();
            }

        }
        function ItemDel() {
            $("#MainContent_termname").val("");
            $("#MainContent_stime").val("");
            $("#MainContent_etime").val("");
            $("#shuru").hide();
        }
        function DataAdd() {
            $("#<%=btnAdd.ClientID %>").click();
        }
        window.onload = function () {
            init();
        }
        //加载样式
        function init() {

            $('#TermTable tr').hover(function () {
                var dm = $(this).find('.operation2_box');
                if (dm.is(":hidden") == true) {
                    $(this).find('.operation1_box').toggle();
                }
            })
            $('#TermTable').each(function () {
                $(this).find('tr:odd').css('background', '#f7f7f7');
            })
        }
    </script>
    <style>
        .operation1_box {
            display: none;
            height: 30px;
            overflow: hidden;
            zoom: 1;
        }
            .operation1_box i {
                float: left;
                margin: 6px 10px 0 0;
                cursor: pointer;
            }
    </style>
</asp:Content>

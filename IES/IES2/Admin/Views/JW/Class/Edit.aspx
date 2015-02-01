<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Class.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../Portal/Edit.js"></script>
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="Call.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>

    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_box">
        <div class="info_box">
            <h5>基本信息</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="width: 900px">
                    <div class="info_detail_list" >
                        <label style="width:80px">行政班编号：</label>
                        <div class="fill_box">
                            <input id="ClassNo" runat="server" class="fill_box" />
                        </div><label style="color: red; width: 5px">*</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ClassNo" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                    </div>
                    <div class="info_detail_list">
                        <label style="width:80px">行政班名称：</label>
                        <div class="fill_box">
                            <input id="ClassName" runat="server" class="fill_box" />
                        </div><label style="color: red; width: 5px">*</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ClassName" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                    </div>
                    <div class="info_detail_list">
                        <label style="width:80px">所属机构：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Organization" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div><label style="color: red; width: 5px">*</label>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Organization" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                        </div>
                    <div class="info_detail_list">
                        <label style="width:80px">所属专业：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Specialty" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label style="width:80px">创建日期：</label>
                        <div>
                            <input id="CreateTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})" runat="server" value="" type="text">
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 400px">
                        <label style="width:80px">辅导老师：</label>
                        <div class="fill_box">
                            <input id="Teacher" runat="server" class="fill_box"  onclick="AddTeacher()"/><a href="javascript:AddTeacher();" style="text-align: center; font-size: 13px; margin: 55px 0 5px 5px; height: 30px; background: #284a51; color: #fff; padding: 5px 18px 5px 18px"">请选择</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="info_box">
            <h5>行政班学生:</h5>
            <div style="background:#f2f2f2;width:945px;height:510px;border:1px solid #ccc">
                <div style="height:50px;line-height:50px;width:900px;padding:0 24px 0 20px">
                <a class="add_student" style="margin-left: 10px;margin-top:5px" href="javascript:NoOpen();"><i class="icon_admin add_btn"></i>清空列表</a>
                <a class="add_student" style="margin-left: 10px;margin-top:5px" href="javascript:AddStudent();"><i class="icon_admin add_btn"></i>添加学生</a>
                    </div>
                <div id="OmGrid1" style="width: 900px; height: 440px; overflow-y:scroll;margin-left:20px; z-index: 999;border-bottom:1px solid #ccc;border-top:1px solid #ccc">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 20px"></col>
                                <col style="width: 140px"></col>
                                <col style="width: 180px"></col>
                                <col style="width: 470px"></col>
                                <col style="width: 70px"></col>
                            </colgroup>
                            <tr>
                                <th></th>
                                <th>姓名</th>
                                <th>学号</th>
                                <th>所属机构</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("UserNo")%></td>
                            <td><%# Eval("OrganizationName")%></td>
                            <td>
                                <p class='operation_box'>
                                    <i title='删除' id='<%#Eval("UserID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                                </p>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            </div>           
        </div>


    <div style="width: 940px">
        <a class="update_btn" runat="server" onserverclick="update_Click">更新资料</a>
    </div>
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="Students" runat="server" />
        <asp:HiddenField ID="hfIDS" runat="server" />
    </div>
    <script type="text/javascript">
        function ParmMethod() {
            $("#<%=btnInfo.ClientID %>").click();
        }
        var stus = "<%= Students.ClientID%>";
        var teacherid = "<%=Teacher.ClientID %>";
        var hfIDS = "<%=hfIDS.ClientID %>";
        window.onload = function () {
            init();
        }
        //加载样式
        function init() {
            $('.result_table tr').hover(function () {
                $(this).find('.operation_box').toggle();
            })
            $('.result_table').each(function () {
                $(this).find('tr:even').css('background', '#f7f7f7');
            })
        }
    </script>
    <style>
        .result_table{ width:100%; border-bottom:1px solid #e8e8e8;}
        .result_table tr th{ text-align:left; border-bottom:1px solid #e8e8e8;}
        .result_table tr th i{ vertical-align:middle;}
        .result_table tr th,.result_table tr td{ height:42px; line-height:42px; color:#333;}
        .result_table tr th input,.result_table tr td input{ height:42px; vertical-align:middle;}
    </style>
</asp:Content>

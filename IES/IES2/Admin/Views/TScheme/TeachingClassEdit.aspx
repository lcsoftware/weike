<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeachingClassEdit.aspx.cs" Inherits="Admin.Views.TScheme.TeachingClassEdit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Js/jquery-1.8.3.min.js"></script>
    <script src="../../Js/G2S.js"></script>
    <script src="../Portal/Edit.js"></script>
    <script src="../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Frameworks/layer/layer.min.js"></script>
    <script src="Display.js"></script>
    <uc1:Nav runat="server" ID="Nav" />

    <div class="data_box">
        <div class="tip_box" id="noticets">
            <p><i class="icon_admin tip_icon"></i><b>提示信息:</b>请尽量完善以下个人资料，并保证其真实性。*为必填项。</p>
            <span class="close_tip" onclick="Hidets()">×</span>
        </div>

        <div class="info_box">
            <h5>基本信息</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="width:900px;">
                    <div class="info_detail_list" style="width:400px;">
                        <label>教学班编号:</label>
                        <div class="fill_box">
                            <input id="TeachingNo" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>教学班名称：</label>
                        <div class="fill_box">
                            <input id="TeachingName" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list" style="width:400px;">
                        <label>所属机构：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Organization" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                        <label style="color: red; width: 5px">*</label>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="Organization" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                    </div>
                    <div class="info_detail_list" style="width:400px;">
                        <label>教授课程：</label>
                        <div class="fill_box">
                            <input id="TeaCourse" runat="server" class="fill_box" onclick="AddCourse()" /><label style="color: red; width: 5px">*</label><a href="javascript:AddCourse();" style="text-align: center; font-size: 13px; margin: 55px 0 5px 5px; height: 30px; background: #284a51; color: #fff; padding: 5px 18px 5px 18px"">请选择</a>                          
                        </div>
                    </div>

                    <div class="info_detail_list" style="width:400px;">
                        <label>开始时间：</label>
                        <div class="fill_box">
                            <input id="BeginTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})"  runat="server" /><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>结束时间：</label>
                        <div class="fill_box">
                            <input id="EndTime" class="Wdate" onfocus="WdatePicker({isShowClear:false,dateFmt:'yyyy-MM-dd',readOnly:true})"  runat="server"/><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list" style="width:400px;">
                        <label>授课教师：</label>
                        <div class="fill_box">
                            <input id="Teacher" runat="server" class="fill_box"  onclick="AddTeacher()"/><label style="color: red; width: 5px">*</label><a href="javascript:AddTeacher();" style="text-align: center; font-size: 13px; margin: 55px 0 5px 5px; height: 30px; background: #284a51; color: #fff; padding: 5px 18px 5px 18px"">请选择</a>
                        </div>
                    </div>
                    <div class="info_detail_list" style="width:400px;">
                        <label>共建教师：</label>
                        <div class="fill_box">
                            <input id="BuidlTeacher" runat="server" class="fill_box" /><%--<a href="javascript:AddTeacher();" style="text-align: center; font-size: 13px; margin: 55px 0 5px 20px; height: 30px; background: #284a51; color: #fff; padding: 5px 18px 5px 18px"">请选择</a>--%>
                        </div>
                    </div>                   
                </div>
            </div>
        </div>

        <div class="info_box">
            <h5>学生列表</h5>
            <span runat="server" id="number" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
            <div class="filter_item" style="border-bottom: none">
                <a class="add_student" style="margin-left: 10px;" href="javascript:NoOpen();"><i class="icon_admin add_btn"></i>清空列表</a>
                <a class="add_student" style="margin-left: 10px" href="javascript:NoOpen();"><i class="icon_admin add_btn"></i>导入学生</a>
                <a class="add_student" style="margin-left: 10px" href="javascript:AddStudent();"><i class="icon_admin add_btn"></i>添加学生</a>
            </div>
            <div id="OmGrid1" style="width: 960px; height: 460px; overflow: auto; z-index: 999">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 20px"></col>
                                <col style="width: 200px"></col>
                                <col style="width: 200px"></col>
                                <col style="width: 490px"></col>
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
                            <td><%# Eval("UserNo")%></i></td>
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

        <div style="width: 940px">
            <a class="update_btn" runat="server" onserverclick="update_Click">更新资料</a>
        </div>
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="Students" runat="server" />
        <asp:HiddenField ID="hfIDS" runat="server" />
        <asp:HiddenField ID="CourIDs" runat="server" />
    </div>
    <script type="text/javascript">
      function ParmMethod() {
           $("#<%=btnInfo.ClientID %>").click();
        }
        var stus = "<%= Students.ClientID%>";
        <%--var Method = "<%= btnInfo.ClientID%>";--%>
        var teacherid = "<%=Teacher.ClientID %>";
        var hfIDS = "<%=hfIDS.ClientID %>";
        var teacourseid = "<%= TeaCourse.ClientID%>";
        var CourIDs = "<%= CourIDs.ClientID%>";
    </script>
</asp:Content>

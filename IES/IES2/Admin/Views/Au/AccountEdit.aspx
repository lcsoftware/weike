<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountEdit.aspx.cs" Inherits="Admin.Views.Au.AccountEdit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Frameworks/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="../Portal/Edit.js"></script>
    <script src="../../Frameworks/zTree_v3/js/jquery.ztree.all-3.5.js"></script>
    <script src="../../Frameworks/layer/layer.min.js"></script>
    <script src="TreeView.js"></script>
    <script src="Account.js"></script>
    <script src="../../Frameworks/My97DatePicker/WdatePicker.js"></script>

    <style>
        .ztree li span.button.add {
            margin-left: 2px;
            margin-right: -1px;
            background-position: -144px 0;
            vertical-align: top;
            *vertical-align: middle;
        }

        ul.ztree {
            /*border: 1px solid #ccc;*/
            width: 350px;
            height: 290px;
            overflow-y: scroll;
            overflow-x: auto;
        }

            ul.ztree li {
                overflow: visible;
                text-overflow: visible;
                display: block;
                clear: both;
            }

            ul.ztree ul {
                height: auto;
            }

        .ztree li {
            line-height: 40px;
        }
    </style>
    <uc1:Nav runat="server" ID="Nav" />

    <div class="data_box">

        <div class="info_box">
            <h5>基本信息</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="width: 900px;">
                    <div class="info_detail_list" style="width: 100%;">
                        <label>用户名：</label>
                        <div id="Oname" runat="server" class="fill_box">
                            <input id="LoginName" runat="server" class="fill_box" />
                            <label style="color: red; width: 5px">*</label>
                            <label style="width: 200px;">(用户名不支持修改,请仔细考虑)</label>
                        </div>
                        <div id="Tname" runat="server" style="display: none;">
                            <asp:Label ID="LabName" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 100%;">
                        <label>密码：</label>
                        <div id="Opwd" runat="server" class="fill_box">
                            <input id="Pwd" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                        <div id="Upwd" runat="server" style="display:none;">
                            <a href="javascript:;" onclick="ShowPwd()">修改密码</a>
                        </div>
                    </div>
                    <div id="Tpwd" class="info_detail_list" runat="server" style="width: 100%;">
                        <label>确认密码：</label>
                        <div class="fill_box">
                            <input id="Password" type="password" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Pwd" ControlToValidate="password" ErrorMessage="CompareValidator" ForeColor="Red">两次密码不一致</asp:CompareValidator>
                        </div>
                    </div>

                    <div class="info_detail_list" style="width: 100%;">
                        <label>姓名：</label>
                        <div class="fill_box">
                            <input id="UserName" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                            <asp:RadioButton ID="RadioButton1" GroupName="sex" runat="server" Checked="true" />男&nbsp;&nbsp;
                            <asp:RadioButton ID="RadioButton2" GroupName="sex" runat="server" />女
                        </div>
                    </div>

                    <div class="info_detail_list" style="width: 900px; height: 350px;">
                        <label>账号身份：</label>
                        <div>
                            <table style="border: 1px solid #999999;">
                                <tr style="border: 1px solid #999999;">
                                    <td style="font-weight: bold; padding-left: 20px; width: 200px; border-right: #999999 1px solid;">身份
                                    </td>
                                    <td style="font-weight: bold; padding-left: 20px; width: 350px;">组织机构
                                    </td>
                                </tr>
                                <tr style="border: 1px solid #999999;">
                                    <td style="width: 200px; height: 300px; border-right: #999999 1px solid;">
                                        <ul style="margin-top: -100px; margin-left: 20px;">
                                            <li>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;教师                                                
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />&nbsp;学生                                                
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="CheckBox3" runat="server" />&nbsp;管理员                                                
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="CheckBox4" runat="server" />&nbsp;子管理员
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="CheckBox5" runat="server" />&nbsp;系统外用户
                                            </li>
                                        </ul>
                                    </td>
                                    <td>
                                        <div style="margin-left: 15px; height: 300px;">
                                            <ul id="treeDemo" class="ztree organization_list"></ul>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="info_box">
            <h5>其他</h5>
            <div class="info_detail_box">
                <div class="info_detail" style="width: 800px;">
                    <div class="info_detail_list" style="width: 100%;">
                        <label>工号/学号：</label>
                        <div class="fill_box">
                            <input id="UserNo" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 100%;">
                        <label>个人手机：</label>
                        <div class="fill_box">
                            <input id="Tel" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div class="info_detail_list" style="width: 100%;">
                        <label>个人邮箱：</label>
                        <div class="fill_box">
                            <input id="Email" runat="server" class="fill_box" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div style="width: 940px; text-align: center;">
            <a class="update_btn" runat="server" onserverclick="Update_Click">保存</a>
            <a class="update_btn" runat="server" onserverclick="Cancel_Click">取消</a>
        </div>
        <div style="width: 600px; display: none; height: 240px; overflow-y: auto;" id="UpdatePwd">
            <div class="wrap_box" style="margin: 0;">
                <div class="add_box">
                    <p>新密码</p>
                    <dl class="add_item">
                        <dd>
                            <div class="num_box">
                                <input type="text" style="width: 300px;" name="OnePwd" value="">
                                <span>*</span>
                                <p style="display: none;" id="NoMsg"><i class="icon_admin wrong_icon"></i>请输入密码</p>
                            </div>
                        </dd>
                    </dl>
                    <dl class="add_item num_box">
                        <dd>
                            <input type="text" style="width: 300px;" name="TwoPwd">
                            <span>*</span>
                            <p style="display: none;" id="NameMsg"><i class="icon_admin wrong_icon"></i>请输入密码</p>
<%--                            <p style="display: none;" id="NameMsg"><i class="icon_admin wrong_icon"></i>两次密码不一致</p>--%>
                        </dd>
                    </dl>
                </div>
            </div>
            <div class="out_wrap">
                <div class="btn_box" style="border: 0;">
                    <a class="save" href="javascript:;" onclick="AddOrEdit()">确定</a>
                    <a class="cancel" href="javascript:;" onclick="layer.closeAll();">取消</a>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="UserID" runat="server" Value="1" />
    </div>
</asp:Content>
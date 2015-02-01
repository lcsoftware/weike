<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.User.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Portal/Edit.js"></script>

    <uc1:Nav runat="server" ID="Nav" />

    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li id="xx" class="active"><a href="javascript:show(1)">个人资料</a></li>
            <li id="yy"><a href="javascript:show(2)">教职信息</a></li>
        </ul>
    </div>
    <div class="data_box">
        <div class="tip_box">
            <p><i class="icon_admin tip_icon"></i><b>提示信息：</b>请尽量完善以下个人资料，并保证其真实性。*为必填项。</p>
            <span class="close_tip">×</span>
        </div>

        <div id="jbxx" style="display:block">
            <div class="info_box">
                <h5>基本信息</h5>
                <div class="info_detail_box">
                    <div class="info_detail">
                        <div class="info_detail_list">
                            <label>教工号：</label>
                            <div class="fill_box">
                                <input id="UserNo" runat="server" class="fill_box" />
                            </div>
                            *</div>
                        <div class="info_detail_list">
                        </div>
                        <div class="info_detail_list">
                            <label>姓名：</label>
                            <div class="fill_box">
                                <input id="Name" runat="server" class="fill_box" />
                            </div>
                            *</div>
                        <div class="info_detail_list">
                            <label>英文名：</label>
                            <div class="fill_box">
                                <input id="EnglishName" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>性别*：</label>
                            <div>
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">男</asp:ListItem>
                                    <asp:ListItem Value="2">女</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>年龄：</label>
                            <div class="fill_box">
                                <input id="Age" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>出生日期：</label>
                            <div class="fill_box">
                                <input id="Text1" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>婚否：</label>
                            <div class="fill_box">
                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">未婚</asp:ListItem>
                                    <asp:ListItem Value="2">已婚</asp:ListItem>
                                    <asp:ListItem Value="3">保密</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>邮箱地址：</label>
                            <div class="fill_box">
                                <input id="Email" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>手机：</label>
                            <div class="fill_box">
                                <input id="Text4" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>证件号码：</label>
                            <div class="fill_box">
                                <input id="Text5" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>政治面貌：</label>
                            <div class="fill_box">
                                <input id="Text6" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>籍贯：</label>
                            <div class="fill_box">
                                <input id="Text7" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>居住地：</label>
                            <div class="fill_box">
                                <input id="Text8" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>地址：</label>
                            <div class="fill_box">
                                <input id="Text9" runat="server" class="fill_box" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="jzxx" style="display:none">
            <div class="info_box">
                <h5>教职信息</h5>
                <div class="info_detail_box">
                    <div class="info_detail">
                        <div class="info_detail_list">
                            <label>所属机构：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Organization" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>岗位类别：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="station" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>专业类别：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Specialty" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>专技职务：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Professional" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>学科：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Discipline" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>身份：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Identity" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>聘任日期：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Appointment" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>是否助教：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Assistant" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>门户展示：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="Gateway" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div style="width: 940px">
            <asp:Button class="update_btn" ID="update" runat="server" Text="提交" OnClick="update_Click" />
            <asp:Button class="update_btn" ID="cancel" runat="server" Text="取消" OnClick="cancel_Click" />
        </div>
    </div>
    <script type="text/javascript">
        function show(i)
        {
            var jbxx = document.getElementById("jbxx");
            var jzxx = document.getElementById("jzxx");
            var xx = document.getElementById("xx");
            var yy = document.getElementById("yy");
            if(i==1)
            {             
                jbxx.style.display = "block";
                jzxx.style.display = "none";
                xx.className = 'active';
                yy.className = '';

            }
            else
            {
                jzxx.style.display = "block";
                jbxx.style.display = "none";
                yy.className = 'active';
                xx.className = '';
            }
        }
    </script>
</asp:Content>

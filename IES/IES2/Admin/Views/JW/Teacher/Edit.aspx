<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Teacher.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Portal/Edit.js"></script>

    <uc1:Nav runat="server" ID="Nav" />

    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li id="xx" class="active"><a href="javascript:show(1)">个人资料</a></li>
            <%--<li id="yy"><a href="javascript:show(2)">教职信息</a></li>--%>
        </ul>
    </div>
    <div class="data_box">
        <div class="tip_box" id="noticets">
            <p><i class="icon_admin tip_icon"></i><b>提示信息:</b>请尽量完善以下个人资料，并保证其真实性。*为必填项。</p>
            <span class="close_tip" onclick="Hidets()">×</span>
        </div>

        <div class="info_box">
            <h5>基本信息</h5>
            <div class="info_detail_box" style="width:940px">
                <div class="info_detail">

                    <div id="Add" style="display: block">
                        <div class="info_detail_list">
                            <label>用户名:</label>
                            <div class="fill_box">
                                <input id="LoginName" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                            </div>
                        </div>
                        <div class="info_detail_list">
                        </div>
                        <div class="info_detail_list">
                            <label>密码:</label>
                            <div class="fill_box">
                                <input id="Pwd" type="password" runat="server" style="width:198px;height:30px;border:1px solid #e4e4e4;" class="fill_box" /><label style="color: red; width: 5px">*</label>
                            </div>
                        </div>
                        <div class="info_detail_list">
                        </div>
                        <div class="info_detail_list" style="width: 500px">
                            <label>确认密码:</label>
                            <div class="fill_box">
                                <input id="rePwd" type="password"  style="width:198px;height:30px;border:1px solid #e4e4e4;" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Pwd" ControlToValidate="rePwd" ErrorMessage="CompareValidator" ForeColor="Red">两次密码不一致</asp:CompareValidator>
                            </div>
                        </div>
                    </div>

                    <div class="info_detail_list">
                        <label>教工号：</label>
                        <div class="fill_box">
                            <input id="UserNo" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list">
                    </div>
                    <div class="info_detail_list">
                        <label>姓名：</label>
                        <div class="fill_box">
                            <input id="Name" runat="server" class="fill_box" /><label style="color: red; width: 5px">*</label>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>英文名：</label>
                        <div class="fill_box">
                            <input id="EnglishName" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div style="float: left; width: 363px; margin-top: 15px; height: 32px; line-height: 32px;">
                        <label style="float: left; margin-right: 10px; width: 65px; text-align: right; font-size: 13px; color: #333;">性别：</label>
                        <div style="float: left">
                            <asp:RadioButtonList ID="Sex" runat="server" Width="125px" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected="True"> 男</asp:ListItem>
                                <asp:ListItem Value="1"> 女</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>职称：</label>
                        <div class="fill_box">
                            <input id="Ranks" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>所属机构：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Organization" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                        </div>
                        <label style="color: red; width: 5px">*</label>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Organization" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>身份：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Identity" Width="200px" Height="32px" runat="server">
                                <asp:ListItem Value="1">校内</asp:ListItem>
                                <asp:ListItem Value="0">校外</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>门户展示：</label>
                        <div class="fill_box">
                            <asp:DropDownList ID="Gateway" Width="200px" Height="32px" runat="server">
                                <asp:ListItem Value="1">展示</asp:ListItem>
                                <asp:ListItem Value="0">不展示</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="info_detail_list">
                        <label>邮箱地址：</label>
                        <div class="fill_box">
                            <input id="Email" runat="server" class="fill_box" />
                        </div>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="Email" runat="server" ErrorMessage="格式不正确" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                    </div>
                    <div class="info_detail_list">
                        <label>手机：</label>
                        <div class="fill_box">
                            <input id="Tel" runat="server" class="fill_box" />
                        </div>
                    </div>
                    <div class="info_detail_list">
                    </div>
                    <div class="info_detail_list" style="width: 680px; height: 295px;">
                        <label>个人简介:</label>
                        <div class="fill_box" style="top: 0px; left: 0px; height: 295px;">
                            <input type="hidden" name="oEditor1" id="oEditor1" runat="server" />
                            <iframe runat="server" id="frmoEditor1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=coolblue" frameborder="0" scrolling="no" width="559" height="290" style="display: block;"></iframe>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 212px; height: 180px; padding-top: 5px">
                    <div style="width: 211px; height: 111px; float: left">
                        <label style="font-size: 13px; color: #333; float: left;">预览:</label>
                        <img runat="server" id="imgyl" style="width: 110px; height: 110px; margin-left: 20px" />
                    </div>
                    <div style="width: 212px; margin-top: 118px; text-align: center">
                        <label>110X110</label>
                        <br />
                        <a runat="server" href="javascript:Upload();" style="font-size: 13px; margin: 55px 0 5px 0px; width: 100px; height: 30px; background: #284a51; color: #fff; padding: 5px 28px 5px 28px">上传头像</a>
                        <div style="display: none">
                            <input id="File1" type="file" onchange="test(this)" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div style="width: 940px">
            <a class="update_btn" runat="server" onserverclick="update_Click">更新资料</a>
        </div>
    </div>
    <script type="text/javascript">
        //function show(i) {
        //    var jbxx = document.getElementById("jbxx");
        //    var jzxx = document.getElementById("jzxx");
        //    var xx = document.getElementById("xx");
        //    var yy = document.getElementById("yy");
        //    if (i == 1) {
        //        jbxx.style.display = "block";
        //        jzxx.style.display = "none";
        //        xx.className = 'active';
        //        yy.className = '';

        //    }
        //    else {
        //        jzxx.style.display = "block";
        //        jbxx.style.display = "none";
        //        yy.className = 'active';
        //        xx.className = '';
        //    }
        //}
        window.onload = function () {
            var id = location.search;
            if (id.indexOf("id") > 0)
                document.getElementById("Add").style.display = 'none';
            else
                document.getElementById("Add").style.display = 'block';
        }
        function Upload() {
            $("#<%=File1.ClientID%>").click();
        }
        function test(obj) {

            var s = obj.value;
            var d = document.getElementById('<%=imgyl.ClientID %>');
            d.src = s;
        }
        function showPic() {
            alert("ss");
            var fso = new ActiveXObject("Scripting.FileSystemObject");
            if (fso.GetExtensionName(document.getElementById("FileUpload1").value).toLowerCase() != "jpg" && fso.GetExtensionName(document.getElementById("FileUpload1").value).toLowerCase() != "bmp") {
                alert("请选择jpg、bmp格式的文件！");
            }
            else {
                document.getElementById("Image1").ImageUrl = document.getElementById("FileUpload1").value;
            }
        }
    </script>
</asp:Content>

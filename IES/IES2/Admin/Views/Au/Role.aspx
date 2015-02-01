<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="Admin.Views.Au.Role" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../Portal/Edit.js"></script>
    <link href="../../Frameworks/zTree_v3/css/zTreeStyle/AuRoleTreeStyle.css" rel="stylesheet" />
    <script src="../../Frameworks/zTree_v3/js/AuRoleModuleTree.js"></script>    
    <link href="TreeView.css" rel="stylesheet" />
    <script src="Au.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <style>
        .ztree li span {
            line-height: 18px;
            margin-right: 2px;
        }
    </style>
    <div>
        <div class="tabTagBox">
            <ul class="tabTagList">
                <li id="top">角色<div><a href="javascript:LiAdd();" style="text-align: center; line-height: 30px; position: absolute; top: 22px; left: 128px; background: #366061; color: #fff; height: 30px; width: 60px; text-indent: 0em">新建</a></div>
                </li>
                <asp:Repeater runat="server" ID="Repeater1">
                    <ItemTemplate>
                        <li id="<%#Eval("RoleID")%>" onclick="CutCss(this.id)">
                            <div style="float: left; width: 150px; text-indent: 2em;">
                                <input id="txt<%#Eval("RoleID")%>" name="txt<%#Eval("RoleID")%>" type="text" style="border: 0; width: 130px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%#Eval("Title")%>">
                            </div>
                            <div id="keng" style="position: absolute; margin: 14px 0 0 170px;">
                                <div style="float: left; width: 25px;"><a class='u_203' style="display: none" onclick="show(this);"></a></div>
                                <div id="remind<%#Eval("RoleID")%>" onclick="hide(this)" onmouseover="changeSh(this,1)" onmouseout="changeSh(this,2)" class="ifm">
                                    <p><a href="javascript:ReName();" onclick="" class="xf_btn">重命名</a></p>
                                    <p style="margin-top: -1px"><a id="<%#Eval("RoleID")%>" href="#" onclick="Del(this.id)" class="xf_btn">删除</a></p>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <li id="newtr" style="display: none; height: 78px">
                    <input style="margin-left: 20px; width: 130px; border: 1px solid #3794ed" runat="server" placeholder="新角色" id="newrol" type="text" />
                    <p style="margin-left: 18px">
                        <a class="wod_btn" runat="server" onserverclick="btnAdd_Click">确定</a>
                        <a class="wrt_btn" href="javascript:LiDel();">取消</a>
                    </p>
                </li>
                <li id="updtr" style="display: none; height: 40px;">
                    <p style="margin-left: 18px; margin-top: 2px">
                        <a class="wod_btn" runat="server" onserverclick="btnUpd_Click">确定</a>
                        <a class="wrt_btn" href="javascript:trDel();">取消</a>
                    </p>
                </li>
                <li style="height:60px;background:#fff;border:0"></li>
            </ul>         
        </div>
        <div id="tabCon" style="padding: 12px 20px; width: 756px">
            <div style="height: 50px; line-height: 50px; border: 1px solid #ccc; margin-left: -20px">
                <label id="RoleName" runat="server" style="float: left; margin-left: 20px"></label>
                <span runat="server" id="number" style="float: left; color: #999; font-size: 12px; font-weight: normal; margin-left: 10px">丨点击编辑角色描述</span>
                <a id="btns1" href="javascript:Chakan(1);" style="float: right; color: #000;margin-right:5px; border: 0; display: block">查看/编辑管理员帐号</a>
                <a id="btns2" href="javascript:Chakan(2);" style="float: right; color: #000;margin-right:5px; border: 0; display: none">查看权限</a>
            </div>
            <div id="jsqx" style="display: block">
                <div class="result_tit">
                    <h4 runat="server" id="SysName" style="width: 145px"></h4>
                    <a runat="server" onserverclick="btnEdit_Click" href="#" style="float: right; color: #000; border: 0; background: #fff">修改权限</a>
                </div>
                <div style="width: 750px;">
                    <ul id="treeDiv" class="ztree" style="width: 750px"></ul>
                </div>
            </div>
            <div id="glzh" style="width: 760px; display: block">
                <div class="filter_item" style="height: 58px; line-height: 58px;background:#fff;padding:4px 0px">
                    <a class="add_student" style="text-align: center; float: left; margin-left: 0px; margin-top: 14px" href="javascript:AddManger();">添加管理员</a>
                    <a class="add_student" style="text-align: center; float: left; margin-top: 14px; color: #000; background: #EDEDED; border: 1px solid #ccc" href="javascript:DelInfo();">移出管理员</a>
                    <p class="search_btn">
                        <input type="text" id="txtKey" runat="server" placeholder="用户名/姓名"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -38px;" onserverclick="RoleUserList_Click" class="icon_admin search_icon"></a>
                    </p>
                </div>
                <div id="OmGrid1" style="width: 760px; z-index: 999">
                    <asp:Repeater ID="Repeater3" runat="server">
                        <HeaderTemplate>
                            <table class="result_table">
                                <colgroup>
                                    <col style="width: 20px" />
                                    <col style="width: 70px" />
                                    <col style="width: 150px" />
                                    <col style="width: 150px" />
                                    <col style="width: 370px" />
                                </colgroup>
                                <tr>
                                    <th></th>
                                    <th>
                                        <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                                    <th>用户名</th>
                                    <th>姓名</th>
                                    <th>组织机构</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item item_row" id='<%# Eval("UserID")%>'>
                                <td></td>
                                <td>
                                    <input type="checkbox" name="ckbItem"></td>
                                <td><%# Eval("UserNo")%></td>
                                <td><%# Eval("UserName")%></td>
                                <td><%# Eval("OrganizationName")%></td>
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
            </div>
        </div>
        <!--隐藏控件-->
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:Button ID="RoleChange" Style="display: none;" runat="server" Text="Button" OnClick="RoleChange_Click" />
        <asp:HiddenField ID="hfID" runat="server" Value="1" />
        <asp:HiddenField ID="hfQG" runat="server" Value="2" />
        <asp:Button ID="BatchDel" Style="display: none;" runat="server" Text="Button" OnClick="BatchDel_Click" />
        <asp:HiddenField ID="hfIDS" runat="server" />
        <asp:Button ID="BatchADD" Style="display: none;" runat="server" Text="Button" OnClick="BatchADD_Click" />
        <!--end-->
    </div>

    <script>
        var zNodes = [<%=TreeData()%>];
        var setting = {
            view: {
                showIcon: false
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        };
        $(document).ready(function () {         
            $.fn.zTree.init($("#treeDiv"), setting, zNodes);
        });
        function ReName() {
            var id = document.getElementById("<%= hfID.ClientID %>").value;
            var txt = document.getElementById("txt" + id);
            txt.readOnly = false;
            txt.style.borderStyle = "solid";
            txt.style.borderColor = "#3399FF";
            txt.style.borderWidth = "1px";
            txt.style.background = "#fff";
            $("#updtr").show();
            $("#newtr").hide();
        }
        function trDel() {
            $("#updtr").hide();
            $("#<%=RoleChange.ClientID %>").click();
        }
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }
        }
        function UserADD(ids) {
            $("#<%=hfIDS.ClientID %>").val(ids);
            $("#<%=BatchADD.ClientID %>").click();
        }
        function Chakan(s) {
            var sbt1 = document.getElementById("btns1");
            var sbt2 = document.getElementById("btns2");
            if (s == 1) {
                sbt1.style.display = 'none';
                sbt2.style.display = 'block';
                document.getElementById("jsqx").style.display = 'none';
                document.getElementById("glzh").style.display = 'block';
                $("#<%=hfQG.ClientID %>").val("1");
            }
            else {
                sbt1.style.display = 'block';
                sbt2.style.display = 'none';
                document.getElementById("jsqx").style.display = 'block';
                document.getElementById("glzh").style.display = 'none';
                $("#<%=hfQG.ClientID %>").val("2");
            }
        }
        function CutCss(obj) {
            var lis = document.getElementsByTagName("li");
            for (var i = 0; i < lis.length; i++) {
                if (lis[i].id == obj) {
                    lis[i].className = 'current';
                }
                else
                    lis[i].className = '';
            }
            var aa = document.getElementsByClassName("u_203");
            for (var i = 0; i < aa.length; i++) {
                if (aa[i].parentNode.parentNode.parentNode.id == obj) {
                    aa[i].style.display = 'block';
                }
                else {
                    aa[i].style.display = 'none';
                }
            }
            var HiddenValue = document.getElementById("<%= hfID.ClientID %>").value;
            if (HiddenValue != obj) {
                $("#<%=hfID.ClientID %>").val(obj);
                $("#<%=RoleChange.ClientID %>").click();
            }
        }
        function DelBatch(ids) {
            $("#<%=hfIDS.ClientID %>").val(ids);
            $("#<%=BatchDel.ClientID %>").click();
        }
        window.onload = function () {
            var s = '<%=Getclass()%>';
        }
        function LiAdd() {
            var li = $("#newtr");
            var temp = li.is(":hidden");
            if (temp == true) {
                li.show();
                $("#updtr").hide();
                textCss();
            }
        }
        function textCss() {
            var id = document.getElementById("<%= hfID.ClientID %>").value;
            var txt = document.getElementById("txt" + id);
            txt.readOnly = true;
            txt.style.border = '0px';
            txt.style.backgroundColor = 'transparent';
        }
        function LiDel() {
            $("#<%=newrol.ClientID %>").val("");
            $("#newtr").hide();
        }
    </script>
</asp:Content>

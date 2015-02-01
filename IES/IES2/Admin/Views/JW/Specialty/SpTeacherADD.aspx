<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpTeacherADD.aspx.cs" Inherits="Admin.Views.JW.Specialty.SpTeacherADD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Specialty.css" rel="stylesheet" />
    <script src="../../../Js/jquery-1.8.3.min.js"></script>
    <script src="../../../Js/G2S.js"></script>
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <link href="Specialty.css" rel="stylesheet" />
    <link href="../../Au/TreeView.css" rel="stylesheet" />
    <script src="../../Au/TreeView.js"></script>
    <script>
        function OnTreeNodeChecked() {
            var ele = window.event.srcElement;
            if (ele.type == 'checkbox') {
                var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
                var div = document.getElementById(childrenDivID);
                if (div == null) return;
                var checkBoxs = div.getElementsByTagName('INPUT');
                for (var i = 0; i < checkBoxs.length; i++) {
                    if (checkBoxs[i].type == 'checkbox')
                        checkBoxs[i].checked = ele.checked;
                }
            }
        }
        var index = parent.layer.getFrameIndex(window.name);
        function Close() {          
            parent.layer.close(index);
        }
        function Json(ids) {
            parent.UserADD(ids);
            parent.layer.close(index);
        }
        var temp = [<%=TreeData()%>];
        $(function () {
            var arr = [''];

            CreateTree(temp, arr, 0);

            var str = arr.join('');
            //去除空的<ul></ul>
            str = str.replace(/<ul>\s*<\/ul>/g, '');

            $('#treeDiv').html(str);
            init();
        });

        //递归创建树
        function CreateTree(Json, arrHtml, parnetID) {
            if (Json == null || Json.length < 1) {
                return;
            }

            arrHtml.push('<ul>');

            for (var i = 0, len = Json.length; i < len; i++) {
                if (parnetID == Json[i]['ParentID']) {
                    arrHtml.push('<li onclick="OrgSelect(this,this.id)" id=' + Json[i]['ID'] + '><div onclick="oncheck(this)"></div>' + Json[i]['Name'] + '</li>');

                    arrHtml.push('<ul>');
                    for (var j = 0, len = Json.length; j < len; j++) {
                        if (Json[i]['ID'] == Json[j]['ParentID']) {
                            arrHtml.push('<li onclick="OrgSelect(this,this.id)" id=' + Json[i+1]['ID'] + '><div onclick="oncheck(this)"></div>' + Json[j]['Name'] + '</li>');
                            CreateTree(Json, arrHtml, Json[j]['ID']);
                        }
                    }
                    arrHtml.push('</ul>');
                }
            }
            arrHtml.push('</ul>');         
        }
        //选择
        function OrgSelect(e,id) {
            var lis = document.getElementsByTagName("li");
            for (var i = 0; i < lis.length; i++) {
                lis[i].style.background = '#fff';
                lis[i].style.color = '#000';
            }
            $(e).css("background", "#366061");
            $(e).css("color", "#fff");
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=OrgChange.ClientID %>").click();
        }
        window.onload = function () {
        }
    </script>
</head>
<body>
    <form runat="server">
        <div style="width: 750px; height: 550px">
            <div style="height: 40px;">
                <p class="search_btn">
                    <input id="Key" runat="server" type="text" placeholder="重新搜索" /><i class="icon_admin search_icon"></i>
                </p>
            </div>
            <div id="treeDiv" style="width: 235px; height: 390px; float: left">
                
            </div>          
            <div style="width: 515px; height: 390px; float: left">
                <div>
                    <p>
                        <label id="OrgName" runat="server" style="color: #999; font-size: 14px; font-weight: 600"></label>
                    </p>
                </div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 15px"></col>
                                <col style="width: 50px"></col>
                                <col style="width: 150px"></col>
                                <col style="width: 150px"></col>
                                <col style="width: 150px"></col>
                            </colgroup>
                            <tr>
                                <th></th>
                                <th>
                                    <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                                <th>教工号</th>
                                <th>姓名</th>
                                <th>所属机构</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id='<%# Eval("UserID")%>'>
                            <td></td>
                            <td>
                                <input name="ckbItem" type="checkbox"></td>
                            <td><%# Eval("UserNo")%></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("OrganizationName") %></td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div style="width: 515px; height: 60px; overflow: hidden; float: right">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                    ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                    HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
                </webdiyer:AspNetPager>
            </div>
            <div style="width: 515px; height: 60px; float: right">
                <p style="margin: 20px 0 0 50px">
                    <a class="wod_btn" href="#" onclick="SelInfo()">确定添加</a>
                    <a class="wrt_btn" href="#" onclick="Close()">取消</a>
                    <asp:HiddenField ID="hfID" runat="server" Value="-1" />
                    <asp:Button ID="OrgChange" Style="display: none;" runat="server" Text="Button" OnClick="OrgChange_Click" />
                </p>
            </div>
        </div>
    </form>
</body>
</html>

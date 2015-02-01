<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="Admin.Views.Au.RoleEdit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="TreeView.css" rel="stylesheet" />
    <script src="../JW/Class/Class.js"></script>
    <script src="../../Frameworks/OmGrid/js/OmGrid-2.0-beta.js"></script>
    <link href="../../Frameworks/zTree_v3/css/zTreeStyle/AuRoleTreeStyle.css" rel="stylesheet" />
    <script src="../../Frameworks/zTree_v3/js/AuRoleModuleTree.js"></script>
    <style>
        .ztree li span {
            line-height: 18px;
            margin-right: 2px;
        }
    </style>
    <uc1:Nav runat="server" ID="Nav" />
    <div class="data_box">
        <div>
            <p>
                <label id="SysName" runat="server" style="color: #999; font-size: 14px; font-weight: 600"></label>
            </p>
        </div>
        <div class="zTreeDemoBackground left" style="width: 925px">
            <ul id="treeDiv" class="ztree" style="width: 925px"></ul>
        </div>
        <!--隐藏控件-->
        <asp:HiddenField ID="hfIDS" runat="server" Value="1" />
        <!--end-->
        <div>
            <p style="margin-left: 400px;">
                <a class="wod_btn" runat="server" onclick="onCheck()" onserverclick="btnSave_Click">确定</a>
                <a class="wrt_btn" href="Role.aspx?PID=A162">取消</a>
            </p>
        </div>
    </div>
    <script type="text/javascript">
        var zNodes = [<%=TreeData()%>];
        var chkeds = '<%=AuRoleModule_List()%>';
        var setting = {
            view: {
                showIcon: false
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            check: {
                enable: true,
                chkStyle: "checkbox",
                chkboxType: { "Y": "ps", "N": "ps" }
            }
        };
        $(document).ready(function () {
            $.fn.zTree.init($("#treeDiv"), setting, zNodes);
        });
        window.onload = function Checked() {
            var treeObj = $.fn.zTree.getZTreeObj("treeDiv");
            var nodes = treeObj.transformToArray(treeObj.getNodes());
            var ary = chkeds.split(',');
            for (var i = 0; i < ary.length; i++) {
                var node = treeObj.getNodeByParam("id", ary[i], null);
                treeObj.checkNode(node, true, true);
            }
        }
        function onCheck() {
            var treeObj = $.fn.zTree.getZTreeObj("treeDiv"),
            nodes = treeObj.getCheckedNodes(true),
            v = "";
            for (var i = 0; i < nodes.length; i++) {
                var halfCheck = nodes[i].getCheckStatus();
                if (!halfCheck.half) {
                    if (i == nodes.length - 1) {
                        v += nodes[i].id;
                    }
                    else {
                        v += nodes[i].id + ",";
                    }
                }
            }
            $("#<%=hfIDS.ClientID %>").val(v);
        }
    </script>
</asp:Content>

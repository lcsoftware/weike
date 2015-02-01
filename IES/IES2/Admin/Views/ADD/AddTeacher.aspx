<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTeacher.aspx.cs" Inherits="Admin.Views.ADD.AddTeacher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Frameworks/OmGrid/css/OmGrid.css" rel="stylesheet" />
    <link href="../../Content/Css/admin.css" rel="stylesheet" />
    <link href="../../Frameworks/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <style>
        .ztree li span {
            line-height: 18px;
            margin-right: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="filter_item">
                <p class="search_btn" style="float: right; margin: 3px">
                    <input id="txtKey" runat="server" type="text" placeholder="请输入学号或姓名搜索" /><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -25px; z-index: 999" onclick="ParmSelect()" class="icon_admin search_icon"></a>
                </p>
            </div>
            <div id="div_tea" style="margin-top: 20px;">
                <div style="float: left; border: 1px solid #ccc; height: 310px; width: 148px;">
                    <div id="courseWebSite" style="margin-left: 15px; height: 280px; margin-top: 5px; overflow-y: auto; overflow-x: hidden;">
                        <ul id="treeDemo" class="ztree organization_list"></ul>
                    </div>
                </div>
                <div style="float: left; border: 1px solid #ccc; height: 310px; width: 700px; margin-left: 20px;">
                    <div id="wName" style="height: 30px; line-height: 30px; vertical-align: middle; width: 100%; font-weight: bold; font-size: 14px; background: #F2F6F8;">
                    </div>
                    <div id="teacherList" style="height: 280px; overflow-y: auto; overflow-x: hidden;">
                    </div>
                    <div id="page" style="margin: 20px; margin-left: 150px;"></div>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style="margin-left: 650px; margin-top: 50px;">
                <input id="btnConfirm" type="button" class="wod_btn" value="确 定" onclick="SelInfo();" />&nbsp;&nbsp;
                <input id="btnClosk" type="button" class="wrt_btn" value="取消" onclick="Close()()" />
            </div>
        </div>
    </form>
    <script src="../../Js/jquery-1.8.3.min.js"></script>
    <script src="../../Frameworks/zTree_v3/js/jquery.ztree.all-3.5.js"></script>
    <script src="../../Js/G2S.js"></script>
    <script src="../../Frameworks/laypage/laypage.js"></script>
    <script src="../../Frameworks/OmGrid/js/OmGrid-2.0-beta.js"></script>
    <script src="AddTeacher.js"></script>
    <%--    <script src="../Portal/Edit.js"></script>--%>

    <script>
        var index = parent.layer.getFrameIndex(window.name);
        function Close() {
            parent.layer.close(index);
        }
        function Json(ids) {
            //debugger;
            var ary = ids.split(',');
            var hidStudents = window.parent.document.getElementById($G2S.request("hfids"));
            hidStudents.value = ary[0];
            var Name = window.parent.document.getElementById($G2S.request("HID"));
            Name.value = ary[1];
            parent.layer.close(index);
        }
    </script>
</body>
</html>

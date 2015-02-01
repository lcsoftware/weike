<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetTeacher.aspx.cs" Inherits="Admin.Views.Index.GetTeacher" %>

<%@ Register Src="~/Views/UserControl/KnowledgeSpotTree8.ascx" TagPrefix="uc5" TagName="KnowledgeSpotTree" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../../Js/AbleTree9.js"></script>
<link href="../../Content/Css/admin.css" rel="stylesheet" />
<link href="../../Content/Css/common.css" rel="stylesheet" />
<link href="../../Content/Css/TreeView.css" rel="stylesheet" />
<script src="../../Js/jquery-1.8.3.min.js"></script>
<script src="../../../Js/G2S.js"></script>
<script src="../../../Frameworks/layer/layer.min.js"></script>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body class="admin_wrap">
    <form id="form1" runat="server" style="margin-top: 10px">
        <div class="pop_400_New">
            <h4>选择教师</h4>
            <i class="icon icon_close" onclick="CancelClick()"></i>
            <div class="pop_wrap">
                <div class="institution">
                    <span class="institution_tit">机构</span>
                    <div class="institution_box">
                        <uc5:KnowledgeSpotTree ID="KnowledgeSpotTree1" runat="server" />
                        <input id="txtOrganizationID" type="text" runat="server" value="" style="display: none;" />
                    </div>
                    <div class="issue_btn_box">
                        <a class="issue_btn" href="javascript:;" onclick="GetCheckedIds()">确定</a>
                        <a class="cancel_btn" href="javascript:;" onclick="CancelClick()">取消</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">

        function CloseWindow() {
            if (window.parent) {
                var index = window.parent.addTeacher;
                parent.layer.close(index);
            }
        }

        function GetCheckedIds() {
            if (window.parent) {
                document.getElementById("<%=txtOrganizationID.ClientID %>").value = TreeView.GetSelectedNode('<%=KnowledgeSpotTree1.ClientID %>');
                var hdnSOrgIDs = document.getElementById("<%=txtOrganizationID.ClientID %>").value;
                if ((',' + hdnSOrgIDs).indexOf(',0,') >= 0) {
                    hdnSOrgIDs = '0';
                }
                window.parent.document.getElementById("hdnTOrgIDs").value = hdnSOrgIDs;

                //var params = {
                //    action: "GetTeasherAndStudentCount", fType: 1, hdnSOrgIDs: hdnSOrgIDs, hdnSSpecialIDs: '', hdnSYearIDs: ''
                //};
                //$.ajax({
                //    url: 'SysMSGManger.ashx', data: params, cache: false, async: false, success: function (str) {
                //        var json = eval("(" + str + ")");
                //        var strHtml = "";
                //        if (json.Rows.length > 0) {
                //            var row = json.Rows[0];
                //            window.parent.document.getElementById("ctl00_ContentPlaceHolder1_hdnTUserCount").value = row.fRowsCount;
                //        }
                //        else {
                //            window.parent.document.getElementById("ctl00_ContentPlaceHolder1_hdnTUserCount").value = 0;
                //        }
                //    }
                //});
                //window.parent.document.getElementById("ctl00_ContentPlaceHolder1_hdnTUserCount").value = row.fRowsCount;

                window.parent.document.getElementById("hdnTHasChange").value = 1;//父页面请求数据是否重新加载

                this.CloseWindow();
            }
        }
        function CancelClick() {
            if (window.parent) {
                window.parent.document.getElementById("hdnTHasChange").value = 0;//父页面请求数据是否重新加载
                this.CloseWindow();
            }
        }
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="Admin.Views.ADD.AddStudent" %>

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
        <div style="margin:0 auto;width:750px">
            <div class="filter_item" style="width:750px;height:40px;padding:0;background:#fff">
                <p class="search_btn" style="float: right; margin:5px 0 0 0;padding-right:0px;">
                    <input id="txtKey" runat="server" type="text" placeholder="请输入学号或姓名搜索" /><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -25px; z-index: 999" onclick="ParmSelect()" class="icon_admin search_icon"></a>
                </p>
            </div>
            <div id="div_tea" style="margin-top: 5px;">
                <div style="float: left; border-right: 1px solid #ccc; height: 375px; width: 235px;margin-top: 5px;">
                    <div id="courseWebSite" style=" height: 375px;  overflow-y: auto;margin-right:5px; overflow-y:scroll">
                        <ul id="treeDemo" class="ztree organization_list"></ul>
                    </div>
                </div>
                <div style="float: left; height: 375px; width: 498px; margin-left: 15px;margin-top:5px">
                    <div id="wName" style="height: 30px; line-height: 30px;text-align:left; vertical-align: middle; width: 100%; font-weight: bold; font-size: 14px; background: #fff;">
                    </div>
                    <div id="stuList" style="height: 330px; overflow-y: auto; overflow-x: hidden;color:#000;font-size:12px">
                    </div>
                    <div id="page" style="margin: 20px; margin-left: 150px;"></div>
                </div>
                <div style="clear: both;">
                </div>
            </div>
            <div style=" margin-top: 60px;float:right;width:480px">
                <a id="btnConfirm" href="javascript:SelInfo();"  class="wod_btn" >确定添加</a>
                <a id="btnClosk" href="javascript:Close();"  class="wrt_btn" >取消</a>
            </div>
        </div>
    </form>
    <script src="../../Js/jquery-1.8.3.min.js"></script>
    <script src="../../Frameworks/zTree_v3/js/jquery.ztree.all-3.5.js"></script>
    <script src="../../Js/G2S.js"></script>
    <script src="../../Frameworks/laypage/laypage.js"></script>
    <script src="../../Frameworks/OmGrid/js/OmGrid-2.0-beta.js"></script>
    <script src="AddStudent.js"></script>
    <style>
        .wod_btn {
    padding: 5px 10px 5px 10px;
    height: 20px;
    width: 60px;
    background: #284a51;
    line-height: 20px;
    color: #fff;
    text-align: center;
    border: 1px solid #ccc;
    text-decoration:none;
}

    .wod_btn:hover {
        background: #233f45;
    }

.wrt_btn {
    padding: 5px 10px 5px 10px;
    height: 20px;
    width: 60px;
    background: #f2f2f2;
    line-height: 20px;
    color: #333;
    text-align: center;
    border: 1px solid #ccc;
    text-decoration:none;
}

    .wrt_btn:hover {
        background: #f8f8f8;
    }

    </style>
    <script>
        var index = parent.layer.getFrameIndex(window.name);
        function Close() {
            parent.layer.close(index);
        }
        function SelInfo() {
            debugger;
            var obj = $(".ckbcss");
            var ids = "";
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].checked) {
                    var id = $(obj[i]).attr("pid");
                        ids += id + ',';
                }
            }
            if (ids != null) {
                Json(ids);
            }
            else {
                alert("请选择要添加的学生！");
            }
        }
        function Json(ids) {
            debugger;
            var ary = ids.split(',');
            var Name = window.parent.document.getElementById($G2S.request("HID"));
            //var btnInfo = window.parent.document.getElementById($G2S.request("method"));
            Name.value += ary;
            //alert(ary);           
            window.parent.ParmMethod();
            parent.layer.close(index);
           
        }
    </script>
</body>
</html>

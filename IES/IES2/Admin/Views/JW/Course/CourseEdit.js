$(document).ready(function () {
    resetTree();
    Checked();
});
//初始化树
function resetTree() {
    zNodes = CourseType_List();
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
}
//对树形结构的一些设置
var setting = {
    view: {
        showIcon: false,
        showLine: false
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "CourseTypeID",
            pIdKey: "ParentID",
            rootPId: 0
        },
        key: {
            title: "Name",  //设置title
            name: "Name"
        }
    },
    check: {
        enable: true,
        chkStyle: "checkbox",
        chkboxType: { "Y": "ps", "N": "ps" }
    }
};
//课程分类列表
var CourseType_List = function () {
    $(".organization_list").html("");
    var url = "Course.ashx";
    var para = { action: "GetSpTypeList" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        return result;
    } else {
        return "";
    }
}
//页面加载是选中
var Checked = function () {
    var chkeds = document.getElementById("MainContent_hfIDS").value;
    var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
    var nodes = treeObj.transformToArray(treeObj.getNodes());
    var ary = chkeds.split(',');
    for (var i = 0; i < ary.length; i++) {
        var node = treeObj.getNodeByParam("CourseTypeID", ary[i], null);
        treeObj.checkNode(node, true, true);
    }
}


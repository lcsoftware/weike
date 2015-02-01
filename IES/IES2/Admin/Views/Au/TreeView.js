$(document).ready(function () {
    resetTree();   
});
var zNodes;

//初始化加载样式
function init() {
    $('#treeDiv').addClass("treeDiv");
    $("#treeDiv li div").addClass("checkbox_0");
}
//选择事件
function oncheck(e) {
    var css = $(e).attr('class');
    if (css == "checkbox_0") {
        $(e).removeClass();
        $(e).addClass("checkbox_1");
    }
    else {
        $(e).removeClass();
        $(e).addClass("checkbox_0");
    }
    sonPar(e);
    parSon(e);
}
//根据子节点改变父节点状态
function sonPar(e) {
    var s = 0;
    var divs = $(e).siblings();
    for (var i = 0; i < divs.length; i++) {
        if (divs[i].attr('class') == "checkbox_0") {
            s++;
        }
    }
    var pli = $(e).parent().parent().prev().children("div");
    if (pli != undefined) {
        if (s > 0) {
            if (divs.length == 0 && s == 1) {
                pli.removeClass();
                pli.addClass("checkbox_0");
            }
            else if (s == divs.length) {
                pli.removeClass();
                pli.addClass("checkbox_0");
            }
            else {
                pli.removeClass();
                pli.addClass("checkbox_2");
            }

        }
        else if (s = 0) {
            pli.removeClass();
            pli.addClass("checkbox_1");
        }
    }
}
//根据父节点改变子节点状态
function parSon(e) {
    var css = $(e).attr('class');
    alert

}

var Organization_List = function () {
    $(".organization_list").html("");
    var strHtml = "";
    var url = "../JW/Organization/OrganizationHandler.ashx";
    var para = { action: "Organization_List" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        return result;
    } else {
        return "";
    }
}
function resetTree() {
    zNodes = Organization_List();
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
    zTree = $.fn.zTree.getZTreeObj("treeDemo");
}

//对树形结构的一些设置
var setting = {
    view: {
        selectedMulti: false,
        showIcon: false,
        dblClickExpand: true,
        showLine: false
    },
    edit: {
        enable: true,
        showRemoveBtn: false,
        showRenameBtn: false
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "OrganizationID",
            pIdKey: "ParentID",
            rootPId: 0
        },
        key: {
            title: "OrganizationName",  //设置title
            name: "OrganizationName"
        }
    },
    callback: {
        beforeDrag: beforeDrag,
        onCollapse: onCollapse
    },
    check: {
        enable: true,
        chkStyle: "checkbox",
        chkboxType: { "Y": "ps", "N": "ps" }
    }
};

function onCollapse(event, treeId, treeNode) {
    zTree.expandNode(treeNode, false, true, true);
}

function beforeDrag(treeId, treeNodes) {
    return false;
}


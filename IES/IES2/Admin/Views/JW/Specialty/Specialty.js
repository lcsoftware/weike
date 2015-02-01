var key;  //搜索关键字
var zTree;  //树
var zNodes = new Array();
var expandNodes;  //所有展开的节点
var nodeList = [];  //搜索结果列表

var pid = 0;  //父id
var type = 0;  // 0为新增,1为编辑
var sid = -1;  //学科ID
var delSptyID = -1;  //被删除的学科ID

//输入验证
var flag = {
    num: false,
    name: false
};

$(document).ready(function () {
    SpecialtyType_P_List();
    resetTree();
    zTree.expandAll(true);
    zTree.expandAll(false);
    key = $("#key");
    key.bind("focus", function (e) {
        if (key.hasClass("empty")) {
            key.removeClass("empty");
        }
    })
    .bind("blur", function (e) {
        if (key.get(0).value === "") {
            key.addClass("empty");
        }
    })
    .bind("propertychange", searchNode)
    .bind("input", searchNode);

    $('.default_status').live('click', function () {
        if (!$(this).hasClass('click')) {
            $(this).addClass('click');
            $(this).children('input').css('border-color', '#366061');
            $(this).next().show();
        } else {
            $(this).removeClass('click');
            $(this).children('input').css('border-color', '#ccc');
            $(this).next().hide();
        }
        layer.autoArea(box);
    })

    $("input[name='SpecialtyTypeNo']").blur(function () {
        $(this).val() == "" ? $("#NoMsg").show() : $("#NoMsg").hide();
        $(this).val() == "" ? flag.num = false : flag.num = true;
    }).focus(function () {
        $("#NoMsg").hide();
        $("#regMsg").hide();
    });


    $("input[name='SpecialtyTypeName']").blur(function () {
        $(this).val() == "" ? $("#NameMsg").show() : $("#NameMsg").hide();
        $(this).val() == "" ? flag.name = false : flag.name = true;
    }).focus(function () {
        $("#NameMsg").hide();
    });   
});

//初始化树
function resetTree() {
    zNodes = SpecialtyType_List();
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
    zTree = $.fn.zTree.getZTreeObj("treeDemo");
}

//对树形结构的一些设置
var setting = {
    view: {
        selectedMulti: false,
        showIcon: false,
        dblClickExpand: true,
        showLine: false,
        fontCss: getFontCss
    },
    edit: {
        enable: true,
        showRemoveBtn: false,
        showRenameBtn: false
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "SpecialtyTypeID",
            pIdKey: "ParentID",
            rootPId: 0
        },
        key: {
            title: "SpecialtyTypeName",  //设置title
            name: "SpecialtyTypeName"
        }
    },
    callback: {
        beforeDrop: beforeDrop,
        onCollapse: onCollapse
    }
};

//搜索学科
function searchNode(e) {
    updateNodes(false);
    var value = $.trim(key.get(0).value);
    nodeList = zTree.getNodesByFilter(function (node) {
        return (node.SpecialtyTypeNo.toLowerCase().indexOf(value.toLowerCase()) > -1 || node.SpecialtyTypeName.toLowerCase().indexOf(value.toLowerCase()) > -1);
    });
    if (value == '' || nodeList.length < 1) {
        updateNodes(false);
    } else {
        updateNodes(true);
    }
}

//改变搜索出来的节点的状态
function updateNodes(highlight) {
    for (var i = 0, l = nodeList.length; i < l; i++) {
        nodeList[i].highlight = highlight;
        zTree.updateNode(nodeList[i]);
    }
}
function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#284a51", "font-weight": "normal" };
}

function onCollapse(event, treeId, treeNode) {
    zTree.expandNode(treeNode, false, true, true);
}

//鼠标按下时
function beforeDrag(treeId, treeNodes) {
    return true;
}
//移动组织机构
function beforeDrop(treeId, treeNodes, targetNode, moveType) {
    Organization_Move(treeNodes[0].OrganizationID, targetNode.OrganizationID, moveType);
}



//刷新前保持原有状态
function SaveState() {
    var nodes = zTree.transformToArray(zTree.getNodes());  //所有节点
    expandNodes = new Array();
    if (nodes.length > 0) {
        for (var i = 0; i < nodes.length; i++) {  //展开的节点
            if (nodes[i].open) {
                expandNodes.push(nodes[i].OrganizationID);
            }
        }

    } else {
        expandNodes = null;
    }
}

//恢复刷新前的状态
function ReCover() {
    if (expandNodes != null && expandNodes.length > 0) {
        for (var i = 0; i < expandNodes.length; i++) {
            zTree.expandNode(zTree.getNodeByParam("SpecialtyType", expandNodes[i]), true, null, null, true);
        }
    }
}

//学科列表
var SpecialtyType_List = function () {
    $(".organization_list").html("");
    var url = "SpecialtyType.ashx";
    var para = { action: "GetSpTypeList" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        return result;
    } else {
        return "";
    }
}

//获取上级学科下拉列表
var SpecialtyType_P_List = function () {
    $("select[name='SpecialtyType']").html("");
    var strHtml = "<option value='0'>无</option>";
    var url = "SpecialtyType.ashx";
    var para = { action: "SpecialtyType_P_List" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];           
            strHtml += "<option value='" + row.SpecialtyTypeID + "'>" + row.SpecialtyTypeName + "</option>";
        }

    } else {
        strHtml += "<option value='-1'>请添加学科类别</option>";
    }
    $("select[name='SpecialtyType']").html(strHtml);
}

//新增或编辑
var AddSptyEdit = function () {
    SaveState();
    var SpecialtyTypeNo = $("input[name='SpecialtyTypeNo']").val();
    var SpecialtyTypeName = $("input[name='SpecialtyTypeName']").val();
    var ParentID = $("select[name='SpecialtyType']").val();
    var IsNo = false;
    if ($("input[name='SpecialtyTypeNo']").val!="") {
        IsNo = true;
    }
    var IsName = false;
    if ($("input[name='SpecialtyTypeName']").val != "" ) {
        IsName = true;
    }
    //验证
    if (!flag.num || !flag.name) {
        alert("请完善信息!");
        return;
    }

    var SpecialtyTypeID = -1;
    if (type == 0) {
        SpecialtyTypeID = -1;
    } else {
        SpecialtyTypeID = sid;
    }

    var url = "SpecialtyType.ashx";
    var para = {
        action: "SpecialtyType_Edit",
        SpecialtyTypeID: SpecialtyTypeID,
        SpecialtyTypeNo: SpecialtyTypeNo,
        SpecialtyTypeName: SpecialtyTypeName,
        ParentID: ParentID
    };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        if (result.OrganizationID == 0) {
            $("#regNoMsg").show();
        } else {
            if (type == 0) {
                alert("新增成功!");
            } else {
                alert("编辑成功!");
            }
            layer.closeAll();
            resetTree();
            ReCover();
        }
    } else {
        return "";
    }

}

//初始化编辑
var InitEdit = function (sid) {
    this.sid = sid;
    flag = {
        num: true,
        name: true
    };
    var node = zTree.getNodeByParam("SpecialtyTypeID", sid);
    var ParentNode = node.getParentNode();
    if (ParentNode == null) {
        $("#SptyName").html("无");
    }
    else {
        $("#SptyName").html(ParentNode.SpecialtyTypeName);
    }
    $("input[name='SpecialtyTypeNo']").val(node.SpecialtyTypeNo);
    $("input[name='SpecialtyTypeName']").val(node.SpecialtyTypeName);
    $("select[name='SpecialtyType']").val(node.ParentID);
}

//初始化添加
var InitAdd = function () {
    flag = {
        num: false,
        name: false
    };
    $("input[name='SpecialtyTypeNo']").val("");
    $("input[name='SpecialtyTypeName']").val("");
    $("select[name='SpecialtyType']").val(0);
}

//删除学科
var SpecialtyType_Del = function (sid) {
    SaveState();
    delSptyID = sid;
    var node = zTree.getNodeByParam("SpecialtyTypeID", sid);
    var para = { action: "SpecialtyType_Del", SpecialtyTypeID: sid };
    var url = "SpecialtyType.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        $("#sptyName").html(node.SpecialtyTypeName);
        $(".delete_tip").slideDown();
    } else {
        alert("请重试!");
    }
    resetTree();
    ReCover();
}

//取消删除组织机构
var SpecialtyType_CancelDel = function () {
    SaveState();
    debugger;
    var para = { action: "SpecialtyType_CancelDel", SpecialtyTypeID: delSptyID };
    var url = "SpecialtyType.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        $(".delete_tip").slideUp();
    } else {
        alert("请重试!");
    }
    resetTree();
    ReCover();
}

//移动组织机构
var Organization_Move = function (SelfID, OptionID, type) {
    var para = { action: "Organization_Move", SelfID: SelfID, OptionID: OptionID, type: type };
    var url = "OrganizationHandler.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {

    } else {
        alert("请重试!");
    }
}

//弹出层
var box;
function ShowBox(sid, name, type) {
    this.pid = sid;
    this.type = type;
    $("#SptyName").html(name);
    $("#NoMsg").hide();
    $("#NameMsg").hide();
    $("#regMsg").hide();
    var title = type == 0 ? "新增" : "编辑";
    box = $.layer({
        type: 1,
        title: [title, true],
        shift: 'top',
        shade: [0.5, '#000'],
        area: ['450px', "auto"],
        page: { dom: '#box' }
    });
    $(".xubox_title").attr("style", "background:none repeat scroll 0 0 #284a51;color:#fff;cursor: move;");

    if (type == 1) {
        InitEdit(sid);
    } else {
        InitAdd();
    }
}

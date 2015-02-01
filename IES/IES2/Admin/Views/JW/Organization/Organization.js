var key;  //搜索关键字
var zTree;  //树
var zNodes = new Array();
var expandNodes;  //所有展开的节点
var nodeList = [];  //搜索结果列表

var pid = 0;  //父id
var type = 0;  // 0为新增,1为编辑
var oid = -1;  //组织机构ID
var delOrgID = -1;  //被删除的组织机构ID

//输入验证
var flag = {
    num: false,
    name: false,
    link: false
};

$(document).ready(function () {
    OrganizationType_List();
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

    $("#inputOutLink").click(function () {
        $("#outLink").show();
        $("#Introduction").hide();
        $("#IntroductionEn").hide();
        layer.autoArea(box);
    });

    $("#inputIntroduction").click(function () {
        $("#Introduction").show();
        $("#IntroductionEn").show();
        $("#outLink").hide();
        layer.autoArea(box);
    });

    $("input[name='OrganizationNo']").blur(function () {
        $(this).val() == "" ? $("#NoMsg").show() : $("#NoMsg").hide();
        $(this).val() == "" ? flag.num = false : flag.num = true;
    }).focus(function () {
        $("#NoMsg").hide();
        $("#regMsg").hide();
    });


    $("input[name='OrganizationName']").blur(function () {
        $(this).val() == "" ? $("#NameMsg").show() : $("#NameMsg").hide();
        $(this).val() == "" ? flag.name = false : flag.name = true;
    }).focus(function () {
        $("#NameMsg").hide();
    });

    $("input[name='Link']").blur(function () {
        var regLink = '^((https|http|ftp|rtsp|mms)://)+'
                + '?(([0-9a-z_!~*\'().&=+$%-]+: )?[0-9a-z_!~*\'().&=+$%-]+@)?' //ftp的user@ 
                + '(([0-9]{1,3}.){3}[0-9]{1,3}'     // IP形式的URL- 199.194.52.184 
                + '|'        // 允许IP和DOMAIN（域名） 
                + '([0-9a-z_!~*\'()-]+.)*'  // 域名- www. 
                + '([0-9a-z][0-9a-z-]{0,61})?[0-9a-z].' // 二级域名 
                + '[a-z]{2,6})'     // first level domain- .com or .museum 
                + '(:[0-9]{1,4})?'  // 端口- :80 
                + '((/?)|'          // a slash isn't required if there is no file name
                + '(/[0-9a-z_!~*\'().;?:@&=+$,%#-]+)+/?)$';
        var re = new RegExp(regLink);
        if (!re.test($(this).val().toLowerCase())) {
            $("#urlMsg").show();
            flag.link = false;
        } else {
            $("#urlMsg").hide();
            flag.link = true;
        }

    }).focus(function () {
        $("#urlMsg").hide();
    });

});

//初始化树
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
        beforeDrop: beforeDrop,
        onCollapse: onCollapse
    }
};

//搜索组织机构
function searchNode(e) {
    updateNodes(false);
    var value = $.trim(key.get(0).value);
    nodeList = zTree.getNodesByFilter(function (node) {
        return (node.OrganizationNo.toLowerCase().indexOf(value.toLowerCase()) > -1 || node.OrganizationName.toLowerCase().indexOf(value.toLowerCase()) > -1 || node.OrganizationNameEn.toLowerCase().indexOf(value.toLowerCase()) > -1);
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
            zTree.expandNode(zTree.getNodeByParam("OrganizationID", expandNodes[i]), true, null, null, true);
        }
    }
}

//组织机构列表
var Organization_List = function () {
    $(".organization_list").html("");
    var url = "OrganizationHandler.ashx";
    var para = { action: "Organization_List" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        return result;
    } else {
        return "";
    }
}

//获取组织机构类别下拉列表
var OrganizationType_List = function () {
    $("select[name='OrganizationType']").html("");
    var strHtml = '';
    var url = "OrganizationHandler.ashx";
    var para = { action: "OrganizationType_List" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            strHtml += "<option value='" + row.OrganizationTypeID + "'>" + row.OrganizationTypeName + "</option>";
        }

    } else {
        strHtml += "<option value='-1'>请添加组织机构类别</option>";
    }
    $("select[name='OrganizationType']").html(strHtml);
}

//新增或编辑
var AddOrEdit = function () {
    SaveState();
    var OrganizationNo = $("input[name='OrganizationNo']").val();
    var OrganizationName = $("input[name='OrganizationName']").val();
    var OrganizationNameEn = $("input[name='OrganizationNameEn']").val();
    var OrganizationTypeID = $("select[name='OrganizationType']").val();
    var IsTeaching = false;
    if ($("input[name='TeachingType']").eq(1).attr("checked") == "checked" || $("input[name='TeachingType']").eq(1).attr("checked") == true) {
        IsTeaching = true;
    }

    var IsShow = false;
    if ($("input[name='IsShow']").eq(0).attr("checked") == "checked" || $("input[name='IsShow']").eq(0).attr("checked") == true) {
        IsShow = true;
    }
    var ParentID = pid;
    var LinkStatus = false;
    if ($("#inputOutLink").attr("checked") == "checked" || $("#inputOutLink").attr("checked")) {
        LinkStatus = true;
    } else {
        LinkStatus = false;
        flag.link = true;  //如果使用内部编辑器,不对外部链接进行验证
    }
    var Link = $("input[name='Link']").val();

    var Introduction = document.getElementById("frmoEditor1").contentWindow.getHTML();
    var IntroductionEn = document.getElementById("frmoEditor2").contentWindow.getHTML();

    //验证
    if (!flag.num || !flag.name || !flag.link) {
        alert("请完善信息!");
        return;
    }

    var OrganizationID = -1;
    if (type == 0) {
        OrganizationID = -1;
    } else {
        OrganizationID = oid;
    }

    var url = "OrganizationHandler.ashx";
    var para = {
        action: "Organization_Edit",
        OrganizationID: OrganizationID,
        OrganizationNo: OrganizationNo,
        OrganizationName: OrganizationName,
        OrganizationNameEn: OrganizationNameEn,
        OrganizationTypeID: OrganizationTypeID,
        IsTeaching: IsTeaching,
        ParentID: ParentID,
        LinkStatus: LinkStatus,
        Link: Link,
        Introduction: encodeURI(Introduction),  //decodeURI
        IntroductionEn: encodeURI(IntroductionEn),
        IsShow: IsShow
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
var InitEdit = function (oid) {
    this.oid = oid;
    flag = {
        num: true,
        name: true,
        link: true
    };
    var node = zTree.getNodeByParam("OrganizationID", oid);
    var ParentNode = node.getParentNode();
    if (ParentNode == null) {
        $("#OrgName").html("无");
    }
    else {
        $("#OrgName").html(ParentNode.OrganizationName);
    }
    $("input[name='OrganizationNo']").val(node.OrganizationNo);
    $("input[name='OrganizationName']").val(node.OrganizationName);
    $("input[name='OrganizationNameEn']").val(node.OrganizationNameEn);
    $("select[name='OrganizationType']").val(node.OrganizationTypeID);
    if (node.IsTeaching) {
        $("input[name='TeachingType']").eq(1).attr("checked", "checked");
    } else {
        $("input[name='TeachingType']").eq(0).attr("checked", "checked");
    }
    if (node.IsShow) {
        $("input[name='IsShow']").eq(0).click();
    } else {
        $("input[name='IsShow']").eq(1).click();
    }

    if (node.LinkStatus) {
        $("#inputOutLink").click();
    } else {
        $("#inputIntroduction").click();
    }
    $("input[name='Link']").val(node.Link);
    var Introduction = decodeURI(node.Introduction);
    var IntroductionEn = decodeURI(node.IntroductionEn);
    $("#oEditor1").val(Introduction);
    $("#oEditor2").val(IntroductionEn);
}

//初始化添加
var InitAdd = function () {
    flag = {
        num: false,
        name: false,
        link: false
    };
    $("input[name='OrganizationNo']").val("");
    $("input[name='OrganizationName']").val("");
    $("input[name='OrganizationNameEn']").val("");
    $("select[name='OrganizationType']").val(0);
    $("input[name='TeachingType']").eq(0).attr("checked", "checked");
    $("input[name='IsShow']").eq(0).attr("checked", "checked");
    $("#inputOutLink").click();
    $("input[name='Link']").val('http://');
    $("#oEditor1").val("");
    $("#oEditor2").val("");
}

//删除组织机构
var Organization_Del = function (oid) {
    SaveState();
    delOrgID = oid;
    var node = zTree.getNodeByParam("OrganizationID", oid);
    var para = { action: "Organization_Del", OrganizationID: oid };
    var url = "OrganizationHandler.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        $("#orgName").html(node.OrganizationName);
        $(".delete_tip").slideDown();
    } else {
        alert("请重试!");
    }
    resetTree();
    ReCover();
}

//取消删除组织机构
var Organization_CancelDel = function () {
    SaveState();
    var para = { action: "Organization_CancelDel", OrganizationID: delOrgID };
    var url = "OrganizationHandler.ashx";
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
function ShowBox(oid, name, type) {
    this.pid = oid;
    this.type = type;
    $("#OrgName").html(name);
    $("#NoMsg").hide();
    $("#regMsg").hide();
    $("#NameMsg").hide();
    $("#urlMsg").hide();
    var title = type == 0 ? "新增" : "编辑";
    box = $.layer({
        type: 1,
        title: [title, true],
        shift: 'top',
        shade: [0.5, '#000'],
        area: ['800px', "auto"],
        page: { dom: '#box' }
    });
    $(".xubox_title").attr("style", "background:none repeat scroll 0 0 #284a51;color:#fff;cursor: move;");

    if (type == 1) {
        InitEdit(oid);
    } else {
        InitAdd();
    }
}

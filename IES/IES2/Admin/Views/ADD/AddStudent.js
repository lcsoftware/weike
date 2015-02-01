
var og = new OmGrid();

var flag = false;
var selectIDs = "";  //选中的ID
var wid = "";
var checkedID = "";
var OrganizationID;
var PageIndex = 1;
var RowsCount;
var PageCount;
$(document).ready(function () {
    resetTree();
    $("#wName").html(zNodes[0].OrganizationName);
    OrganizationID = zNodes[0].OrganizationID;
    GetStuList();
    myfunction();
    $(".ckb_div").css('margin', '7px');
    $(".ckbcss").css('margin', '7px');
}
);
var zTree;
var zNodes;

//组织机构下的学生
function GetStuList() {
    var wname = '';
    var row2WebID = '';
    //wid = OrganizationID;
    var strHtml = '';
    $("#stuList").html("");
    strHtml += "<table border='0' cellpadding='0' cellspacing='0' width='496px' >";
    strHtml += "      <colgroup>"
    strHtml += "        <col width='30px' height='30px' /><col width='118px' height='30px' /><col width='120px' height='30px' /><col width='226px' height='30px' />";
    strHtml += "      </colgroup>";
    strHtml += "      <tr class='item_title' style='background:#ccc;color:#000;font-weight: bold;line-height:30px'>";
    strHtml += "            <td style='border-right:1px solid #fff;border-left:1px solid #ccc;'><input type='checkbox' name='ckbAll' /></td>";
    strHtml += "            <td style='border-right:1px solid #fff;text-indent:1em'>学号</td>";
    strHtml += "            <td style='border-right:1px solid #fff;text-indent:1em'>姓名</td>";
    strHtml += "            <td style='border-right:1px solid #ccc;text-indent:1em'>所属机构</td>";
    strHtml += "      </tr>";
    var data = { action: "GetStuList", Key: $("#txtKey").val(), OrganizationID: OrganizationID, SpecialtyID: -1, ClassID: -1, IsRegister: -1, StudentIDs: "", PageSize: 10, PageIndex: PageIndex };
    var stuList = $G2S.GetAjaxJson(data, "AddStudent.ashx");
    if (stuList != "") {
        for (var i = 0; i < stuList.length; i++) {
            var row = stuList[i];
            var UserID = row.UserID;
            var UserNo = row.UserNo;
            var Name = row.UserName;
            var OrganizationName = row.OrganizationName;
            var Classname = row.classname
            RowsCount = row.rowscount;
            PageCount = (RowsCount % 10 == 0 ? RowsCount / 10 : parseInt(RowsCount / 10) + 1);
            strHtml += "<tr class='item item_row' style='height:31px' id=" + UserID + ">";
            strHtml += "    <td style='border-right:1px solid #ccc;border-top:1px solid #ccc;border-left:1px solid #ccc;'><input type='checkbox' onclick='' name='ckbItem' pid='" + UserID + "' sname='" + Name + "' class='ckbcss'/></td>";
            strHtml += "    <td style='border-right:1px solid #ccc;border-top:1px solid #ccc;text-indent:1em'>" + UserNo + "</td>";
            strHtml += "    <td style='border-right:1px solid #ccc;border-top:1px solid #ccc;text-indent:1em'>" + Name + "</td>";
            strHtml += "    <td style='border-right:1px solid #ccc;border-top:1px solid #ccc;text-indent:1em'>" + OrganizationName + "</td>";
            strHtml += "</tr>";
        }
        $("#wName").html(stuList[0].OrganizationName + "(" + stuList[0].rowscount + ")");
    }
    strHtml += "</table>";
    $("#stuList").html(strHtml);
    og.init("stuList");

    og.title("[name='ckbAll']").click(function () {
        Check(og.checkedID(), 0);
    });

    og.item("[name='ckbItem']").click(function (sender, id) {
        Check(og.checkedID(), id);
    });
}

function ParmSelect() {
    $("#page").html("");
    flag = false;
    GetStuList();
    myfunction();
}

//复选框点击
function Check(ids, id) {
    var sid = ids.split(",");
    if (ids != "") {
        for (var i = 0; i < sid.length; i++) {
            var name = $("input[pid='" + sid[i] + "']").attr("sname");
            if (selectIDs.indexOf(sid[i] + "," + name + "[@&_username_&@]") < 0) {
                selectIDs += sid[i] + "," + name + "[@&_username_&@]";
                checkedID += sid[i] + ",";
            }
        }
    }
    if (id != 0) {
        if (!$("input[pid=" + id + "]").attr("checked")) {
            var name = $("input[pid='" + id + "']").attr("sname");
            var n = selectIDs.indexOf(id + "," + name + "[@&_username_&@]");
            if (n > -1) {
                selectIDs = selectIDs.replace(id + "," + name + "[@&_username_&@]", "");
                checkedID = checkedID.replace(id + ",", "");
            }
        }
    }
    if (sid.length < $(".ckbcss").length) {
        $("#c" + wid).attr("class", "ckb_div ckb_part");
    }
    if (sid.length == $(".ckbcss").length) {
        $("#c" + wid).attr("class", "ckb_div ckb_all");
    }
    if (ids == "") {
        $(".ckbcss").each(function (i) {
            var pid = $(this).attr("pid");
            var sname = $(this).attr("sname");
            selectIDs = selectIDs.replace(pid + "," + sname + "[@&_username_&@]", "");
            checkedID = checkedID.replace(pid + ",", "");
        });
        $("#c" + wid).attr("class", "ckb_div");
    }
    if (id == 0 && ids != "") {
        $("#c" + wid).attr("class", "ckb_div ckb_all");
    }
    //alert(selectIDs);
};

//左边复选框点击
function selectAll(o) {
    var num = $(o).attr("class").indexOf("ckb_all");
    var num1 = $(o).attr("class").indexOf("ckb_part");
    if (num > 0) {
        $(".ckbcss").each(function (i) {
            $(this).attr("checked", false);
        });
        $("div[id^='tri_chk_']").attr("class", "ckb_div");
        $("div[id^='tri_chk_']").attr("status", "none");
        Check("", 0);
        $("tr").removeClass("item_cked");
    } else {
        var ids = "";
        $(".ckbcss").each(function (i) {
            var pid = $(this).attr("pid");
            ids += pid + ",";
        });
        og.checkedID(ids);
        $("div[id^='tri_chk_']").attr("class", "ckb_div ckb_all");
        $("div[id^='tri_chk_']").attr("status", "all");
        Check(og.checkedID(), 0);
    }
}

//去除重复项目
function Getchongfu(str) {
    debugger;
    var ret = [];
    var re = str.split('[@&_username_&@]');
    str.replace(/[^,]+/g, function ($0, $1) { (str.indexOf($0) == $1) && ret.push($0) })
    return ret;
}
//获取url参数
function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
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
        onCollapse: onCollapse,
        onMouseDown: onMouseDown
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

function onMouseDown(event, treeId, treeNode) {
    if (treeNode == null) {
        return;
    }
    $("#page").html("");
    $("#wName").html(treeNode.OrganizationName);
    OrganizationID = treeNode.OrganizationID;
    flag = false;
    GetStuList();
    myfunction();
    $(".ckb_div").css('margin', '7px');
    $(".ckbcss").css('margin', '7px');
}

function myfunction() {
    laypage({
        cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
        pages: PageCount, //总页数
        skin: '#AF0000', //选中的颜色
        groups: 3,//连续显示分页数
        first: '首页', //若不显示，设置false即可
        last: '尾页', //若不显示，设置false即可
        jump: function (e) { //触发分页后的回调
            PageIndex = e.curr;
            if (flag) {
                GetStuList();
            } else {
                flag = true;
            }
        }
    });
    if (PageCount <= 1 || PageCount == undefined) {
        $("#page").html("");
    }
}


function SelInfo() {
    debugger;
    var obj = document.getElementsByName('ckbItem');
    var ids = null;
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {
            var id = obj[i].parentNode.parentNode.id;
            if (ids == null) {
                ids = id ;
            }
        }
    }
    //ids = og.selectedID();
    //alert(123);
    if (ids != null) {
        Json(ids);
    }
    else {
        alert("请选择要添加的学生！");
    }
}
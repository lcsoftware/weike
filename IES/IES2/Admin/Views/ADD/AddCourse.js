
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
    $("#wName").html("&nbsp;&nbsp;" + zNodes[0].OrganizationName);
    OrganizationID = zNodes[0].OrganizationID;
    GetCourseList();
    myfunction();
}
);
var zTree;
var zNodes;

//组织机构下的教师
function GetCourseList() {
    var wname = '';
    var row2WebID = '';
    var strHtml = '';
    $("#courseList").html("");
    strHtml += "<table border='0' cellpadding='0' cellspacing='0' width='700' >";
    strHtml += "      <colgroup>"
    strHtml += "        <col width='5%' /><col width='5%' /><col width='30%' /><col width='30%' /><col width='30%' />";
    strHtml += "      </colgroup>";
    strHtml += "      <tr class='item_title'>";
    strHtml += "            <td></td>";
    strHtml += "            <td></td> "
    strHtml += "            <td>课程编号</td>";
    strHtml += "            <td>课程名称</td>";
    strHtml += "            <td>所属机构</td>";
    strHtml += "      </tr>";
    var data = { action: "GetCourseList", Key: $("#txtKey").val(), TermTypeID: -1, OrganizationID: OrganizationID, CourseTypeID: -1, TeachingTypeID: -1, SubjectID1: -1, SubjectID2: -1, BeginFen: 1, EndFen: 100, PageIndex: PageIndex, PageSize: 10 };
    var courList = $G2S.GetAjaxJson(data, "AddCourse.ashx");
    if (courList != "暂无数据") {
        //debugger;
        for (var i = 0; i < courList.length; i++) {
            var row = courList[i];
            var CourseID = row.CourseID;
            var CourseNo = row.CourseNo;
            var CourseName = row.CourseName;
            var OrganizationName = row.OrganizationName;
            RowsCount = row.rowscount;
            PageCount = (RowsCount % 10 == 0 ? RowsCount / 10 : parseInt(RowsCount / 10) + 1);
            strHtml += "<tr class='item item_row' id=" + CourseID + " title=" + CourseName + ">";
            strHtml += "    <td style='float: right; '><input type='radio' onclick='' name='ckbItem' pid='" + CourseID + "' sname='" + CourseName + "' class='ckbcss'/></td>";
            strHtml += "    <td></td>";
            strHtml += "    <td>" + CourseNo + "</td>";
            strHtml += "    <td>" + CourseName + "</td>";
            strHtml += "    <td>" + ((OrganizationName == null || OrganizationName == '') ? '' : OrganizationName) + "</td>";
            strHtml += "</tr>";
        }

    }
    strHtml += "</table>";
    $("#courseList").html(strHtml);
}

function ParmSelect() {
    $("#page").html("");
    flag = false;
    GetCourseList();
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
    //debugger;
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
        //onCheck: onMouseDown
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
    $("#wName").html("&nbsp;&nbsp;" + treeNode.OrganizationName);
    OrganizationID = treeNode.OrganizationID;
    flag = false;
    GetCourseList();
    myfunction();

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
                GetCourseList();
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
    //var test = window.parent.document.getElementById("Teacher");
    //test.value = '1,2,3';
    var obj = document.getElementsByName('ckbItem');
    var ids = null;
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {
            var id = obj[i].parentNode.parentNode.id;
            var name = obj[i].parentNode.parentNode.title;
            if (ids == null) {
                ids = id + "," + name;
            }
        }
    }
    if (ids != null) {
        Json(ids);
    }
    else {
        alert("请选择要添加的课程！");
    }
}
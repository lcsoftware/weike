
var obj;
//显示
function show(cd) {
    var id = cd.parentNode.parentNode.parentNode.id;
    var div = document.getElementById("remind" + id);
    obj = div;
    div.style.display = 'block';
}
function hide(obj) {
    obj.style.display = 'none';
}
//隐藏判断
function changeSh(obj, n) {

    var end = setTimeout(function () { obj.style.display = 'none'; }, 1000);
    if (n == 1) {
        var start = (end - 100) > 0 ? end - 100 : 0;
        for (var i = start; i <= end; i++) {
            clearTimeout(i);
        }
    }
}
//角色权限编辑页面跳转
function RoleEdit(id) {
    window.location.href = "RoleEdit.aspx?id=" + id; 
}
//添加管理员
function AddManger() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['添加管理员', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['905px', '500px'],
        iframe: { src: 'AddManger.aspx' }
    });
}

function OnTreeNodeChecked() {
    var ele = window.event.srcElement;
    if (ele.type == 'checkbox') {
        var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
        var div = document.getElementById(childrenDivID);
        if (div == null) return;
        var checkBoxs = div.getElementsByTagName('INPUT');
        for (var i = 0; i < checkBoxs.length; i++) {
            if (checkBoxs[i].type == 'checkbox')
                checkBoxs[i].checked = ele.checked;
        }
    }
}

//treeview--------------------------------------------------
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











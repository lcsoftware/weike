var IsEmail = '';
var IsMSM = '';
var IsTop = '';
var AllTeacher = '';
var AllStudent = '';
$(document).ready(function () {
    IsEmail = document.getElementById('IsEmail');
    IsMSM = document.getElementById('IsMSM');
    IsTop = document.getElementById('IsTop');
    AllTeacher = document.getElementById('AllTeacher');
    AllStudent = document.getElementById('AllStudent');

    //把表情字体替换为0只算一个字符
    $("#noticeText").keyup(function () {
        var $count = $(this).next().next().children().last();
        calculateRestChar($(this), $count);
    }).change(function () {
        var $count = $(this).next().next().children().last();
        calculateRestChar($(this), $count);
    }).bind('paste', function () {
        var $count = $(this).next().next().children().last();
        calculateRestChar($(this), $count);
    }).focus(function () {
        var $count = $(this).next().next().children().last();
        calculateRestChar($(this), $count);
    });
});

//保存通知
function SaveNotice() {
    if ($("#txtTitle").val() == "") {
        alert("请输入通知内容！");
        return;
    }
    //学生
    var Source = "SpecialtyID";
    var SourceIDs = "";
    var EntryDates = "";
    //教师
    var Source2 = "OrganizationID";
    var SourceIDs2 = "";
    if ($("#allUster").attr("checked")) {
        if (AllTeacher.checked || AllStudent.checked) {
            if (AllTeacher.checked) {
                SourceIDs2 = "-1";
            }
            if (AllStudent.checked) {
                SourceIDs = "-1";
                EntryDates = "-1";
            }
        }
        else {
            alert("请选择通知对象！");
            return;
        }
    }
    else {
        SourceIDs2 = $("#hdnTOrgIDs").val();
        SourceIDs = $("#hdnSSpecialIDs").val();
        EntryDates = $("#hdnSYearIDs").val();
    }
    var url = "Notice.ashx";
    var params = {
        action: "AddNotice",
        Title: $("#txtTitle").val(),
        Conten: $("#noticeText").val(),
        IsTop: IsTop.checked,
        IsEmail: IsEmail.checked,
        IsMSM: IsMSM.checked,
        SysID: 1,
        ModuleID: 0,
        Source2: Source2,
        SourceIDs: SourceIDs,
        Source: Source,
        SourceIDs2: SourceIDs2,
        EntryDates: EntryDates
    };
    var AjaxXML = $G2S.GetAjaxXML(params, url);
    if (AjaxXML != "暂无数据") {
        if (AjaxXML == "True") {
            alert("添加通知成功！")
            var index = window.parent.addNotice;
            parent.layer.close(index);
            Getdengl();
        }
        else {
            var index = window.parent.addNotice;
            parent.layer.close(index);
            Getdengl();
        }

    }
    else {
        alert("添加通知失败！")
    }
   // Getdengl();
}
var pageii = '';
function Getdengl() {
    var strHtml = "";
    strHtml += "<div style='width:400px; position:absolute; left:50%; margin-left:-200px; top:0px; background:#fff; z-index:999'><h4>提示</h4><i class='icon icon_close' id='pagebtn'></i>";
    strHtml += "<div class='tip_close'></div>";
    strHtml += "<p>非常抱歉，由于您的短信额度已经用完，短信通知发送失败，请及时告知学校 '系统管理员' 联系客服增加短信条数。</p>";
    strHtml += "<span><div id='show'>3秒</div>后自动关闭</span>";
    strHtml += "</div>";
    pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0.8, '#000'], //去掉遮罩
        closeBtn: [0, true], //去掉默认关闭按钮
        shift: 'left', //从左动画弹出
        page: {
            html: strHtml
        }
    });
    //show();
}
var strtime = 4;
function show() {
    strtime -= 1;
    document.getElementById('show').innerHTML = strtime;
    if (strtime == 1) {

    }
    window.setTimeout("show()", 1000);
}
//自设关闭
$('#pagebtn').on('click', function () {
    layer.close(pageii);
});





//计算剩余的字数
function calculateRestChar($this, $count) {
    var val = $this.val();
    var length = (val == "" ? 0 : val.length);
    var restCount = 67 - parseInt(length);
    if (IsMSM.checked) {
        if (restCount >= 0) {
            $count.html('<em>还能输入' + restCount + '字</em>');
            //$count.find("em").css("color", "");
        } else {
            $count.html('<em>超过' + Math.abs(restCount) + '字</em>');
            $count.find("em").css("color", "red");
            //$this.css("border", "1px solid rgb(255,55,2)");
            return;
        }
    }
    else {
        $count.html('<em>已输入' + parseInt(length) + '字</em>');
    }
    //if (length == 0) {
    //    $this.css("border", "1px solid rgb(255,55,2)");
    //} else {
    //    $this.css("border", "1px solid rgb(204,204,204)");
    //}
}


//添加教师
var addTeacher;
function AddTeacher() {
    var TOrgIDs = $("#hdnTOrgIDs").val();
    $("#hdnTHasChange").val(0);
    var url = 'GetTeacher.aspx?TOrgIDs=' + TOrgIDs;
    addTeacher = $.layer({
        type: 2,
        shadeClose: false,
        fix: false,
        title: false,
        closeBtn: [0, false],
        shade: [0.8, '#000'],
        border: [0],
        offset: ['100px', ''],
        area: ['410px', '400px'],
        iframe: { src: url },
        end: function (index, save) {
            if ($("#hdnTHasChange").val() == 1) {
                ShowTeacherDetials();
            }
        }
    });
}

var TeacherDetialsJson;

function ShowTeacherDetials() {
    var url = "Notice.ashx";
    var params = {
        action: "GetTeacher",
        OrganizationIDs: $("#hdnTOrgIDs").val()
    };

    TeacherDetialsJson = $G2S.GetAjaxJson(params, url);
    GetTHtml(TeacherDetialsJson);
    //json.Rows.slipe(0, 1);

}
function GetTHtml(json) {
    var strHtml = "";
    var OrganizationIDs = "";
    if (json != undefined && json != "暂无数据") {
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var OrganizationID = rows.OrganizationID;
            OrganizationIDs += OrganizationID + ',';
            var OrganizationName = rows.OrganizationName;
            strHtml += '<span><a href="javascript:;">' + OrganizationName + '</a><i class="icon_admin remove_btn" onclick="DeleteOrange(' + i + ')"></i></span>';
        }
    }
    if (OrganizationIDs.indexOf(',') > 0) {
        OrganizationIDs = OrganizationIDs.substring(0, OrganizationIDs.length - 1);
    }
    $("#hdnTOrgIDs").val(OrganizationIDs);
    $("#SelectedTeacher").html(strHtml);
}
function DeleteOrange(index) {
    TeacherDetialsJson.Rows.splice(index, 1);

    GetTHtml(TeacherDetialsJson);
}
//end添加教师

//添加学生
var addStudent;
function AddStudent() {
    $("#hdnSHasChange").val(0);

    var url = 'GetStudents.aspx';
    addStudent = $.layer({
        type: 2,
        shadeClose: false,
        title: false,
        closeBtn: [0, false],
        shade: [0.8, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['600px', '400px'],
        iframe: { src: url },
        end: function (index, save) {
            if ($("#hdnSHasChange").val() == 1) {
                ShowStudentDetials();
                SessionStudents();
                alert($("#hdnSYearIDs").val());
            }
        }
    });
}

function SessionStudents()
{
    var url1 = "Notice.ashx";
    var params = {
        action: "SessionStudents",
        hdnSSpecialIDs: $("#hdnSSpecialIDs").val(),
        hdnSYearIDs: $("#hdnSYearIDs").val()
    }
    alert($("#hdnSYearIDs").val());
    $G2S.GetAjaxJson(params, url1);
}

var StudentDetialsJson;
function ShowStudentDetials() {
    var url = "Notice.ashx";
    var params = {
        action: "GetStudent",
        SpecialtyIDs: $("#hdnSSpecialIDs").val()
    };
    StudentDetialsJson = $G2S.GetAjaxJson(params, url);
    GetSHtml(StudentDetialsJson);
}

function GetSHtml(json) {
    var strHtml = "";
    var SpecialtyIDs = "";
    if (json != undefined && json != "暂无数据") {
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var SpecialtyID = rows.SpecialtyID;
            SpecialtyIDs += SpecialtyID + ',';
            var SpecialtyName = rows.SpecialtyName;
            strHtml += '<span><a href="javascript:;">' + SpecialtyName + '</a><i class="icon_admin remove_btn" onclick="DeleteSpecial(' + i + ')"></i></span>';
        }
    }
    if (SpecialtyIDs.indexOf(',') > 0) {
        SpecialtyIDs = SpecialtyIDs.substring(0, SpecialtyIDs.length - 1);
    }
    $("#hdnSSpecialIDs").val(SpecialtyIDs);
    SessionStudents();
    $("#SelectedStudent").html(strHtml);

}
function DeleteSpecial(index) {
    StudentDetialsJson.Rows.splice(index, 1);
    GetSHtml(StudentDetialsJson);
}

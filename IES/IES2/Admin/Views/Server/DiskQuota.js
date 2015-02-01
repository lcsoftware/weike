
var UserTye = '';
var DiskSize = '';
function shows(i) {
    var jbxx = document.getElementById("TeacherOne");
    var jzxx = document.getElementById("StudentTwo");
    var bzxx = document.getElementById("PersonalityThr");
    var xx = document.getElementById("xx");
    var yy = document.getElementById("yy");
    var xxyy = document.getElementById("xxyy");
    if (i == 1) {
        jbxx.style.display = "block";
        jzxx.style.display = "none";
        bzxx.style.display = "none";
        xx.className = 'active';
        yy.className = '';
        xxyy.className = '';
        $("<%=Count.ClientID %>").val("8");
        $('<%=btnInfo.ClientID %>').click();
        //UserTye = 8;
        //GetDiskSpaceList();        
    }
    if (i == 2) {
        jzxx.style.display = "block";
        jbxx.style.display = "none";
        bzxx.style.display = "none";
        xx.className = '';
        yy.className = 'active';
        xxyy.className = '';
        $("<%=Count.ClientID %>").val("4");
        $('<%=btnInfo.ClientID %>').click();
    }
    if (i == 3) {
        bzxx.style.display = "block";
        jbxx.style.display = "none";
        jzxx.style.display = "none";
        xx.className = '';
        yy.className = '';
        xxyy.className = 'active';
        $("<%=Count.ClientID %>").val("12");
        $("<%=btnInfo.ClientID %>").click();
    }
}

//存储空间封闭
function Del(id) {
    if (confirm("你确定要封闭该用户的存储空间吗?封闭后对方将不能上传任何资源!") == true) {
        $("#<%=hfID.ClientID %>").val(id);
        $("#<%=btnInfo.ClientID %>").click();
    }

}

//空间使用情况列表
function GetDiskSpaceList() {
    var strHtml = '';
    $("#TeacherOne").html("");
    strHtml += "<table border='0' cellpadding='0' cellspacing='0' width='700' >";
    strHtml += "      <colgroup>"
    strHtml += "        <col width='10%' /><col width='5%' /><col width='20%' /><col width='15%' /><col width='15%' /><col width='15%' /><col width='20%' />";
    strHtml += "      </colgroup>";
    strHtml += "      <tr class='item_title'>";
    strHtml += "            <td>工号</td>";
    strHtml += "            <td>姓名</td> "
    strHtml += "            <td>所属机构</td>";
    strHtml += "            <td>分配空间</td>";
    strHtml += "            <td>已用空间</td>";
    strHtml += "            <td>剩余空间空间</td>";
    strHtml += "            <td></td>";
    strHtml += "      </tr>";
    var data = { action: "GetTeacherList", UserName: ""};
    var stuList = $G2S.GetAjaxJson(data, "AddTeacher.ashx");
    if (stuList != "暂无数据") {
        for (var i = 0; i < stuList.length; i++) {
            var row = stuList[i];
            var UserID = row.UserID;
            var UserNo = row.UserNo;
            var Name = row.UserName;
            var OrganizationName = row.OrganizationName;
            RowsCount = row.rowscount;
            PageCount = (RowsCount % 10 == 0 ? RowsCount / 10 : parseInt(RowsCount / 10) + 1);
            strHtml += "<tr class='item item_row' id=" + UserID + " title=" + Name + ">";
            strHtml += "    <td style='float: right; '><input type='radio' onclick='' name='ckbItem' pid='" + UserID + "' sname='" + Name + "' class='ckbcss'/></td>";
            strHtml += "    <td></td>";
            strHtml += "    <td>" + UserNo + "</td>";
            strHtml += "    <td>" + Name + "</td>";
            strHtml += "    <td>" + ((OrganizationName == null || OrganizationName == "") ? "" : OrganizationName) + "</td>";
            strHtml += "</tr>";
        }

    }
    strHtml += "</table>";
    $("#teacherList").html(strHtml);
}
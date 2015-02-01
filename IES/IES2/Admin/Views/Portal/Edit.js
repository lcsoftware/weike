var ii = 100;
var timer1;
//编辑
function Edit(thi) {
    var pid = location.search;
    if (pid == "")
    { window.location.href = "Edit.aspx?id=" + $(thi).attr("pid"); }
    else
    { window.location.href = "Edit.aspx" + pid + "&id=" + $(thi).attr("pid"); }
}
//新增
function ADD() {
    var pid = location.search;
    window.location.href = "Edit.aspx"+pid;
}
//全选
function selectAll(form) {
    var obj = document.getElementsByName('ckbAll');
    var cks = document.getElementsByName("ckbItem");
    var ckslen = cks.length;
    for (var i = 0; i < ckslen; i++) {
        if (cks[i].type == 'checkbox') {
            cks[i].checked = obj[0].checked;
        }
    }
}
//未开放此功能
function NoOpen()
{
    alert("暂未开放此功能");
}
//隐藏提示信息
function Hidets() {
    var o = document.getElementById("noticets");
    if (o != null)
    {
        timer1 = setInterval(change,10)
    }   
}
function change() {
    var o = document.getElementById("noticets");
    ii--;
    o.style.filter = "Alpha(Opacity=" + ii + ")"; //for IE	
    o.style.opacity = ii / 100; //for FF
    if (ii < 1)
    {
        o.style.display = 'none';
        clearInterval(timer1);      
    }
        

}
//批量删除
function DelInfo()
{
    var obj = document.getElementsByName('ckbItem');
    var ids=null;
    for(var i=0;i<obj.length;i++)
    {
        if(obj[i].checked)
        {
            var id = obj[i].parentNode.parentNode.id;
            if (ids == null) {
                ids = id;
            }
            else {
                ids = id + "," + ids;
            }            
        }
    }
    if(ids!=null)
        DelBatch(ids);
    else
        alert("请选择要删除的项")
}
//获取选择项
function SelInfo() {
    var obj = document.getElementsByName('ckbItem');
    var ids = null;
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].checked) {
            var id = obj[i].parentNode.parentNode.id;
            if (ids == null) {
                ids = id;
            }
            else {
                ids = id + "," + ids;
            }
        }
    }
    if (ids != null) {
        Json(ids);
    }
    else
        alert("请选择要添加的用户")
}
//关闭
function Close() {
    alert(123);
    parent.layer.close(index);
}
//判断条件选择情况
function ParmInfo()
{
    var parm = "";
    //专业10
    var sps = document.getElementById("spParm");
    if (sps != null) {
        var sp = sps.getElementsByTagName("span");
        for (var i = 0; i < sp.length; i++) {
            if (sp[i].className == "active") {
                parm = sp[i].id + "," + parm;
            }
        }
    }
    //所属机构9
    var orgs = document.getElementById("orgParm");
    if (orgs != null) {
        var org = orgs.getElementsByTagName("span");
        for (var i = 0; i < org.length; i++) {
            if (org[i].className == "active") {
                parm = org[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //学期8
    var trs = document.getElementById("trParm");
    if (trs != null) {
        var tr = trs.getElementsByTagName("span");
        for (var i = 0; i < tr.length; i++) {
            if (tr[i].className == "active") {
                parm = tr[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //课程分类7
    var crtys = document.getElementById("crtyParm");
    if (crtys != null) {
        var crty = crtys.getElementsByTagName("span");
        for (var i = 0; i < crty.length; i++) {
            if (crty[i].className == "active") {
                parm = crty[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //授课方式6
    var crtchtys = document.getElementById("crtchtyParm");
    if (crtchtys != null) {
        var crtchty = crtchtys.getElementsByTagName("span");
        for (var i = 0; i < crtchty.length; i++) {
            if (crtchty[i].className == "active") {
                parm = crtchty[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //学制5
    var schlens = document.getElementById("schlenParm")    
    if (schlens != null) {
        var schlen = schlens.getElementsByTagName("span");
        for (var i = 0; i < schlen.length; i++) {
            if (schlen[i].className == "active") {
                parm = schlen[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //入学时间4
    var etys = document.getElementById("etyParm");
    if (etys != null) {
        var ety = etys.getElementsByTagName("span");
        for (var i = 0; i < ety.length; i++) {
            if (ety[i].className == "active") {
                parm = ety[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //行政班3
    var clss = document.getElementById("clsParm");
    if (clss != null) {
        var cls=clss.getElementsByTagName("span")
        for (var i = 0; i < cls.length; i++) {
            if (cls[i].className == "active") {
                parm = cls[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //是否校内2
    var schools = document.getElementById("IsInSchool");
    if (schools != null) {
        var school = schools.getElementsByTagName("span")
        for (var i = 0; i < school.length; i++) {
            if (school[i].className == "active") {
                parm = school[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //是否助教1
    var asstants = document.getElementById("IsAssistant");
    if (asstants != null) {
        var asstant = asstants.getElementsByTagName("span")
        for (var i = 0; i < asstant.length; i++) {
            if (asstant[i].className == "active") {
                parm = asstant[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    //是否展示0
    var shows = document.getElementById("IsShow");
    if (shows != null) {
        var show = shows.getElementsByTagName("span")
        for (var i = 0; i < show.length; i++) {
            if (show[i].className == "active") {
                parm = show[i].id + "," + parm;
            }
        }
    }
    else { parm = -1 + "," + parm; }
    return parm;
}
//选择条件改变样式
function Gbclass(obj)
{
    if(obj!=null)
    {
        var pid = obj.parentNode.id;
        var div = document.getElementById(pid);
        if(div!=null)
        {
            var span=div.getElementsByTagName("span");
            for(var i=0;i<span.length;i++)
            {
                span[i].className = '';
            }
            obj.className = 'active';
        }
    }
}
//样式保存
function Saveclass(parms)
{
    var ary = parms.split(',');
    //专业
    var sps = document.getElementById("spParm");
    if (sps != null) {
        var sp = sps.getElementsByTagName("span");
        for (var i = 0; i < sp.length; i++) {
            if (sp[i].id == ary[10]) {
                sp[i].className = 'active';
            }
        }
    }
    //所属机构
    var orgs = document.getElementById("orgParm");
    if (orgs != null) {
        var org = orgs.getElementsByTagName("span");
        for (var i = 0; i < org.length; i++) {
            if (org[i].id == ary[9]) {
                org[i].className = 'active';
            }
        }
    }
    //学期
    var trs = document.getElementById("trParm");
    if (trs != null) {
        var tr = trs.getElementsByTagName("span");
        for (var i = 0; i < tr.length; i++) {
            if (tr[i].id == ary[8]) {
                tr[i].className = 'active';
            }
        }
    }
    //课程分类
    var crtys = document.getElementById("crtyParm");
    if (crtys != null) {
        var crty = crtys.getElementsByTagName("span");
        for (var i = 0; i < crty.length; i++) {
            if (crty[i].id == ary[7]) {
                crty[i].className = 'active';
            }
        }
    }
    //授课方式
    var crtchtys = document.getElementById("crtchtyParm");
    if (crtchtys != null) {
        var crtchty = crtchtys.getElementsByTagName("span");
        for (var i = 0; i < crtchty.length; i++) {
            if (crtchty[i].id == ary[6]) {
                crtchty[i].className = 'active';
            }
        }
    }
    //学制
    var schlens = document.getElementById("schlenParm")
    if (schlens != null) {
        var schlen = schlens.getElementsByTagName("span");
        for (var i = 0; i < schlen.length; i++) {
            if (schlen[i].id == ary[5]) {
                schlen[i].className = 'active';
            }
        }
    }
    //入学时间
    var etys = document.getElementById("etyParm");
    if (etys != null) {
        var ety = etys.getElementsByTagName("span");
        for (var i = 0; i < ety.length; i++) {
            if (ety[i].id == ary[4]) {
                ety[i].className = 'active';
            }
        }
    }
    //行政班
    var clss = document.getElementById("clsParm");
    if (clss != null) {
        var cls = clss.getElementsByTagName("span");
        for (var i = 0; i < cls.length; i++) {
            if (cls[i].id == ary[3]) {
                cls[i].className = 'active';
            }
        }
    }
    //是否校内
    var schools = document.getElementById("IsInSchool");
    if (schools != null) {
        var school = schools.getElementsByTagName("span");
        for (var i = 0; i < school.length; i++) {
            if (school[i].id == ary[2]) {
                school[i].className = 'active';
            }
        }
    }
    //是否助教
    var asstants = document.getElementById("IsAssistant");
    if (asstants != null) {
        var asstant = asstants.getElementsByTagName("span");
        for (var i = 0; i < asstant.length; i++) {
            if (asstant[i].id == ary[1]) {
                asstant[i].className = 'active';
            }
        }
    }
    //是否展示
    var shows = document.getElementById("IsShow");
    if (shows != null) {
        var show = shows.getElementsByTagName("span");
        for (var i = 0; i < show.length; i++) {
            if (show[i].id == ary[0]) {
                show[i].className = 'active';
            }
        }
    }
}
//教学班编辑
function TeachingEdit(thi) {
    var pid = location.search;
    if (pid == "")
    { window.location.href = "TeachingClassEdit.aspx?id=" + $(thi).attr("pid"); }
    else
    { window.location.href = "TeachingClassEdit.aspx" + pid + "&id=" + $(thi).attr("pid"); }
}
//教学班新增
function TeachingADD() {
    var pid = location.search;
    window.location.href = "TeachingClassEdit.aspx" + pid;
}
//校历课节编辑
function CalendarEdit(thi) {
    var pid = location.search;
    if (pid == "")
    { window.location.href = "CalendarEdit.aspx?id=" + $(thi).attr("pid"); }
    else
    { window.location.href = "CalendarEdit.aspx" + pid + "&id=" + $(thi).attr("pid"); }
}
//账户新增
function AccountADD() {
    var pid = location.search;
    window.location.href = "AccountEdit.aspx" + pid;
}
//校历课节新增
function CalendarADD() {
    var pid = location.search;
    window.location.href = "CalendarEdit.aspx" + pid;
}
//角色权限样式保存
function Roleclass(parms) {
    var ary = parms.split(',');
    CutCss2(ary[0]);
    Chakan2(ary[1]);
}
function Chakan2(s) {
    var sbt1 = document.getElementById("btns1");
    var sbt2 = document.getElementById("btns2");
    if (s == 1) {
        sbt1.style.display = 'none';
        sbt2.style.display = 'block';
        document.getElementById("jsqx").style.display = 'none';
        document.getElementById("glzh").style.display = 'block';
    }
    else {
        sbt1.style.display = 'block';
        sbt2.style.display = 'none';
        document.getElementById("jsqx").style.display = 'block';
        document.getElementById("glzh").style.display = 'none';
    }
}
function CutCss2(aid) {
    var lis = document.getElementsByTagName("li");
    for (var i = 0; i < lis.length; i++) {
        if (lis[i].id == aid) {
            lis[i].className = 'current';
        }
        else
            lis[i].className = '';
    }
    var aa = document.getElementsByClassName("u_203");
    for (var i = 0; i < aa.length; i++) {
        if (aa[i].parentNode.parentNode.parentNode.id == aid) {
            aa[i].style.display = 'block';
        }
        else {
            aa[i].style.display = 'none';
        }
    }
}
//用户权限样式保存
function Userclass(parm) {
    OrgTab(parm);
}
function OrgTab(parm) {
    var lis = document.getElementsByTagName("li");
    for (var i = 0; i < lis.length; i++) {
        if (lis[i].id == parm) {
            lis[i].className = 'current';
        }
        else
            lis[i].className = '';
    }
}


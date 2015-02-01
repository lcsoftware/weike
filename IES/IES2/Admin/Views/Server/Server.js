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
function page3() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['新增管理员', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['800px', ($(window).height() - 50) + 'px'],
        iframe: { src: '../JW/Specialty/SpTeacherADD.aspx' }

    });
}
//新增存储服务器
function PageServer1() {
    ClearEdit(1);
    var pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'top', //从左动画弹出
        page: { dom: '#ServerAdd' }
    });
    //自设关闭
    $('#pagebtn1').on('click', function () {
        layer.close(pageii);
    });
    $('#pagebtn2').on('click', function () {
        layer.close(pageii);
    });
}
//编辑存储服务器
function PageServer2(sid) {
    InitEdit(sid);
    var pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'top', //从上动画弹出
        page: { dom: '#ServerEdit' }
    });
    //自设关闭
    $('#pagebtn3').on('click', function () {
        layer.close(pageii);
    });
    $('#pagebtn4').on('click', function () {
        layer.close(pageii);
    });
}
//初始化编辑
var InitEdit = function (sid) {
    ClearEdit(2);
    var url = "Status.ashx";
    var para = { action: "GetServer", ServerID: sid };
    var result = $G2S.GetAjaxJson(para, url);
    var bref = result.Brief;
    if (bref != null)
    {
        var ary = bref.split(';');
        $("#MainContent_pinpai2").val(ary[0].split(':')[1]);
        $("#MainContent_xinhao2").val(ary[1].split(':')[1]);
        $("#MainContent_chuliqi2").val(ary[2].split(':')[1]);
        $("#MainContent_neicun2").val(ary[3].split(':')[1]);
        $("#MainContent_yinpan2").val(ary[4].split(':')[1]);
        $("#MainContent_xitong2").val(ary[5].split(':')[1]);
    };
    $("#MainContent_host2").val(result.Host);
    $("#MainContent_iispost2").val(result.IISPort);
    $("#MainContent_iisfolder2").val(result.IISFolder);
    $("#MainContent_mmsfolder2").val(result.MMSFolder);
    $("#MainContent_mmsport2").val(result.MMSPort);
    $("#MainContent_nginxf2").val(result.NginxFolder);
    $("#MainContent_nginxp2").val(result.NginxPort);
    $("#MainContent_pubkey2").val(result.PubKey);   
}
//清空
var ClearEdit = function (i)
{
    $("#MainContent_host"+i).val("");
    $("#MainContent_iispost"+i).val("");
    $("#MainContent_iisfolder"+i).val("");
    $("#MainContent_mmsfolder"+i).val("");
    $("#MainContent_mmsport"+i).val("");
    $("#MainContent_nginxf"+i).val("");
    $("#MainContent_nginxp"+i).val("");
    $("#MainContent_pubkey"+i).val("");

    $("#MainContent_pinpai"+i).val("");
    $("#MainContent_xinhao"+i).val("");
    $("#MainContent_chuliqi"+i).val("");
    $("#MainContent_neicun"+i).val("");
    $("#MainContent_yinpan"+i).val("");
    $("#MainContent_xitong"+i).val("");
}
//存储空间删除警告
function NoticDel() {
    ClearEdit(1);
    var pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'left', //从左动画弹出
        page: { dom: '#NoticDel' }
    });
    //自设关闭
    $('#pagebtn1').on('click', function () {
        layer.close(pageii);
    });
    $('#pagebtn2').on('click', function () {
        layer.close(pageii);
    });
}

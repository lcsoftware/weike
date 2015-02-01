$(document).ready(function () {

    $("input[name='OnePwd']").blur(function () {
        $(this).val() == "" ? $("#NoMsg").show() : $("#NoMsg").hide();
    }).focus(function () {
        $("#NoMsg").hide();
    });

    $("input[name='TwoPwd']").blur(function () {
        $(this).val() == "" ? $("#NameMsg").show() : $("#NameMsg").hide();
    }).focus(function () {
        $("#NameMsg").hide();
    });
});
//弹出层
var box;
function ShowPwd() {
    box = $.layer({
        type: 1,
        title: ["修改密码", true],
        shift: 'top',
        shade: [0.5, '#000'],
        area: ['600px', "240px"],
        page: { dom: '#UpdatePwd' }
    });
    $(".xubox_title").attr("style", "background:none repeat scroll 0 0 #284a51;color:#fff;cursor: move;");
}

//修改密码
var AddOrEdit = function () {
    if ($("input[name='OnePwd']").val() == "")
    {
        $("#NoMsg").show()
    }else if ( $("input[name='TwoPwd']").val()=="") {
        $("#NameMsg").show()
    }
    //debugger;
    var Pwd = $("input[name='TwoPwd']").val();
    var UserID = $("#<%=UserID.ClientID %>").val();
    return;

    var url = "Account.ashx";
    var para = {
        action: "UpdAccount",
        UserID: UserID,
        Pwd: Pwd
    };
    var result = $G2S.GetAjaxJson(para, url);
    if (result == true) {
        alert("修改成功!");
    } else {
        alert("修改失败!");
    }
    layer.closeAll();
}
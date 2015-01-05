$(document).ready(function () {
    GetNoticeList();
});


//通知列表
var rowscount = "0"; //总页数
var Pageindex = 1;
var pagesize = 20;
function GetNoticeList() {

    if ($("#sel_pageSize").val() != "") {
        pagesize = $("#sel_pageSize").val();
    }
    if (Pageindex <= 1) {
        Pageindex = 1;
    }
    var url = "Notice.ashx";
    var params = {
        action: "GetNoticeList",
        PageSize: pagesize,
        PageIndex: Pageindex
    };
    var strHtml = "";

    var json = $G2S.GetAjaxJson(params, url);
    if (json != "暂无数据") {
        strHtml += "<table class='result_table'>";
        strHtml += "    <tr>";
        strHtml += "                        <th width='50'></th>";
        strHtml += "                        <th width='50'><input type='checkbox'></th>";
        strHtml += "                        <th width='470'>主题</th>";
        strHtml += "                        <th width='150'>收件人</th>";
        strHtml += "                        <th width='170'>发送时间</th>";
        strHtml += "                        <th>操作</th>";
        strHtml += "                    </tr>";
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var NoticeID = rows.NoticeID;
            var Title = rows.Title;
            var UpdateTime = rows.UpdateTime;
            var EndDate = rows.EndDate;
            var SysID = rows.SysID;
            var UserID = rows.UserID;
            var IsTop = rows.IsTop;
            rowscount = rows.rowscount;
            strHtml += " <tr id='" + NoticeID + "'>";
            strHtml += "                        <td></td>";
            strHtml += "                        <td><input type='checkbox'></td>";
            strHtml += "                        <td>" + Title + "</td>";
            strHtml += "                        <td>" + UserID + "</td>";
            strHtml += "                        <td>" + UpdateTime + "</td>";
            strHtml += "                        <td><p class='operation_box'><i title='编辑' pid='" + NoticeID + "' onclick='Eidt(this)' class='icon_admin edit_btn'></i><i title='删除' onclick='Del(this)'  pid='" + NoticeID + "' class='icon_admin delete_btn'></i></p></td>";
            strHtml += "                    </tr>";
        }
        strHtml += "</table>";
    }
    $("#div_List").html(strHtml);
    Page(Pageindex, rowscount);
    init();

}

//编辑
function Eidt(thi) {
    alert("这是编辑:id " + $(thi).attr("pid"));
}

//删除
function Del(thi) {
    alert("这是删除:id " + $(thi).attr("pid"));
}

//分页
function Page(count, rowscount) {
    if ($("#sel_pageSize").val() != "") {
        pagesize = $("#sel_pageSize").val();
    }
    var strHtml = "";
    var flagcount = Math.ceil(rowscount / pagesize);

    strHtml += "<div class='page_box'>";

    if (flagcount > 9) {
        strHtml += " <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count - 1) + ")'>前一页</a>";
        for (var i = count; i < count + 4; i++) {

            strHtml += "  <a href='javascript:void(0);' onclick='GetPageSizeCount(" + i + ")' >" + i + "</a>";
        }
        strHtml += "    <span class='more'>...</span>";
        strHtml += "     <a href='javascript:void(0);' onclick='GetPageSizeCount(" + (parseInt(count) + 8) + ")' >" + (parseInt(count) + 8) + "</a>";
        strHtml += "     <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count + 1) + ")'>后一页</a>";
        strHtml += "     <span>共" + rowscount + "条，到第<input onkeyup=\"this.value=this.value.replace(/\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\D/g,'')\" id='txt_pageindex' type='text'>页</span>";
        strHtml += "     <a class='confirm' href='javascript:void(0);' onclick='GetPageSizeCount(" + $("#txt_pageindex").val() + ")' >确认</a>";

    } else {
        strHtml += " <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count - 1) + ")'>前一页</a>";
        for (var i = count; i <= flagcount; i++) {

            strHtml += "  <a href='javascript:void(0);' onclick='GetPageSizeCount(" + i + ")' >" + i + "</a>";
        }
        strHtml += "   <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count + 1) + ")'>后一页</a>";
        strHtml += "   <span>共" + rowscount + "条，到第<input onkeyup=\"this.value=this.value.replace(/\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\D/g,'')\" id='txt_pageindex' type='text'>页</span>";
        strHtml += "   <a class='confirm' href='javascript:void(0);' onclick='GetPageSizeCount(" + $("#txt_pageindex").val() + ")' >确认</a>";
    }
    strHtml += "</div>";

    $("#div_page_wrap").html(strHtml);
}

//分页按钮方法
function GetPageSizeCount(index) {
    Pageindex = index;
    GetNoticeList();
}



//加载样式
function init() {
    $('.class_operation').hover(function () {
        $(this).find('ul').toggle();
    })

    $('.fold_btn').click(function () {
        if (!$(this).hasClass('click')) {
            $(this).addClass('click');
            $(this).siblings('.select_require').css('height', 'auto');
            $(this).text('[收起]');
        } else {
            $(this).removeClass('click');
            $(this).siblings('.select_require').css('height', '30px');
            $(this).text('[更多]');
        }
    })

    $('.close_box').live('click', function () {
        $(this).parent().slideToggle();
    })

    $('.result_table tr').hover(function () {
        $(this).find('.operation_box').toggle();
    })

    $('.result_table').each(function () {
        $(this).find('tr:odd').css('background', '#f7f7f7');
    })
}

//选择分页
function GetListChange(thi) {
    GetNoticeList();
}

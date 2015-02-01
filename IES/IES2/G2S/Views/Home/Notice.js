
var noticePageIndex = 1;
var noticePageSize = 3
var commentPageSize = 5;//
var ShortPageSize = 3;//

$(document).ready(function () {
    loadFace($("#myBody"));//载入表情选择框
    $(document).bind("click", function () {
        hideFace();
    });//载入表情选择框
    SearchMessage();
});

//获取通知列表
function SearchMessage() {
    var noticeType = GetSearchType();
    var url = "Notice.ashx";
    var params = {
        action: "GetNoticeList",
        fType: noticeType,
        CourseName: "",
        PageSize: noticePageSize,
        PageIndex: noticePageIndex
    };
    var strHtml = "";
    var json = $G2S.GetAjaxJson(params, url);
    if (json != undefined && json != "暂无数据") {
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var NoticeID = rows.NoticeID;
            var Title = unescape(rows.Title);
            var Conten = unescape(rows.Conten);
            var UpdateTime = rows.UpdateTime;
            var EndDate = rows.EndDate;
            var SysID = rows.SysID;
            var UserID = rows.UserID;
            var UserName = rows.UserName;
            var IsTop = rows.IsTop;
            var Responses = rows.Responses;
            var rowscount = rows.rowscount;
            strHtml += '  <div class="message_list">';
            strHtml += '         <div class="message_tit">';
            if (IsTop) {
                strHtml += '               <i class="icon star"></i>';
            }
            strHtml += '               <h4>' + Title + '</h4>';
            strHtml += '         </div>';
            strHtml += '         <p class="notice_content">' + Conten + '</p>';
            strHtml += '         <div class="issue_detail">';
            strHtml += '         <a class="comment_btn" id="comment_btn_' + NoticeID + '" onclick="ShowComment(this,' + NoticeID + ')" loaded="false"  href="javascript:;"><span id="cmt_text_' + NoticeID + '">评论</span>（' + Responses + '）</a>';
            strHtml += '         <p>2' + UpdateTime + '&nbsp;来自<a href="#">智慧树</a></p>';
            strHtml += '         </div>';
            strHtml += '         <div class="comment_box" id="comment_box_' + NoticeID + '">';
            strHtml += '               <span class="triangle"></span>';
            strHtml += '               <div class="comment_content" style="display: block;">';
            strHtml += '                   <div class="chat_box">';
            strHtml += '                       <input class="chat_detail" id="chat_detail_' + NoticeID + '" type="text" value="请输入评论内容">';
            strHtml += '                       <textarea class="text_area"  id="text_area_' + NoticeID + '" onblur="afterText(this,' + NoticeID + ')"  style="display: none; border: 1px solid rgb(255, 55, 2);"></textarea>';
            strHtml += '                   </div>';
            strHtml += '                   <div class="expression" id="expression_' + NoticeID + '">';
            strHtml += '                        <a class="Face_a fl" id="biaoqing_' + NoticeID + '"  onmouseover="faceChange(' + NoticeID + ',true)" onmouseout="faceChange(' + NoticeID + ',false)" href="javascript:;">表情</a>';
            strHtml += '                        <a class="issue_comment" href="javascript:;" onclick="ResponseNotice(' + NoticeID + ')" >发表</a>';
            strHtml += '                        <span class="talkCountTxt fr">还能输入<em>250</em>字</span>';
            strHtml += '                   </div>';
            strHtml += '                   <div class="comment_detail" id="comment_detail_' + NoticeID + '">';
            //strHtml += '                        <ul class="comment_list">';
            //strHtml += '                            <li>';
            //strHtml += '                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />';
            //strHtml += '                                <p><a href="#" target="_blank">客服小慧</a><span>:你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒</span><b>(发表于46分钟前)</b></p>';
            //strHtml += '                            </li>';
            //strHtml += '                        </ul>';
            //strHtml += '                        <a class="more_comment" href="javascript:;">点击查看更多评论</a>';
            //strHtml += '                        <div class="page_wrap" id="div_page_wrap"></div>';
            strHtml += '                    </div>';

            strHtml += '               </div>';
            strHtml += '         </div>';
            strHtml += '   </div>';


            //绑定展开提问表情事件
            $("#biaoqing_" + NoticeID).live("click", function () {
                var NoticeIDTemp = this.id.split('_')[this.id.split('_').length - 1]
                showFace($(this), $("#text_area_" + NoticeIDTemp), function () {
                    var $this = $("#text_area_" + NoticeIDTemp);
                    var $count = $this.parent().next().children().last();
                    calculateRestChar($this, $count);
                });
                selectFace();
            });
            //
            $("#text_area_" + NoticeID).live("focus", function () {
                $(this).keyup(function () {
                    var $count = $(this).parent().next().children().last();
                    calculateRestChar($(this), $count);
                }).change(function () {
                    var $count = $(this).parent().next().children().last();
                    calculateRestChar($(this), $count);
                }).bind('paste', function () {
                    var $count = $(this).parent().next().children().last();
                    calculateRestChar($(this), $count);
                }).focus(function () {
                    var $count = $(this).parent().next().children().last();
                    calculateRestChar($(this), $count);
                });
            });
            //selectFace();
        }
    }
    $("#message_detail").html(strHtml);

}



//获取评论
function GetCommentShortDetail(NoticeID) {
    GetCommentDetail(1, NoticeID, ShortPageSize);
}

//获取评论  当前页  通知编号  ，显示条数
function GetCommentDetail(index, NoticeID, PageSize) {
    var url = "Notice.ashx";
    var params = {
        action: "GetCommentList",
        NoticeID: NoticeID,
        PageIndex: index,
        PageSize: PageSize
    };

    var strHtml = "";
    var json = $G2S.GetAjaxJson(params, url);
    var rowscount = 0;
    if (json != undefined && json != "暂无数据") {
        strHtml += '                        <ul class="comment_list">';
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var ResponseID = rows.ResponseID;
            var Conten = unescape(rows.Conten);
            var UserID = rows.UserID;
            var UserName = rows.UserName;
            var ClassName = rows.ClassName;
            var UpdateTime = rows.UpdateTime;
            var IsTop = rows.IsTop;
            var RowNum = rows.RowNum;
            var CurrentTime = rows.CurrentTime;
            var html = '';
            html = replaceStrToImg(Conten)
            rowscount = rows.rowscount;
            strHtml += '                            <li>';
            strHtml += '                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />';
            strHtml += '                                <p><a href="#" target="_blank">' + UserName + '</a><span>:' + html + '</span><b>(' + getDateDiff("发表于", UpdateTime, CurrentTime) + ')</b></p>';
            strHtml += '                            </li>';
        }
        strHtml += '                        </ul>';
        if (PageSize == ShortPageSize) {
            strHtml += '                        <a class="more_comment" href="javascript:;" onclick="GetCommentDetail(1,' + NoticeID + ',' + commentPageSize + ')" >点击查看更多评论</a>';
        }
        else {
            if (rowscount > commentPageSize) {
                strHtml += '                        <div class="page_wrap" id="page_wrap_' + NoticeID + '"></div>';
            }
        }
    }
    $("#comment_detail_" + NoticeID).html(strHtml);
    if (rowscount > commentPageSize) {
        Page(index, rowscount, NoticeID);
    }

}


//通知类型
function GetSearchType() {
    var ftype = -1
    var objS = document.getElementById('ckbSystem');//1
    var objC = document.getElementById('ckbCourse');//2


    if (objS.checked && !objC.checked) {
        ftype = 0;
    }
    else if (!objS.checked && objC.checked) {
        ftype = 1;
    }
    else {
        ftype = -1;
    }

}


//分页方法 当前页 ， 总条数，通知编号  //已共显示7页
function Page(count, rowscount, NoticeID) {
    var strHtml = "";
    var flagcount = Math.ceil(rowscount / commentPageSize);//总页数
    strHtml += "<div class='page_box'>";
    if (flagcount > 9) {
        if (count > 1) {
            strHtml += " <a class='prev' href='javascript:void(0);' onclick='GetCommentDetail(" + (count - 1) + "," + NoticeID + "," + commentPageSize + ")'>前一页</a>";
        }
        else {
            strHtml += " <a class='prev' >前一页</a>";
        }
        strHtml += "     <a href='javascript:void(0);' onclick='GetCommentDetail(1," + (parseInt(count) + 8) + "," + NoticeID + "," + commentPageSize + ")' >1</a>";//第一页

        if (count > 4 && count < flagcount - 3) {
            strHtml += "    <span class='more'>...</span>";
            strHtml += "    <a href='javascript:void(0);' onclick='GetCommentDetail(" + (parseInt(count) - 2) + "," + NoticeID + "," + commentPageSize + ")' >," + (parseInt(count) - 2) + "</a>";
            strHtml += "    <a href='javascript:void(0);' onclick='GetCommentDetail(" + (parseInt(count) - 1) + "," + NoticeID + "," + commentPageSize + ")' >," + (parseInt(count) - 1) + "</a>";
            //当前页
            strHtml += "    <a class='active' href='javascript:void(0);' onclick='GetCommentDetail(" + parseInt(count) + "," + NoticeID + "," + commentPageSize + ")' >," + parseInt(count) + "</a>";

            strHtml += "    <a href='javascript:void(0);' onclick='GetCommentDetail(" + (parseInt(count) + 1) + "," + NoticeID + "," + commentPageSize + ")' >," + (parseInt(count) - 2) + "</a>";
            strHtml += "    <a href='javascript:void(0);' onclick='GetCommentDetail(" + (parseInt(count) + 2) + "," + NoticeID + "," + commentPageSize + ")' >," + (parseInt(count) - 1) + "</a>";
            strHtml += "    <span class='more'>...</span>";
        }
        else if (count <= 4) {
            for (var i = 2; i < 7; i++) {
                if (i == count) {
                    strHtml += "  <a class='active' href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
                }
                else {
                    strHtml += "  <a href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
                }
            }
            strHtml += "    <span class='more'>...</span>";
        }
        else {
            strHtml += "    <span class='more'>...</span>";
            for (var i = flagcount - 6; i < flagcount - 1; i++) {
                if (i == count) {
                    strHtml += "  <a class='active' href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
                }
                else {
                    strHtml += "  <a href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
                }
            }
        }

        if (count < flagcount) {
            strHtml += "     <a class='prev' href='javascript:void(0);' onclick='GetCommentDetail(" + (count + 1) + "," + NoticeID + "," + commentPageSize + ")'>后一页</a>";
        }
        else {
            strHtml += "     <a class='prev'>后一页</a>";
        }

        strHtml += "     <span>共" + rowscount + "条，到第<input onkeyup=\"this.value=this.value.replace(/\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\D/g,'')\" id='txt_Index_" + NoticeID + "' type='text'>页</span>";
        strHtml += "     <a class='confirm' href='javascript:void(0);' onclick='GetCommentDetail(" + $("#txt_Index_" + NoticeID).val() + "," + NoticeID + "," + commentPageSize + ")' >确认</a>";
    }
    else {
        for (var i = 1; i <= flagcount; i++) {
            if (i == count) {
                strHtml += "  <a class='active' href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
            }
            else {
                strHtml += "  <a href='javascript:void(0);' onclick='GetCommentDetail(" + i + "," + NoticeID + "," + commentPageSize + ")' >" + i + "</a>";
            }
        }
        strHtml += "   <span>共" + rowscount + "条，到第<input class='JumpPage' id='txt_Index_" + NoticeID + "' type='text' value=" + count + " />页</span>";
        strHtml += "   <a class='confirm' href='javascript:void(0);' onclick='ChangePage(" + NoticeID + "," + flagcount + ")' >确认</a>";
    }
    strHtml += "</div>";
    $("#page_wrap_" + NoticeID).html(strHtml);
    // document.getElementById("page_wrap_" + NoticeID).innerHTML = strHtml;
    //document.getElementById("page_wrap_" + NoticeID).innerHTML = strHtml;

}


//跳转页面
$('.JumpPage').live('keyup', function () {
    this.value = this.value.replace(/\D/g, '');

});

$('.JumpPage').live('afterpaste', function () {
    this.value = this.value.replace(/\D/g, '');

});

//评论
function ShowComment(ele, NoticeID) {
    var $cmt_text = $("#cmt_text_" + NoticeID);
    if ($cmt_text.text() == '评论') {
        $cmt_text.text("收起");
    } else {
        $cmt_text.text("评论");
    }
    var $source = $(ele);
    if ($source.attr("loaded") == "true") {
        var $insert_list_comment = $("#comment_box_" + NoticeID);
        $insert_list_comment.toggle();
        return;
    }
    else {
        GetCommentShortDetail(NoticeID);
        var $insert_list_comment = $("#comment_box_" + NoticeID);
        $insert_list_comment.toggle();
    }
    $source.attr("loaded", "true");

}

////评论
$('.chat_detail').live('click', function () {
    $(this).hide();
    $(this).next().show().focus();
    $(this).parent().next().show();

});



//获得评论失去焦点
function afterText(obj, mainId) {
    if ($("#biaoqing_" + mainId).attr("isShow") == "true") {
        return false;
    }
    var commentText = $.trim($(obj).val());
    //var tipText = $(obj).attr("tip"); || commentText == tipText
    if (commentText == "") {
        $(obj).hide();
        $("#expression_" + mainId).hide();
        $("#chat_detail_" + mainId).show();
    }
};




/*判断鼠标是否在表情上面
	如果鼠标移到表情上面的话就说明，要触发点击事件，此时文件框失去焦点不会隐藏
*/
function faceChange(mainId, isShow) {
    $("#biaoqing_" + mainId).attr("isShow", isShow);
};

//计算剩余的字数
function calculateRestChar($this, $count) {
    //把表情字体替换为0只算一个字符
    var val = $this.val().replace(expandReplaceReg, "0");
    var length = (val == "" ? 0 : val.length);
    var restCount = 250 - parseInt(length);
    if (restCount >= 0) {
        $count.html('还能输入<em>' + restCount + '</em>字');
        $count.find("em").css("color", "");
    } else {
        $count.html('超过<em>' + Math.abs(restCount) + '</em>字');
        $count.find("em").css("color", "red");
        $this.css("border", "1px solid rgb(255,55,2)");
        return;
    }
    if (length == 0) {
        $this.css("border", "1px solid rgb(255,55,2)");
    } else {
        $this.css("border", "1px solid rgb(204,204,204)");
    }
    //cookie保存
    //$.cookie("NoticeComment", $this.val(), {
    //    expires: 30
    //});
}


//
function ResponseNotice(NoticeID) {
    var obj = $("#text_area_" + NoticeID);
    if (obj.attr("isFirst") != null) {
        return;
    }
    obj.attr("isFirst", "1");
    var text = $.trim(obj.val());
    var val = text.replace(expandReplaceReg, "0");
    var length = (val == "" ? 0 : val.length);

    if (length == 0) {
        obj.attr("isFirst", null);
        //alert(请填写评论内容!");
        return;
    }
    if (length > 250) {
        obj.attr("isFirst", null);
        alert("评论内容不能超过250字符!");
        obj.removeAttr("isFirst");
        return;
    }
    var url = "Notice.ashx";
    var params = {
        action: "NoticeResponse_ADD",
        NoticeID: NoticeID,
        Conten: escape(text)
    };
    var json = $G2S.GetAjaxNoPar(params, url);
    if (json == "true") {
        GetCommentDetail(1, NoticeID, ShortPageSize);
        //$.cookie("NoticeComment", "");
        obj.attr("isFirst", null);
        obj.val("");
        var $count = obj.parent().next().children().last();
        calculateRestChar(obj, $count);
        //selectFace();
        bindDocumentClickBlur();
    }
    else {
        alert("评论失败！");
    }
}



//添加通知
var addNotice;
function AddNotice() {
    var url = 'AddNotice.aspx';
    addNotice = $.layer({
        type: 2,
        shadeClose: false,
        fix: false,
        title: false,
        closeBtn: [0, false],
        shade: [0.8, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['620px', ($(window).height() - 30) + 'px'],
        iframe: { src: url },
        end: function (index, save) {
            //if ($("#hdnTHasChange").val() == 1) {
            //
            //}
        }
    });
}
//end: function(index) {
//   
//}
//跳转页面
function ChangePage(NoticeID, MaxIndex) {
    var index = $("#txt_Index_" + NoticeID).val();

    if (index <= 0) {
        index = 1;
    }
    if (index > MaxIndex) {
        index = MaxIndex
    }

    if (index > 0 && index <= MaxIndex) {
        GetCommentDetail(index, NoticeID, commentPageSize);
    }

}



//计算时间差
// 描述  
function getDateDiff(desc, dateTimeStamp, CurrentTime) {
    if (dateTimeStamp == null || dateTimeStamp == '') {
        return;
    }
    var re = /-?\d+/;
    var m = re.exec(dateTimeStamp);
    if (m < 0) { return ""; }
    var minute = 1000 * 60;
    var hour = minute * 60;
    var day = hour * 24;
    var halfamonth = day * 15;
    var month = day * 30;
    var now = re.exec(CurrentTime);
    var diffValue = now - m;

    var monthC = diffValue / month;
    var weekC = diffValue / (7 * day);
    var dayC = diffValue / day;
    var hourC = diffValue / hour;
    var minC = diffValue / minute;

    if (monthC >= 1) {
        result = desc + parseInt(monthC) + "个月前";
    }
    else if (weekC >= 1) {
        result = desc + parseInt(weekC) + "个星期前";
    }
    else if (dayC >= 1) {
        result = desc + parseInt(dayC) + "天前";
    }
    else if (hourC >= 1) {
        result = desc + parseInt(hourC) + "个小时前";
    }
    else if (minC >= 1) {
        result = desc + parseInt(minC) + "分钟前";
    } else {
        result = "刚刚发表";
    }
    return result;
}
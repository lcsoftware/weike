$(document).ready(function () {
    //GetCourseList();
});


//课程列表
var rowscount = "0"; //总页数
var Pageindex = 1;
var pagesize = 20;
function GetCourseList()
{
  
    if ($("#sel_pageSize").val() != "") {
        pagesize = $("#sel_pageSize").val();
    }
    if (Pageindex <= 1) {
        Pageindex = 1;
    }
    var url = "Course.ashx";
    var params = {
        action: "GetCourseList",
        CourseNo: "",
        CourseName: "",
        PageSize: pagesize,
        PageIndex: Pageindex
    };
    var strHtml = "";
   
    var json = $G2S.GetAjaxJson(params, url);
    if (json != "暂无数据") {
        strHtml += "<table class='result_table'>";
        strHtml += "    <tr>";
        strHtml += "                        <th width='10'></th>";
        strHtml += "                        <th width='30'><input type='checkbox'></th>";
        strHtml += "                        <th width='100'>用户名<i class='icon_admin rank_btn'></i></th>";
        strHtml += "                        <th width='80'>姓名</th>";
        strHtml += "                        <th width='100'>学号<i class='icon_admin rank_btn'></i></th>";
        strHtml += "                        <th width='50'>性别</th>";
        strHtml += "                        <th width='200'>所属机构</th>";
        strHtml += "                        <th width='80'>专业</th>";
        strHtml += "                        <th width='70'>是否助教</th>";
        strHtml += "                        <th width='80'>入学年份</th>";
        strHtml += "                        <th width='90'>行政班</th>";
        strHtml += "                        <th>操作</th>";
        strHtml += "                    </tr>";
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i];
            var CourseID = rows.CourseID;
            var CourseNo = rows.CourseNo;
            var CourseName = rows.CourseName;
            var CourseNameEn = rows.CourseNameEn;
            var TermNo = rows.TermNo;
            var OrganizationID = rows.OrganizationID;
            var CourseType = rows.CourseType;
            var OrganizationName = rows.OrganizationName;
            var Name = rows.Name;
            rowscount = rows.rowscount;
            strHtml += " <tr id='" + CourseID + "'>";
            strHtml += "                        <td></td>";
            strHtml += "                        <td><input type='checkbox'></td>";
            strHtml += "                        <td><p class='user_id'>" + CourseNo + "</p></td>";
            strHtml += "                        <td>" + CourseName + "</td>";
            strHtml += "                        <td>10101201</td>";
            strHtml += "                        <td>女</td>";
            strHtml += "                        <td>" + OrganizationName + "</td>";
            strHtml += "                        <td>金融管理</td>";
            strHtml += "                        <td>助教</td>";
            strHtml += "                        <td>2013</td>";
            strHtml += "                        <td>班级0701</td>";
            strHtml += "                        <td><p class='operation_box'><i title='编辑' pid='" + CourseID + "' onclick='Eidt(this)' class='icon_admin edit_btn'></i><i title='删除' onclick='Del(this)'  pid='" + CourseID + "' class='icon_admin delete_btn'></i></p></td>";
            strHtml += "                    </tr>";
        }
        strHtml += "</table>";
    }
    $("#div_List").html(strHtml);
    Page(Pageindex, 100000);
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
function Page(count, rowscount)
{
    if ($("#sel_pageSize").val() != "") {
        pagesize = $("#sel_pageSize").val();
    }
    var strHtml = "";
    var flagcount = Math.ceil(rowscount / pagesize);

    strHtml += "<div class='page_box'>";
    
    if (flagcount > 9) {
        strHtml += " <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count - 1) + ")'>前一页</a>";
        if (count > 1) {
            strHtml += "  <a href='javascript:void(0);' onclick='GetPageSizeCount(" + 1 + ")' >" + "首页" + "</a>";
        }
        for (var i = count; i < count + 4; i++) {

            strHtml += "  <a href='javascript:void(0);' onclick='GetPageSizeCount(" + i + ")' >" + i + "</a>";
        }
        strHtml += "    <span class='more'>...</span>";
        strHtml += "     <a href='javascript:void(0);' onclick='GetPageSizeCount(" + (parseInt(count) + 8)+ ")' >" + (parseInt(count) + 8) + "</a>";
        strHtml += "     <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count + 1) + ")'>后一页</a>";
        strHtml += "     <span>共" + rowscount + "条，到第<input onkeyup=\"this.value=this.value.replace(/\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\D/g,'')\" id='txt_pageindex' type='text'>页</span>";
        strHtml += "     <a class='confirm' href='javascript:void(0);' onclick='GetPageSizeCount(" + $("#txt_pageindex").val() + ")' >确认</a>";
      
    } else {
        strHtml += " <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count - 1) + ")'>前一页</a>";
        for (var i = count; i <= flagcount; i++) {
           
            strHtml += "  <a href='javascript:void(0);' onclick='GetPageSizeCount(" + i + ")' >" + i + "</a>";
        }
        strHtml += "   <a class='prev' href='javascript:void(0);' onclick='GetPageSizeCount(" + (count + 1) + ")'>后一页</a>";
        strHtml += "   <span>共" + rowscount + "条，到第<input onkeyup=\"this.value=this.value.replace(/\D/g,'')\" onafterpaste=\"this.value=this.value.replace(/\D/g,'')\" id='txt_pageindex' type='text'/>页</span>";
        strHtml += "   <a class='confirm' href='javascript:void(0);' onclick='GetPageSizeCount(" + parseInt($("#txt_pageindex").text()) + ")' >确认</a>";
    }
    strHtml += "</div>";

   
    $("#div_page_wrap").html(strHtml);
}

//分页按钮方法
function GetPageSizeCount(index) {
    Pageindex = index;
    GetCourseList();
}



//加载样式
function  init()
{
    $('.class_operation').hover(function(){
        $(this).find('ul').toggle();	
    })

    $('.fold_btn').click(function(){
        if(!$(this).hasClass('click')){
            $(this).addClass('click');
            $(this).siblings('.select_require').css('height','auto');
            $(this).text('[收起]');
        }else{
            $(this).removeClass('click');
            $(this).siblings('.select_require').css('height','30px');
            $(this).text('[更多]');	
        }	
    })
	
    $('.close_box').live('click',function(){
        $(this).parent().slideToggle();	
    })
	
    $('.result_table tr').hover(function(){
        $(this).find('.operation_box').toggle();	
    })
	
    $('.result_table').each(function(){
        $(this).find('tr:odd').css('background','#f7f7f7');	
    })
}

//选择分页
function GetListChange(thi)
{
    GetCourseList();
}


//弹出框

function Getalter()
{
    $G2S.alert("这是弹出框");
}

//确认框
function message()
{
   $G2S.confirm("这是信息框", "aa()");
}

//确认框 方法
function aa()
{
    alert("确认框方法");
    layer.closeAll();//关闭所有的层;
}

//弹出本页面层
var pageii;
function page2()
{
     pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'left', //从左动画弹出
        page: {
            html: '<div style="width:420px; height:260px; padding:20px; border:1px solid #ccc; background-color:#eee;"><p>我从左边来，我自定了风格。</p><button id="pagebtn" class="btns" onclick="closepage2()">关闭</button></div>'
        },
        end: function (index) {
            alert(1);
           
           
        }
    });
}

function closepage2()
{
    layer.close(pageii);
}

//弹出其他页
function page3()
{
    $.layer({
        type: 2,
        shadeClose: true,
        title: false,
        closeBtn: [0, false],
        shade: [0.8, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['1000px', ($(window).height() - 50) + 'px'],
        iframe: { src: 'http://192.168.4.100:5555/G2S/ShowSystem/Index.aspx' }
    });
}
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

var key;  //搜索关键字
var zTree;  //树
var zNodes = new Array();
var expandNodes;  //所有展开的节点
var nodeList = [];  //搜索结果列表

var pid = 0;  //父id
var type = 0;  // 0为新增,1为编辑
var cid = -1;  //学科ID
var delSptyID = -1;  //被删除的学科ID

//输入验证
var flag = {
    num: false,
    name: false
};

$(document).ready(function () {
    CourseType_P_List();
    resetTree();
    zTree.expandAll(true);
    zTree.expandAll(false);
    key = $("#key");
    key.bind("focus", function (e) {
        if (key.hasClass("empty")) {
            key.removeClass("empty");
        }
    })
    .bind("blur", function (e) {
        if (key.get(0).value === "") {
            key.addClass("empty");
        }
    })
    .bind("propertychange", searchNode)
    .bind("input", searchNode);

    $('.default_status').live('click', function () {
        if (!$(this).hasClass('click')) {
            $(this).addClass('click');
            $(this).children('input').css('border-color', '#366061');
            $(this).next().show();
        } else {
            $(this).removeClass('click');
            $(this).children('input').css('border-color', '#ccc');
            $(this).next().hide();
        }
        layer.autoArea(box);
    })

    $("input[name='CourseTypeNo']").blur(function () {
        $(this).val() == "" ? $("#NoMsg").show() : $("#NoMsg").hide();
        $(this).val() == "" ? flag.num = false : flag.num = true;
    }).focus(function () {
        $("#NoMsg").hide();
        $("#regMsg").hide();
    });


    $("input[name='Name']").blur(function () {
        $(this).val() == "" ? $("#NameMsg").show() : $("#NameMsg").hide();
        $(this).val() == "" ? flag.name = false : flag.name = true;
    }).focus(function () {
        $("#NameMsg").hide();
    });
});

//初始化树
function resetTree() {
    zNodes = CourseType_List();
    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
    zTree = $.fn.zTree.getZTreeObj("treeDemo");
}

//对树形结构的一些设置
var setting = {
    view: {
        selectedMulti: false,
        showIcon: false,
        dblClickExpand: true,
        showLine: false,
        fontCss: getFontCss
    },
    edit: {
        enable: true,
        showRemoveBtn: false,
        showRenameBtn: false
    },
    data: {
        simpleData: {
            enable: true,
            idKey: "CourseTypeID",
            pIdKey: "ParentID",
            rootPId: 0
        },
        key: {
            title: "Name",  //设置title
            name: "Name"
        }
    },
    callback: {
        beforeDrop: beforeDrop,
        onCollapse: onCollapse
    }
};

//搜索分类
function searchNode(e) {
    updateNodes(false);
    var value = $.trim(key.get(0).value);
    nodeList = zTree.getNodesByFilter(function (node) {
        return (node.CourseTypeNo.toLowerCase().indexOf(value.toLowerCase()) > -1 || node.Name.toLowerCase().indexOf(value.toLowerCase()) > -1);
    });
    if (value == '' || nodeList.length < 1) {
        updateNodes(false);
    } else {
        updateNodes(true);
    }
}

//改变搜索出来的节点的状态
function updateNodes(highlight) {
    for (var i = 0, l = nodeList.length; i < l; i++) {
        nodeList[i].highlight = highlight;
        zTree.updateNode(nodeList[i]);
    }
}
function getFontCss(treeId, treeNode) {
    return (!!treeNode.highlight) ? { color: "#A60000", "font-weight": "bold" } : { color: "#284a51", "font-weight": "normal" };
}

function onCollapse(event, treeId, treeNode) {
    zTree.expandNode(treeNode, false, true, true);
}

//鼠标按下时
function beforeDrag(treeId, treeNodes) {
    return true;
}
//移动组织机构
function beforeDrop(treeId, treeNodes, targetNode, moveType) {
    Organization_Move(treeNodes[0].OrganizationID, targetNode.OrganizationID, moveType);
}



//刷新前保持原有状态
function SaveState() {
    var nodes = zTree.transformToArray(zTree.getNodes());  //所有节点
    expandNodes = new Array();
    if (nodes.length > 0) {
        for (var i = 0; i < nodes.length; i++) {  //展开的节点
            if (nodes[i].open) {
                expandNodes.push(nodes[i].OrganizationID);
            }
        }

    } else {
        expandNodes = null;
    }
}

//恢复刷新前的状态
function ReCover() {
    if (expandNodes != null && expandNodes.length > 0) {
        for (var i = 0; i < expandNodes.length; i++) {
            zTree.expandNode(zTree.getNodeByParam("CourseType", expandNodes[i]), true, null, null, true);
        }
    }
}

//课程分类列表
var CourseType_List = function () {
    $(".organization_list").html("");
    var url = "Course.ashx";
    var para = { action: "GetSpTypeList" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        return result;
    } else {
        return "";
    }
}

//获取上级学科下拉列表
var CourseType_P_List = function () {
    $("select[name='CourseType']").html("");
    var strHtml = "<option value='0'>无</option>";
    var url = "Course.ashx";
    var para = { action: "CourseType_P_List" };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        for (var i = 0; i < result.length; i++) {
            var row = result[i];
            strHtml += "<option value='" + row.CourseTypeID + "'>" + row.Name + "</option>";
        }

    } else {
        strHtml += "<option value='-1'>请添加学科类别</option>";
    }
    $("select[name='CourseType']").html(strHtml);
}

//新增或编辑
var AddSptyEdit = function () {
    SaveState();
    var CourseTypeNo = $("input[name='CourseTypeNo']").val();
    var Name = $("input[name='Name']").val();
    var ParentID = $("select[name='CourseType']").val();
    var IsNo = false;
    if ($("input[name='CourseTypeNo']").val != "") {
        IsNo = true;
    }
    var IsName = false;
    if ($("input[name='Name']").val != "") {
        IsName = true;
    }
    //验证
    if (!flag.num || !flag.name) {
        alert("请完善信息!");
        return;
    }

    var CourseTypeID = -1;
    if (type == 0) {
        CourseTypeID = -1;
    } else {
        CourseTypeID = cid;
    }

    var url = "Course.ashx";
    var para = {
        action: "CourseType_Edit",
        CourseTypeID: CourseTypeID,
        CourseTypeNo: CourseTypeNo,
        Name: Name,
        ParentID: ParentID
    };
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        if (result.OrganizationID == 0) {
            $("#regNoMsg").show();
        } else {
            if (type == 0) {
                alert("新增成功!");
            } else {
                alert("编辑成功!");
            }
            layer.closeAll();
            resetTree();
            ReCover();
        }
    } else {
        return "";
    }

}

//初始化编辑
var InitEdit = function (cid) {
    this.cid = cid;
    flag = {
        num: true,
        name: true
    };
    var node = zTree.getNodeByParam("CourseTypeID", cid);
    var ParentNode = node.getParentNode();
    if (ParentNode == null) {
        $("#SptyName").html("无");
    }
    else {
        $("#SptyName").html(ParentNode.Name);
    }
    $("input[name='CourseTypeNo']").val(node.CourseTypeNo);
    $("input[name='Name']").val(node.Name);
    $("select[name='CourseType']").val(node.ParentID);
}

//初始化添加
var InitAdd = function () {
    flag = {
        num: false,
        name: false
    };
    $("input[name='CourseTypeNo']").val("");
    $("input[name='Name']").val("");
    $("select[name='CourseType']").val(0);
}

//删除课程分类
var CourseType_Del = function (cid) {
    SaveState();
    delSptyID = cid;
    var node = zTree.getNodeByParam("CourseTypeID", cid);
    var para = { action: "CourseType_Del", CourseTypeID: cid };
    var url = "Course.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        $("#sptyName").html(node.Name);
        $(".delete_tip").slideDown();
    } else {
        alert("请重试!");
    }
    resetTree();
    ReCover();
}

//取消删除组织机构
var CourseType_CancelDel = function () {
    SaveState();
    var para = { action: "CourseType_CancelDel", CourseTypeID: cid };
    var url = "CourseType.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {
        $(".delete_tip").slideUp();
    } else {
        alert("请重试!");
    }
    resetTree();
    ReCover();
}

//移动组织机构
var Organization_Move = function (SelfID, OptionID, type) {
    var para = { action: "Organization_Move", SelfID: SelfID, OptionID: OptionID, type: type };
    var url = "OrganizationHandler.ashx";
    var result = $G2S.GetAjaxJson(para, url);
    if (result != "暂无数据") {

    } else {
        alert("请重试!");
    }
}

//弹出层
var box;
function ShowBox(cid, name, type) {
    this.pid = cid;
    this.type = type;
    $("#SptyName").html(name);
    $("#NoMsg").hide();
    $("#NameMsg").hide();
    $("#regMsg").hide();
    var title = type == 0 ? "新增" : "编辑";
    box = $.layer({
        type: 1,
        title: [title, true],
        shift: 'top',
        shade: [0.5, '#000'],
        area: ['450px', "auto"],
        page: { dom: '#box' }
    });
    $(".xubox_title").attr("style", "background:none repeat scroll 0 0 #284a51;color:#fff;cursor: move;");

    if (type == 1) {
        InitEdit(cid);
    } else {
        InitAdd();
    }
}

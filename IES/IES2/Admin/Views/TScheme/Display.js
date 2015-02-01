
//添加授课教师
function IncreaseTeacher() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['添加授课教师', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['800px', ($(window).height() - 50) + 'px'],
        iframe: { src: '../JW/Specialty/SpTeacherADD.aspx' }
    });
}

//教学班添加学生
function AddStudent() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['添加学生', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['905px', '500px'],
        iframe: { src: '../ADD/AddStudent.aspx?HID=' + stus }
    });
}

//教学班添加教师
function AddTeacher() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['添加授课教师', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['905px', '500px'],
        iframe: { src: '../ADD/AddTeacher.aspx?HID=' + teacherid + '&hfids=' + hfIDS }
    });
}

//教学班添加教授课程
function AddCourse() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['添加教授课程', true],
        closeBtn: [0, true],
        shade: [0.7, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['905px', '500px'],
        iframe: { src: '../ADD/AddCourse.aspx?HID=' + teacourseid + '&hfids=' + CourIDs }
    });
}
//新增校历
function CalendarAdd() {
    InitEdit(sid);
    var pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'left', //从左动画弹出
        page: { dom: '#CalendarAdd' }
    });
    //自设关闭
    $('#pagebtn3').on('click', function () {
        layer.close(pageii);
    });
    $('#pagebtn4').on('click', function () {
        layer.close(pageii);
    });
}
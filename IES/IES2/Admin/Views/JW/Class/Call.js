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
        area: ['785px', '590px'],
        iframe: { src: '../../ADD/AddStudent.aspx?HID=' + stus }
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
        iframe: { src: '../../ADD/AddTeacher.aspx?HID=' + teacherid + '&hfids=' + hfIDS }
    });
}
function PageTeacher() {
    $.layer({
        type: 2,
        shadeClose: false,
        title: ['选择教师', true],
        closeBtn: [0, true],
        shade: [0.5, '#000'],
        border: [0],
        offset: ['40px', ''],
        area: ['565px', ($(window).height() - 102) + 'px'],
        iframe: { src: 'SelTeacher.aspx' }

    });
}
function NewColm() {
    var pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'top', //从左动画弹出
        page: { dom: '#NewColm' }
    });
    //自设关闭
    $('#pagebtn1').on('click', function () {
        layer.close(pageii);
    });
}
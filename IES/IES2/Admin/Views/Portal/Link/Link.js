//编辑
function Edit(thi) {
    window.location.href = "Edit.aspx?id=" + $(thi).attr("pid");
}

//删除
function Del(thi) {
    alert("这是删除:id " + $(thi).attr("pid"));
}

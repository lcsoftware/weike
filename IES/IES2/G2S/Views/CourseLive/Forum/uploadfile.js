$(function () {
    //var btn = $("#Button1");
    var btn = $("#fileupload").uploadFile({
        url: "FileUpload.ashx",
        fileSuffixs: ["jpg", "png", "gif", "txt","*","zip","rar"],
        maximumFilesUpload: 10,//最大文件上传数
        onComplete: function (msg) {
            //$("#testdiv").append(msg + "<br/>");
        },
        onAllComplete: function () {
            alert("全部上传完成");
        },
        isGetFileSize: true,//是否获取上传文件大小，设置此项为true时，将在onChosen回调中返回文件fileSize和获取大小时的错误提示文本errorText
        onChosen: function (file, obj, fileSize, errorText) {
            if (!errorText) {
                $("#file_size").text(file + "文件大小为：" + fileSize + "KB");
            } else {
                alert(errorText);
                return false;
            }
            return true;//返回false将取消当前选择的文件
        },
        perviewElementId: "fileList", //设置预览图片的元素id
        perviewImgStyle: { width: '100px', height: '100px', border: '1px solid #ebebeb' }//设置预览图片的样式
    });

    var upload = btn.data("uploadFileData");

    $("#files").click(function () {
        upload.submitUpload();
    });
});
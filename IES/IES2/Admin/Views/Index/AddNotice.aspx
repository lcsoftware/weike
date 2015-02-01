<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNotice.aspx.cs" Inherits="Admin.Views.Index.AddNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../Content/Css/admin.css" rel="stylesheet" />
    <link href="../../Content/Css/common.css" rel="stylesheet" />
    <link href="../../Content/Css/TreeView.css" rel="stylesheet" />
    <script src="../../Js/jquery-1.8.3.min.js"></script>
    <script src="../../../Js/G2S.js"></script>
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="AddNotice.js"></script>
    <script type="text/javascript">
        function CloseDialog() {
            if (window.parent) {
                var index = window.parent.addNotice;
                parent.layer.close(index);
            }
        }

        function CancelClick() {
            if (window.parent) {
                //window.parent.document.getElementById("Ref").value = 0;//父页面请求数据是否重新加载
                this.CloseDialog();
            }
        }
    </script>
</head>
<body class="admin_wrap">
    <form id="form1" runat="server">
        <div class="pop_bg"></div>
        <div class="pop_600_New" style="display: block;">
            <h4>发布通知</h4>
            <i class="icon icon_close" onclick="CancelClick()"></i>
            <div class="inform_box">
                <div class="inform_item">
                    <p class="inform_style">
                        <span>
                            <input type="checkbox" name="IsEmail" id="IsEmail">同时发送邮件</span>
                        <span>
                            <input type="checkbox" name="IsMSM" id="IsMSM">同时发送短信<em>（不得超过67字符，含标点符号、空格、签名）</em></span>
                    </p>
                    <div class="issue_box">
                        <input class="issue_tit" type="text" id="txtTitle" placeholder="通知标题（可不填）">
                        <div class="issue_content">
                            <textarea id="noticeText"></textarea>
                            <span class="red red_label">*</span>
                            <p class="inform_setting">
                                <span>
                                    <input type="checkbox" name="IsTop" id="IsTop">设为重要通知</span><em>已输入 0 字符</em>
                            </p>
                            <div class="attach_box">
                                <a class="attach_btn" href="javascript:;" onclick="Getdengl()">上传附件</a>
                                <ul class="attach_list">
                                    <li>
                                        <img src="http://placehold.it/76x60/cccccc" width="76" height="60" /></li>
                                    <li>
                                        <div class="file_box">
                                            <i class="icon_24 ppt"></i>
                                            <p>关于爱国教育关于爱国教育</p>
                                            <span><i class="icon throw_btn"></i></span>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="inform_item inform_item2">
                    <div class="user_select">
                        <span class="user_style">
                            <input type="radio" name="objUster" id="allUster">全部用户</span>
                        <p class="object_box">
                            <span class="all_select">
                                <input type="checkbox" id="AllTeacher">全部教师</span>
                            <span class="all_select">
                                <input type="checkbox" id="AllStudent">全部学生</span>
                        </p>
                    </div>
                    <div class="user_select user_select2">
                        <span class="user_style">
                            <input type="radio" name="objUster" id="partUster">部门用户</span>
                        <div class="object_box object_box2">
                            <div class="object_item">
                                <a class="select_btn" href="javascript:;" onclick="AddTeacher()">选择教师</a>
                                <div class="section_box" id="SelectedTeacher">
                                    <span><a href="javascript:;">护理医学院</a><i class="icon_admin remove_btn"></i></span>
                                    <span><a href="javascript:;">基础医学院</a><i class="icon_admin remove_btn"></i></span>
                                </div>
                                <input type="hidden" value="" id="hdnTOrgIDs" />
                                <input type="hidden" value="0" id="hdnTHasChange" />
                            </div>
                            <div class="object_item">
                                <a class="select_btn" href="javascript:;" onclick="AddStudent()">选择学生</a>
                                <div class="section_box" id="SelectedStudent">
                                    <span><a href="javascript:;">2014临床医学8年制</a><i class="icon_admin remove_btn"></i></span>
                                    <span><a href="javascript:;">2014临床医学8年制</a><i class="icon_admin remove_btn"></i></span>
                                    <span><a href="javascript:;">2014临床医学8年制</a><i class="icon_admin remove_btn"></i></span>
                                </div>
                                <input type="hidden" value="" id="hdnSSpecialIDs" />
                                <input type="hidden" value="" id="hdnSYearIDs" />
                                <input type="hidden" value="0" id="hdnSHasChange" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="issue_btn_box">
                    <a class="issue_btn" href="javascript:;" onclick="SaveNotice()">发布</a>
                    <a class="cancel_btn" href="javascript:; " onclick="CancelClick()">取消</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

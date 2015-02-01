<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Notice.ascx.cs" Inherits="App.G2S.Views.Home.Notice1" %>
<script src="../../Js/jquery-1.8.3.min.js"></script>
<script src="../../Js/G2S.js"></script>
<script src="../../../Frameworks/layer/layer.min.js"></script>
<script src="../../Js/face.js"></script>
<link href="../../Content/Css/face.css" rel="stylesheet" />
<script src="Notice.js"></script>
<div class="message_box">
    <div class="greenBoxBg" ></div>
    <ul class="message_nav">
        <li class="active"><a href="javascript:;">全部通知</a></li>
    </ul>
    <div class="issue_notice">
        <a href="javascript:;"><em><i class="icon"></i></em><span onclick="AddNotice()">发布通知</span></a>
        <div class="filter_notice">
            <p>
                <input type="checkbox" name="ckbSystem" id="ckbSystem">系统通知
            </p>
            <p style="visibility: hidden">
                <input type="checkbox" name="ckbCourse" id="ckbCourse">课程通知
            </p>
            <span class="search_notice" style="visibility: hidden; display: none">
                <input type="text" placeholder="请输入课程名称">
                <i class="icon_admin search_notice_btn" onclick="javascript:SearchMessage()"></i>
            </span>
        </div>
    </div>
    <div class="message_detail" id="message_detail">
        <div class="message_list">
            <div class="message_tit">
                <i class="icon star"></i>
                <h4>关于某些视频播放时候卡顿</h4>
            </div>
            <p class="notice_content">最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。</p>
            <div class="issue_detail">
                <a class="comment_btn" href="javascript:;" onclick="ShowComment(this)">评论（330）</a>
                <p>2014-10-20&nbsp;来自<a href="#">智慧树</a></p>
            </div>
            <div class="comment_box" id="comment_box_001">
                <span class="triangle"></span>
                <div class="comment_content">
                    <div class="chat_box">
                        <input class="chat_detail" type="text" value="请输入评论内容">
                        <textarea class="text_area" style="display: none; border: 1px solid rgb(255, 55, 2);"></textarea>
                    </div>
                    <div class="expression">
                        <a class="biaoqing" href="javascript:;"><i class="icon"></i>表情</a>
                        <a class="issue_comment" href="javascript:;">发表</a>
                        <p>还能输入<span>5</span>字</p>
                    </div>
                    <div class="comment_detail" id="comment_detail_001">
                        <ul class="comment_list">
                            <li>
                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />
                                <p><a href="#" target="_blank">客服小慧</a><span>:你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒</span><b>(发表于46分钟前)</b></p>
                            </li>
                            <li>
                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />
                                <p><a href="#" target="_blank">客服小慧</a><span>:你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒</span><b>(发表于46分钟前)</b></p>
                            </li>
                            <li>
                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />
                                <p><a href="#" target="_blank">客服小慧</a><span>:你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒</span><b>(发表于46分钟前)</b></p>
                            </li>
                            <li>
                                <img src="http://placehold.it/30x30/cccccc" width="30" height="30" />
                                <p><a href="#" target="_blank">客服小慧</a><span>:你真棒你真棒你真棒你真棒你真棒你真棒你真棒你真棒</span><b>(发表于46分钟前)</b></p>
                            </li>
                        </ul>
                        <a class="more_comment" href="javascript:;" onclick="GetCommentDetail()">点击查看更多评论</a>
                        <div class="page_wrap" id="div_page_wrap"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>


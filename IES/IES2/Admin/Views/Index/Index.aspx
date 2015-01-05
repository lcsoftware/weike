<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Admin.Views.Index.Index" %>
<%@ Register Src="~/Views/Index/DiskSize.ascx" TagPrefix="uc1" TagName="DiskSize" %>
<%@ Register Src="~/Views/Index/OnlineUser.ascx" TagPrefix="uc1" TagName="OnlineUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="core_content">
        <div class="matter_box">
            <div class="date_box"></div>		
            <div class="matter_item">
                    
            </div>
        </div>
        <div class="message_box">
            <ul class="message_nav">
                <li class="active"><a href="javascript:;">全部通知</a></li>
            </ul>
            <div class="issue_notice">
                <a href="javascript:;"><em><i class="icon"></i></em><span>发布通知</span></a>
                        
            </div>
            <div class="message_detail">
                <div class="message_list">
                    <div class="message_tit">
                        <i class="icon star"></i>
                        <h4>关于某些视频播放时候卡顿</h4> 
                    </div>
                    <p class="notice_content">最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。</p>
                    <div class="issue_detail">
                        <a class="comment_btn" href="javascript:;">评论（330）</a>
                        <p>2014-10-20&nbsp;来自<a href="#">智慧树</a></p>
                    </div>
                    <div class="comment_box">
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
                            <div class="comment_detail">
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
                                <a class="more_comment" href="javascript:;">点击查看更多评论</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="message_list">
                    <div class="message_tit">
                        <i class="icon star"></i>
                        <h4>关于某些视频播放时候卡顿</h4> 
                    </div>
                    <p class="notice_content">最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。最近有同学反映有些视频观看的时候，相比其他视频卡顿比较多。</p>
                    <div class="issue_detail">
                        <a class="comment_btn" href="javascript:;">评论（330）</a>
                        <p>2014-10-20&nbsp;来自<a href="#">智慧树</a></p>
                    </div>
                    <div class="comment_box">
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
                            <div class="comment_detail">
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
                                <a class="more_comment" href="javascript:;">点击查看更多评论</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="right_side">
        <uc1:OnlineUser runat="server" ID="OnlineUser" />
        <uc1:DiskSize runat="server" ID="DiskSize" />         
    </div>
</asp:Content>

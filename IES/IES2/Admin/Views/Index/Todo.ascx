<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Todo.ascx.cs" Inherits="Admin.Views.Index.Todo" %>

       <div class="matter_box">
               		<div class="date_box">
                    
                           <asp:Calendar ID="Calendar1" runat="server"  width="250"   height="325"   >
                                 
                           </asp:Calendar>

                    </div>		
                	<div class="matter_item">
                    	<div class="matter_tit">
                        	<h4>2014-11-11待处理事项<span>（33条）</span></h4>
                            <a class="add_schedule" href="javascript:;">+添加日程</a>
                            <p><input type="checkbox" checked="checked">待处理</p>
                            <p><input type="checkbox" checked="checked">系统</p>
                            <p><input type="checkbox" checked="checked">我的</p>
                        </div>
                        <div class="matters">
                        	<div class="matters_box">
                            	<ul class="matters_list">
                            		<li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[备份]</a>
                                        <p>数据备份提醒</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[审核]</a>
                                        <p>MOOC课程发布申请《中医药传统文化》王小明</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[密码重置]</a>
                                        <p>用户<span class="red">陈晨</span>已忘记密码，申请密码重置</p>
                                    </li>
                                    <li>
                                    	<span class="status_label system">系统</span>
                                        <p>系统短信剩余<span class="red">1000</span>条，请尽快补充短信条数</p>
                                    </li>
                                    <li>
                                    	<span class="status_label system">系统</span>
                                        <p>资料服务器存储空间已到达<span class="red">80%</span>，请尽快增加存储容量</p>
                                    </li>
                                    <li>
                                    	<span class="status_label mine">我的</span>
                                        <a href="javascript:;">[删除]</a>
                                        <p><span class="gray">08:00</span>跑步</p>
                                    </li>
                                    <li>
                                    	<span class="status_label system">系统</span>
                                        <p><span class="gray">14:30</span>课程中心培训，第二教学楼301</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[备份]</a>
                                        <p>数据备份提醒</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[审核]</a>
                                        <p>MOOC课程发布申请《中医药传统文化》王小明</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[密码重置]</a>
                                        <p>用户<span class="red">陈晨</span>已忘记密码，申请密码重置</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[备份]</a>
                                        <p>数据备份提醒</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[审核]</a>
                                        <p>MOOC课程发布申请《中医药传统文化》王小明</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[密码重置]</a>
                                        <p>用户<span class="red">陈晨</span>已忘记密码，申请密码重置</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[审核]</a>
                                        <p>MOOC课程发布申请《中医药传统文化》王小明</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[密码重置]</a>
                                        <p>用户<span class="red">陈晨</span>已忘记密码，申请密码重置</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[审核]</a>
                                        <p>MOOC课程发布申请《中医药传统文化》王小明</p>
                                    </li>
                                    <li>
                                    	<span class="status_label pending">待处理</span>
                                        <a href="javascript:;">[密码重置]</a>
                                        <p>用户<span class="red">陈晨</span>已忘记密码，申请密码重置</p>
                                    </li>
                                </ul>
                            </div>
                            <ul class="switch_btn_list"></ul>
                        </div>
                	</div>
                    
                </div>

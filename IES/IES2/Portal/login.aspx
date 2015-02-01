<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Portal.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>课程中心4.0登陆</title>
<link type="text/css" rel="stylesheet" href="css/index.css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<div class="wrap_box">
	<div class="login_wrap">
        <div class="logo_box">
        	<img src="images/logo.png" width="80" height="35" alt="">
        	<h1>课程中心4.0</h1>
            <span class="version_box"><i></i>Beta V1.0</span>
        </div>
        <div class="login_box">
    
        	    <div class="login_content">
            	<p class="wrong_tip"> <asp:Label ID="lberror" Visible ="false"  runat="server" Text="用户名或密码错误！请重新输入"></asp:Label> </p>
                <p class="input_box">用户名：<input class="user_name" id="txtAccount" runat="server"  type="text" value=""></p>
                <p class="input_box">密码：<input class="password" id="txtPassport" runat="server" type="password"></p>
                <div class="login_btn">
                    <div class="btn_box">
                        <asp:LinkButton ID="LinkButton3"  CommandName="teacher" class="teacher_btn"  OnClick="Button1_Click"  runat="server">教师登陆</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" CommandName="student" class="admin_btn"  OnClick="Button1_Click"  runat="server">学生登陆</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1"  CommandName="admin"  class="admin_btn"  OnClick="Button1_Click"  runat="server">管理员登陆</asp:LinkButton>
                    </div>
                    <a class="password_guide" href="javascript:;">忘记密码？</a>
                </div>
            </div>
   
        </div>
        
	</div>
</div>
<div id="lay_background" class="lay_background">
	<div id="lay_background_img" class="lay_background_img lay_background_img_fade_out">
    	<img src="images/back1.jpg" width="1600">
	</div>
</div>
<div class="pop_bg"></div>
<div class="pop_400">
	<h4>提示</h4>
    <i class="close_btn"></i>
    <div class="pop_wrap">
        <div class="tip_box">
            <img src="images/cry.png" width="64" height="64" alt="">
            <p>您没有访问管理员空间的权限，请重新输入账号密码，或改换其他身份访问系统</p>
        </div>
        <p class="close_tip"><span>3秒</span>后自动关闭？</p>
    </div>
</div>
    <script type="text/javascript" src="Frameworks/jquery-1.8.3.min.js"></script>
<script type="text/javascript" src="js/index.js"></script>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Left.ascx.cs" Inherits="Admin.Views.Share.Left" %>
  <!--用户信息开始-->
<div class="user">
    <img src=<%=Admin.SessionUser.CurrentUserIMG %> width="110" height="100" />
    <a class="teacher_name" href="#">     <%=IES.Service.UserService.CurrentUser.UserName %> </a>
    <a class="switch_btn" href="#">管理员端<i class="icon icon_arrow"></i></a>
</div>
<!--用户信息结束-->
<!--侧边导航开始-->
<div class="side_box">
    <ul class="side_nav">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                        <li id='leftmenu<%# Eval("MenuID")%>'><a href='<%# Eval("URL")%>?PID=<%# Eval("MenuID")%>' ><%# Eval("Title")%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
  <%--  <a class="more_tool" href="javascript:;">更多工具<i class="icon icon_more"></i></a>--%>
</div>

<script type="text/javascript">


  
    var MenuID = request("PID").substr(0, 3);
    if ($(".side_nav #leftmenu" + MenuID).html() != undefined) {
        $("#leftmenu" + MenuID).addClass("active");
    } else {
        $(".side_nav li:first").addClass("active");
    }

    function request(paras) {
        var url = location.href;
        var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
        var paraObj = {}
        for (i = 0; j = paraString[i]; i++) {
            paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
        }
        var returnValue = paraObj[paras.toLowerCase()];
        if (typeof (returnValue) == "undefined") {
            return "";
        } else {
            return returnValue;
        }
    }
</script>
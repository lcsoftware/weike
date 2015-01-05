<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubMenuNav.ascx.cs" Inherits="Admin.Views.Share.SubMenuNav" %>
<div class="admin_nav_box">
    <ul class="admin_nav_list">


<%--        <li><a href="#">机构</a></li>
        <li><a href="#">专业</a></li>
        <li><a href="#">课程</a></li>
        <li><a href="#">教师</a></li>
        <li><a href="#">学生</a></li>
        <li class="active"><a href="#">行政班</a></li>--%>

    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
                <li id='submenu<%# Eval("MenuID")%>' ><a href='<%# Eval("URL")%>?PID=<%# Eval("MenuID")%>' ><%# Eval("Title")%></a></li>
        </ItemTemplate>
    </asp:Repeater>
       

    </ul>
</div>

<script type="text/javascript">
   
  
   
    var MenuID = request("PID");
    if ($(".admin_nav_list #submenu" + MenuID).html()!=undefined) {
        $("#submenu" + MenuID).addClass("active");
    } else {
        $(".admin_nav_list li:first").addClass("active");
    }

     function request (paras) {
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
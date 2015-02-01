<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Top.ascx.cs" Inherits="Admin.Views.Share.Top" %>
<div class="header_center">
    <div class="company_logo">
        <img src=<%=IES.Service.Common.ConfigService.CfgSchool_CC.LOGO %> width="70" height="30" alt=""><%=IES.Service.Common.ConfigService.CfgSchool_CC.SchoolName %>
    </div>
    <div class="user_box">
        <p class="user_name">
            <img src=<%=IES.Service.UserService.CurrentUserIMG %> width="20" height="20" />
            <%=IES.Service.UserService.CurrentUser.UserName %>
            <span class="icon icon_arrow"></span>
        </p>
        <ul class="user_info">
        <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                        <li><a href='<%# Eval("URL")%>?PID=<%# Eval("MenuID")%>' ><%# Eval("Title")%></a></li>
            </ItemTemplate>
        </asp:Repeater>
        </ul>
    </div>
    <ul class="nav_box">
        <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                        <li id='topmenu<%# Eval("MenuID")%>' ><a href='<%# Eval("URL")%>?PID=<%# Eval("MenuID")%>' ><%# Eval("Title")%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<script type="text/javascript">



    var MenuID = request("PID").substr(0,2);
    if ($(".nav_box #topmenu" + MenuID).html() != undefined) {
        $("#topmenu" + MenuID).addClass("active");
    } else {
        $(".nav_box li:first").addClass("active");
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
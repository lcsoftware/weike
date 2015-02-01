<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav.ascx.cs" Inherits="Admin.Views.Share.Nav" %>
                <div class="admin_nav_box">
            	<p class="crumbs_box">
                    
                         
                    <a href='<%=parentmenu.URL%>?PID=<%=parentmenu.MenuID%>' style="color:#fff" ><%=parentmenu.Title %></a>
                    
                     &gt;  <a href='<%=currentmenu.URL%>?PID=<%=currentmenu.MenuID%>' style="color:#fff" ><%=currentmenu.Title %></a>

            	</p>
</div>
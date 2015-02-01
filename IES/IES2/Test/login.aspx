<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Test.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <asp:Label ID ="lb1" runat="server"></asp:Label>

    用户名<asp:TextBox ID="tbuser" runat="server"></asp:TextBox>
         密码<asp:TextBox ID="tbpassword" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="登录" OnClick="Button1_Click" />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:FileUpload ID="FileUpload3" runat="server" />
    </div>
        <p>
            &nbsp;</p>
        <asp:TreeView ID="TreeView1" runat="server"  ImageSet="Simple" NodeIndent="20">
            <HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#DD5555" HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
    </form>
</body>
</html>

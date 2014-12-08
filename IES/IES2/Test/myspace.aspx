<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myspace.aspx.cs" Inherits="Test.myspace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        我的空间22:<asp:Button ID="Button1" runat="server" Visible="true"   OnClick="Button1_Click" Text="删除文件夹" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="新增文件夹" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
    </div>
       
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="App.G2S.Views.Home.test" %>

<%@ Register Src="~/Views/Home/Notice.ascx" TagPrefix="uc1" TagName="Notice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body id="myBody">
    <form id="form1" runat="server">
        <uc1:Notice runat="server" ID="Notice" />
    </form>
</body>
</html>

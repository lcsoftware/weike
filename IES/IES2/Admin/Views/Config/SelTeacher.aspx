<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelTeacher.aspx.cs" Inherits="Admin.Views.Config.SelTeacher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../../Js/jquery-1.8.3.min.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script>
        var index = parent.layer.getFrameIndex(window.name);
        function Close() {
            parent.layer.close(index);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="SelTeacher" style="width: 550px;margin:0; height: 515px; display: block">
            <!--<div style="width: 100%; height: 40px; background: #366061; border: 1px solid #fff; color: #fff">
                <span style="font-size: 16px; font-weight: 600; line-height: 40px; margin-left: 10px">选择教师</span>
                <button id="pagebtn1" style="color: #fff; background-color: transparent; border: 0; float: right; margin: 10px 20px 0px 0px" onclick="">关闭</button>
            </div>-->
            <div style="float: left; width: 510px; height: 460px; padding-left: 20px; padding-top: 20px; padding-right: 20px;border-bottom:1px solid #000">
                <div style="height: 40px; width: 100%; line-height: 40px">
                    <input type="text" id="SelKey" placeholder="请输入“工号”“姓名”关键字" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: left" />
                    <label style="color: red; line-height: 40px; width: 20px; float: left; text-align: center">*</label>
                    <a class="wrt_btn" style="margin-top:3px;text-decoration:none" runat="server"  onserverclick="SelTeach_Click">搜索</a>
                </div>
                <div style="height: 330px; width: 100%">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <HeaderTemplate>
                            <table class="result_table">
                                <colgroup>
                                    <col style="width: 50px"></col>
                                    <col style="width: 120px"></col>
                                    <col style="width: 120px"></col>
                                    <col style="width: 228px"></col>
                                </colgroup>
                                <tr>
                                    <th></th>
                                    <th>工号</th>
                                    <th>姓名</th>
                                    <th>所属机构</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item item_row" id='<%# Eval("UserID")%>'>
                                <td>
                                    <input name="ckbItem" type="checkbox"></td>
                                <td><%# Eval("UserNo")%></td>
                                <td><%# Eval("UserName")%></td>
                                <td><%# Eval("OrganizationName")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div style="width: 100%; height: 40px; line-height: 40px; overflow: hidden">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                        ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                        HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                        OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
                    </webdiyer:AspNetPager>
                </div>
                <div style="height: 40px; width: 100%;float:left">
                    <a class="wod_btn" style="margin-left: 225px;margin-top:10px;text-decoration:none">选择</a>
                </div>
            </div>
            <div style="width: 100%; height: 40px;float:left;background:#ccc;margin:0px">
                <span style="float:left;line-height:40px;width:100px;margin-left:10px">已选择<label>10</label>人</span>
                <a class="wod_btn" runat="server" style="margin-left: 100px;margin-top:5px; float: left;text-decoration:none">保存</a>
                <a class="wrt_btn" href="javascript:Close()" style="margin-left: 10px;margin-top:5px;text-decoration:none; float: left">取消</a>
            </div>
        </div>
    </form>
</body>
</html>

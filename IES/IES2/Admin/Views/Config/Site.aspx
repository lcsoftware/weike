<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Site.aspx.cs" Inherits="Admin.Views.Config.Site" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <script src="Config.js"></script>
    <style>
        .wod_btn{padding:0 5px 0 5px;height:28px;width:50px;background:#1E5975;line-height:29px;color:#fff;text-align:center;border:1px solid #ccc}
        .wod_btn:hover{background:#1E5975;}
        .wrt_btn{padding:0 5px 0 5px;height:29px;width:50px;background:#f2f2f2;line-height:29px;color:#333;text-align:center;border:1px solid #ccc}
        .wrt_btn:hover{background:#f8f8f8;}
        .u8_btn{display:inline-block; width:16px; height:16px; background:url(../../Content/Images/u8.png) no-repeat;}
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">
    </div>

    <div class="search_result_box">
        <div class="result_tit" style="line-height:40px">
            <span style="color: #999; float: left; font-size: 12px; line-height: 40px; font-weight: normal">双击可编辑栏目标题，初始栏目仅作为给教师的参考，教师可在建设课程网站时修改初始栏目标题</span>
            <a style="background:#35557B;text-align:center;height:24px;line-height:24px;color:#fff;margin:5px 0 0 0;border:0;" href="javascript:NewColm();">新增栏目</a>
            <a style="background:#213F63;width:25px;height:24px;display:block;padding:0;border:0;margin:5px 0 0 0" href="javascript:NewColm();"><i class="icon_admin add_btn" style="margin-left:4px"></i></a>
        </div>
        <div id="OmGrid1" style="width: 960px; z-index: 999">
            <table class="result_table" >
                <colgroup>
                    <col style="width: 100px">
                    <col style="width: 760px">
                    <col style="width: 100px">
                </colgroup>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr class="item item_row" style="border-bottom:1px solid #ccc">
                            <td style="text-align:center;border-left:1px solid #ccc;border-right:1px solid #ccc;"><%# Eval("Orde")%></td>
                            <td style="text-indent:1em;" id="<%# Eval("ColumID")%>" ondblclick="ReTitleName(this.id)">
                                <input id="Title<%#Eval("ColumID")%>" name="Title<%#Eval("ColumID")%>" onblur="ReName(this.id)" type="text" style="border: 0; width: 400px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%#Eval("Name")%>"></td>
                            <td style="border-right:1px solid #ccc;">
                                <p class='operation_box'>
                                    <i title='上移' id='<%#Eval("ColumID")%>,<%#Eval("Orde")%>' onclick='topMove(this.id)' class='icon_admin up_btn'></i>
                                    <i title='下移' id='<%#Eval("ColumID")%>,<%#Eval("Orde")%>' onclick='bottomMove(this.id)' class='icon_admin down_btn'></i>
                                    <i title='删除' id='<%#Eval("ColumID")%>,<%#Eval("Orde")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                                </p>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div id="NewColm" style="width: 300px; height: 80px;border:1px solid #ccc; display: none">
            <div style="width: 300px; height: 40px; background: #1E5975; border-bottom: 1px solid #ccc; color: #fff">
                <span style="font-size: 15px; font-weight: 600; line-height: 40px; margin-left: 10px">新增栏目</span>
                <a class="u8_btn" id="pagebtn1" style="float:right;margin:14px 8px 0 0"></a>
            </div>
            <div style="height: 40px; width: 298px;margin:auto">
                <label style="float: left; line-height: 40px; margin-left: 10px">名称:</label>
                <input type="text" id="pinpai1" runat="server" style="height: 30px; width: 170px; margin-top: 3px; margin-left: 10px; float: left" />

                <a class="wod_btn" style="float: left; margin-top: 4px;margin-left:10px" href="javascript:ColADD();">确定</a>
            </div>
        </div>
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:Button ID="btnTp" Style="display: none;" runat="server" Text="Button" OnClick="btnTp_Click" />
        <asp:Button ID="btnBm" Style="display: none;" runat="server" Text="Button" OnClick="btnBm_Click" />
        <asp:Button ID="btnADD" Style="display: none;" runat="server" Text="Button" OnClick="btnADD_Click" />
        <asp:Button ID="btnReName" Style="display: none;" runat="server" Text="Button" OnClick="btnReName_Click" />
        <asp:HiddenField ID="hfID" runat="server" />
    </div>
    <script>
        function topMove(id) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnTp.ClientID %>").click();
        }
        function bottomMove(id) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnBm.ClientID %>").click();
        }
        function ColADD(id) {
            $("#<%=hfID.ClientID %>").val(id);
            $("#<%=btnADD.ClientID %>").click();
        }
        function ReName(id) {
            var txt = document.getElementById(id);
            if (txt.readOnly == false) {
                var pid = txt.parentNode.id;
                $("#<%=hfID.ClientID %>").val(pid);
            $("#<%=btnReName.ClientID %>").click();
            }
        }
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }
        }
        function ReTitleName(id) {
            var txt = document.getElementById("Title" + id);
            txt.readOnly = false;
            txt.style.borderStyle = "solid";
            txt.style.borderColor = "#3794ed";
            txt.style.borderWidth = "1px";
            txt.style.background = "#fff";
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="File.aspx.cs" Inherits="Admin.Views.Config.File" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <style>
        .wod_btn{padding:0 5px 0 5px;height:30px;width:60px;background:#284a51;line-height:30px;color:#fff;text-align:center;border:1px solid #ccc}
        .wod_btn:hover{background:#233f45;}
        .wrt_btn{padding:0 5px 0 5px;height:30px;width:60px;background:#f2f2f2;line-height:30px;color:#333;text-align:center;border:1px solid #ccc}
        .wrt_btn:hover{background:#f8f8f8;}
    </style>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">

        <div class="tip_box" style="margin: 10px 20px 10px 20px">
            <p>暂不支持【编辑器】自定义上传文件大小，若要修改配置，可以联系系统客服，由后技术人员帮助配置！</p>
        </div>

    </div>

    <div class="search_result_box">
        <div style="margin-left: 10px; margin-right: 10px; border-bottom: 1px solid #ccc; height: 40px; line-height: 40px">
            <span style="font-size: 15px; font-weight: 600">编辑器上传文件</span>
        </div>
        <div style="width: 700px; margin-top: 10px; margin-left: 10px">
            <div style="height: 40px; width: 100%">
                <label style="color: red; line-height: 40px; width: 10px; float: left">*</label>
                <label style="float: left; line-height: 40px; margin-right: 20px">单个文件大小:</label>
                <input type="text" id="Bjq" runat="server" readonly="readonly" style="height: 30px; width: 400px; margin-top: 3px; margin-right: 5px; float: left" />
                <label style="line-height: 40px">MB</label>
                <span style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px">大小不可超过50MB</span>
            </div>
        </div>
        <div style="margin-left: 10px; margin-right: 10px; border-bottom: 1px solid #ccc; height: 40px; line-height: 40px">
            <span style="font-size: 15px; font-weight: 600">资料库上传文件</span>
        </div>
        <div style="width: 700px; margin-top: 10px; margin-left: 10px">
            <div style="height: 40px; width: 100%">
                <label style="color: red; line-height: 40px; width: 10px; float: left">*</label>
                <label style="float: left; line-height: 40px; margin-right: 20px">单个文件大小:</label>
                <input type="text" id="Zlk" runat="server" readonly="readonly" style="height: 30px; width: 400px; margin-top: 3px; margin-right: 5px; float: left" />
                <label style="line-height: 40px">MB</label>
            </div>
            <div style="height: 40px; width: 560px; float: left">

                <a class="wrt_btn" href="javascript:editSave();" style="float: right; margin-top: 5px">取消</a>
                <a class="wod_btn" id="edittrue" href="javascript:editSave();" style="float: right; margin-top: 5px;margin-right:20px">编辑</a>
            </div>
            <asp:Button ID="btnSave" Style="display: none;" runat="server" Text="Button" OnClick="btnSave_Click" />
        </div>
    </div>
    <script>
        function editSave() {
            var aa = document.getElementById("edittrue");
            if (aa.innerText == "编辑" || aa.textContent == "编辑") {
                aa.innerText = "确定";
                aa.textContent = "确定";
                document.getElementById('<%=Bjq.ClientID %>').readOnly = false;
                document.getElementById('<%=Zlk.ClientID %>').readOnly = false;
            }
            else if (aa.innerText == "确定" || aa.textContent == "确定") {
                $("#<%=btnSave.ClientID %>").click();
            }
    }
    </script>
</asp:Content>

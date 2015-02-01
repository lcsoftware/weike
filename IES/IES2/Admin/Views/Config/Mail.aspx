<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mail.aspx.cs" Inherits="Admin.Views.Config.Mail" %>
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



    </div>

    <div class="search_result_box">
        <div style="margin-left:10px;margin-right:10px;border-bottom:1px solid #ccc;height:40px;line-height:40px">
            <span style="font-size:15px;font-weight:bold">系统邮箱参数</span>
        </div>
        <div style="width:478px;margin-top:10px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq0" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port0" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account0" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password0" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM0" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave0" href="javascript:editSave(0);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel0"  href="javascript:cancel(0);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
        </div>
        <div style="width:478px;margin-top:10px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq1" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port1" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account1" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password1" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM1" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave1" href="javascript:editSave(1);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel1"  href="javascript:cancel(1);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
        </div>
        <div style="width:478px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq2" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port2" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account2" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password2" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM2" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave2" href="javascript:editSave(2);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel2"  href="javascript:cancel(2);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
        </div>
        <div style="width:478px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq3" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port3" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account3" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password3" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM3" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave3" href="javascript:editSave(3);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel3"  href="javascript:cancel(3);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
        </div>
        <div style="width:478px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq4" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port4" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account4" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password4" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM4" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave4" href="javascript:editSave(4);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel4"  href="javascript:cancel(4);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
        </div>
        <div style="width:478px;float:left;border:1px solid #ccc">
            <div style="height:20px;width:100%"></div>
            <div style="height:40px;width:100%">
                <input type="text" id="Emailfwq5" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱服务器:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Port5" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">端口:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="text" id="Account5" readonly="readonly" runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">邮箱账户:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:40px;width:100%">
                <input type="password" id="Password5" readonly="readonly"  runat="server" style="height:30px;width:330px;margin-top:3px;margin-right:25px;float:right" />
                <label style="float:right;line-height:40px;margin-right:20px">密码:</label>
                <label style="color: red;line-height:40px; width: 10px;float:right">*</label>
            </div>
            <div style="height:30px;width:358px;float:right;margin-top:5px">
                <input type="checkbox" runat="server" style="height:20px;float:left" id="sslJM5" onclick="return false" />
                <label style="line-height:20px">SSL加密</label>               
            </div>
            <div style="height:40px;width:65%;float:right">            
                <a class="wod_btn" id="editsave5" href="javascript:editSave(5);" style="float:left;margin-top:5px">编辑</a> 
                <a class="wrt_btn" id="cancel5"  href="javascript:cancel(5);" style="float:left;margin-top:5px;margin-left:30px">删除</a>             
            </div>
            <asp:Button ID="btnEdit" Style="display: none;" runat="server" Text="Button" OnClick="btnEdit_Click" />
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        </div>
    </div>  
    <script>
        function editSave(i) {
            var aa = document.getElementById("editsave" + i);
            var cc = document.getElementById("cancel" + i);
            if (aa.innerText == "编辑" || aa.textContent=="编辑")
            {
                aa.innerText = "确定";
                cc.innerText = "取消";
                aa.textContent = "确定";
                cc.textContent = "取消";
                document.getElementById('MainContent_Emailfwq'+i).readOnly = false;
                document.getElementById('MainContent_Port'+i).readOnly = false;
                document.getElementById('MainContent_Account'+i).readOnly = false;
                document.getElementById('MainContent_Password'+i).readOnly = false;
                document.getElementById('MainContent_sslJM'+i).onclick = 'return true';
            }
            else if (aa.innerText == "确定" || aa.textContent == "编辑")
            {
                $("#<%=hfID.ClientID %>").val(i);
                $("#<%=btnEdit.ClientID %>").click();
            }
        }
        function cancel(i) {
            var aa = document.getElementById("editsave" + i);
            var cc = document.getElementById("cancel" + i);
            if (cc.innerText == "删除" || cc.textContent == "删除") {
                $("#<%=hfID.ClientID %>").val(i);
                $("#<%=btnInfo.ClientID %>").click();
            }
            else if (cc.innerText == "取消" || cc.textContent == "取消") {
                aa.innerText = "编辑";
                cc.innerText = "删除";
                aa.textContent = "编辑";
                cc.textContent = "删除";
                document.getElementById('MainContent_Emailfwq' + i).readOnly = true;
                document.getElementById('MainContent_Port' + i).readOnly = true;
                document.getElementById('MainContent_Account' + i).readOnly = true;
                document.getElementById('MainContent_Password' + i).readOnly = true;
                document.getElementById('MainContent_sslJM' + i).onclick = 'return false';
            }
        }
    </script>
</asp:Content>
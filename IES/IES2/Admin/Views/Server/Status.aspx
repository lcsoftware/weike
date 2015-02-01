<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="Admin.Views.Server.Status" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <script src="Server.js"></script>
    <style>
        .wod_btn{padding:0 5px 0 5px;height:30px;width:60px;background:#284a51;line-height:30px;color:#fff;text-align:center;border:1px solid #ccc}
        .wod_btn:hover{background:#233f45;}
        .wrt_btn{padding:0 5px 0 5px;height:30px;width:60px;background:#f2f2f2;line-height:30px;color:#333;text-align:center;border:1px solid #ccc}
        .wrt_btn:hover{background:#f8f8f8;}
        .u8_btn{display:inline-block; width:16px; height:16px; background:url(../../Content/Images/u8.png) no-repeat;}
    </style>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">
    </div>

    <div class="search_result_box" style="padding: 0; margin-top: 5px">
        <div class="result_tit">
            <h4 style="margin-left: 25px">存储服务器信息</h4>
            <a style="background:#35557B;text-align:center;height:24px;line-height:24px;padding-left:5px;padding-right:5px;color:#fff;margin:5px 30px 0 0;border:0;" href="javascript:PageServer1();">新增存储服务</a>
            <a style="background:#213F63;width:25px;height:24px;display:block;padding:0;border:0;margin:5px 0 0 0" href="javascript:PageServer1();"><i class="icon_admin add_btn" style="margin-left:4px"></i></a>
        </div>
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:HiddenField ID="hfID" runat="server" />
        <div id="OmGrid1" style="width: 1000px; z-index: 999">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 20px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 110px"></col>
                            <col style="width: 70px"></col>
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>服务器</th>                           
                            <th>IP/域名</th>
                            <th>HTTP端口</th>
                            <th>HTTP目录</th>
                            <th>视频点播端口</th>
                            <th>视频服务目录</th>
                            <th>媒体发布点</th>
                            <th>媒体发布点端口</th>                           
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id='<%# Eval("ServerID")%>'>
                        <td></td>
                        <td><a style="padding: 5px; width: 90px; background:#69A9AB">存储服务器</a></td>                       
                        <td><%# Eval("Host") %></td>
                        <td><%#Eval("IISPort") %></td>
                        <td><%# Eval("IISFolder")%></td>
                        <td><%# Eval("NginxPort")%></td>
                        <td><%# Eval("NginxFolder") %></td>
                        <td><%#Eval("MMSPort") %></td>
                        <td><%#Eval("MMSFolder") %></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' id='<%#Eval("ServerID")%>' onclick='Edit(this.id)' class='icon_admin edit_btn'></i>
                                <i title='删除' id='<%#Eval("ServerID")%>' onclick="Del(this.id)" class='icon_admin delete_btn'></i>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="8">
                            <label>服务器信息:<%#Eval("Brief")%></label></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="ServerAdd" style="width: 800px; height: 460px; border: 1px solid #366061; display: none">
        <div style="width: 100%; height: 40px; background: #1E5975; border: 1px solid #fff; color: #fff">
            <span style="font-size: 16px; font-weight: 600; line-height: 40px; margin-left: 10px">新增存储服务器</span>
            <a class="u8_btn" id="pagebtn1" style="float:right;margin:14px 8px 0 0"></a>
        </div>
        <div style="float: left; width: 50%; height: 280px">
            <span style="font-weight: 600; height: 40px; line-height: 40px; margin-left: 10px">服务器基本信息</span>
            <div style="height: 40px; width: 398px">
                <input type="text" id="pinpai1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">品牌:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="xinhao1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">型号:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="chuliqi1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">处理器:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="neicun1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">内存:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="yinpan1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">硬盘:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="xitong1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">操作系统:</label>
            </div>
        </div>
        <div style="float: left; width: 50%; height: 360px">
            <span style="font-weight: 600; height: 40px; line-height: 40px; margin-left: 10px">服务器配置信息</span>
            <div style="height: 40px; width: 398px">
                <input type="text" id="host1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">IP/域名:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="iispost1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">HTTP端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="iisfolder1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">HTTP目录:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="mmsfolder1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">媒体发布点:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="mmsport1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">媒体发布端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="nginxf1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">视频服务目录:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="nginxp1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">视频点播端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="password" id="pubkey1" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">密钥:</label>
            </div>
            
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 58px; margin: auto">
                <a class="wod_btn" runat="server" onserverclick="btnAdd_Click" style="position: absolute; left: 320px; margin-top: 370px">保存</a>
                <a class="wrt_btn" id="pagebtn2" href="#" style="position: absolute; left: 410px; margin-top: 370px">取消</a>
            </p>
        </div>
    </div>

    <div id="ServerEdit" style="width: 800px; height: 460px; border: 1px solid #366061; display: none">
        <div style="width: 100%; height: 40px; background: #1E5975; border: 1px solid #fff; color: #fff">
            <span style="font-size: 16px; font-weight: 600; line-height: 40px; margin-left: 10px">编辑存储服务器</span>
            <a class="u8_btn" id="pagebtn3" style="float:right;margin:14px 8px 0 0"></a>
        </div>
        <div style="float: left; width: 50%; height: 280px">
            <span style="font-weight: 600; height: 40px; line-height: 40px; margin-left: 10px">服务器基本信息</span>
            <div style="height: 40px; width: 398px">
                <input type="text" id="pinpai2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">品牌:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="xinhao2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">型号:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="chuliqi2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">处理器:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="neicun2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">内存:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="yinpan2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">硬盘:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="xitong2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">操作系统:</label>
            </div>
        </div>
        <div style="float: left; width: 50%; height: 360px">
            <span style="font-weight: 600; height: 40px; line-height: 40px; margin-left: 10px">服务器配置信息</span>
            <div style="height: 40px; width: 398px">
                <input type="text" id="host2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">IP/域名:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="iispost2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">HTTP端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="iisfolder2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">HTTP目录:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="mmsfolder2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">媒体发布点:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="mmsport2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">媒体发布端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="nginxf2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">视频服务目录:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="text" id="nginxp2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">视频点播端口:</label>
            </div>
            <div style="height: 40px; width: 398px">
                <input type="password" id="pubkey2" runat="server" style="height: 30px; width: 280px; margin-top: 3px; margin-right: 5px; float: right" />
                <label style="float: right; line-height: 40px; margin-right: 20px">密钥:</label>
            </div>
            
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 58px; margin: auto">
                <a class="wod_btn" runat="server" onserverclick="btnEdit_Click" style="position: absolute; left: 320px; margin-top: 370px">保存</a>
                <a class="wrt_btn" id="pagebtn4" href="#" style="position: absolute; left: 410px; margin-top: 370px">取消</a>
            </p>
        </div>
    </div>

    <script>
        function Del(id) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfID.ClientID %>").val(id);
                $("#<%=btnInfo.ClientID %>").click();
            }
        }
        function Edit(id) {
            $("#<%=hfID.ClientID %>").val(id);
            PageServer2(id);
        }
    </script>
</asp:Content>

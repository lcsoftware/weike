<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiskQuota.aspx.cs" Inherits="Admin.Views.Server.DiskQuota" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <link href="Server.css" rel="stylesheet" />
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <script src="DiskQuota.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="sousuo_box">
    </div>

    <div class="search_result_box">
        <table>
            <tr class="trone">
                <td class="bold">
                    <span class="pleft">统一分配空间</span>
                </td>
                <td>
                    <span class="pright">存储容量：</span>
                </td>
            </tr>
            <tr style="height: 50px;">
                <td class="border"><span>教师存储空间：</span>
                    <asp:Repeater runat="server" ID="TeacherSpace">
                        <ItemTemplate>
                            <span id="<%#Eval("CfgSchoolID")%>" style="width: 300px; height: 40px;">
                                <input id="txt<%#Eval("CfgSchoolID")%>" name="txt<%#Eval("CfgSchoolID")%>" type="text" style="border: 0; width: 45px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%#Eval("TeacherSpace")%>">MB/人<a id="update" style="margin-left: 20px;" class='icon_admin edit_btn' href="javascript:ReName();"></a>
                                <span id="updtr" style="float: right; padding-right: 20px; display: none;">
                                    <a class="wod_btn" runat="server" onserverclick="btnUpd_Click">保存</a>
                                    <a class="wrt_btn" href="javascript:Disappear();">取消</a>
                                </span>
                            </span>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td class="border"><span>学生存储空间：</span>
                    <asp:Repeater runat="server" ID="StudentSpace">
                        <ItemTemplate>
                            <span id="<%#Eval("CfgSchoolID")%>">
                                <input id="txts<%#Eval("CfgSchoolID")%>" name="txts<%#Eval("CfgSchoolID")%>" type="text" style="border: 0; width: 45px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%#Eval("StudentSpace")%>">MB/人<a id="Updates" style="margin-left: 20px;" class='icon_admin edit_btn' href="javascript:ReNames();"></a>
                                <span id="Updtrs" style="float: right; padding-right: 20px; display: none;">
                                    <a class="wod_btn" runat="server" onserverclick="btnUpdate_Click">保存</a>
                                    <a class="wrt_btn" href="javascript:Disappears();">取消</a>
                                </span>
                            </span>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>

    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li id="xx" class="active"><a href="javascript:shows(1)">教师空间使用</a></li>
            <li id="yy"><a href="javascript:shows(2)">学生空间使用</a></li>
            <li id="xxyy"><a href="javascript:shows(3)">个性化分配用户</a></li>
        </ul>
    </div>
    <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
    <asp:HiddenField ID="Count" runat="server" Value="8" />
    <asp:HiddenField ID="Parms" runat="server" />
    <asp:HiddenField ID="hfID" runat="server" Value="1" />
    <asp:HiddenField ID="fhID" runat="server" Value="1" />
    <div class="data_box">
        <div id="TeacherOne">
            <div class="filter_item" style="width:100%">
                <div style="float: left; padding-left:-50px;">
                    <asp:CheckBox ID="Check" runat="server" />仅显示已封闭的用户（被封闭存储空间的用户将不可再上传任何资料）
                </div>
                <p class="search_btn">
                    <input runat="server" id="txtKey" type="text" placeholder="请输入“姓名”关键字"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -30px; z-index: 999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
                </p>
            </div>
            <div id="OmGrid1" style="width: 1000px; z-index: 999; margin-left: -80px; margin-top: -1px;">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 20px" />
                                <col style="width: 70px" />
                                <col style="width: 100px" />
                                <col style="width: 140px" />
                                <col style="width: 80px" />
                                <col style="width: 100px" />
                                <col style="width: 80px" />
                                <col style="width: 70px" />
                            </colgroup>
                            <tr>
                                <th></th>
                                <th>工号</th>
                                <th>姓名</th>
                                <th>所属机构</th>
                                <th>分配空间</th>
                                <th>已用空间</th>
                                <th>剩余空间</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>                      
                        <tr id='<%# Eval("UserID")%>' class="item item_row">
                            <td></td>
                            <td><%# Eval("UserNo")%></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("OrganizationName")%></td>
                            <td><%# Eval("DiskSize") %></td>
                            <td><%# Eval("DiskSpaceUsed") %></td>
                            <td><%# Convert.ToInt32(Eval("DiskSize"))-Convert.ToInt32(Eval("DiskSpaceUsed")) %></td>
                            <td>
                                <p class='operation_box'>
                                    <i title='个性化空间分配' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='存储空间封闭' id='<%#Eval("UserID")%>' onclick='Del(id)' class='icon_admin delete_btn'></i>
                                </p>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="page_wrap">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true"
                    ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                    HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
                </webdiyer:AspNetPager>
            </div>
        </div>

        <div id="StudentTwo" style=" display: none;">
            <div class="filter_item">
                <div style="float: left;">
                    <asp:CheckBox ID="CheckBox1" runat="server" />仅显示已封闭的用户（被封闭存储空间的用户将不可再上传任何资料）
                </div>
                <p class="search_btn">
                    <input runat="server" id="Text1" type="text" placeholder="请输入“姓名”关键字"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -30px; z-index: 999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
                </p>
            </div>
            <div id="OmGrid2" style="width: 1000px; z-index: 999; margin-left: -80px; margin-top: -1px;">
                <asp:Repeater ID="Repeater2" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 10px" />
                                <col style="width: 70px" />
                                <col style="width: 100px" />
                                <col style="width: 140px" />
                                <col style="width: 80px" />
                                <col style="width: 100px" />
                                <col style="width: 80px" />
                                <col style="width: 70px" />
                            </colgroup>
                            <tr>
                                <th></th>
                                <th>学号</th>
                                <th>姓名</th>
                                <th>所属机构</th>
                                <th>分配空间</th>
                                <th>已用空间</th>
                                <th>剩余空间</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id='<%# Eval("UserID")%>' class="item item_row">
                            <td></td>
                            <td><%# Eval("UserNo")%></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("OrganizationName")%></td>
                            <td><%# Eval("DiskSize") %></td>
                            <td><%# Eval("DiskSpaceUsed") %></td>
                            <td><%# Convert.ToInt32(Eval("DiskSize"))-Convert.ToInt32(Eval("DiskSpaceUsed")) %></td>
                            <td>
                                <p class='operation_box'>
                                    <i title='个性化空间分配' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='存储空间封闭' id='<%#Eval("UserID")%>' onclick='Del(id)' class='icon_admin delete_btn'></i>
                                </p>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="page_wrap">
                <webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" UrlPaging="true"
                    ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                    HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
                </webdiyer:AspNetPager>
            </div>
        </div>

        <div id="PersonalityThr" style="display: none;">
            <div class="filter_item">
                <p class="search_btn">
                    <input runat="server" id="Text2" type="text" placeholder="请输入“姓名”关键字"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -30px; z-index: 999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
                </p>
            </div>
            <div id="OmGrid3" style="width: 1000px; z-index: 999; margin-left: -80px; margin-top: -1px;">
                <asp:Repeater ID="Repeater3" runat="server">
                    <HeaderTemplate>
                        <table class="result_table">
                            <colgroup>
                                <col style="width: 10px" />
                                <col style="width: 70px" />
                                <col style="width: 100px" />
                                <col style="width: 140px" />
                                <col style="width: 80px" />
                                <col style="width: 100px" />
                                <col style="width: 80px" />
                                <col style="width: 70px" />
                            </colgroup>
                            <tr>
                                <th></th>
                                <th>工号</th>
                                <th>姓名</th>
                                <th>所属机构</th>
                                <th>分配空间</th>
                                <th>已用空间</th>
                                <th>剩余空间</th>
                                <th></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id='<%# Eval("UserID")%>' class="item item_row">
                            <td></td>
                            <td><%# Eval("UserNo")%></td>
                            <td><%# Eval("UserName")%></td>
                            <td><%# Eval("OrganizationName")%></td>
                            <td><%# Eval("DiskSize") %></td>
                            <td><%# Eval("DiskSpaceUsed") %></td>
                            <td><%# Convert.ToInt32(Eval("DiskSize"))-Convert.ToInt32(Eval("DiskSpaceUsed")) %></td>
                            <td>
                                <p class='operation_box'>
                                    <i title='个性化设置' pid='<%#Eval("UserID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='取消个性化空间分配' id='<%#Eval("UserID")%>' onclick='Del(id)' class='icon_admin delete_btn'></i>
                                </p>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="page_wrap">
                <webdiyer:AspNetPager ID="AspNetPager3" runat="server" Width="100%" UrlPaging="true"
                    ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: "
                    HorizontalAlign="Center" PageIndexBoxStyle="width:60px" PageSize="12" ShowMoreButtons="true"
                    OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true" CssClass="page_box">
                </webdiyer:AspNetPager>
            </div>
        </div>
    </div>

    <div id="NoticDel" style=" display:none; width: 400px; height: 161px; border: 1px solid #ccc;">
        <div style="width: 100%; height: 40px; background: #366061; border: 1px solid #fff; color: #fff">
            <span style="font-size: 16px; font-weight: 600; line-height: 40px; margin-left: 10px">提示</span>
            <button id="pagebtn1" style="color: #fff; background-color: transparent; border: 0; float: right; margin: 10px 20px 0px 0px" onclick="">关闭</button>
        </div>
        <div style="height: 61px; width: 358px; padding: 10px 20px 0px 20px">
            <label style="text-indent: 2em">该用户的存储已用了<span style="color: red">2180MB</span>，超过统一分配数值，取消个性化分配标准后，对方将无法再上传任何资源，您确定要取消吗？</label>
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 58px; margin: auto">
                <a class="wod_btn" runat="server" style="position: absolute; left: 120px; margin-top: 10px">确定</a>
                <a class="wrt_btn" id="pagebtn2" href="#" style="position: absolute; left: 210px; margin-top: 10px">取消</a>
            </p>
        </div>
    </div>

    <div id="Personalization" style=" display:none; width: 600px; height: 400px; border: 1px solid #ccc;">
        <div style="width: 100%; height: 40px; background: #366061; border: 1px solid #fff; color: #fff">
            <span style="font-size: 14px; font-weight: 600; line-height: 40px; margin-left: 10px">个性化设置</span>
            <button id="Close" style="color: #fff; background-color: transparent; border: 0; float: right; margin: 10px 20px 0px 0px" onclick="">关闭</button>
        </div>
        <div style="height: 150px; width: 550px; padding: 10px 20px 0px 20px">
            <label>123123123</label>
            <label style="padding-left: 10px;">王晓明</label>
            <label style="padding-left: 10px;">商学院</label>
            <label style="float: right">总计已用空间：1420M</label>
            <div id="Display" style="width: 500px; z-index: 999; background-color: blue; margin-left: 20px;">
                    <asp:Repeater ID="Information" runat="server">
                        <HeaderTemplate>
                            <table class="result_table">
                                <colgroup>
                                    <col style="width: 30%" />
                                    <col style="width: 50%" />
                                    <col style="width: 20%" />
                                </colgroup>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td></td>
                                <td>视频&nbsp;&nbsp;个,文档&nbsp;&nbsp;份,其他&nbsp;&nbsp;个</td>
                                <td>大小：</td>                                
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
            </div>
        </div>
        <div style="width: 600px; height: 50px; background-color: #FFF3CC; margin: 0 auto;">
            <div style="padding-top: 15px; text-align: center;">个性化存储空间：<input runat="server" />MB</div>
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 58px; text-align: center;">
                <a class="wod_btn" runat="server" style="position: absolute; left: 220px; margin-top: 10px">保存</a>
                <a class="wrt_btn" id="Cancel" href="#" style="position: absolute; left: 310px; margin-top: 10px">取消</a>
            </p>
        </div>
    </div>

    <script>
        function ParmSelect() {
            //var parm = ParmInfo();
            $("#<%=Parms.ClientID %>").val();
        }
        function ReName() {
            var id = document.getElementById("<%= hfID.ClientID %>").value;
        var txt = document.getElementById("txt" + id);
        txt.readOnly = false;
        txt.style.borderStyle = "solid";
        txt.style.borderColor = "#3794ed";
        txt.style.borderWidth = "1px";
        txt.style.background = "#fff";
        txt.style.textAlign = "center";
        txt.style.width = "90px";
        $("#updtr").show();
        $("#update").hide();
    }
    function Disappear() {
        var id = document.getElementById("<%= hfID.ClientID %>").value;
            var txt = document.getElementById("txt" + id);
            txt.readOnly = true;
            txt.style.border = "0";
            txt.style.borderColor = "transparent";
            txt.style.cursor = "pointer";
            txt.style.width = "45px";
            $("#updtr").hide();
            $("#update").show();
        }
        function ReNames() {
            var id = document.getElementById("<%= fhID.ClientID %>").value;
            var txt = document.getElementById("txts" + id);
            txt.readOnly = false;
            txt.style.borderStyle = "solid";
            txt.style.borderColor = "#3794ed";
            txt.style.borderWidth = "1px";
            txt.style.background = "#fff";
            txt.style.textAlign = "center";
            txt.style.width = "90px";
            $("#Updtrs").show();
            $("#Updates").hide();
        }
        function Disappears() {
            var id = document.getElementById("<%= fhID.ClientID %>").value;
            var txt = document.getElementById("txts" + id);
            txt.readOnly = true;
            txt.style.borderColor = "transparent";
            txt.style.cursor = "pointer";
            txt.style.width = "45px";
            $("#Updtrs").hide();
            $("#Updates").show();
        }

    </script>
</asp:Content>

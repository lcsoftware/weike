<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="Admin.Views.Au.Account" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../Portal/Edit.js"></script>
    <link href="TreeView.css" rel="stylesheet" />
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div>
        <div class="tabTagBox">
            <ul class="tabTagList">
                <li id="top" style="font-weight: bold;">用户筛选</li>
                <li id="-1" style="text-indent: 2em;" onclick="CutCss(-1)">全部用户</li>
            </ul>
            <ul class="tabTagList">
                <li id="top" style="font-weight: bold;">机构筛选</li>
                <asp:Repeater runat="server" ID="Repeater1">
                    <ItemTemplate>
                        <li id="<%#Eval("OrganizationID")%>" onclick="CutCss(this.id)">
                            <div style="float: left; width: 150px; text-indent: 2em;">
                                <input id="txt<%#Eval("OrganizationID")%>" type="text" style="border: 0; width: 130px; background-color: transparent; cursor: pointer;" readonly="readonly" value="<%#Eval("OrganizationName")%>">
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>

        <div id="tabCon" style="padding: 12px 5px; width: 760px">
            <div style="padding-left: 20px;">
                <label>当前账号使用情况:正常(<span runat="server" id="number" style="color: #999; font-size: 12px; font-weight: normal; margin: 0 10px"></span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;冻结(<span runat="server" id="Count" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>)</label>
            </div>
            <div>
                <p class="search_btn" style="margin-right: -20px;">
                    <input type="text" id="txtKey" runat="server" placeholder="输入用户名、姓名搜索用户"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -25px; z-index: 999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
                </p>
                <a class="add_student" href="javascript:AccountADD()" style="float:left"><i class="icon_admin add_btn"></i>新增账号</a>
            </div>
            <div class="search_result_box" style="width: 740px;">
                <div id="OmGrid1" style="width: 960px; z-index: 999">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <HeaderTemplate>
                            <table class="result_table">
                                <colgroup>
                                    <col style="width: 10px" />
                                    <col style="width: 20px" />
                                    <col style="width: 50px" />
                                    <col style="width: 50px" />
                                    <col style="width: 50px" />
                                    <col style="width: 80px" />
                                    <col style="width: 100px" />
                                </colgroup>
                                <tr>
                                    <th></th>
                                    <th><input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                                    <th>用户名</th>
                                    <th>姓名</th>
                                    <th>身份</th>
                                    <th>所属机构</th>
                                    <th>角色</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="height: 70px">
                                <td></td>
                                <td><input id='<%# Eval("UserID")%>' type="checkbox"></td>
                                <td><%# Eval("UserNo")%></td>
                                <td><%# Eval("UserName")%></td>
                                <td style="line-height: 18px"><%# GetUser(Eval("UserType").ToString()) %></td>
                                <td><%# Eval("OrganizationName")%></td>
                                <td><%# Eval("IsInSchool").ToString().Trim()=="0"?"校外":"校内" %></td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                        </SeparatorTemplate>
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
        </div>
        <!--隐藏控件-->
        <asp:Button ID="RoleChange" Style="display: none;" runat="server" Text="Button" OnClick="RoleChange_Click" />
        <asp:HiddenField ID="hfID" runat="server" Value="-1" />
        <asp:HiddenField ID="hfIDS" runat="server" />
        <asp:HiddenField ID="Parms" runat="server" />
        <!--end-->
    </div>
    <script>
        function ParmSelect() {
            var parm = ParmInfo();
            $("#<%=Parms.ClientID %>").val(parm);
        }

        function CutCss(obj) {
            var lis = document.getElementsByTagName("li");
            for (var i = 0; i < lis.length; i++) {
                if (lis[i].id == obj) {
                    lis[i].className = 'current';
                }
                else
                    lis[i].className = '';
            }           
            var HiddenValue = document.getElementById("<%= hfID.ClientID %>").value;
            if (HiddenValue != obj) {
                $("#<%=hfID.ClientID %>").val(obj);
                $("#<%=RoleChange.ClientID %>").click();
            }
        }
        window.onload = function () {
            var s = '<%=Getclass()%>';
        }
    </script>
</asp:Content>

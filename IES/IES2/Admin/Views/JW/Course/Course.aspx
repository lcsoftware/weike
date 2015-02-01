<%@ Page Title="课程" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Course.aspx.cs" Inherits="Admin.Views.JW.Course.Course" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <script src="../../../Frameworks/My97DatePicker/WdatePicker.js"></script>
    <style>
        .wod_btn {
            padding: 5px 10px 5px 10px;
            height: 20px;
            width: 60px;
            background: #284a51;
            line-height: 20px;
            color: #fff;
            text-align: center;
            border: 1px solid #ccc;
        }

            .wod_btn:hover {
                background: #233f45;
            }

        .wrt_btn {
            padding: 5px 10px 5px 10px;
            height: 20px;
            width: 60px;
            background: #f2f2f2;
            line-height: 20px;
            color: #333;
            text-align: center;
            border: 1px solid #ccc;
        }

            .wrt_btn:hover {
                background: #f8f8f8;
            }
    </style>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sousuo_box">
        <div class="filter_item">
            <div class="class_operation">
                <a href="javascript:NoOpen();"><i class="icon_admin daoru"></i>导入课程</a>
                <ul>
                    <a href="javascript:NoOpen();"><i class="icon_admin download_btn"></i>模板下载</a>
                    <a href="javascript:NoOpen();"><i class="icon_admin daochu"></i>导出课程</a>
                </ul>
            </div>
            <a class="add_student" href="javascript:ADD();" style="margin-left: 10px"><i class="icon_admin add_btn"></i>新增课程</a>
            <a class="add_student" style="margin-left: 10px; width: 110px" href="javascript:GetTeachTypeList();"><i class="icon_u425"></i>管理授课方式</a>
            <a class="add_student" style="margin-left: 10px" href="javascript:GetCourseList();"><i class="icon_u425"></i>管理学期</a>
            <p class="search_btn">
                <input runat="server" id="txtKey" type="text" placeholder="输入编号、名称搜索课程"><a runat="server" style="float: right; position: relative; margin-right: 3px; margin-top: -23px; z-index: 999" onclick="ParmSelect()" onserverclick="btnSelect_Click" class="icon_admin search_icon"></a>
            </p>
        </div>

        <div class="select_requirement_box">
            <!--数据绑定-->
            <asp:Repeater runat="server" ID="rptorg" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>所属机构</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="orgParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("OrganizationID")%>"><a href="#"><%# Eval("OrganizationName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                    <a class="fold_btn" href="javascript:;">[更多]</a>
                    </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <%--<asp:Repeater runat="server" ID="rpttr" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>学期</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="trParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a  href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("TermNo")%>"><a  href="#"><%# Eval("TermName")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>--%>
            <asp:Repeater runat="server" ID="rptcrty" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>课程分类</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="crtyParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("CourseTypeID")%>"><a href="#"><%# Eval("Name")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ID="rptcrtchty" EnableViewState="false">
                <HeaderTemplate>
                    <dl class="requirement_box">
                        <dt>授课方式</dt>
                        <dd>
                            <div class="all_requirement">
                                <div id="crtchtyParm" class="select_require">
                                    <span onclick="Gbclass(this)" id="-1"><a href="#">不限</a></span>
                </HeaderTemplate>
                <ItemTemplate>
                    <span onclick="Gbclass(this)" id="<%# Eval("TeachingTypeID")%>"><a href="#"><%# Eval("Name")%></a></span>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </div>
                </dd>
                </dl>
                </FooterTemplate>
            </asp:Repeater>
            <!--数据绑定-->
            <dl class="requirement_box">
                <dt>学分区间</dt>
                <dd>
                    <div class="date_time">
                        <p style="width: 70px">
                            <input id="BeginFen" onkeyup="this.value=this.value.replace(/[^0-9]+/,'');" style="width: 40px" runat="server" value="0" type="text">
                        </p>
                        <span>~</span>
                        <p style="width: 70px">
                            <input id="EndFen" onkeyup="this.value=this.value.replace(/[^0-9]+/,'');" style="width: 40px" runat="server" value="10" type="text">
                        </p>
                    </div>
                </dd>
            </dl>
            <a class="search_button" runat="server" onclick="ParmSelect()" onserverclick="btnSelect_Click">搜索</a>
            <%--<span class="close_box"><i class="icon_admin shouqi_icon"></i></span>--%>
        </div>
    </div>

    <div class="search_result_box">
        <div class="result_tit">
            <h4>课程列表</h4>
            <span runat="server" id="number" style="color: #999; font-size: 12px; font-weight: normal; margin-left: 10px"></span>
            <p class="show_num">
                每页显示               
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
            </p>
            <a href="javascript:DelInfo();">删除</a>
            <%--<a href="javascript:;" onclick="page2();">设置授课方式</a>
            <a href="javascript:;void(0)" onclick="message();">设置课程分类</a>
            <a href="javascript:void(0);" onclick="Getalter();">批量设置学期</a>--%>
        </div>
        <!--隐藏控件-->
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:Button ID="btnInfo" Style="display: none;" runat="server" Text="Button" OnClick="btnInfo_Click" />
        <asp:Button ID="BatchDel" Style="display: none;" runat="server" Text="Button" OnClick="BatchDel_Click" />
        <asp:HiddenField ID="hfIDS" runat="server" />
        <asp:HiddenField ID="Parms" runat="server" />
        <!--end-->
        <div id="OmGrid1" style="width: 960px; z-index: 999;">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="result_table">
                        <colgroup>
                            <col style="width: 10px" />
                            <col style="width: 30px" />
                            <col style="width: 70px" />
                            <col style="width: 100px" />
                            <col style="width: 140px" />
                            <col style="width: 80px" />
                            <col style="width: 100px" />
                            <col style="width: 80px" />
                            <col style="width: 50px" />
                            <col style="width: 50px" />
                            <col style="width: 70px" />
                        </colgroup>
                        <tr>
                            <th></th>
                            <th>
                                <input type="checkbox" name="ckbAll" onclick="selectAll()" /></th>
                            <th>课程编号</th>
                            <th>课程名称</th>
                            <th>学期</th>
                            <th>课程分类</th>
                            <th>所属机构</th>
                            <th>授课方式</th>
                            <th>学分</th>
                            <th>学时</th>
                            <th>操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id='<%# Eval("CourseID")%>' class="item item_row">
                        <td></td>
                        <td>
                            <input type="checkbox" name="ckbItem"></td>
                        <td><%# Eval("CourseNo")%></td>
                        <td><%# Eval("CourseName")%></td>
                        <td><%# Eval("TermTypeName")%></td>
                        <td><%# Eval("CourseTypeName") %></td>
                        <td><%# Eval("OrganizationName") %></td>
                        <td><%# Eval("TeachingTypeName") %></td>
                        <td><%# Eval("Credit") %></td>
                        <td><%# Eval("Hours") %></td>
                        <td>
                            <p class='operation_box'>
                                <i title='编辑' pid='<%#Eval("CourseID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                <i title='删除' onclick='Del(this.id)' id='<%#Eval("CourseID")%>' class='icon_admin delete_btn'></i>
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

    <div id="TermEdit" style="width: 640px; height: 500px; border: 1px solid #ccc; display: none">
        <div style="width: 100%; height: 35px; background: #366061; border: 1px solid #fff; color: #fff">
            <span style="font-size: 16px; font-weight: 600; line-height: 35px; margin-left: 10px">管理学期</span>
            <button id="pagebtn1" style="color: #fff; background-color: transparent; border: 0; float: right; margin: 8px 10px 0px 0px" onclick="">关闭</button>
        </div>
        <div id="TermTable" style="height: 330px; width: 520px; margin: 41px 59px 27px 59px; overflow-y: scroll">
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 60px; margin: auto">
                <a class="wod_btn" runat="server" style="position: absolute; left: 230px; margin-top: 10px" href="#">确定</a>
                <a class="wrt_btn" id="pagebtn2" href="#" style="position: absolute; left: 330px; margin-top: 10px">取消</a>
            </p>
        </div>
    </div>
    <div id="TeachEdit" style="width: 640px; height: 500px; border: 1px solid #ccc; display: none">
        <div style="width: 100%; height: 35px; background: #366061; border: 1px solid #fff; color: #fff">
            <span style="font-size: 16px; font-weight: 600; line-height: 35px; margin-left: 10px">管理授课方式</span>
            <button id="pagebtn3" style="color: #fff; background-color: transparent; border: 0; float: right; margin: 8px 10px 0px 0px" onclick="">关闭</button>
        </div>
        <div id="TeachTypeTable" style="height: 330px; width: 520px; margin: 41px 59px 27px 59px; overflow-y: scroll">
        </div>
        <div style="width: 100%; height: 58px;">
            <p style="height: 60px; margin: auto">
                <a class="wod_btn" runat="server" style="position: absolute; left: 230px; margin-top: 10px" href="#">确定</a>
                <a class="wrt_btn" id="pagebtn4" href="#" style="position: absolute; left: 330px; margin-top: 10px">取消</a>
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
        function DelBatch(ids) {
            if (confirm("是否确定删除!") == true) {
                $("#<%=hfIDS.ClientID %>").val(ids);
                $("#<%=BatchDel.ClientID %>").click();
            }
        }
        function ParmSelect() {
            var parm = ParmInfo();
            $("#<%=Parms.ClientID %>").val(parm);
        }
        window.onload = function () {
            var s = '<%=Getclass()%>';
        }
        function GetCourseList() {
            var url = "Course.ashx";
            var params = { action: "GetTermList" };
            var strHtml = "";
            var json = $G2S.GetAjaxJson(params, url);
            if (json != "暂无数据") {
                strHtml += "<table id='termlist' style='border:1px solid #ccc;line-height:30px;width:500px'>";
                strHtml += "<colgroup><col style='width: 100px;height:30px' /><col style='width: 320px;height:30px' /><col style='width: 80px;height:30px' /></colgroup>";
                strHtml += "    <tr style='background:#ccc'>";
                strHtml += "                        <th style='text-align:left;text-indent:1em;border-right:1px solid #fff'>编号</th>";
                strHtml += "                        <th style='text-align:left;text-indent:1em'>学期列表</th>";
                strHtml += "                        <th></th>";
                strHtml += "                    </tr>";
                for (var i = 0; i < json.length; i++) {
                    var rows = json[i];
                    var TermID = rows.TermID;
                    var TermNo = rows.TermNo;
                    var TermYear = rows.TermYear;
                    var TermTypeName = rows.TermTypeName;
                    strHtml += " <tr id='" + TermID + "' style='border-top:1px solid #ccc'>";
                    strHtml += "                        <td><div  style='overflow:hidden;width:100px;height:30px;text-indent:1em;border-right:1px solid #ccc'>" + TermNo + "</div></td>";
                    strHtml += "                        <td style='text-indent:1em'>" + TermYear + "学年" + TermTypeName + "</td>";
                    strHtml += "                        <td><p class='operation1_box'><i title='编辑' pid='" + TermID + "' onclick='Eidt(this)' class='icon_admin edit_btn'></i><i title='删除' onclick='Del(this)'  pid='" + TermID + "' class='icon_admin delete_btn'></i></p></td>";
                    strHtml += "                    </tr>";
                }
                strHtml += "</table>";
            }
            $("#TermTable").html(strHtml);
            init();
            PageTerm();
        }
        function GetTeachTypeList() {
            var url = "Course.ashx";
            var params = { action: "GetTeachTypeList" };
            var strHtml = "";
            var json = $G2S.GetAjaxJson(params, url);
            if (json != "暂无数据") {
                strHtml += "<table id='termlist' style='border:1px solid #ccc;line-height:30px;width:500px'>";
                strHtml += "<colgroup><col style='width: 100px;height:30px' /><col style='width: 320px;height:30px' /><col style='width: 80px;height:30px' /></colgroup>";
                strHtml += "    <tr style='background:#ccc'>";
                strHtml += "                        <th style='text-align:left;text-indent:1em;border-right:1px solid #fff'>编号</th>";
                strHtml += "                        <th style='text-align:left;text-indent:1em'>授课方式列表</th>";
                strHtml += "                        <th></th>";
                strHtml += "                    </tr>";
                for (var i = 0; i < json.length; i++) {
                    var rows = json[i];
                    var TeachingTypeID = rows.TeachingTypeID;
                    var TeachingTypeNo = rows.TeachingTypeNo;
                    var Name = rows.Name;
                    strHtml += " <tr id='" + TeachingTypeID + "' style='border-top:1px solid #ccc'>";
                    strHtml += "                        <td><div  style='overflow:hidden;width:100px;height:30px;text-indent:1em;border-right:1px solid #ccc'>" + TeachingTypeNo + "</div></td>";
                    strHtml += "                        <td style='text-indent:1em'>" + Name + "</td>";
                    strHtml += "                        <td><p class='operation1_box'><i title='编辑' pid='" + TeachingTypeID + "' onclick='Eidt(this)' class='icon_admin edit_btn'></i><i title='删除' onclick='Del(this)'  pid='" + TeachingTypeID + "' class='icon_admin delete_btn'></i></p></td>";
                    strHtml += "                    </tr>";
                }
                strHtml += "</table>";
            }
            $("#TeachTypeTable").html(strHtml);
            init();
            PageTeach();
        }
        //加载样式
        function init() {

            $('#termlist tr').hover(function () {
                $(this).find('.operation1_box').toggle();
            })
            $('#termlist').each(function () {
                $(this).find('tr:odd').css('background', '#f7f7f7');
            })
        }
        //学期管理
        function PageTerm() {
            var pageii = $.layer({
                type: 1,
                title: false,
                area: ['auto', 'auto'],
                border: [0], //去掉默认边框
                shade: [0], //去掉遮罩
                closeBtn: [0, false], //去掉默认关闭按钮
                shift: 'top', //从左动画弹出
                page: { dom: '#TermEdit' }
            });
            //自设关闭
            $('#pagebtn1').on('click', function () {
                layer.close(pageii);
            });
            $('#pagebtn2').on('click', function () {
                layer.close(pageii);
            });
        }
        //授课方式管理
        function PageTeach() {
            var pageii = $.layer({
                type: 1,
                title: false,
                area: ['auto', 'auto'],
                border: [0], //去掉默认边框
                shade: [0], //去掉遮罩
                closeBtn: [0, false], //去掉默认关闭按钮
                shift: 'top', //从左动画弹出
                page: { dom: '#TeachEdit' }
            });
            //自设关闭
            $('#pagebtn3').on('click', function () {
                layer.close(pageii);
            });
            $('#pagebtn4').on('click', function () {
                layer.close(pageii);
            });
        }
    </script>
    <style>
        .operation1_box {
            display: none;
            height: 30px;
            overflow: hidden;
            zoom: 1;
        }

            .operation1_box i {
                float: left;
                margin: 6px 10px 0 0;
                cursor: pointer;
            }
    </style>
</asp:Content>

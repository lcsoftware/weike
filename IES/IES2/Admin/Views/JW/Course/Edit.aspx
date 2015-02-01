<%@ Page Title="课程编辑" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Course.Edit" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Frameworks/zTree_v3/css/zTreeStyle/AuRoleTreeStyle.css" rel="stylesheet" />
    <script src="../../../Frameworks/zTree_v3/js/jquery.ztree.all-3.5.js"></script>
    <script src="CourseEdit.js"></script>
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Frameworks/layer/layer.min.js"></script>
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>

    <style>
        .ztree li span.button.add {
            margin-left: 2px;
            margin-right: -1px;
            background-position: -144px 0;
            vertical-align: top;
            *vertical-align: middle;
        }

        ul.ztree li {
            overflow: visible;
            text-overflow: visible;
            display: block;
            clear: both;
        }

        ul.ztree ul {
            height: auto;
        }

        .edit_box {
            margin-left: 70px;
            float: none;
        }

            .edit_box i {
                float: none;
                margin-top: 5px;
            }

        .ztree li a {
            height: 25px;
            font-size: 14px;
            color: #284a51;
            font-family: "Microsoft YaHei";
        }

            .ztree li a:hover {
                text-decoration: none;
            }

            .ztree li a.curSelectedNode {
                height: 25px;
                background: none;
                border: 0;
                color: #05965b;
            }

            .ztree li a.tmpTargetNode_inner {
                background-color: #316ac5;
                border: 1px solid #316ac5;
                color: white;
                height: 25px;
                opacity: 0.5;
                padding-top: 0;
            }
    </style>
    <uc1:Nav runat="server" ID="Nav" />

    <div class="data_nav_box">
        <ul class="data_nav_list">
            <li id="xx" class="active"><a href="javascript:show(1)">基本资料</a></li>
            <li id="yy"><a href="javascript:show(2)">教学信息</a></li>
        </ul>
    </div>
    <div class="data_box">
        <div class="tip_box" id="noticets">
            <p><i class="icon_admin tip_icon"></i><b>提示信息：</b>请尽量完善以下个人资料，并保证其真实性。*为必填项。</p>
            <span class="close_tip" onclick="Hidets()">×</span>
        </div>
        <div id="resume">
            <div class="info_box">
                <h5>基本信息</h5>
                <div class="info_detail_box">
                    <div class="info_detail" style="width: 920px;">
                        <div class="info_detail_list">
                            <label>课程编号：</label>
                            <div class="fill_box">
                                <input id="CourseNo" runat="server" class="fill_box" /> 
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CourseNo" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                        <div class="info_detail_list">
                        </div>
                        <div class="info_detail_list">
                            <label>课程名称：</label>
                            <div class="fill_box">
                                <input id="Name" runat="server" class="fill_box" />
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Name" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                        <div class="info_detail_list">
                            <label>英文名称：</label>
                            <div class="fill_box">
                                <input id="EnglishName" runat="server" class="fill_box" />
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>所属机构：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="DDLOrganization" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLOrganization" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="请选择">请选择</asp:CompareValidator>
                        </div>
                        <div class="info_detail_list">
                            <label>授课方式：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="DDLMethods" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list">
                            <label>学  期：</label>
                            <div>
                                <asp:DropDownList ID="DDLSemester" Width="200px" Height="32px" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="info_detail_list" style="width: 520px">
                            <label>学  科：</label>
                            <div class="fill_box">
                                <asp:DropDownList ID="DDLDiscipline" Width="200px" Height="32px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLDiscipline_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="fill_box" style="margin-left: 10px">
                                <asp:DropDownList ID="SpecialtyType" Width="150px" Height="32px" runat="server"></asp:DropDownList>
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="SpecialtyType" ErrorMessage="CompareValidator" ForeColor="Red" Operator="NotEqual" ValueToCompare="0">请选择</asp:CompareValidator>
                        </div>
                        <div class="info_detail_list">
                            <label>学分：</label>
                            <div class="fill_box">
                                <input id="Credit" onkeyup="this.value=this.value.replace(/[^0-9-]+/,'');" runat="server" class="fill_box" />
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Credit" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                        <div class="info_detail_list">
                            <label>学时：</label>
                            <div class="fill_box">
                                <input id="Hours" onkeyup="this.value=this.value.replace(/[^0-9-]+/,'');" runat="server" class="fill_box" />
                            </div>
                            <label style="color: red; width: 5px">*</label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Hours" ErrorMessage="RequiredFieldValidator" ForeColor="Red">未填写</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="info_box">
                <h5>学时组成</h5>
                <div class="info_detail_box">
                    <div id="OmGrid1" style="width: 602px; height: 82px;">
                        <table class="zxy_table">
                            <colgroup>
                                <col style="width: 150px" />
                                <col style="width: 150px" />
                                <col style="width: 150px" />
                                <col style="width: 150px" />
                            </colgroup>
                            <tr>
                                <th>理论<p class='operation_box'>
                                    <i title='编辑' pid='<%#Eval("CourseID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='删除' onclick='Del(this.id)' id='<%#Eval("CourseID")%>' class='icon_admin delete_btn'></i>
                                </p>
                                </th>
                                <th>实践<p class='operation_box'>
                                    <i title='编辑' pid='<%#Eval("CourseID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='删除' onclick='Del(this.id)' id='<%#Eval("CourseID")%>' class='icon_admin delete_btn'></i>
                                </p>
                                </th>
                                <th>上机<p class='operation_box'>
                                    <i title='编辑' pid='<%#Eval("CourseID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='删除' onclick='Del(this.id)' id='<%#Eval("CourseID")%>' class='icon_admin delete_btn'></i>
                                </p>
                                </th>
                                <th>课外<p class='operation_box'>
                                    <i title='编辑' pid='<%#Eval("CourseID")%>' onclick='Edit(this)' class='icon_admin edit_btn'></i>
                                    <i title='删除' onclick='Del(this.id)' id='<%#Eval("CourseID")%>' class='icon_admin delete_btn'></i>
                                </p>
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <input id="SpecialtyNo" runat="server" class="fill_box" /></td>
                                <td>
                                    <input id="Text1" runat="server" class="fill_box" /></td>
                                <td>
                                    <input id="Text2" runat="server" class="fill_box" /></td>
                                <td>
                                    <input id="Text3" runat="server" class="fill_box" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div class="info_box">
                <h5>课程分类</h5>
                <div style="width: 725px">
                    <div class="zTreeDemoBackground left" style="width: 725px">
                        <ul id="treeDemo" class="ztree organization_list" style="width: 725px"></ul>
                    </div>
                </div>
            </div>

        </div>

        <div id="Information" style="display: none">

            <div class="info_box">
                <h5>课程简介</h5>
                <div class="info_detail_box">
                    <div class="info_detail" style="width: 900px; margin-left: 70px;">
                        <input type="hidden" name="oEditor1" id="oEditor1" runat="server" />
                        <iframe runat="server" id="frmoEditor1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=coolblue" frameborder="0" scrolling="no" width="759" height="290" style="display: block;"></iframe>
                    </div>
                </div>
            </div>

            <div class="info_box">
                <h5>教学大纲</h5>
                <div class="info_detail_box">
                    <div class="info_detail" style="width: 900px; margin-left: 70px;">
                        <input type="hidden" name="oEditor1" id="oEditor2" runat="server" />
                        <iframe runat="server" id="Iframe1" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor2&style=coolblue" frameborder="0" scrolling="no" width="759" height="290" style="display: block;"></iframe>
                    </div>
                </div>
            </div>

            <div class="info_box">
                <h5>教学团队</h5>
                <div class="info_detail_box">
                    <div class="info_detail" style="width: 900px; margin-left: 70px;">
                        <input type="hidden" name="oEditor1" id="oEditor3" runat="server" />
                        <iframe runat="server" id="Iframe2" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor3&style=coolblue" frameborder="0" scrolling="no" width="759" height="290" style="display: block;"></iframe>
                    </div>
                </div>
            </div>

            <div class="info_box">
                <h5>教学进度表</h5>
                <div class="info_detail_box">
                    <div class="info_detail" style="width: 900px; margin-left: 70px;">
                        <input type="hidden" name="oEditor1" id="oEditor4" runat="server" />
                        <iframe runat="server" id="Iframe3" src="../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor4&style=coolblue" frameborder="0" scrolling="no" width="759" height="290" style="display: block;"></iframe>
                    </div>
                </div>
            </div>

        </div>
        <!--隐藏控件-->
        <asp:HiddenField ID="hfIDS" runat="server" Value="1" />
        <!--end-->
        <div style="width: 940px">
            <a class="update_btn" onclick="onCheck()" runat="server" onserverclick="update_Click">更新资料</a>
        </div>
    </div>
    <script type="text/javascript">
        //获取选择项
        function onCheck() {
            var treeObj = $.fn.zTree.getZTreeObj("treeDemo"),
            nodes = treeObj.getCheckedNodes(true),
            v = "";
            for (var i = 0; i < nodes.length; i++) {
                var halfCheck = nodes[i].getCheckStatus();
                if (!halfCheck.half) {
                    if (i == nodes.length - 1) {
                        v += nodes[i].CourseTypeID;
                    }
                    else {
                        v += nodes[i].CourseTypeID + ",";
                    }
                }
            }
            $("#<%=hfIDS.ClientID %>").val(v);
        }
        
        function show(i) {
            var jbxx = document.getElementById("resume");
            var jzxx = document.getElementById("Information");
            var xx = document.getElementById("xx");
            var yy = document.getElementById("yy");
            if (i == 1) {
                jbxx.style.display = "block";
                jzxx.style.display = "none";
                xx.className = 'active';
                yy.className = '';

            }
            else {
                jzxx.style.display = "block";
                jbxx.style.display = "none";
                yy.className = 'active';
                xx.className = '';
            }
        }
    </script>
    <style type="text/css">
        .zxy_table {
            width: 100%;
            border: 1px solid #e8e8e8;
            margin-left: 18px;
        }

            .zxy_table tr th {
                padding-left: 9px;
                text-align: left;
                border: 1px solid #e8e8e8;
                height: 40px;
                font-size: 12px;
            }

            .zxy_table tr td {
                text-align: left;
                border: 1px solid #e8e8e8;
                height: 40px;
            }

            .zxy_table tr th a {
                vertical-align: middle;
            }

            .zxy_table tr td input {
                height: 28px;
                width: 128px;
                vertical-align: middle;
                margin-left: 9px;
            }
    </style>
</asp:Content>


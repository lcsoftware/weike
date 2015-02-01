<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpecialtyType.aspx.cs" Inherits="Admin.Views.JW.Specialty.SpecialtyType" %>

<%@ Register Src="~/Views/Share/Nav.ascx" TagPrefix="uc1" TagName="Nav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../Js/G2S.js"></script>
    <link href="../../../Content/Css/admin.css" rel="stylesheet" />
    <script src="../../../Js/admin.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <script src="../../../Frameworks/zTree_v3/js/SpecialtyTypeTree.js"></script>
    <link href="../../../Frameworks/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="Specialty.js"></script>
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
            height: 40px;
            font-size: 14px;
            color: #284a51;
            font-family: "Microsoft YaHei";
        }

            .ztree li a:hover {
                text-decoration: none;
            }

            .ztree li a.curSelectedNode {
                height: 40px;
                background: none;
                border: 0;
                color: #05965b;
            }

            .ztree li a.tmpTargetNode_inner {
                background-color: #316ac5;
                border: 1px solid #316ac5;
                color: white;
                height: 40px;
                opacity: 0.5;
                padding-top: 0;
            }
    </style>
    <uc1:Nav runat="server" ID="Nav" />
    <div class="sousuo_box">
        <div class="filter_item">
            <div class="class_operation">
                <a href="javascript:NoOpen();"><i class="icon_admin download_btn"></i>模板下载</a>
            </div>
            <a class="add_student" href="javascript:ShowBox(0,'无',0);" style="margin-left:10px"><i class="icon_admin add_btn"></i>新增学科</a>
            <p class="search_btn">
                <input type="text" id="key" placeholder="输入编号、名称搜索学科"><i class="icon_admin search_icon"></i>
            </p>
        </div>
    </div>
    <p class="delete_tip" style="display: none;">删除成功：<span id="sptyName"></span><a href="javascript:;" onclick="SpecialtyType_CancelDel()">[撤销删除]</a></p>
    <div class="organization_box">
        <div class="organization_tit">
            <h4>学科列表</h4>
        </div>
        <ul id="treeDemo" class="ztree organization_list"></ul>
    </div>
    <div style="width:450px;display:none" id="box">
	<div class="wrap_box" style="margin:0">
    	<div class="add_box">
            <dl class="add_item">
                <dt>编　号：</dt>
                <dd>
                    <div class="num_box">	
                        <input type="text" value="1" name="SpecialtyTypeNo">
                        <span>*</span>
                        <p style="display: none;" id="regMsg"><i class="icon_admin wrong_icon"></i>编号已存在</p> 
                        <p style="display: none;" id="NoMsg"><i class="icon_admin wrong_icon"></i>请输入学科编号</p>                          
                    </div>
                </dd>
            </dl>
            <dl class="add_item">
                <dt>名　称：</dt>
                <dd>
                    <div class="num_box">
                    <input type="text" name="SpecialtyTypeName">
                    <span>*</span> 
                    <p style="display: none;" id="NameMsg"><i class="icon_admin wrong_icon"></i>请输入学科名称</p> 
                        </div> 
                </dd>
            </dl>
            <dl class="add_item">
                <dt>上级门类：</dt>
                <dd>
                     <select name="SpecialtyType" class="department_select" style="width: 180px;height:30px">
                     </select>
                </dd>
            </dl>           
        </div>
    </div>
    <div class="out_wrap">
        <div class="btn_box">
            <a class="save" href="javascript:AddSptyEdit();">确定</a>
            <a class="cancel" href="javascript:layer.closeAll();">取消</a>
        </div>
    </div>
</div>
    <script>
        
    </script>
</asp:Content>

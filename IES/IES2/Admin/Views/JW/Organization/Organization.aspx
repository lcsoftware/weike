<%@ Page Title="组织机构" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="Admin.Views.JW.Organization.Organization" %>

<%@ Register Src="~/Views/Share/SubMenuNav.ascx" TagPrefix="uc1" TagName="SubMenuNav" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Frameworks/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <link href="Organization.css" rel="stylesheet" />
    <script src="../../../Frameworks/zTree_v3/js/OrganizatiopnTree.js"></script>
    <script src="Organization.js"></script>
    <script src="../../Portal/Edit.js"></script>
    <uc1:SubMenuNav runat="server" ID="SubMenuNav" />
    <div class="filter_item">
        <div class="class_operation">
            <a href="Organization.xls"><i class="icon_admin download_btn"></i>模板下载</a>
        </div>
        <a class="add_student" href="javascript:;" style="margin-left:10px" onclick="ShowBox(0,'无',0)"><i class="icon_admin add_btn"></i>新增机构</a>
        <p class="search_btn">
            <input type="text" placeholder="输入编号、名称搜索机构" id="key"><i class="icon_admin search_icon"></i>
        </p>
    </div>
    <p class="delete_tip" style="display: none;">删除成功：<span id="orgName"></span><a href="javascript:;" onclick="Organization_CancelDel()">[撤销删除]</a></p>
    <div class="organization_box">
        <div class="organization_tit">
            <h4>组织机构列表</h4>
        </div>
        <ul id="treeDemo" class="ztree organization_list"></ul>
    </div>
    <div class="pop_bg"></div>
    <div style="width: 800px; display: none; height: 440px; overflow-y: auto;" id="box">
        <div class="wrap_box" style="margin: 0;">
            <div class="add_box">
                <dl class="add_item">
                    <dt>编　号：</dt>
                    <dd>
                        <div class="num_box">
                            <input type="text" style="width: 300px;" name="OrganizationNo" value="">
                            <span>*</span>
                            <p style="display: none;" id="NoMsg"><i class="icon_admin wrong_icon"></i>请输入组织机构编号</p>
                            <p style="display: none;" id="regNoMsg"><i class="icon_admin wrong_icon"></i>编号已存在</p>
                        </div>
                    </dd>
                </dl>
                <dl class="add_item num_box">
                    <dt>机构名称：</dt>
                    <dd>
                        <input type="text" style="width: 300px;" name="OrganizationName">
                        <span>*</span>
                        <p style="display: none;" id="NameMsg"><i class="icon_admin wrong_icon"></i>请输入组织机构名称</p>
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>英文名称：</dt>
                    <dd>
                        <input type="text" style="width: 300px;" name="OrganizationNameEn">
                        <%--<span>*</span>--%>
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>上级机构：</dt>
                    <dd>
                        <div class="select_organization">
                            <p class="default_status" id="OrgName">
                            </p>
                        </div>
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>机构类别：</dt>
                    <dd>
                        <select class="department_select" style="width: 300px;" name="OrganizationType">
                            
                        </select>
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>门户显示：</dt>
                    <dd>
                        <input type="radio" name="IsShow" checked="checked" />&nbsp;&nbsp;是&nbsp;&nbsp;
                        <input type="radio" name="IsShow" />&nbsp;&nbsp;否
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>组织简介： 
                    </dt>
                    <dd>
                        <input type="radio" name="LinkStatus" id="inputOutLink" checked="checked" />&nbsp;&nbsp;启用外部链接&nbsp;&nbsp;
                        <input type="radio" name="LinkStatus" id="inputIntroduction" />&nbsp;&nbsp;使用内部编辑器
                    </dd>
                </dl>
                <dl class="add_item num_box" id="outLink">
                    <dt>外部链接：</dt>
                    <dd>
                        <input type="text" style="width: 300px;" name="Link" value="http://">
                        <span>*</span>
                        <p style="display: none;" id="urlMsg"><i class="icon_admin wrong_icon"></i>请输入正确的Url地址!</p>
                    </dd>
                </dl>
                <dl class="add_item" id="Introduction" style="display: none;">
                    <dt>中文简介：</dt>
                    <dd>
                        <input type="hidden" name="oEditor1" value="" id="oEditor1" />
                        <iframe id="frmoEditor1" src="../../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor1&style=mini" frameborder="0" scrolling="no" width="650" height="250" style="display: block;"></iframe>
                    </dd>
                </dl>
                <dl class="add_item" id="IntroductionEn" style="display: none;">
                    <dt>英文简介：</dt>
                    <dd>
                        <input type="hidden" name="oEditor2" value="" id="oEditor2" />
                        <iframe id="frmoEditor2" src="../../../../Frameworks/ewebeditor/ewebeditor.htm?id=oEditor2&style=mini" frameborder="0" scrolling="no" width="650" height="250" style="display: block;"></iframe>
                    </dd>
                </dl>
                <dl class="add_item">
                    <dt>开课机构:</dt>
                    <dd>
                        <div class="sort_organization">
                            <p>
                                <input type="radio" name="TeachingType" checked="checked">行政类
                            </p>
                            <p>
                                <input type="radio" name="TeachingType">教学类
                            </p>
                        </div>
                    </dd>
                </dl>

            </div>
        </div>
        <div class="out_wrap">
            <div class="btn_box" style="border: 0;">
                <a class="save" href="javascript:;" onclick="AddOrEdit()">确定</a>
                <a class="cancel" href="javascript:;" onclick="layer.closeAll();">取消</a>
            </div>
        </div>
    </div>
</asp:Content>

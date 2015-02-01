
'use strict';
var siteModule = angular.module('app.site', []);
siteModule.controller('SiteController', ['$scope', '$state', 'siteProviderUrl', function ($scope, $state, siteProviderUrl) {
    ////设置模板
    //$scope.hid_sidemod = "1";
    //$scope.indexTitle = "首页";
    //$scope.ContentType;//网站风格
    //$scope.ColumnDetail;
    $scope.ParentID = 0;
    //$scope.columnchick = true;
    //更改模板
    $scope.chickbanmian = function (type) {
        $scope.hid_sidemod = type;
        OCSite_DisplayStyle_Upd(type);
    }
    //加载模块
    var OCSite_GetLoad = function () {
       // debugger;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSite_Get";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.hid_sidemod = data.d[0].DisplayStyle;
            var Language = data.d[0].Language;
            $scope.BuildMode = data.d[0].BuildMode;
            $('input[name="construction"]').each(function () {
                if ($(this).val() == $scope.BuildMode) {
                    $(this).attr("checked", true);
                }
            });
            $scope.OutSiteLink = data.d[0].OutSiteLink;
            if (data.d[0].BuildMode == 1) {
                $scope.BuildModetrue = true;
                $scope.BuildModefalse = false;
            } else {
                $scope.BuildModetrue = false;
                $scope.BuildModefalse = true;
            }
            if (Language == 1) {
                $("#Language_1").removeClass("english").addClass("chinese");
                $("#Language_0").removeClass("chinese").addClass("english");
            } else {
                $("#Language_1").removeClass("chinese").addClass("english");
                $("#Language_0").removeClass("english").addClass("chinese");
            }
            GetMod();
        });
    }

    //新增主栏目
    $scope.OCSiteColumn_Edit = function () {
        var txtname = $scope.txt_column_name;
        var ContentType = $('input[name="rad_addcolumns"]:checked').val();
        var ocid = 1;
        var columnID = -1;
        if ($scope.columnid_edit != null) {
            columnID = $scope.columnid_edit;
        }
        var parentid = $scope.ParentID;
        var url = siteProviderUrl + "/OCSiteColumn_Edit";
        var param = { columnsname: txtname, type: ContentType, OCID: ocid, ColumnID: columnID, ParentID: parentid };
        $scope.baseService.post(url, param, function (data) {
            if (parseInt(data.d) > -1) {
                $('#myModal').modal("hide"); 
                OCSite_Get();
            }
        });
    }
    //弹出新增主栏目
    $scope.addColumn = function (type) {
        if ($scope.columnid_edit != null && type!=-1) {
            var url = siteProviderUrl + "/OCSiteColumn_Get";
            var param = { ColumnID: $scope.columnid_edit };
            $scope.baseService.post(url, param, function (data) {
                $scope.ColumnDetail = data.d;
                var item = $scope.ColumnDetail[0];
                $scope.txt_column_name = item.Title;
                $scope.ContentType = item.ContentType;
                $('input[name="rad_addcolumns"]').each(function () {
                    if ($(this).val() == $scope.ContentType) {
                        $(this).attr("checked", true);
                    }
                });
            });
        } else {
            $scope.txt_column_name = "";
            $('input[name="rad_addcolumns"]').each(function () {
                if ($(this).val() == 0) {
                    $(this).attr("checked", true);
                }
            });
        }
    }

    //获取网站栏目
    var OCSite_Get = function () {
        $scope.columnid_edit = null;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSiteColumn_Tree";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.ColumnList = data.d;
        });
        $scope.$on('ngColumnGet', function (ngRepeatFinishedEvent) {
            $('.column_list li').hover(function () {
                $(this).children('.column_btn').show();
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).children('.column_btn').hide();
                $(this).removeClass('active');
            })
        });
    }

    //切换语言
    $scope.OCSite_Language_Upd = function (type) {
        if (type == 1) {
            $("#Language_1").removeClass("english").addClass("chinese");
            $("#Language_0").removeClass("chinese").addClass("english");
        } else {
            $("#Language_1").removeClass("chinese").addClass("english");
            $("#Language_0").removeClass("english").addClass("chinese");
        }
        var siteid = 1;
        var url = siteProviderUrl + "/OCSite_Language_Upd";
        var param = { SiteID: siteid, Language: type };
        $scope.baseService.post(url, param, function (data) {

        });
    }

    //更换网站风格
    var OCSite_DisplayStyle_Upd = function (type) {
        var siteid = 1;
        var url = siteProviderUrl + "/OCSite_DisplayStyle_Upd";
        var param = { SiteID: siteid, DisplayStyle: type };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                GetMod();
            }
        });
    }

    //导航点击方法
    $scope.OCSiteColumn_Conten_Upd = function (type, name, ColumnID, hierarchy) {
        //debugger;
        //type 1: 内页模式 3:列表模式 0:文本模式
        if (name == "首页") {
            $("#div_indexhome").show();
            $("#div_ewebeditor").hide();
            $("#div_mainList").hide();
        }
        else if (type == 0) {
            $("#div_indexhome").hide();
            $("#div_ewebeditor").show();
            $("#div_mainList").hide();
            OCSiteColumn_Get(ColumnID);
        } else if (type == 3) {
            $("#div_indexhome").hide();
            $("#div_mainList").show();
            $("#div_ewebeditor").hide();
            OCSiteColumn_List(ColumnID);
        }
        $scope.indexTitle = name;
    }

    //加载模板样式
    var GetMod = function () {
        if ($scope.hid_sidemod == 1) {
            //left
            $("#side_top").removeClass("across_nav side_right");
            $("#side_top").addClass("side_left");
            var oHeight = $('.main_content').outerHeight(true);
           // var oHeight = $(window).height();
            $('.side_left').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_left').width();
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
        } else if ($scope.hid_sidemod == 2) {
            //right
            $("#side_top").removeClass("across_nav ");
            $("#side_top").addClass("side_right");
            $("#side_top").removeClass("side_left");
            var oHeight = $('.main_content').outerHeight(true);
            $('.side_right').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_right').width();
            $('.main_content').css('left', (screenWidth - boxWidth - sideWidth) / 2);
        } else if ($scope.hid_sidemod == 3) {
            //top
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_right').width();
            var strWidth = document.body.clientWidth;
            strWidth = (strWidth - 1300) / 2 + 28;
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
            $("#side_top").addClass("across_nav");
            $('#side_top').height(49);
        }
        $(".version_box.active").removeClass("active");
        $("#active_" + $scope.hid_sidemod).addClass("active");
    }

    //保存
    $scope.Save = function ()
    {
        if ($scope.ContentType == 0) {
            $scope.frmoEditor1 = document.getElementById('frmoEditor1').contentWindow.getHTML();
            var url = siteProviderUrl + "/OCSiteColumn_Conten_Upd";
            var param = { ColumnID: $scope.ColumnID, Conten: $scope.frmoEditor1 };
            $scope.baseService.post(url, param, function (data) {
                OCSiteColumn_Get($scope.ColumnID);
            });
        } else {
            OC_Brief_Upd();
        }
    }

    //过滤 type:0 返回对象 1 返回序号
    var Columnfilter = function (id, type) {
        if (type == 0) {
            for (var i = 0; i < $scope.ColumnList.length; i++) {
                if (id == $scope.ColumnList[i].ColumnID) {
                    return $scope.ColumnList[i];
                }
            }
        } else if(type==1) {
            for (var i = 0; i < $scope.ColumnList.length; i++) {
                if (id == $scope.ColumnList[i].ColumnID) {
                    return i;
                }
            }
        }
    }

    //获取网站的栏目下子栏目列表
    var OCSiteColumn_List = function (columnID) {
        var url = siteProviderUrl + "/OCSiteColumn_List";
        var param = { ColumnID: columnID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.ColumnSonList = data.d;            } 
        });
    }

    //获取单个栏目详细
    var OCSiteColumn_Get = function (columnID) {
        var url = siteProviderUrl + "/OCSiteColumn_Get";
        var param = { ColumnID: columnID };
        $scope.baseService.post(url, param, function (data) {
            $scope.ColumnDetail = data.d;
            var item = $scope.ColumnDetail[0];
            $scope.Updatetime = item.Updatetime;
            $scope.ContentType = item.ContentType;
            $scope.ColumnID = item.ColumnID;
            $scope.frmoEditor1 = item.Conten;
            if ($scope.frmoEditor1 == null) {
                $scope.frmoEditor1 = "";
            }
            document.getElementById('frmoEditor1').contentWindow.setHTML($scope.frmoEditor1);
        });
    }
   
    //获取在线课程的基本信息
    var OC_Get = function () {
        var ocid = 1;
        var url = siteProviderUrl + "/OC_Get";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.Tags = data.d[0].Tags;
                $scope.SubjectName = data.d[0].SubjectName;
                $scope.Brief = data.d[0].Brief;
                $scope.CreateTime = data.d[0].CreateTime;
            }
        });
    }
    //主讲教师
    var OCTeam_List = function (role) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCTeam_List";
        var param = { OCID: ocid, Role: role };
        $scope.baseService.post(url, param, function (data) {
            $scope.OCTeam_List = data.d;
        });
    }

    //课程负责人
    var OCTeam_principal = function (role) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCTeam_List";
        var param = { OCID: ocid, Role: role };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.principalname = data.d[0].UserName;
            }
        });
    }

    //删除栏目
    $scope.OCSiteColumn_Del = function (columnid) {
        if (confirm("您确认删除吗?")) {
            var url = siteProviderUrl + "/OCSiteColumn_Del";
            var param = { ColumnID: columnid };
            $scope.baseService.post(url, param, function (data) {
                OCSite_Get();
            });
        }
    }

    //子栏目新增
    $scope.SonAdd = function (parentid) {
        $scope.ParentID = parentid;
        $('#myModal').modal('show');
        $scope.addColumn(-1);
    }

    //子栏目编辑
    $scope.sinEdit = function (columnid, parentid) {
        $scope.columnid_edit = columnid;
        $scope.ParentID = parentid;
        $('#myModal').modal('show');
        $scope.addColumn(1);
    }

    //上移 下移 上移一层 下移一层
    $scope.OCSiteColumn_Move = function (columnid, direction) {
        var url = siteProviderUrl + "/OCSiteColumn_Move";
        var param = { ColumnID: columnid, Direction: direction };
        $scope.baseService.post(url, param, function (data) {
            OCSite_Get();
        });
    }

    //网站栏目的启用
    $scope.OCSite_Field_Upd = function (conttype) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCSite_Field_Upd";
        var param = { OCID: ocid, ContentType: conttype };
        $scope.baseService.post(url, param, function (data) {
            OCSite_Get();
        });
    }

    //获取网站下视频的预览
    var File_OCPreviewMP4_List = function () {
        var ocid = 1;
        var url = siteProviderUrl + "/File_OCPreviewMP4_List";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.PreviewMP4List = data.d;
            }
        });
        $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
            //下面是在table render完成后执行的js 麻烦你看下 
            $('.video_item').listScroll({
                run_ul: '.video_list', //运动的列表；
                btn_l: '.icon_l',    //左按钮
                btn_r: '.icon_r',    //右按钮
                run_number: 1         //运动张数,超过可见数量就默认显示可见数量
            });
           

            GetMod();
        });
    }
    //课程网站推荐词
    var OC_Brief_Upd = function () {
        var ocid = 1;
        var brief = $scope.site_brief;
        var url = siteProviderUrl + "/OC_Brief_Upd";
        var param = { OCID: ocid, Brief: brief };
        $scope.baseService.post(url, param, function (data) {
           
        });
    }

    //获取课程通知
    var OCNotice_List = function () {
        var ocid = 1;
        var pageindex = 10;
        var pagesize = 1;
        var url = siteProviderUrl + "/OCNotice_List";
        var param = { OCID: ocid, PageIndex: pageindex, PageSize: pagesize };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.OCNotice_List = data.d;
            } else {
                $scope.OCNoticeCount = 0;
            }
        });
    }
    //预览
    $scope.preview = function () {
        $location.url("/Preview.cshtml");
    }


    var init = function () {
        File_OCPreviewMP4_List();
        OCTeam_principal(1);
        OCTeam_List(2);
        OC_Get();
        OCSite_GetLoad();
        OCSite_Get();
        OCNotice_List();
    }
    init();
}]);


//预览
siteModule.controller('PreviewController', ['$scope', '$state', 'siteProviderUrl', function ($scope, $state, siteProviderUrl) {

    //加载模块
    var OCSite_GetLoad = function () {
        // debugger;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSite_Get";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.hid_sidemod = data.d[0].DisplayStyle;
            var Language = data.d[0].Language;
            $scope.BuildMode = data.d[0].BuildMode;
            $('input[name="construction"]').each(function () {
                if ($(this).val() == $scope.BuildMode) {
                    $(this).attr("checked", true);
                }
            });
            $scope.OutSiteLink = data.d[0].OutSiteLink;
            if (data.d[0].BuildMode == 1) {
                $scope.BuildModetrue = true;
                $scope.BuildModefalse = false;
            } else {
                $scope.BuildModetrue = false;
                $scope.BuildModefalse = true;
            }
            if (Language == 1) {
                $("#Language_1").removeClass("english").addClass("chinese");
                $("#Language_0").removeClass("chinese").addClass("english");
            } else {
                $("#Language_1").removeClass("chinese").addClass("english");
                $("#Language_0").removeClass("english").addClass("chinese");
            }
            GetMod();
        });
    }
    var GetMod = function () {
        if ($scope.hid_sidemod == 1) {
            //left
            $("#side_top").removeClass("across_nav side_rightPreview");
            $("#side_top").addClass("side_leftPreview preview_side");
            //var oHeight = $('.main_content').outerHeight(true);
            var oHeight = $(window).height();
            $('.side_leftPreview').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_left').width();
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
        } else if ($scope.hid_sidemod == 2) {
            //right
            $("#side_top").removeClass("across_nav ");
            $("#side_top").addClass("side_rightPreview preview_side");
            $("#side_top").removeClass("side_leftPreview");
            //var oHeight = $('.main_content').outerHeight(true);
            var oHeight = $(window).height();
            $('.side_rightPreview').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_rightPreview').width();
            $('.main_content').css('left', (screenWidth - boxWidth - sideWidth) / 2);
        } else if ($scope.hid_sidemod == 3) {
            //top
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_rightPreview').width();
            var strWidth = document.body.clientWidth;
            strWidth = (strWidth - 1300) / 2 + 28;
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
            $("#side_top").addClass("across_nav preview_across");
            $("#side_top").removeClass("preview_sidePreview");
            $('#side_top').height(49);
        }
        $(".version_box.active").removeClass("active");
        $("#active_" + $scope.hid_sidemod).addClass("active");
    }

    //课程负责人
    var OCTeam_principal = function (role) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCTeam_List";
        var param = { OCID: ocid, Role: role };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.principalname = data.d[0].UserName;
            }
        });
    }
    //获取网站栏目
    var OCSite_Get = function () {
        $scope.columnid_edit = null;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSiteColumn_Tree";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.ColumnList = data.d;
        });

        $scope.$on('ngColumnGet', function (ngRepeatFinishedEvent) {
            $('.column_list li').hover(function () {
                $(this).children('.column_btn').show();
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).children('.column_btn').hide();
                $(this).removeClass('active');
            })
        });
    }

    //获取在线课程的基本信息
    var OC_Get = function () {
        var ocid = 1;
        var url = siteProviderUrl + "/OC_Get";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.Tags = data.d[0].Tags;
                $scope.SubjectName = data.d[0].SubjectName;
                $scope.Brief = data.d[0].Brief;
                $scope.CreateTime = data.d[0].CreateTime;
                $scope.Name = data.d[0].Name;
            }
        });
    }

    //主讲教师
    var OCTeam_List = function (role) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCTeam_List";
        var param = { OCID: ocid, Role: role };
        $scope.baseService.post(url, param, function (data) {
            $scope.OCTeam_List = data.d;
        });
    }
    //导航点击方法
    $scope.OCSiteColumn_Conten_Upd = function (type, name, ColumnID, hierarchy) {
        //debugger;
        //type 1: 内页模式 3:列表模式 0:文本模式
        if (name == "首页") {
            $("#div_indexhome").show();
            $("#div_ewebeditor").hide();
            $("#div_mainList").hide();
        }
        else if (type == 0) {
            $("#div_indexhome").hide();
            $("#div_ewebeditor").show();
            $("#div_mainList").hide();
            OCSiteColumn_Get(ColumnID);
        } else if (type == 3) {
            $("#div_indexhome").hide();
            $("#div_mainList").show();
            $("#div_ewebeditor").hide();
            OCSiteColumn_List(ColumnID);
        }
        $scope.indexTitle = name;
        OCSiteColumn_Nav_Tree(ColumnID);
    }

    //点击导航栏跳转
    var OCSiteColumn_NavClick = function (thi) {
        var name = $(thi).attr("nid");
        var ColumnID = $(thi).attr("kid");
        var type = $(thi).attr("pid");
        if (name == "首页") {
            $("#div_indexhome").show();
            $("#div_ewebeditor").hide();
            $("#div_mainList").hide();
        }
        else if (type == 0) {
            $("#div_indexhome").hide();
            $("#div_ewebeditor").show();
            $("#div_mainList").hide();
            OCSiteColumn_Get(ColumnID);
        } else if (type == 3) {
            $("#div_indexhome").hide();
            $("#div_mainList").show();
            $("#div_ewebeditor").hide();
            OCSiteColumn_List(ColumnID);
        }
        OCSiteColumn_Nav_Tree(ColumnID);
    }

    //获取导航栏
    var OCSiteColumn_Nav_Tree = function (columnid) {
        var ocid = 1;
        var url = siteProviderUrl + "/OCSiteColumn_Nav_Tree";
        var param = { OCID: ocid, ColumnID: columnid };
        $scope.baseService.post(url, param, function (data) {
            $scope.NavnameList = data.d;
            $scope.Column_NavCount = data.d.length;
        });
        OCSiteColumn_List(columnid);
      
    }

    var OCSiteColumn_Get = function (columnID) {
        var url = siteProviderUrl + "/OCSiteColumn_Get";
        var param = { ColumnID: columnID };
        $scope.baseService.post(url, param, function (data) {
            $scope.ColumnDetail = data.d;
            var item = $scope.ColumnDetail[0];
            $scope.Updatetime = item.Updatetime;
            $scope.ContentType = item.ContentType;
            $scope.ColumnID = item.ColumnID;
            $scope.frmoEditor1 = item.Conten;
            if ($scope.frmoEditor1 == null) {
                $scope.frmoEditor1 = "";
            }
            $("#div_Conten").html($scope.frmoEditor1);
        });
    }
    //获取网站的栏目下子栏目列表
    var OCSiteColumn_List = function (columnID) {
        var url = siteProviderUrl + "/OCSiteColumn_List";
        var param = { ColumnID: columnID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d.length > 0) {
                $scope.OCColumnCount = data.d.length;
                $scope.ColumnSonList = data.d;
            } else {
                $scope.OCColumnCount = 0;
                $scope.ColumnSonList = null;
            }
        });
    }
    //获取网站下视频的预览
    var File_OCPreviewMP4_List = function () {
        var ocid = 1;
        var url = siteProviderUrl + "/File_OCPreviewMP4_List";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.PreviewMP4List = data.d;
            }
        });
        $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
            //下面是在table render完成后执行的js
            $('.video_item').listScroll({
                run_ul: '.video_list', //运动的列表；
                btn_l: '.icon_l',    //左按钮
                btn_r: '.icon_r',    //右按钮
                run_number: 1         //运动张数,超过可见数量就默认显示可见数量
            });
            
        });
    }
    //获取课程通知
    var OCNotice_List = function () {
        var ocid = 1;
        var pageindex = 10;
        var pagesize = 1;
        var url = siteProviderUrl + "/OCNotice_List";
        var param = { OCID: ocid, PageIndex: pageindex, PageSize: pagesize };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.OCNotice_List = data.d;
            } else {
                $scope.OCNoticeCount = 0;
            }
        });
    }

    //标签点击事件 
    $scope.NvaColumnSonClick = function (thi) {
        //遍历所有的元素 操作isshow 如果为true的样式 全部为false 来达到单个选中的目的;
        for (var i = 0; i < $scope.ColumnSonList.length; i++) {
            if ($scope.ColumnSonList[i].IsShow) {
                $scope.ColumnSonList[i].IsShow = false;
            }
        }
        thi.columnson.IsShow = true;
        if (thi.columnson.ContentType == 0) {
            $("#div_indexhome").hide();
            $("#div_ewebeditor").show();
            $("#div_mainList").hide();
            OCSiteColumn_Get(thi.columnson.ColumnID);
        } else if (thi.columnson.ContentType == 3) {
            $("#div_indexhome").hide();
            $("#div_mainList").show();
            $("#div_ewebeditor").hide();
            OCSiteColumn_List(thi.columnson.ColumnID);
        }
       
      
    }
    var init = function () {
        File_OCPreviewMP4_List();
        OCSite_Get();
        OCSite_GetLoad();
        OCTeam_principal(1);
        OCTeam_List(2);
        OC_Get();
        OCNotice_List();

    }


    init();


   
}]);

//过滤器
siteModule.filter('sitefilter', function () {
    return function (item, param1) {
        for (var i = 0; i < $scope.ColumnList.length; i++) {
            if (param1 == $scope.ColumnList[i].ColumnID) {
                item = $scope.ColumnList[i];
                return item;
            }
        }
    };
});

////移除焦点事件
//siteModule.directive('ngBlur', function ($parse) {
//    return function (scope, element, attr) {
//        var fn = $parse(attr['ngBlur']);
//        $(element).on('focusout', function (event) {
//            fn(scope, { $event: event });
//        });
//    }
//});
//指令在这
siteModule.directive('onFinishRenderFilters', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
});

siteModule.directive('onColumnGet', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngColumnGet');
                });
            }
        }
    };
});
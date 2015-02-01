/// <reference path="../../../../DataProvider/OC/FC/FCProvider.aspx" />
/// <reference path="../../../../DataProvider/OC/FC/FCProvider.aspx" />
'use strict';

var FCModule = angular.module('app.fc', []);


//userModule.directive('mytransclude', function () {
//    var directive = {};

//    directive.restrict = 'E'; /* restrict this directive to elements */
//    directive.transclude = true;
//    //directive.templateUrl = "Register.cshtml";
//    directive.template = "我的控件";

//    return directive;
//});

//自定义内容
//FCModule.directive("classList", function ($document) {
//    return {
//        restrict: 'E',
//        require: 'ngModel',
//        link: function (scope, element, attrs, ngModel) {
//            alert(ngModel.$modelValue.id);
//        }
//    }
//})

//筛选器
//FCModule.filter('brDateFilter', function () {
//    return function (dateSTR) {
//        return Date.parse(dateString);
//    }
//});


FCModule.controller('FCController', ['$scope', '$state', '$stateParams', 'fcProviderUrl', function ($scope, $state, $stateParams,fcProviderUrl) {
    $scope.termName = $(".classroom_time").val();
    $scope.OCName = "大学英语";
    $scope.termSelect = '';
    $scope.coursebox = true;
    $scope.offbox = false;
    $scope.courseClass = "active";
    $scope.offClass = "";
    //alert($(".classroom_time").val());


    //校历列表 暂时去掉
    //var getTermList = function () {
    //    var url = fcProviderUrl + "/TermInfo_List";
    //    var param = { Term: "" }
    //    $scope.baseService.post(url, param, function (data) {

    //        if (data.d != null) {
    //            $scope.termInfoList = data.d;
    //        }
    //    });
    //}

    var getOCFCInfo = function (fcid) {

        var url = fcProviderUrl + "/OCFCInfo_Get";

        var param = { FCID: fcid }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.fcInfo = data.d;
            }
        });
    }

    var getOCFCList = function () {

        var url = fcProviderUrl + "/OCFC_List";
        var OCID = 1;
        var param = { OCID: OCID }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.fclist = data.d;
            }
        });
    }

    //初始数据加载，翻转课堂列表
    getOCFCList();

    $scope.FCAdd_Click = function () {
        $state.go('/OC/FC/step1?FCID=0');
    }
    //小组学习进度 浮上去显示弹出框
    $('.progress_bar li').hover(function () {
        $(this).find('.group_detail').toggle();
    })

    //选项卡点击
    $scope.Type_Click=function(type)
    {
        if (type == "course") {
            $scope.offbox = false;
            $scope.coursebox = true;
            $scope.courseClass = "active";
            $scope.offClass = "";
        } else if (type == "off") {
            $scope.offbox = true;
            $scope.coursebox = false;
            $scope.courseClass = "";
            $scope.offClass = "active";
        }
    }

    //鼠标移动到“导入出勤名单”按钮
    $scope.Import_but_move = function()
    {
            $scope.Down_Excel_but = true;
    }

    $scope.Import_but_over = function()
    {
        $scope.Down_Excel_but = false;
    }

    //展开--收起
    $scope.Up_Down = function (id) {
        //debugger;
        //alert($("#unfold_" + id).hasClass('click'));
        if (!$("#unfold_" + id).hasClass('click')) {
            $(".click").prev().children('.course_detail').slideUp();
            $(".click").text('展开  ∨');
            $(".click").removeClass('click');
            getOCFCInfo(id);
            $("#unfold_" + id).addClass('click');
            $("#unfold_" + id).text('收起 ∧');
            $("#unfold_" + id).prev().children('#cd_' + id).slideDown();
        } else {
            $("#unfold_" + id).removeClass('click');
            $("#unfold_" + id).text('展开  ∨');
            $("#unfold_" + id).prev().children('#cd_' + id).slideUp();
        }
    }
}]);

FCModule.controller('FCControllerStep1', ['$scope', '$state', '$stateParams', 'fcProviderUrl', function ($scope, $state, $stateParams, fcProviderUrl) {
    $scope.class = "mp4";
    
    var OCID = 1;
    $scope.fc = null;
    //$scope.FCID = $stateParams.FCID;
    $scope.FCID = 1;
    $scope.file_data_show = '展开';
    $scope.exam_show = '展开';
    $scope.live_show = '展开';
    
    
    
    //翻转课堂主要信息
    var OCFCGet = function (fcid) {
        var url = fcProviderUrl + "/OCFC_Get";
        var param = { fcid: fcid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.fc = data.d;
            }
        });
    }


    
    
    if ($scope.FCID != 0)
    {
        OCFCGet($scope.FCID);
    }

    //资料列表
    var OCFCFileList = function() {
        if ($scope.FCID != 0) {
            //debugger;
            var url = fcProviderUrl + "/OCFCFile_List";
            var param = { fcid: $scope.FCID };
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.fcfilelist = data.d;
                }
            });
        }
    }

    //互动列表
    var OCFCLiveList = function () {
        if ($scope.FCID != 0) {
            var url = fcProviderUrl + "/OCFCLive_List";
            var param = { fcid: $scope.FCID };
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.fclivelist = data.d;
                }
            });
        }
    }

    //资料上传
    $scope.FCFileAdd = function (model)
    {
        if (model.FileUrl == "")
        {
            alert("请选择文件！");
            return;
        }
        var url = fcProviderUrl + "/OCFCFile_Add";
        var param = { file: model };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                alert("上传成功！");
            } else {
                alert("上传失败！");
            }
        });
    }
    
    //添加论题
    $scope.FCLiveAdd = function (model)
    {
        //要加验证
        var url = fcProviderUrl + "/OCFCLive_Add";
        var param = { file: model };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                alert("添加成功！");
            } else {
                alert("添加失败！");
            }
        });
    }

    //删除资料
    $scope.OCFCFileDelete = function (model)
    {
        var url = fcProviderUrl + "/OCFCFile_Del";
        var param = { file: model };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                alert("删除成功！");
            } else {
                alert("删除失败！");
            }
        });
    }
    //设为必读
    $scope.OCFCFileMust = function (model)
    {
        var url = fcProviderUrl + "/OCFCFile_Must";
        var param = { file: model };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                alert("设置成功！");
            } else {
                alert("设置失败！");
            }
        });
    }

    //删除互动
    $scope.OCFCLiveDelete = function (model)
    {
        var url = fcProviderUrl + "/OCFCLive_Del";
        var param = { live: model };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                alert("删除成功！");
            } else {
                alert("删除失败！");
            }
        });
    }

    //教学资料展开
    $scope.file_data_click = function () {
        if ($scope.file_data) {
            $scope.file_data_show = '展开';
            $scope.file_data = false;
        }
        else {
            $scope.file_data_show = '收起';
            $scope.file_data = true;
            OCFCFileList($scope.FCID);
        }
    }
    //作业展开
    $scope.exam_click = function () {
        
        if ($scope.exam_data) {
            $scope.exam_show = '展开';
            $scope.exam_data = false;
        }
        else {
            $scope.exam_show = '收起';
            $scope.exam_data = true;
        }
    }
    //互动展开
    $scope.live_click = function () {
        if ($scope.live_data) {
            $scope.live_show = '展开';
            $scope.live_data = false;
        }
        else {
            $scope.live_show = '收起';
            $scope.live_data = true;
            OCFCLiveList();
        }
    }

 
    var start = {
        elem: '#start',
        format: 'YYYY-MM-DD',
        min: laydate.now(), //设定最小日期为当前日期
        max: '2099-06-16', //最大日期
        istime: true,
        istoday: false,
        choose: function (datas) {
            end.min = datas; //开始日选好后，重置结束日的最小日期
            end.start = datas //将结束日的初始值设定为开始日
            $scope.fc.StartDate = datas;
        }
    };

    var end = {
        elem: '#end',
        format: 'YYYY-MM-DD',
        min: laydate.now(),
        max: '2099-06-16',
        istime: true,
        istoday: false,
        choose: function (datas) {
            start.max = datas; //结束日选好后，重置开始日的最大日期
            $scope.fc.EndDate = datas;
        }
    };

    laydate(start);
    laydate(end);






}]);
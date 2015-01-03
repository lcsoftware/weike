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
FCModule.directive("classList", function ($document) {
    return {
        restrict: 'E',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            alert(ngModel.$modelValue.id);
        }
    }
})

//筛选器
//FCModule.filter('brDateFilter', function () {
//    return function (dateSTR) {
//        return Date.parse(dateString);
//    }
//});


FCModule.controller('FCController', ['$scope', '$state', 'fcProviderUrl', function ($scope, $state, fcProviderUrl) {
    $scope.termName = $(".classroom_time").val();
    $scope.OCName = "大学英语";
    //alert($(".classroom_time").val());

    var getMessageList = function () {
        
        var url = fcProviderUrl + "/OCFCInfo_List";
        var OCID = '1';
        var param = { OCID: OCID }
        $scope.baseService.post(url, param, function (data) {
            alert(data.d[0].UpdateTime);
            if (data.d != null) {
                $scope.fcInfoList = data.d;
            }
        });
    }


    getMessageList();

    //小组学习进度 浮上去显示弹出框
    $('.progress_bar li').hover(function () {
        $(this).find('.group_detail').toggle();
    })

    //展开--收起
    $scope.Up_Down = function (fc) {
        debugger;
        alert(fc.model);
        if (!$("#unfold_" + id).hasClass('click')) {
            $("#unfold_" + id).addClass('click');
            $("#unfold_" + id).text('收起 ∧');
            $("#unfold_" + id).prev().children('#cd_' + id).slideDown();
        } else {
            $("#unfold_" + id).removeClass('click');
            $("#unfold_" + id).text('展开  ∨');
            $("#unfold_" + id).prev().children('#cd_' + id).slideUp();
        }
    }
}
]);
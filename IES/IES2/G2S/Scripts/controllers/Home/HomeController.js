'use strict';

var homeModule = angular.module('app.home', []);

homeModule.controller('HomeController', ['$scope', '$state', 'homeProviderUrl', function ($scope, $state, homeProviderUrl) {
    $scope.PageSize = 2;
    $scope.PageIndex = 1;
    $scope.PagesNum = 1;
    //获取课程列表
    $scope.OcList = null;//课程列表
    $scope.OcResourseList = null; //教学资源列表

    var GetOcList = function () {
        var url = homeProviderUrl + "/OC_List";
        var param = {
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                $scope.PageIndex = 1;//
            } else {
                $scope.OcList = data.d;

                $scope.PagesNum = Math.ceil(data.d.length / $scope.PageSize);
            }
        });
    }
    GetOcList();
    // 0 上一页  1 下一页
    $scope.GetPageList = function (move) {
        if (move == '0') {
            if ($scope.PageIndex > 1) {
                $scope.PageIndex = $scope.PageIndex - 1;
            }
            else {
                $scope.PageIndex = $scope.PagesNum;
            }
        }
        else {
            if ($scope.PageIndex < $scope.PagesNum) {
                $scope.PageIndex = $scope.PageIndex + 1;
            }
            else {
                $scope.PageIndex = 1;
            }
        }
    }
    //课程让是
    $scope.$on('ngGetOcList', function (ngRepeatFinishedEvent) {
        $('.img_tit').hover(function () {
            $(this).find('.course_detail').stop(true).animate({ top: 0 }, 500);
        }, function () {
            $(this).find('.course_detail').stop(true).animate({ top: '-150px' }, 500);
        })
        $('.icon_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })

        //首页课程鼠标经过动画
        $('.course_all>li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
            $(this).find('p').stop(true).animate({ bottom: '32px' }, 300);
            $(this).find('.small_icon').stop(true).animate({ top: '70px' }, 300);
        }, function () {
            $(this).removeClass('active');
            $(this).find('p').stop(true).animate({ bottom: '0' }, 300);
            $(this).find('.small_icon').stop(true).animate({ top: '102px' }, 300);
        })

        ////收起全部课程
        //$('.shouqi').live('click', function () {
        //    if (!$(this).hasClass('click')) {
        //        $(this).addClass('click');
        //        $(this).next().slideUp();
        //        $(this).html('展开全部课程↓')
        //    } else {
        //        $(this).removeClass('click');
        //        $(this).next().slideDown();
        //        $(this).html('收起全部课程↑')
        //    }
        //})
    });

    //获取资料列表
    $scope.FileModel = {
        FileTitle: "",
        OCID: 0,
        CourseID: -1,
        FolderID: -1,
        FileType: -1,
        UploadTime: '2011-1-1'
    };
    //获取资料列表
    var GetResourseList = function () {
        var url = homeProviderUrl + "/File_Search";
        var param = {
            file: $scope.FileModel,
            PageSize: 3,
            PageIndex: 1
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
            } else {
                $scope.OcResourseList = data.d;
                console.log($scope.OcResourseList);
            }
        });
    }
    GetResourseList();
    //删除文件
    $scope.DelFile = function(file)
    {
        if (confirm("您确定删除吗？")) {
            var url = homeProviderUrl + "/File_Del";
            var param = {
                file: file
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d === null) {
                } else {
                    GetResourseList();
                }
            });
        }
    }

}]);

homeModule.filter('ocListFilter', function () {
    return function (arr, ope, num, size) {
        if (arr == null || arr == '') {
            return;
        }
        return arr.filter(function (item) {
            if (ope == 'between') {
                return item.RowNum <= num * size && item.RowNum >= ((num - 1) * size + 1);
            }
        });
    }
});


homeModule.directive('onGetOcList', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngGetOcList');
                });
            }
        }
    };
});
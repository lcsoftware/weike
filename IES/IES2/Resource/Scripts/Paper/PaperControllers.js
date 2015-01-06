'use strict';

var appPaper = angular.module('app.paper.controllers', ['app.paper.services']);

appPaper.controller('PaperListCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

    $scope.searchKey = '';

    $scope.model = {};
    $scope.paperTypes = [];
    $scope.paperTypeFilters = [];
    $scope.shareRangeFilters = [];

    $scope.paperTypeSelection = -1;
    $scope.createrSelection = -1; 
    $scope.shareRangeSelection = -1;

    $scope.tabs = [];

    $scope.tabChanged = function (tab) {
        console.log(tab);
    }

    $scope.paperTypeChanged = function (v) {
        $scope.paperTypeSelection = v;
        $scope.filterChanged();
    }

    $scope.createrChanged = function (v) {
        $scope.createrSelection = v;
        $scope.filterChanged();
    }

    $scope.shareRangeChanged = function (v) {
        $scope.shareRangeSelection = v;
        $scope.filterChanged();
    }

    ///查询
    $scope.filterChanged = function () { 
        var paper = {};
        var pageSize = 10;
        var pageIndex = 1;
        PaperService.search(paper, pageSize, pageIndex, function (data) {
        });
    } 

    ///初始化试卷类型
    PaperService.getPaperTypes(function (data) {
        $scope.paperTypes = data.d;
        if ($scope.paperTypes.length > 0) {
            angular.copy($scope.paperTypes, $scope.paperTypeFilters);
            var item = {};
            angular.copy($scope.paperTypeFilters[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.paperTypeFilters.insert(0, item); 
        }
    });

    ///初始化使用权限
    PaperService.getShareRanges(function (data) {
        if (data.d) {
            $scope.shareRangeFilters = data.d;
            var item = {};
            angular.copy($scope.shareRangeFilters[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.shareRangeFilters.insert(0, item);
        }
    }); 
    ///创建Paper对象
    PaperService.paperGet(function (data) {
        if (data.d) {
            $scope.model = data.d;
        }
    });
    ///获取课程列表
    PaperService.User_OC_List(function (data) {
        if (data.d) {
            $scope.tabs = data.d;
        }
    });
}]); 
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

    $scope.tabs = [
        { id: 1, name: '毛泽东思想和中国特色社会主义毛泽东思想和中国特色社会主义' },
        { id: 2, name: '大学英语' },
        { id: 3, name: '形式与政策' }
    ];

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
        console.log(11111);
    }

    $scope.tabChanged = function (tab) {
        console.log(tab);
    }

    ///初始化试卷类型
    PaperService.getPaperTypes(function (data) {
        $scope.paperTypes = data;
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
        if (data) {
            $scope.shareRangeFilters = data;
            var item = {};
            angular.copy($scope.shareRangeFilters[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.shareRangeFilters.insert(0, item);
        }
    }); 

    PaperService.paperGet(function (data) {
        if (data) {
            $scope.model = data; 
        }
    });

}]); 
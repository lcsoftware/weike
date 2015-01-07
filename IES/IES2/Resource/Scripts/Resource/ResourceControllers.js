'use strict';

var appResource = angular.module('app.resource.controllers', ['app.res.services']);

appResource.controller('ResourceCtrl', ['$scope', 'resourceService', function ($scope, resourceService) {

    $scope.fileTypes = [];//文件类型
    $scope.timePass = [];//上传时间
    $scope.shareRange = []; //使用权限
    $scope.fileSelection = -1;
    $scope.timeSelection = -1;
    $scope.shareSelection = -1;
    $scope.tabSelection = -1;
    $scope.checksSelect = [];//复选框选中的值
    
    
    $scope.fileShow = false;//是否显示

    $scope.fileChanged = function (v) {
        $scope.fileSelection = v;
        $scope.filterChanged();
    }
    $scope.timeChanged = function (v) {
        $scope.timeSelection = v;
        $scope.filterChanged();
    }
    $scope.shareChanged = function (v) {
        $scope.shareSelection = v;
        $scope.filterChanged();
    }

    $scope.tabs = [];

    $scope.tabChanged = function (tab) {        
        $scope.tabSelection = tab;
    }
    
    //查询文件夹
    $scope.filterChanged = function () {
        var file = {};
        var pageSize = 10;
        var pageIndex = 1;
        resourceService.File_Search(file, pageSize, pageIndex, function (data) {
            if (data.d) {
                $scope.data = data.d;
            }
        });
    }

    //文件类型初始化
    resourceService.Resource_Dict_FileType_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限'; 
            $scope.fileTypes = data.d;
            $scope.fileTypes.insert(0, item);
        }
    });
    //上传时间初始化
    resourceService.Resource_Dict_TimePass_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.timePass = data.d;
            $scope.timePass.insert(0, item);
        }
    });
    //使用权限初始化
    resourceService.Resource_Dict_ShareRange_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.shareRange = data.d;
            $scope.shareRange.insert(0, item);
        }
    });

    //复选框值
    $scope.checkAdd = function(file)
    {
        if (file) {
            $scope.checksSelect.push(file);
        }        
    }
    //全选
    $scope.checkALL = function()
    {

    }

    resourceService.User_OC_List(function (data) {
        if (data.d) {
            $scope.tabs = data.d;
            var item = {};
            angular.copy($scope.tabs[0], item);
            item.OCID = -1;
            item.Name = '个人资料';
            $scope.tabs.insert(0, item);
        }
    });
}]);
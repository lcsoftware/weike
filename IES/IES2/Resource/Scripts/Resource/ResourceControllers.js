'use strict';

var appResource = angular.module('app.resource.controllers', ['app.res.services']);

appResource.controller('ResourceCtrl', ['$scope', 'resourceService', 'pageService', function ($scope, resourceService, pageService) {
    $scope.model = {};
    $scope.fileTypes = [];//文件类型
    $scope.timePass = [];//上传时间
    $scope.shareRange = []; //使用权限
    $scope.model.fileSelection = -1;
    $scope.model.timeSelection = -1;
    $scope.model.shareSelection = -1;
    $scope.tabSelection = -1;
    $scope.checksSelect = [];//复选框选中的值
    $scope.folders = [];//文件夹数组
    
    
    $scope.fileShow = false;//是否显示

    $scope.fileChanged = function (v) {
        $scope.model.fileSelection = v;
        $scope.filterChanged();
    }
    $scope.timeChanged = function (v) {
        $scope.model.timeSelection = v;
        $scope.filterChanged();
    }
    $scope.shareChanged = function (v) {
        $scope.model.shareSelection = v;
        $scope.filterChanged();
    }

    $scope.tabs = [];

    $scope.tabChanged = function (tab) {        
        $scope.tabSelection = tab;
    }
    
    var load = function()
    {
        $scope.filterChanged();
    }   
    
    $scope.mToggle = function (a) {
        console.log(a);
        $('.more_operation').hover(function () {
            a.find('.mouse_right').toggle();
        })
    }

    //查询
    $scope.filterChanged = function () {
        var folder = {};
        var pageSize = 10;
        var pageIndex = 1;
        resourceService.Folder_List(folder, function (data) {
            if (data.d) {
                $scope.folders = data.d;                
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

    $scope.updName = function(item)
    {
        //新建
        if (item.FolderID == 0) {
            var folder = { FolderName: item.FolderName };
            resourceService.Folder_Name_Upd(folder, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
        else {//修改            
            var folder = { FolderName: item.FolderName, FolderID: item.FolderID };
            resourceService.Folder_Name_Upd(item, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
    }

    //新建文件夹
    $scope.AddFolder = function()
    {
        resourceService.Folder_Get(function (data) {
            var folder = data.d;
            folder.FolderName = 'NewFolder';
            $scope.folders.push(folder);
        });        
    }

    load();
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

    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        //下面是在table render完成后执行的js
        $('.more_operation').hover(function () {
            $(this).find('.mouse_right').toggle();
        })
    });
}]);
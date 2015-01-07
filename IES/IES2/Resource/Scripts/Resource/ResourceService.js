'use strict';

var aService = angular.module('app.res.services', [
    'app.common.services'
]);

aService.factory('resourceService', ['httpService', function (httpService) {
    var service = {};
    var resourceProviderUrl = '/DataProvider/Resource/ResourceProvider.aspx';

    var ajaxPost = function (method, param, callback) {
        var url = resourceProviderUrl + '/' + method;
        httpService.post(url, param, callback);
    }

    service.Resource_Dict_FileType_Get = function (callback) {
        ajaxPost('Resource_Dict_FileType_Get', null, callback);
    }
    service.Resource_Dict_TimePass_Get = function (callback) {
        ajaxPost('Resource_Dict_TimePass_Get', null, callback);
    }
    service.Resource_Dict_ShareRange_Get = function (callback) {
        ajaxPost('Resource_Dict_ShareRange_Get', null, callback);
    }
    ///查询文件
    service.File_Search = function (file, pageSize, pageIndex, callback) {
        var param = { file: file, pageSize: pageSize, pageIndex: pageIndex };
        ajaxPost('File_Search', param, callback);
    }    
    //删除文件
    service.File_Del = function () {
        ajaxPost('File_Del', { file: file }, callback);
    }
    //查询文件夹
    service.Folder_List = function (folder, callback) {
        var param = { folder: folder };
        ajaxPost('Folder_List', param, callback);
    }
    //新增文件夹
    service.Folder_ADD = function (folder, callback)
    {
        ajaxPost('Folder_ADD', { folder: folder }, callback);
    }
    //修改文件夹名称
    service.Folder_Name_Upd = function (folder, callback) {
        ajaxPost('Folder_Name_Upd', { folder: folder }, callback);
    }
    
    ///课程列表
    service.User_OC_List = function (callback) {
        ajaxPost('User_OC_List', null, callback);
    }
    return service;
}]);
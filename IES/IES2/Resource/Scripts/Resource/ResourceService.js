﻿'use strict';

var aService = angular.module('app.res.services', [
    'app.common.services'
]);

aService.factory('resourceService', ['httpService', function (httpService) {
    var service = {};
    var url = '/DataProvider/Resource/ResourceProvider.aspx';

    service.Resource_Dict_FileType_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_FileType_Get', null, callback);
    }
    service.Resource_Dict_TimePass_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_TimePass_Get', null, callback);
    }
    service.Resource_Dict_ShareRange_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_ShareRange_Get', null, callback);
    }
    ///查询文件
    service.File_Search = function (file, callback) {
        var param = { file: file };
        httpService.ajaxPost(url, 'File_Search', param, callback);
    }
    //删除文件
    service.File_Del = function (file, callback) {
        httpService.ajaxPost(url, 'File_Del', { file: file }, callback);
    }
    //文件新增
    service.File_ADD = function (file, callback) {
        httpService.ajaxPost(url, 'File_ADD', { file: file }, callback);
    }
    //文件重命名
    service.File_FileTitle_Upd = function (file, callback) {
        httpService.ajaxPost(url, 'File_FileTitle_Upd', { file: file }, callback);
    }
    //文件移动
    service.File_FolderID_Upd = function (file, callback) {
        httpService.ajaxPost(url, 'File_FolderID_Upd', { file: file }, callback);
    }
    //批量设置文件使用权限
    service.File_Batch_ShareRange = function (FileIDS, ShareRange, callback) {
        httpService.ajaxPost(url, 'File_Batch_ShareRange', { FileIDS: FileIDS, ShareRange: ShareRange }, callback);
    }
    //设置文件使用权限
    service.File_ShareRange_Upd = function (file, callback) {
        httpService.ajaxPost(url, 'File_ShareRange_Upd', { model: file }, callback);
    }
    service.File_Chapter_Ken_Edit = function (file, chapter, ken, callback) {
        httpService.ajaxPost(url, 'File_Chapter_Ken_Edit', { model: file, chapter: chapter, ken: ken }, callback);
    }

    //查询文件夹
    service.Folder_List = function (folder, callback) {
        var param = { folder: folder };
        httpService.ajaxPost(url, 'Folder_List', param, callback);
    }
    //新增文件夹
    service.Folder_ADD = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_ADD', { folder: folder }, callback);
    }
    //设置文件夹权限
    service.Folder_ShareRange_Upd = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_ShareRange_Upd', { model: folder }, callback);
    }
    //设置文件夹权限
    service.Folder_Batch_ShareRange = function (folderIds, shareRange, callback) {
        httpService.ajaxPost(url, 'Folder_Batch_ShareRange', { folderIds: folderIds, shareRange: shareRange }, callback);
    }
    //修改文件夹名称
    service.Folder_Name_Upd = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_Name_Upd', { folder: folder }, callback);
    }
    //获取文件夹空对象
    service.Folder_Get = function (callback) {
        httpService.ajaxPost(url, 'Folder_Get', null, callback);
    }
    //获得文件夹对象
    service.Folder_GetModel = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_GetModel', { folder: folder }, callback);
    }
    //文件夹移动
    service.Folder_ParentID_Upd = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_ParentID_Upd', { folder: folder }, callback);
    }
    //删除文件夹
    service.Folder_Del = function (folder, callback) {
        httpService.ajaxPost(url, 'Folder_Del', { folder: folder }, callback);
    }

    var resetTimePass = function (timePass) {
        var UploadTime = new Date();
        switch (timePass) {
            case "1":
                UploadTime.setTime(UploadTime.getTime() - 7 * 24 * 3600 * 1000);
                break;
            case "2":
                UploadTime.setTime(UploadTime.getTime() - 30 * 24 * 3600 * 1000);
                break;
            case "3":
                UploadTime.setTime(UploadTime.getTime() - 180 * 24 * 3600 * 1000);
                break;
            default:
                UploadTime = new Date("2011", "00", "01");
                break;
        }
        return UploadTime;
    }

    //查询列表方法
    service.FolderRelation_List = function (folder, file, callback) {
        if (file) {
            file.UploadTime = resetTimePass(file.timePass);
        }
        httpService.ajaxPost(url, 'FolderRelation_List', { folder: folder, file: file }, callback);
    }

    ///课程列表
    service.User_OC_List = function (callback) {
        httpService.ajaxPost(url, 'User_OC_List', null, callback);
    }
    return service;
}]);
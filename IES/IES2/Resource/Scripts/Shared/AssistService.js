'use strict';

var aService = angular.module('app.assist.services', []);

aService.factory('assistService', ['httpService', function (httpService) {
    var service = {};

    var url = '/DataProvider/Shared/AssistProvider.aspx';

    service.Resource_Dict_Requirement_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_Requirement_Get', null, callback);
    }

    service.Resource_Dict_Diffcult_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_Diffcult_Get', null, callback);
    }
    service.Resource_Dict_ExerciseType_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_ExerciseType_Get', null, callback);
    }
    service.Resource_Dict_Scope_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_Scope_Get', null, callback);
    }
    service.Resource_Dict_ShareRange_Get = function (callback) {
        httpService.ajaxPost(url, 'Resource_Dict_ShareRange_Get', null, callback);
    }
    service.Key_List = function (model, callback) {
        httpService.ajaxPost(url, 'Key_List', { model: model }, callback);
    }

    service.Resource_Key_List = function (searchKey, source, topNum, callback) {
        httpService.ajaxPost(url, 'Resource_Key_List', { searchKey: searchKey, source: source, topNum: topNum }, callback);
    }

    return service;
}])
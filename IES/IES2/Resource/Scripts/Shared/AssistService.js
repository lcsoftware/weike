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
    return service;
}])
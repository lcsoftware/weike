'use strict';

var aService = angular.module('app.res.services', [
    'app.common.services',
    'app.common.assistant'
]);

aService.factory('resourceService', ['httpService', 'resourceProviderUrl', function (httpService, resourceProviderUrl) {
    var service = {};

    service.Resource_Dict_FileType_Get = function (callback) {
        var url = resourceProviderUrl + '/Resource_Dict_FileType_Get';
        var param = {};
        httpService.post(url, param, callback);
    }
    service.Resource_Dict_TimePass_Get = function (callback) {
        var url = resourceProviderUrl + '/Resource_Dict_TimePass_Get';
        var param = {};
        httpService.post(url, param, callback);
    }
    service.Resource_Dict_ShareRange_Get = function (callback) {
        var url = resourceProviderUrl + '/Resource_Dict_ShareRange_Get';
        var param = {};
        httpService.post(url, param, callback);
    }

    return service;
}]);
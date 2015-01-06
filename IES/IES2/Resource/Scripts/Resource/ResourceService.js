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
    ///课程列表
    service.User_OC_List = function (callback) {
        ajaxPost('User_OC_List', null, callback);
    }
    return service;
}]);
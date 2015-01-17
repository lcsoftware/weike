'use strict';

var contentApp = angular.module('app.content.services', []);

contentApp.factory('contentService', ['httpService', function (httpService) {
    var service = {};

    ///在线课程列表
    service.User_OC_List = function (callback) {
        var url = '/DataProvider/Shared/ContentProvider.aspx/User_OC_List';
        httpService.post(url, null, callback);
    } 

    return service;
}]);
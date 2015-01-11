'use strict';

var contentApp = angular.module('app.content.services', []);

contentApp.factory('contentService', ['httpService', function (httpService) {
    var service = {};

    service.User_OC_List = function (callback) {
        var url = '/DataProvider/Shared/ContentProvider.aspx/User_OC_List';
        httpService.post(url, null, callback);
    }

    service.Chapter_List = function (chapter, callback) {
        var url = '/DataProvider/Shared/ContentProvider.aspx/Chapter_List';
        httpService.post(url, { model: chapter }, callback);
    }

    return service;
}]);
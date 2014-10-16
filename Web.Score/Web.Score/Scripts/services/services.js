﻿'use strict';

var aService = angular.module('app.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('softname', '成绩分析系统');

aService.factory('baseService', ['$http', function ($http) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }

    service.post = function (url, param, success) {
        $http.post(url, param).success(function (data) {
            if (success) {
                success(data);
            }
        });
    }

    return service;
}]);

aService.factory('menuService', ['baseService', function (baseService) {
    var service = {};

    service.getMenus = function (callback) {
        var url = '../Assets/menu.json';
        baseService.get(url, callback);
    }

    service.readMenu = function (callback) {
        var url = '/DataProvider/DataProvider.aspx/GetMenuFromFile';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.filterMenus = function (funcs, callback) {

    }

    return service;
}]);

aService.factory('userService', ['baseService', function (baseService) {
    var service = {};

    service.verify = function (userName, pwd, callback) {
        var url = '/DataProvider/DataProvider.aspx/Verify';
        var param = { user: userName, pwd: pwd };
        baseService.post(url, param, callback);
    }

    service.logout = function (callback) {
        var url = '/DataProvider/DataProvider.aspx/Logout';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.isAuthorized = function (callback) {
        var url = '/DataProvider/DataProvider.aspx/GetCookieInfo';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getUser = function (callback) {
        var url = '/DataProvider/DataProvider.aspx/GetCookieInfo';
        var param = null;
        baseService.post(url, param, function (data) {
            callback(data);
        });
    }

    service.getFuncs = function (teacher, callback) {
        var url = '/DataProvider/DataProvider.aspx/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    return service;
}]);
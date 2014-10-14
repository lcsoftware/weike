'use strict';

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
            success(data);
        });
    }

    return service;
}]);

aService.factory('cookieService', ['$cookies', function ($cookies) {

    var service = {};

    service.getUser = function () {
        return $cookies.user !== '' ? JSON.parse($cookies.user) : {};
    }

    service.setUser = function (user) {
        $cookies.user = JSON.stringify(user);
    }

    service.logout = function () {
        $cookies.user = ''; 
    }

    service.isAuthorize = function () {
        return $cookies.user !== '';
    }

    return service;
}]);

aService.factory('userService', ['baseService', function (baseService) {
    var service = {};
 
    service.verify = function (userName, pwd, callback) {
        var url = '/DataProvider/DataProvider.aspx/Verify';
        var param = { user: userName, password: pwd };
        baseService.post(url, param, callback);
    }

    service.getFuncs = function (teacher, callback) {
        var url = '/DataProvider/DataProvider.aspx/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    return service;
}]);
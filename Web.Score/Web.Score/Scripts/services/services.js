'use strict';

var aService = angular.module('app.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('softname', '成绩分析系统');

aService.constant('dataProviderUrl', '/DataProvider/DataProvider.aspx');

aService.factory('baseService', ['$http', function ($http) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }

    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) {
                if (thenFn) {
                    thenFn(data);
                }
            })
            .error(function (reason) {
                if (errFn) {
                    errFn(reason);
                }
            });
    }
    return service;
}]);

aService.factory('menuService', ['baseService', 'dataProviderUrl', 'appUtils', function (baseService, dataProviderUrl, appUtils) {
    var service = {};

    service.getMenus = function (callback) {
        var url = '../Assets/menu.json';
        baseService.get(url, callback);
    }

    service.readMenu = function (callback) {
        var url = dataProviderUrl + '/GetMenuFromFile';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getFuncs = function (teacher, callback) {
        var url = dataProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    var filterMenus = function (funcs, menus) {
        var length = funcs.length;
        for (var i = 0; i < length; i++) {
            var func = funcs[i];
            var count = menus.length;
            for (var j = 0; j < count; j++) {
                var menu = menus[j];
                menu.visable = func.FuncID === menu.FuncID;
            }
        }
    }

    service.initMenus = function (teacher, callback) {
        var url = dataProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        var funcPromise = appUtils.createPromise(url, param);

        url = dataProviderUrl + '/GetMenuFromFile';
        param = null;
        var menuPromise = appUtils.createPromise(url, param);

        appUtils.runPromises({ funcs: funcPromise, menus: menuPromise }, function (results) {
            var aMenus = [];
            var aFuncs = null;
            if (results.menus.d !== null) {
                aMenus = JSON.parse(results.menus.d);
            }
            if (results.funcs.d !== null) {
                aFuncs = results.funcs.d;
            }

            if (aFuncs.length > 0 && aMenus.children.length > 0) { 
                for (var menu in aMenus.children) {
                    filterMenus(aFuncs, menu.children);
                }
            }
            callback(aMenus);
        });
    }

    return service;
}]);

aService.factory('userService', ['baseService', 'dataProviderUrl', function (baseService, dataProviderUrl) {
    var service = {};

    service.verify = function (userName, pwd, callback) {
        var url = dataProviderUrl + '/Verify';
        var param = { user: userName, pwd: pwd };
        baseService.post(url, param, callback);
    }

    service.logout = function (callback) {
        var url = dataProviderUrl + '/Logout';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.isAuthorized = function (callback) {
        var url = dataProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getUser = function (callback) {
        var url = dataProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, function (data) {
            callback(data.d !== '' ? JSON.parse(data.d) : null);
        });
    }

    service.getFuncs = function (teacher, callback) {
        var url = dataProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    service.changePwd = function (teacher, oldPwd, newPwd, status, callback) {
        var url = dataProviderUrl + '/ChangePwd';
        var param = { teacherID: teacher, oldPwd: oldPwd, newPwd: newPwd, status: status };
        baseService.post(url, param, callback);
    }

    service.getUserGroup = function (callback) { 
        var url = dataProviderUrl + '/GetUserGroup';
        var param = null;
        baseService.post(url, param, callback);
    }

    return service;
}]);
'use strict';

var aService = angular.module('app.common.services', ['ngCookies']);

///XHR调用
aService.factory('httpService', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }
    //异步post
    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) { if (thenFn) { thenFn(data); } })
            .error(function (reason) { if (errFn) { errFn(reason); } });
    }
    //同步
    service.promise = function (url, param) {
        var deferred = $q.defer();

        $http.post(url, param)
            .success(function (data) { deferred.resolve(data); })
            .error(function (reason) { deferred.reject(reason) });

        return deferred.promise;
    }

    service.all = function (promises, thenFn) {
        $q.all(promises).then(function (results) {
            thenFn(results);
        });
    }

    return service;
}]);

aService.factory('pageService', ['httpService', function (httpService) {

    var service = {};
    ///页大小
    service.size = 10;
    ///当前页索引
    service.current = 1;
    ///记录数据
    service.rows = 0; 

    var changeFn = null;

    ///初始化页大小、当前页、分页函数等
    service.init = function (size, current, changeFunc) {
        service.size = size || 10;
        service.current = current || 1;
        changeFn = changeFunc;
    }

    ///下一页
    service.next = function () {
        service.change(service.current + 1);
    }

    ///上一页
    service.previous = function () {
        service.change(service.current - 1); 
    }

    ///切换至某页
    service.change = function (pageIndex) { 
        var total = service.rows / service.size + service.rows % service.size;
        if (pageIndex >= 1 && pageIndex < total) { 
            service.current = pageIndex;
            if (changeFn) changeFn(service.current, service.size);
        } 
    }

    return service;
}]);
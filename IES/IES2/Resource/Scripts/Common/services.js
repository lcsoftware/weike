﻿'use strict';

var aService = angular.module('app.common.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('appName', 'IES')

///XHR调用
aService.factory('httpService', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }

    service.ajaxPost = function (url, method, param, callback) {
        var url = url + '/' + method;
        service.post(url, param, callback);
    }

    //异步post
    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) {
                if (thenFn) {
                    thenFn(data);
                }
            })
            .error(function (reason) {
                console.log(reason);
                if (errFn) {
                    errFn(reason);
                }
            });
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

    ///分页控件最多显示10个索引号：【1,2,3,4,5,6,7,8,9,10】
    var maxPages = 10;
    service.group = 1;

    service.pageIndex = 1;

    service.pages = [];
    service.allPages = [];

    var changeFn = null;

    var getMaxPageIndex = function(){
        var maxIndex = service.rows / service.size;
        return service.rows % service.size === 0 ? maxIndex : maxIndex + 1;
    }

    var changePages = function (groupIndex) { 
        service.pages = [];
        service.allPages = [];
        var maxPageIndex = getMaxPageIndex();
        var maxIndex = Math.min(groupIndex * maxPages, maxPageIndex);
        var minIndex = maxIndex - maxPages <= 0 ? 1 : maxIndex - maxPages;
        for (var i = minIndex; i <= maxIndex; i++) {
            service.pages.push(i);
        }             
        for (var i = 1; i <= maxPageIndex; i++) {
            service.allPages.push(i);
        }
    }

    ///初始化页大小、当前页、分页函数等
    service.init = function (size, current, rows, changeFunc) {
        service.size = size || 10;
        service.current = current || 1;
        service.rows = rows || 0;
        changePages();
        changeFn = changeFunc;
    }

    ///下一页
    service.next = function () {
        service.change(service.current + 1);
    }

    ///下一批次
    service.nextGroup = function () {
        var maxIndex = getMaxPageIndex();
        if (service.group + 1 <= maxIndex) {
            changePages(service.group++);
        }
    }

    ///上一页
    service.previous = function () {
        service.change(service.current - 1); 
    }

    ///上一批次
    service.prevGroup = function () {
        if (service.group > 1) {
            changePages(service.group--);
        }
    }

    ///切换至某页
    service.change = function (pageIndex) { 
        var total = service.rows / service.size + service.rows % service.size;
        if (pageIndex >= 1 && pageIndex < total) { 
            service.current = pageIndex;
            if (changeFn) changeFn(service.current, service.size);
        } 
    }

    ///下拉框切换页面
    service.locate = function(pageIndex) {
        var groupIndex = pageIndex / maxPages;
        service.group = pageIndex % maxPages === 0 ? groupIndex : groupIndex + 1;
        service.current = pageIndex;
        changePages(service.group);
        if (changeFn) changeFn(service.current, service.size);
    }
    return service;
}]);
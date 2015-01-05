'use strict';

var aService = angular.module('app.paper.services', ['app.common.assistant']);

aService.factory('PaperService', ['httpService', 'paperProviderUrl', function (httpService, paperProviderUrl) {
    var service = {};
    
    ///获取试题类型
    service.getPaperTypes = function (callback) {
        var url = paperProviderUrl + '/GetPaperTypes'
        var param = null;
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    }

    ///获取共享范围
    service.getShareRanges = function (callback) {
        var url = paperProviderUrl + '/GetShareRanges'
        var param = null;
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    }

    ///创建Paper对象
    service.paperGet = function (callback) {
        var url = paperProviderUrl + '/Paper_Get'
        var param = null;
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        }); 
    }

    ///查询
    service.search = function (paper, pageSize, pageIndex, callback) {
        var url = paperProviderUrl + '/Paper_Search'
        var param = { paper: paper, pageSize: pageSize, pageIndex: pageIndex };
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    } 

    return service;
}]);
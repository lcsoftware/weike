'use strict';

var aService = angular.module('app.paper.services', []);

aService.factory('PaperService', ['httpService', 'paperProviderUrl', function (httpService, paperProviderUrl) {
    var service = {};
    
    ///生成试卷对象
    service.paperMake = function (callback) {
        var url = paperProviderUrl + '/PaperMake'
        var param = null;
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    }

    ///获取试题类型
    service.getPaperTypes = function (callback) {
        var url = paperProviderUrl + '/GetPaperTypes'
        var param = null;
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    }

    /// 试卷列表查询
    service.paperSearch = function (paper, pageSize, pageIndex, callback) {
        var url = paperProviderUrl + '/Paper_Search'
        var param = { model: paper, pageSize: pageSize, pageIndex: pageIndex };
        httpService.post(url, param, function (data) {
            if (callback) callback(data.d);
        });
    }
    return service;
}]);
'use strict';

var aService = angular.module('app.paper.services', []);

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

    service.search = function (paper, pageSize, pageIndex) {
        var url = paperProviderUrl + '/Paper_Search'
        var param = {}

    }
    return service;
}]);
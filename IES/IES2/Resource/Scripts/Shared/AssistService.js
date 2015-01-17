'use strict';

var aService = angular.module('app.assist.services', []);

aService.factory('assistService', ['httpService', function (httpService) {
    var service = {};

    var assistProviderUrl = '/DataProvider/Shared/AssistProvider.aspx'; 

    service.getImportances = function (callback) {
        var importances = [];
        importances.push({ id: 1, name: '重要性001' });
        importances.push({ id: 2, name: '重要性002' });
        importances.push({ id: 3, name: '重要性003' });
        importances.push({ id: 4, name: '重要性004' });
        importances.push({ id: 5, name: '重要性005' });
        if (callback) callback(importances);
    }
    return service;
}])
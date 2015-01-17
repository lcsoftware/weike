'use strict';

var aService = angular.module('app.knowledge.services', []);

aService.factory('knowledgeService', ['httpService', function (httpService) {
    var service = {};

    var knowProviderUrl = '/DataProvider/Knowledge/KnowProvider.aspx';
 
    service.Ken_Get = function (callback) {
        httpService.ajaxPost(knowProviderUrl, 'Ken_Get', null, callback);
    }

    service.save = function (model, callback) {
        httpService.ajaxPost(knowProviderUrl, 'Ken_ADD', { model: model }, callback);
    }
    return service;
}]);
'use strict';

var aService = angular.module('app.ken.services', []);

aService.factory('kenService', ['httpService', function (httpService) {
    var service = {};

    var kenProviderUrl = '/DataProvider/Knowledge/KnowProvider.aspx';

    service.Ken_List = function (model, callback) {
        httpService.ajaxPost(kenProviderUrl, 'Ken_List', { model: model }, callback);
    }

    service.Ken_ADD = function (model, callback) {
        httpService.ajaxPost(kenProviderUrl, 'Ken_ADD', { model: model }, callback);
    }

    service.Ken_Upd = function (model, callback) {
        httpService.ajaxPost(kenProviderUrl, 'Ken_Upd', { model: model }, callback);
    } 
    service.Ken_Chapter_List = function (model, callback) {
        httpService.ajaxPost(kenProviderUrl, 'Ken_Chapter_List', { model: model }, callback);
    } 
    return service;
}]);
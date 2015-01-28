'use strict';

var contentApp = angular.module('app.resken.services', []);

contentApp.factory('resourceKenService', ['httpService', function (httpService) {
    var service = {};

    var url = '/DataProvider/ResourceKen/ResourceKenProvider.aspx';

    service.ResourceKen_ADD = function (model, callback) {
        httpService.ajaxPost(url, 'ResourceKen_ADD', {model: model}, callback);
    } 

    service.ResourceKen_List_OCID = function (ocid, callback) {
        httpService.ajaxPost(url, 'ResourceKen_List_OCID', { ocid: ocid}, callback);
    }

    return service;
}]);
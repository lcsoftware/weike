'use strict';

var aService = angular.module('app.res.services', []);

aService.factory('resourceService', ['$http', function ($http) {
    var service = {};

<<<<<<< HEAD
    service.Resource_Dict_FileType_Get = function (callback) {
        var url = '/DataProvider/Resource/ResourceProvider.aspx/Resource_Dict_FileType_Get';
        var param = {};
        httpService.post(url, param, callback);
    }
    service.Resource_Dict_TimePass_Get = function (callback) {
        var url = '/DataProvider/Resource/ResourceProvider.aspx/Resource_Dict_TimePass_Get';
        var param = {};
        httpService.post(url, param, callback);
    }
    service.Resource_Dict_ShareRange_Get = function (callback) {
        var url = '/DataProvider/Resource/ResourceProvider.aspx/Resource_Dict_ShareRange_Get';
        var param = {};
        httpService.post(url, param, callback);
    }

=======
>>>>>>> 401bc7c75e28e47471b651cc44c5b24708657140
    return service;
}]);
'use strict';

var aService = angular.module('app.common.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('appName', '我的项目');

aService.constant('userProviderUrl', '/DataProvider/User/UserProvider.aspx');
aService.constant('authProviderUrl', '/DataProvider/Authority/AuthProvider.aspx');
aService.constant('resourceProviderUrl', '/DataProvider/Resource/ResourceProvider.aspx');
aService.constant('knowProviderUrl', '/DataProvider/Knowledge/KnowProvider.aspx');
aService.constant('exerciseProviderUrl', '/DataProvider/Exercise/ExerciseProvider.aspx');
aService.constant('paperProviderUrl', '/DataProvider/Paper/PaperProvider.aspx');

Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};

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
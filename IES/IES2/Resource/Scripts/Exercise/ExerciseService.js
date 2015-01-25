'use strict';

var aService = angular.module('app.exercise.services', [
    'app.common.services'
]);

aService.factory('exerciseService', ['httpService', function (httpService) {
    var service = {};
    var resourceProviderUrl = '/DataProvider/Exercise/ExerciseProvider.aspx';

    var ajaxPost = function (method, param, callback) {
        var url = resourceProviderUrl + '/' + method;
        httpService.post(url, param, callback);
    }

    return service;
}]);
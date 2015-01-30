'use strict';

var aService = angular.module('app.exercise.services', [
    'app.common.services'
]);

aService.factory('exerciseService', ['httpService', function (httpService) {
    var service = {};
    var url = '/DataProvider/Exercise/ExerciseProvider.aspx';
    
    service.Exercise_Search = function (model, key, pageSize, pageIndex, callback) {
        var param = {
            model: model,
            key: key,
            pageSize: pageSize,
            pageIndex: pageIndex
        }
        httpService.ajaxPost(url, 'Exercise_Search', param, callback);
    }

    
    service.Exercise_Model_Info = function (callback) {
        httpService.ajaxPost(url, 'Exercise_Model_Info', null, callback);
    }
    service.ExerciseInfo_Get = function (model, callback) {
        httpService.ajaxPost(url, 'ExerciseInfo_Get', { model: model }, callback);
    }

    //习题新增
    service.Exercise_ADD = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_ADD', { model: model }, callback);
    }

    return service;
}]);
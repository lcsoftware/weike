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
    service.ExerciseInfo_GetListen = function (model, callback) {
        httpService.ajaxPost(url, 'ExerciseInfo_GetListen', { model: model }, callback);
    }
    service.Exercise_Model_Info_Get = function (callback) {
        httpService.ajaxPost(url, 'Exercise_Model_Info_Get', null, callback);
    }
    

    //习题新增
    service.Exercise_ADD = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_ADD', { model: model }, callback);
    }

    //习题删除
    service.Exercise_Del = function (exerciseID, callback) {
        httpService.ajaxPost(url, 'Exercise_Del', { exerciseID: exerciseID}, callback);
    }
    //判断题增改
    service.Exercise_Judge_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_Judge_M_Edit', { model: model }, callback);
    }
    //判断题获取
    service.Exercise_Judge_Get = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_Judge_Get', { ExerciseID: model }, callback);
    }
    //单选多选题增改
    service.Exercise_MultipleChoice_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_MultipleChoice_M_Edit', { ExerciseID: model }, callback);
    }
    //填空题增改
    service.Exercise_FillInBlanks_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_FillInBlanks_M_Edit', { model: model }, callback);
    }

    return service;
}]);
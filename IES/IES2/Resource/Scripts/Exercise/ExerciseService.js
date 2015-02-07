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
        httpService.ajaxPost(url, 'Exercise_Del', { exerciseID: exerciseID }, callback);
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
        httpService.ajaxPost(url, 'Exercise_MultipleChoice_M_Edit', { model: model }, callback);
    }
    //单选多选题获取
    service.Exercise_MultipleChoice_Get = function (ExerciseID, callback) {
        httpService.ajaxPost(url, 'Exercise_MultipleChoice_Get', { ExerciseID: ExerciseID }, callback);
    }
    //填空题增改
    service.Exercise_FillInBlanks_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_FillInBlanks_M_Edit', { model: model }, callback);
    }
    //名词解释、 分析题、解答题、计算题 基本信息维护
    service.Exercise_Analysis_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_Analysis_M_Edit', { model: model }, callback);
    }
    //问答题、写作题信息维护
    service.Exercise_Writing_M_Edit = function (model, callback) {
        httpService.ajaxPost(url, 'Exercise_Writing_M_Edit', { model: model }, callback);
    }
    //问答题、 写作题详细信息
    service.Exercise_Writing_Get = function (ExerciseID, callback) {
        httpService.ajaxPost(url, 'Exercise_Writing_Get', { ExerciseID: ExerciseID }, callback);
    }

    //多个题目的获取
    service.Exercise_Analysis_Get = function (ExerciseID, callback) {
        httpService.ajaxPost(url, 'Exercise_Analysis_Get', { ExerciseID: ExerciseID }, callback);
    }

    return service;
}]);
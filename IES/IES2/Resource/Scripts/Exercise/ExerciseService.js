'use strict';

var aService = angular.module('app.exercise.services', [
    'app.common.services'
]);

aService.factory('exerciseService', ['httpService', function (httpService) {
    var service = {};
    var exerciseProviderUrl = '/DataProvider/Exercise/ExerciseProvider.aspx';
    

    //service.Resource_Dict_Requirement_Get = function (callback) {
    //    httpService.ajaxPost(exerciseProviderUrl, 'Resource_Dict_Requirement_Get', null, callback);
    //}

    return service;
}]);
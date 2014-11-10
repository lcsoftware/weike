var aService = angular.module('app.queryService', []);

aService.factory('queryService', ['baseService', 'queryProviderUrl', function (baseService, queryProviderUrl) {
    var service = {};
    
    service.GetGradeCourseByTeacherId = function (micyear, teacherid, callback)
    {
        var url = queryProviderUrl + '/GetGradeCourseByTeacherId';
        var param = { micyear: micyear, teacherid: teacherid };
        baseService.post(url, param, callback);
    }    

    return service;
}]);
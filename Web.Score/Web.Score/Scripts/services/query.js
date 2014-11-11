var aService = angular.module('app.queryService', []);

aService.factory('queryService', ['baseService', 'queryProviderUrl', function (baseService, queryProviderUrl) {
    var service = {};
    //获得课程
    service.GetGradeCourseByTeacherId = function (micyear, teacherid, callback)
    {
        var url = queryProviderUrl + '/GetGradeCourseByTeacherId';
        var param = { micyear: micyear, teacherid: teacherid };
        baseService.post(url, param, callback);
    }
    //获得年级班级
    service.GetGradeCodeByTeacherId = function (micyear, teacherid, callback) {
        var url = queryProviderUrl + '/GetGradeCodeByTeacherId';
        var param = { micyear: micyear, teacherid: teacherid };
        baseService.post(url, param, callback);
    }
    //获取成绩列表
    service.GetQueryTeacher = function (micyear, teacherid, gradeCourse, gradecode, testtypes, testno, callback) {
        var url = queryProviderUrl + '/GetQueryTeacher';
        var param = {
            micyear: micyear, teacherid: teacherid, gradeCourse: gradeCourse,
            gradecode: gradecode, testtypes: testtypes, testno: testno
        };
        baseService.post(url, param, callback);
    }
    return service;
}]);
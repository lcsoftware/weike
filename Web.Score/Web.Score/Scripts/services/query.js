var aService = angular.module('app.queryService', []);

aService.factory('queryService', ['baseService', 'queryProviderUrl', function (baseService, queryProviderUrl) {
    var service = {};
    //获得任教老师课程
    service.GetGradeCourseByTeacherId = function (micyear, teacherid, callback) {
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
    service.GetQueryTeacher = function (micyear, teacherid, gradeCourse, gradecode, testtypes, testno, stuId, callback) {
        var url = queryProviderUrl + '/GetQueryTeacher';
        var param = {
            micyear: micyear, teacherid: teacherid, gradeCourse: gradeCourse,
            gradecode: gradecode, testtypes: testtypes == null ? null : testtypes.Code,
            testno: testno == null ? null : testno.TestNo, stuId: stuId
        };
        baseService.post(url, param, callback);
    }
    //获得考试号
    service.GetTestNo = function (micyear, testtype, callback) {
        var url = queryProviderUrl + '/GetTestNo';
        var param = { micyear: micyear, testtype: testtype };
        baseService.post(url, param, callback);
    }
    //获得班主任年级
    service.GetScope = function(teacherId,callback)
    {
        var url = queryProviderUrl + '/GetScope';
        var param = { teacherId: teacherId };
        baseService.post(url, param, callback);
    }
    //获得班主任课程
    service.GetBCourse = function(teacherScope,callback)
    {
        var url = queryProviderUrl + '/GetBCourse';
        var param = { teacherScope: teacherScope };
        baseService.post(url, param, callback);
    }

    return service;
}]);
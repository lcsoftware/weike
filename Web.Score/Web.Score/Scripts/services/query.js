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
    //获取任课教师成绩列表
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
    service.GetScope = function (teacherId, callback) {
        var url = queryProviderUrl + '/GetScope';
        var param = { teacherId: teacherId };
        baseService.post(url, param, callback);
    }
    //获得年级领导年级
    service.GetGradeScope = function (teacherId, callback) {
        var url = queryProviderUrl + '/GetGradeScope';
        var param = { teacherId: teacherId };
        baseService.post(url, param, callback);
    }
    //获得班主任课程
    service.GetBCourse = function (teacherScope, callback) {
        var url = queryProviderUrl + '/GetBCourse';
        var param = { teacherScope: teacherScope };
        baseService.post(url, param, callback);
    }
    //获取班主任成绩列表
    service.GetQueryBTeacher = function (micyear, gradeCourse, gradecode, testtypes, testno, stuId, order, callback) {
        var url = queryProviderUrl + '/GetQueryBTeacher';
        var param = {
            micyear: micyear, gradeCourse: gradeCourse,
            gradecode: gradecode, testtypes: testtypes == null ? null : testtypes.Code,
            testno: testno == null ? null : testno.TestNo, stuId: stuId,
            order: order == null ? null : order
        };
        baseService.post(url, param, callback);
    }
    //获得年级领导成绩
    service.GetQueryGradeManager = function (micyear, gradeCourse, gradecode, testtypes, testno, classCode, stuId, order, callback) {
        var url = queryProviderUrl + '/GetQueryGradeManager';
        var param = {
            micyear: micyear, gradeCourse: gradeCourse,
            gradecode: gradecode == null ? null : gradecode,
            testtypes: testtypes == null ? null : testtypes.Code,
            testno: testno == null ? null : testno.TestNo, classCode: classCode,
            stuId: stuId == null ? null : stuId.StudentId, order: order == null ? null : order
        };
        baseService.post(url, param, callback);
    }
    //获得教务员成绩
    service.GetQuerySchoolManager = function (micyear, gradeCourse, gradecode, testtypes, testno, classCode, stuId, teacherId, order, callback) {
        var url = queryProviderUrl + '/GetQuerySchoolManager';
        var param = {
            micyear: micyear, gradeCourse: gradeCourse,
            gradecode: gradecode == null ? null : gradecode,
            testtypes: testtypes == null ? null : testtypes.Code,
            testno: testno == null ? null : testno.TestNo, classCode: classCode,
            stuId: stuId == null ? null : stuId.StudentId, teacherId: teacherId == null ? null : teacherId.TeacherID,
            order: order == null ? null : order
        };
        baseService.post(url, param, callback);
    }
    //根据年级获得该年级的所有班级
    service.GetGradeByGradeNo = function (micyear, gradeNo, callback) {
        var url = queryProviderUrl + '/GetGradeByGradeNo';
        var param = { micyear: micyear, gradeNo: gradeNo };
        baseService.post(url, param, callback);
    }
    return service;
}]);
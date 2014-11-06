var aService = angular.module('app.school', []);

aService.factory('schoolService', ['baseService', 'schoolProviderUrl', function (baseService, schoolProviderUrl) {
    var service = {};

   
   
    service.logonUser = {};

    service.school = {};

    service.gradeClass = {};

    service.loadSchool = function (callback) {
        var url = schoolProviderUrl + '/LoadSchool';
        var param = null;
        var promise = baseService.postPromise(url, param);

        baseService.runPromises({ school: promise }, function (results) {
            if (results.school.d !== null) {
                service.school = results.school.d;
            }
        });
    }

    service.loadGradeClass = function (AcademicYear, callback) {
        var url = schoolProviderUrl + '/LoadGradeClass';
        var param = { academicYear: AcademicYear, andStudent: true };
        baseService.post(url, param, function (data) {
            service.gradeClass = data.d;
            if (callback) callback(data);
        });
    }

    service.upDown = function (acadeMicYear, downStudents, grades, callback) { 
        var url = schoolProviderUrl + '/UpDown';
        var param = { acadeMicYear: acadeMicYear, downStudents: downStudents, grades: grades };
        baseService.post(url, param, callback);
    }

    service.tryCalculate = function (micYear, gradeNo, courseCode, testType, callback) {
        var url = schoolProviderUrl + '/TryCalculate';
        var param = { micYear: micYear, gradeNo: gradeNo, courseCode: courseCode, testType: testType };
        baseService.post(url, param, callback);
    }

    service.TryCalculateAgain = function (c, k, micYear, gradeNo, courseCode, testType, callback) {
        var url = schoolProviderUrl + '/TryCalculate';
        var param = { c: c, k: k, micYear: micYear, gradeNo: gradeNo, courseCode: courseCode, testType: testType };
        baseService.post(url, param, callback);
    }

    service.tryOk = function (c, k, calcK,micYear, gradeNo, courseCode, testType, callback) {
        var url = schoolProviderUrl + '/TryOk';
        var param = { c: c, K:k, micYear: micYear, gradeNo: gradeNo, courseCode: courseCode, testType: testType };
        baseService.post(url, param, callback);
    }

    
    return service;
}]);
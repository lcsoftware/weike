var aService = angular.module('app.school', []);

aService.factory('schoolService', ['baseService', 'schoolProviderUrl', function (baseService, schoolProviderUrl) {
    var service = {};

    service.logonUser = {}

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

    return service;
}]);
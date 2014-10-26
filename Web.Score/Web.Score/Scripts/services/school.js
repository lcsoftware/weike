var aService = angular.module('app.school', []);

aService.factory('schoolService', ['baseService', 'schoolProviderUrl', function (baseService, schoolProviderUrl) {
    var service = {}; 

    service.logonUser = {}

    service.schoolInfo = {};

    service.gradeClass = {};

    service.loadSchool = function (callback) {
        var url = schoolProviderUrl + '/LoadSchool';
        var param = null;
        baseService.post(url, param, function (data) {
            service.schoolInfo = data.d;
            if (callback) callback(data.d);
        });
    }

    service.loadGradeClass = function (AcademicYear, callback) {
        var url = schoolProviderUrl + '/LoadGradeClass';
        var param = { academicYear: AcademicYear };
        baseService.post(url, param, function (data) {
            service.gradeClass = data.d;
            if (callback) callback(data);
        });
    }

    return service;
}]);
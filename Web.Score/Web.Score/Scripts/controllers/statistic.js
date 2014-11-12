'use strict';

var stat = angular.module('app.stat', []);

// Path: /
stat.controller('StudentStatController', ['$scope', function ($scope) {
    var moduleName = '考试情况统计';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];
    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.ScoreSorts = $scope.constService.ScoreSorts;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });

    $scope.$watch('conditionData.GradeCourse', function (testType) {
        if (testType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = $scope.conditionData.GradeCourse ? $scope.conditionData.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        $scope.GradeCourses = null;
        if (gradeCode) {
            $scope.utilService.GetGradeCourse(gradeCode, -1, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        if (testType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = $scope.conditionData.GradeCourse ? $scope.conditionData.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
}]);


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

    $scope.GradeClasses = [];
    $scope.Students = [];

    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.ScoreTypes = $scope.constService.ScoreTypes;
    $scope.ScoreSorts = $scope.constService.ScoreSorts;
    

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });
    
    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });

            url = "/DataProvider/Util.aspx/GetGradeCourse";
            param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });

    $scope.$watch('conditionData.GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if ($scope.conditionData.MicYear) {
            var url = "/DataProvider/Util.aspx/GetStudent";
            var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.Students = data.d;
            });
        }
    });

    $scope.$watch('conditionData.GradeCourse', function (gradeCourse) {
        $scope.TestTypes.length = 0;
        if ($scope.conditionData.MicYear && $scope.conditionData.GradeCode) {
            var url = "/DataProvider/Util.aspx/GetTestTypeByCourse";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCourse: gradeCourse, gradeCode: $scope.conditionData.GradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        $scope.TestLogins.length = 0;
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


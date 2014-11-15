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

    var chart1 = {};

    $scope.chartService.chartCreate('main', function (data) {
        chart1 = data;
    });

    $scope.changeOption = function () {
        var legend = { legend: { data: ['蒸发量1', '降水量'] } };
        var xAxis = {
            xAxis: [
                {
                    type: 'category',
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                }
            ]
        };
        var series = {
            series: [
                {
                    name: '蒸发量1',
                    type: 'line',
                    data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3]
                },
                {
                    name: '降水量',
                    type: 'line',
                    data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3]
                }
            ]
        };
        $scope.chartService.refresh(chart1, legend, xAxis, series); 
    }

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


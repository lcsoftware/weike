'use strict';

var analysis = angular.module('app.analysis', []);

//细目分析
analysis.controller('MinutiaAnalyseController', ['$scope', function ($scope) {
    var moduleName = '细目分析';
    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.Grades = [];
    $scope.Students = [];
    $scope.GradeCourses = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];

    //获得学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });
    //获得所有年级
    $scope.utilService.GetGradeAll(function (data) {
        $scope.GradeCodes = data.d;
    });
    //绑定考试类型，考试号
    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });
    //根据年级获得班级
    $scope.$watch('GradeCode', function (gradecode) {
        $scope.Grades.length = 0;
        if (gradecode) {
            //获得班级
            $scope.queryService.GetGradeByGradeNo($scope.MicYear.MicYear, gradecode.GradeNo, function (data) {
                if (data.d != '') {
                    $scope.Grades = JSON.parse(data.d);
                }
            });
        }
    });
    //根据班级获得学生
    $scope.$watch('GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if (gradeClass) {
            //根据班级获得学生
            $scope.utilService.GetStudentsByGrade($scope.MicYear.MicYear, gradeClass.classNo, function (data) {
                $scope.Students = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradecode) {
        //获得课程
        $scope.GradeCourses.length = 0;
        if (gradecode) {
            $scope.utilService.GetGradeCourse($scope.MicYear.MicYear, gradecode, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType && $scope.MicYear) {
            var micYear = $scope.MicYear;
            var gradeCode = $scope.GradeCode ? $scope.GradeCode.GradeNo : '';
            var gradeCourse = $scope.GradeCourse ? $scope.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    
}]);

//高三选课排名
analysis.controller('ThirdOrderController', ['$scope', function ($scope) {

}]);
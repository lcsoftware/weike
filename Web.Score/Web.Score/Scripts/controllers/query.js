'use strict';

var appQuery = angular.module('app.query', ['checklist-model']);

// Path: /
appQuery.controller('TeacherQueryController', ['$scope', function ($scope) {
    $scope.$root.title = $scope.softname + ' | 任课教师查询';
    $scope.$root.moduleName = '任课教师查询';
    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];
    $scope.Students = [];
    $scope.studentsGrid = [];
    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.testShow = false;

    $scope.userService.getUser(function (data) {
        $scope.user = data;
    });

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    
    
        
    var load = function () {        
        $scope.queryService.GetQueryTeacher($scope.MicYear.MicYear, $scope.user.TeacherID, $scope.GradeCourse.CourseCode, $scope.GradeCode.GradeNo, null, null, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
        });        
        $scope.testShow = true;
        $scope.utilService.GetTestType(function (data) {
            $scope.TestTypes = data.d;
            if ($scope.TestType != null) {
                $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.GradeCode.GradeNo, $scope.GradeCourse.CourseCode, $scope.TestType, function (data) {
                    $scope.TestLogins = data.d;
                });
            }
        });
    }

    $scope.query = function () {
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        load();
    }


    $scope.$watch('MicYear', function (micyear) {
        $scope.GradeCourses = null;
        if (micyear) {
            $scope.queryService.GetGradeCourseByTeacherId($scope.MicYear.MicYear, $scope.user.TeacherID, function (data) {
                $scope.GradeCourses = data.d;
            });
            $scope.queryService.GetGradeCodeByTeacherId($scope.MicYear.MicYear, $scope.user.TeacherID, function (data) {
                $scope.GradeCodes = data.d;
            });            
        }
    })    
    $scope.$watch('GradeCode', function (classcode) {
        $scope.Students = null;
        if(classcode)
        {
            $scope.utilService.GetStudent($scope.MicYear.MicYear, classcode.GradeNo, function (data) {
                $scope.Students = data.d;
            });
        }
    });
    //获得复选框值
    var getdeptlist = function (value) {
        var selected = "";
        for (var i = 0; i < value.length ; i++) {
            if (value[i].checked) {
                selected += value[i].value + ",";
            }
        }
        selected = selected.substring(0, selected.length - 1);
        if (selected.length == 0) {
            selected = "-1";
        }
        return selected;
    }
}]);


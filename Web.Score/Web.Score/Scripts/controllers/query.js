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

    $scope.userService.getUser(function (data) {
        $scope.user = data;
        console.log($scope.user);
    });
    
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });
    
    $scope.$watch('MicYear', function (micyear) {
        $scope.GradeCourses = null;
        if(micyear)
        {
            $scope.queryService.GetGradeCourseByTeacherId($scope.MicYear.MicYear, $scope.user.TeacherID, function (data) {
                $scope.GradeCourses = data.d;
                console.log($scope.GradeCourses);
            });
        }
    })
    $scope.$watch('GradeCourse', function (micyear) {
    });
}]);


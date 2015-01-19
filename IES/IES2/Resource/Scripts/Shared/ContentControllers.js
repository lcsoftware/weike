'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.currentCourse = {};
    $scope.courses = [];
    ///初始化课程
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.courses = data.d;
            $scope.currentCourse = data.d[0];
            $scope.$broadcast('willCourseChanged', { course: $scope.currentCourse });
            $scope.$broadcast('courseLoaded', { course: $scope.currentCourse });
        }
    });

    $scope.courseChanged = function (course) {
        $scope.currentCourse = course;
        $scope.$broadcast('willCourseChanged', { course: $scope.currentCourse });
    }

    $scope.$on('willResetCourse', function () {
        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.currentCourse = data.d[0];
                $scope.$broadcast('willCourseChanged', { course: $scope.currentCourse });
            }
        });
    });
}]);




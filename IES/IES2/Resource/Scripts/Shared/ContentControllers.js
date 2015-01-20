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
            $scope.$broadcast('willCourseChanged', $scope.currentCourse);
            $scope.$broadcast('courseLoaded', $scope.currentCourse);
            console.log('Course loaded!');
        }
    });
    ///课程切换
    $scope.courseChanged = function (course) {
        $scope.currentCourse = course;
        $scope.$broadcast('willCourseChanged', $scope.currentCourse);
        console.log('willCourseChanged!');

    }
    ///请求重置课程
    $scope.$on('willResetCourse', function () {
        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.currentCourse = data.d[0];
                $scope.$broadcast('willCourseChanged', $scope.currentCourse);
            }
        });
    });
}]);




'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.course = {};
    $scope.courses = [];
    $scope.moreCourses = [];
    ///初始化在线课程
    contentService.User_OC_List(function (data) {
        if (data.d) {
            var moreIndex = 3;
            var allCourses = data.d;
            $scope.courses = allCourses.slice(0, moreIndex);
            $scope.moreCourses = allCourses.slice(moreIndex); 
            $scope.course = data.d[0];
            $scope.$broadcast('courseLoaded', $scope.course);
        }
    });
    ///课程切换
    $scope.courseChanged = function (course) {
        $scope.course = course;
        console.log(course);
        $scope.$broadcast('willCourseChanged', $scope.course); 
    }
    ///请求重置课程
    $scope.$on('willResetCourse', function () {
        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.course = data.d[0];
                $scope.$broadcast('willCourseChanged', $scope.course);
            }
        });
    });
}]);




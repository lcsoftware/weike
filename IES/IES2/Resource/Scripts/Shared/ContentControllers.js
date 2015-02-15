'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.course = {};
    $scope.courses = [];
    $scope.$emit('onSideLeftSwitch', true);
    $scope.$emit('onWizardSwitch', true);
    ///初始化在线课程
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.courses = data.d;
            $scope.course = data.d[0];
            $scope.$broadcast('courseLoaded', $scope.course);
        }
    });

    $scope.findCourse = function (ocid) {
        var length = $scope.courses.length;
        for (var i = 0; i < length; i++) {
            if ($scope.courses[i].OCID === ocid) {
                return $scope.courses[i];
            }
        }
        return null;
    }

    ///课程切换
    $scope.courseChanged = function (course) {
        $scope.course = course;
        $scope.$broadcast('willCourseChanged', $scope.course);
    }

    ///请求重置课程
    $scope.$on('willResetCourse', function () {
        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.course = data.d[0];
                $scope.$broadcast('courseLoaded', $scope.course);
            }
        });
    });
}]);




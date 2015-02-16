'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.course = {};
    $scope.courses = [];
    $scope.courseMore = [];

    $scope.$emit('onSideLeftSwitch', true);
    $scope.$emit('onWizardSwitch', true);

    ///初始化在线课程
    var init = function () {
        contentService.User_OC_List(function (data) {
            $scope.courses.length = 0;
            $scope.courseMore.length = 0;

            if (data.d) {
                var courses = [];
                var courseMore = [];
                for (var i = 0; i < data.d.length; i++) {
                    if (i < 3) {
                        courses.push(data.d[i]);
                    } else {
                        courseMore.push(data.d[i]);
                    }
                }
                $scope.courses = courses;
                $scope.courseMore = courseMore;
                $scope.course = data.d[0];
                $scope.$broadcast('courseLoaded', $scope.course);
            }
        });
    }

    init();

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
    $scope.$on('onWillCourseChanged', function (event, course) {
        $scope.course = course;
        $scope.$broadcast('willCourseChanged', $scope.course);
    });
  

    ///请求重置课程
    $scope.$on('willResetCourse', function () {
        init(); 
    });
}]);




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
    //var init = function () {
    //    contentService.User_OC_List(function (data) {
    //        $scope.courses.length = 0;
    //        $scope.courseMore.length = 0;

    //        if (data.d) {
    //            var courses = [];
    //            var courseMore = [];
    //            for (var i = 0; i < data.d.length; i++) {
    //                if (i < 3) {
    //                    courses.push(data.d[i]);
    //                } else {
    //                    courseMore.push(data.d[i]);
    //                }
    //            }
    //            $scope.courses = courses;
    //            $scope.courseMore = courseMore;
    //            $scope.course = data.d[0];
    //            $scope.$broadcast('courseLoaded', $scope.course);
    //        }
    //    });
    //}

    //init();

    $scope.findCourse = function (ocid) {
        var length = $scope.courses.length;
        for (var i = 0; i < length; i++) {
            if ($scope.courses[i].OCID === ocid) {
                return $scope.courses[i];
            }
        }
        return null;
    }

    $scope.moreTitle = '查看更多';

    $scope.$on('onResetMoreTitle', function (event) {
        $scope.moreTitle = '查看更多';
    });

    ///更多菜单 课程切换
    $scope.$on('onWillCourseChanged', function (event, course, isMore) {
        $scope.moreTitle = isMore === 0 ? '查看更多' : course.Name;
        $scope.course = course;
        $scope.$broadcast('willCourseChanged', $scope.course);
    });
  

    ///请求重置课程
    $scope.$on('willResetCourse', function (event, from) {
        contentService.User_OC_List(function (data) {
            $scope.courses.length = 0;
            $scope.courseMore.length = 0;
 

            if (data.d) {
                var courses = [];
                var courseMore = [];

                if (from === 'Resource') {
                    var personResource = angular.copy(data.d[0]);
                    personResource.OCID = 0;
                    personResource.Name = '个人资料';
                    courses.insert(0, personResource);
                }
                var limitIndex = from === 'Resource' ? 2 : 3;
                for (var i = 0; i < data.d.length; i++) {
                    if (i < limitIndex) {
                        courses.push(data.d[i]);
                    } else {
                        courseMore.push(data.d[i]);
                    }
                }
                $scope.courses = courses;
                $scope.courseMore = courseMore;
                $scope.course = $scope.courses[0];
                $scope.$broadcast('courseLoaded', $scope.course);
            }
        });
    });
}]);




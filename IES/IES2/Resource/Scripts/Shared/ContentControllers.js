'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.courseSelection = -1;
    $scope.courses = [];

    $scope.courseChanged = function (chapter) {
        $scope.$broadcast('willCourseChanged', { chapter: chapter });
    }
}]);




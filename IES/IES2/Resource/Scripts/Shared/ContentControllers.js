'use strict';

var contentApp = angular.module('app.content.controllers', [
    'app.content.services'
]);
contentApp.controller('ContentCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.chapterSelection = -1;
    $scope.chapters = []; 

    $scope.chapterChanged = function (chapter) {
        $scope.$broadcast('willChapterChanged', { chapter: chapter });
    }
}]);




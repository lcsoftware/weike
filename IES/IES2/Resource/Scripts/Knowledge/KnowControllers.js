'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services'
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.$parent.chapters = data.d;
        }
    });
}]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'contentService', function ($scope, contentService) {
 
    $scope.$on('willChapterChanged', function (event, chapter) {
        contentService.Chapter_List(chapter, function (data) {
            if (data) {
                $scope.chapters = data.d;
            }
        });
    });
}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

    $scope.$on('chapterChanged', function (event, chapter) {

    });
}]);


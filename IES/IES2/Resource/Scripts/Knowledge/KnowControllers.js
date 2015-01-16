'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services'
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', function ($scope, contentService) {

    ///初始化课程
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.$parent.chapters = data.d;
            $scope.$parent.chapterSelection = data.d[0].OCID;
        }
    });

    $scope.item = {};
    $scope.itemName = 'ssss';
    $scope.itemType = 1;
    $scope.changeItem = function (itemType) {
        $scope.itemType = itemType;
        $scope.itemName = itemType === 2 ? "知识点名称" : "章节名称";
    }

    $scope.cancel = function () {
        console.log('cancel fired!');
    }

    $scope.selected = function () {
        console.log('selected fired!'); 
    }

    $scope.save = function () {

    } 

    $scope.dddd = function () {
        $()
    }
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


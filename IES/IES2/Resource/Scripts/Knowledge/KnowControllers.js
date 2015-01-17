'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.knowledge.services',
    'app.assist.services',
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', 'knowledgeService', 'chapterService', 'assistService',
    function ($scope, contentService, knowledgeService, chapterService, assistService) {

    ///初始化课程
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.$parent.chapters = data.d;
            $scope.$parent.chapterSelection = data.d[0].OCID;
        }
    });

    /// 添加知识点
    $scope.importances = [];
    $scope.importance = {};

    assistService.getImportances(function (data) {
        if (data.length > 0) {
            $scope.importances = data;
            $scope.importance = $scope.importances[0];
        }
    });

    $scope.knowledge = {};
    $scope.knowledge.name = '';
    $scope.knowledge.chapter = {};
    $scope.knowledge.chapters = [];

    chapterService.init_ChapterList(function (data) {
        if (data.d && data.d.length > 0) { 
            $scope.knowledge.chapters = data.d; 
            $scope.knowledge.chapter = $scope.knowledge.chapters[0];
        }
    });

    $scope.knowledge.save = function () {
        console.log('save fired');
    }
    $scope.knowledge.saveNew = function () {
        console.log('saveNew fired');
    }
    $scope.knowledge.cancel = function () {
        $scope.knowledge = {};
    }

    $scope.chapter = {};
    $scope.chapter.name = 'sss';
    $scope.chapter.knowledge = {};
    $scope.chapter.knowledges = [];

    $scope.chapter.save = function () {
        console.log('save fired');
    }
    $scope.chapter.saveNew = function () {
        console.log('saveNew fired');
    }
    $scope.chapter.cancel = function () {
        console.log('cancel fired');
    }
    /// end 添加知识点

}]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'contentService', function ($scope, contentService) {

    $scope.$on('willCourseChanged', function (event, course) {
    });
}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

    $scope.$on('willCourseChanged', function (event, course) {
    });
}]);


'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.knowledge.services',
    'app.assist.services',
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', 'knowledgeService', 'chapterService', 'assistService',
    function ($scope, contentService, knowledgeService, chapterService, assistService) {

        $scope.$emit('willResetCourse');

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

appKnow.controller('KnowChapterCtrl', ['$scope', 'chapterService', function ($scope, chapterService) {

    $scope.model = {};
    $scope.chapters = [];
    $scope.editChapterId = -1;
    $scope.canEdit = false;
    $scope.canAdd = false;
    $scope.currCourse = {};

    $scope.$on('willCourseChanged', function (event, course) {
        $scope.currCourse = course;
        $scope.model.OCID = $scope.currCourse.OCID;
        $scope.model.CourseID = $scope.currCourse.CourseID;
        chapterService.Chapter_List($scope.model, function (data) {
            $scope.chapters = data.d;
        });
    });

    $scope.$on('courseLoaded', function (course) {
        $scope.currCourse = course;
        $scope.model.OCID = $scope.currCourse.OhID;
        $scope.model.CourseID = $scope.currCourse.CourseID;
    });

    chapterService.Chapter_Get(function (data) {
        $scope.model = data.d;
        $scope.model.OCID = $scope.$parent.$parent.currentCourse.OCID;
        chapterService.Chapter_List($scope.model, function (data) {
            $scope.chapters = data.d;
        });
    });

    ///添加章节
    $scope.addChapter = function () {
        var newCahpter = angular.copy($scope.model);
        newCahpter.id = 0;
        $scope.canAdd = true;
    }
    ///编辑
    $scope.editChapter = function (chapter) {
        $scope.editChapterId = chapter.ChapterID;
    }

    ///添加章节输入框失去焦点
    $scope.onBlurAdd = function (title) {
        var newChapter = angular.copy($scope.model);
        newChapter.Title = title;

        chapterService.Chapter_ADD(newChapter, function (data) {
            if (data.d) {
                $scope.chapters.push(data.d);
                $scope.title = '';
                $scope.canAdd = false;
            }
        });
    }

    $scope.onBlurEdit = function (chapter) {
        chapterService.Chapter_Upd(chapter, function (data) {
            if (data.d) {
                $scope.editChapterId = -1;
            }
        });
    }
}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

    $scope.$on('willCourseChanged', function (event, course) {
    });
}]);


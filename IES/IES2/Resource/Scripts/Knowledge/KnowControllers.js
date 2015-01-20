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

        $scope.chapterModel = {};
        $scope.knowledgeModel = {};

        $scope.course = {};
        $scope.ocChapters = [];
        $scope.ocKnowledges = [];

        chapterService.Chapter_Get(function (data) {
            $scope.chapterModel = data.d;
            $scope.chapterModel.OCID = $scope.course.OCID;
        });

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            $scope.chapterModel.OCID = $scope.course.OCID;
            chapterService.Chapter_List($scope.chapterModel, function (data) {
                $scope.ocChapters = data.d;
                $scope.knowledge.chapters = data.d;
            });
        });

        $scope.$on('courseLoaded', function (course) {
            $scope.course = course;
            chapterService.Chapter_List($scope.chapterModel, function (data) {
                $scope.ocChapters = data.d;
                $scope.knowledge.chapters = data.d;
            });
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

        chapterService.Chapter_List($scope.chapter, function (data) {
            if (data.d && data.d.length > 0) {
                $scope.knowledge.chapters = data.d;
                $scope.knowledge.chapter = $scope.knowledge.chapters[0];
            }
        });

        $scope.knowledge.save = function () {
            console.log('save fired');
            $scope.$broadcast('knowledgeChanged', $scope.knowledge);
        }
        $scope.knowledge.saveNew = function () {
            console.log('saveNew fired');
            var copyObject = angular.copy($scope.knowledge);
            $scope.$broadcast('knowledgeChanged', copyObject);
        }
        $scope.knowledge.cancel = function () {
            $scope.knowledge = {};
        }

        /// end 添加知识点

        ///添加章节
        $scope.chapter = {};
        $scope.chapter.name = 'sss';
        $scope.chapter.knowledge = {};
        $scope.chapter.knowledges = [];

        $scope.chapter.save = function () {
            console.log('save fired');
            $scope.$broadcast('chapterChanged', $scope.chapter);
        }
        $scope.chapter.saveNew = function () {
            console.log('saveNew fired');
            var copyObject = $scope.chapter;
            $scope.$broadcast('chapterChanged', copyObject);
        }
        $scope.chapter.cancel = function () {
            console.log('cancel fired');
        }
        /// end 添加章节


    }]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'chapterService', function ($scope, chapterService) {

    $scope.editChapterId = -1;
    $scope.canEdit = false;
    $scope.canAdd = false;
    
    $scope.$on('willCourseChanged', function (event, course) {
        $scope.chapter.OCID = $scope.course.OCID;
        $scope.chapter.CourseID = $scope.course.CourseID;
    });

    $scope.$on('courseLoaded', function (course) {
        $scope.chapter.OCID = $scope.course.OhID;
        $scope.chapter.CourseID = $scope.course.CourseID;
    }); 

    ///添加章节
    $scope.addChapter = function () {
        var newCahpter = angular.copy($scope.chapter);
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
    ///关联
    $scope.association = {};
    $scope.association.selection = 1;
    ///资料
    $scope.association.linkDoc = function () {
        $scope.association.selection = 1; 
    }
    ///试题
    $scope.association.linkExercise = function () {
        $scope.association.selection = 2; 
    }
    ///话题
    $scope.association.linkTopic = function () {
        $scope.association.selection = 3; 
    }

}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

    $scope.$on('willCourseChanged', function (event, course) {
    });
}]);


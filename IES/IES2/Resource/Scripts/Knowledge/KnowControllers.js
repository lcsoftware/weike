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

        var loadChapters = function (model) {
            if (model) {
                chapterService.Chapter_List(model, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.knowledge.chapters = data.d;
                    if ($scope.knowledge.chapters.length > 0) {
                        $scope.knowledge.chapter = $scope.knowledge.chapters[0];
                    }
                });
            }
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            $scope.chapterModel.OCID = $scope.course.OCID;
            loadChapters($scope.chapterModel);
        });

        $scope.$on('courseLoaded', function (course) {
            $scope.course = course;
            loadChapters($scope.chapterModel);
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

    $scope.chapter = {};
    $scope.canAdd = false;
    $scope.chapterPick = -1;

    $scope.$on('willCourseChanged', function (event, course) {
        $scope.chapter = angular.copy($scope.$parent.chapterModel);
        $scope.chapter.OCID = $scope.course.OCID;
        $scope.chapter.CourseID = $scope.course.CourseID;
    });

    $scope.$on('courseLoaded', function (course) {
        $scope.chapter = angular.copy($scope.$parent.chapterModel);
        $scope.chapter.OCID = $scope.course.OCID;
        $scope.chapter.CourseID = $scope.course.CourseID;
    });

    ///添加章节
    $scope.addChapter = function () {
        $scope.canAdd = true;
    }

    ///添加章节输入框失去焦点
    $scope.onBlurAdd = function (title) {
        var newChapter = angular.copy($scope.chapter);
        newChapter.Title = title;

        chapterService.Chapter_ADD(newChapter, function (data) {
            if (data.d) {
                $scope.$parent.knowledge.chapters.push(data.d);
                $scope.title = '';
                $scope.canAdd = false;
            }
        });
    }

    $scope.chapterSelected = function (chapter) {
        $scope.chapterPick = chapter;
    }

    var resetOrder = function (baseChapter) {

    }

    $scope.moveLeft = function () {
        var chapter = $scope.chapterPick;
        if (chapter.Parent === 0) return;
        if (chapter.ChapterParent.ChapterID === 0) {
            ///二级节点左移，变为一级节点
            chapter.Parent = 0;
            chapter.Orde = chapter.ChapterParent.Orde + 1;
            chapter.ChapterParent = null;
            resetOrder(chapter);
        } else if (chapter.ChapterParent.ChapterParent) {
            chapter.Parent = chapter.ChapterParent.ChapterParent.ChapterID; 
        }
        chapterService.Chapter_Upd(chapter, function (data) {
        });
    }

    $scope.moveRight = function () {
        var chapter = $scope.chapterPick;
        if (chapter.ChapterParent && chapter.ChapterParent.ChapterParent) {
            chapter.Parent = chapter.ChapterParent.ChapterParent.ChapterID;
            chapterService.Chapter_Upd(chapter, function (data) {
            });
        }
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


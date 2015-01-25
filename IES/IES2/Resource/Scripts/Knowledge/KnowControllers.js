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

        $scope.$on('courseLoaded', function (event, course) {
            $scope.course = course;
            $scope.chapterModel.OCID = course.OCID;
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

    $scope.parentPicker = {};
    $scope.parentPicker.ChapterID = -1;

    $scope.childPicker = {};
    $scope.childPicker.ChapterID = -1;

    $scope.selection = null;

    $scope.parentSelected = function (chapter) {
        $scope.parentPicker = chapter;
        $scope.selection = chapter;
    }

    $scope.childSelected = function (chapter) {
        $scope.childPicker = chapter;
        $scope.selection = chapter;
    } 

    $scope.$on('willCourseChanged', function (event, course) {
        $scope.chapter.OCID = course.OCID;
        $scope.chapter.CourseID = course.CourseID;
    });

    $scope.$on('courseLoaded', function (event, course) {
        chapterService.Chapter_Get(function (data) {
            $scope.chapter = data.d;
            $scope.chapter.OCID = course.OCID;
            $scope.chapter.CourseID = course.CourseID;
        });
    });

    ///添加章节
    $scope.addChapter = function () {
        $scope.canAdd = true;
    }

    ///添加章节输入框失去焦点
    $scope.onBlurAdd = function (title) {
        var newChapter = angular.copy($scope.chapter);
        newChapter.Title = title;

        chapterService.Chapter_ADD($scope.ocChapters, newChapter, function (data) {
            if (data.d) {
                $scope.$parent.knowledge.chapters.push(data.d);
                $scope.title = '';
                $scope.canAdd = false;
            }
        });
    }
    ///章节移动成功
    var moveSuccess = function (data) {
        if (data.d) {
            $scope.ocChapters = data.d;
        }
    }

    $scope.move = function (direction) {
        if (!$scope.selection) return;
        switch (direction) {
            case 1:
                chapterService.MoveLeft($scope.ocChapters, $scope.selection, moveSuccess);
                break;
            case 2:
                chapterService.MoveRight($scope.ocChapters, $scope.selection, moveSuccess);
                break;
            case 3:
                chapterService.MoveUp($scope.ocChapters, $scope.selection, moveSuccess);
                break;
            default:
                chapterService.MoveDown($scope.ocChapters, $scope.selection, moveSuccess);
                break;
        }
    }

    $scope.delete = function () {
        if ($scope.selection) {
            chapterService.Chapter_Del($scope.selection, function (data) {
                if (data.d === true) {
                    var length = $scope.ocChapters.length;
                    for (var i = 0; i < length; i++) {
                        if ($scope.ocChapters[i].ChapterID === $scope.selection.ChapterID) {
                            $scope.ocChapters.splice(i, 1);
                            break;
                        }
                    }
                }
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


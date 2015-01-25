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

        $scope.course = {};
        $scope.chapterModel = {};
        $scope.knowledgeModel = {};

        $scope.ocChapters = [];
        $scope.ocKnowledges = [];

        chapterService.Chapter_Get(function (data) {
            $scope.chapterModel = data.d;
        });

        knowledgeService.Ken_Get(function (data) {
            $scope.knowledgeModel = data.d;
            $scope.knowledgeModel.UpdateTime = new Date();
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

        var loadKnowledges = function (model) {
            if (model) {
                knowledgeService.Ken_List(model, function (data) {
                    $scope.ocKnowledges = data.d;
                    $scope.chapter.knowledges = data.d;
                    console.log($scope.chapter.knowledges);
                    if ($scope.chapter.knowledges.length > 0) {
                        $scope.chapter.knowledge = $scope.chapter.knowledges[0];
                    }
                });
            }
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            $scope.chapterModel.OCID = $scope.course.OCID;
            $scope.knowledgeModel.OCID = $scope.course.OCID;
            loadChapters($scope.chapterModel);
            loadKnowledges($scope.knowledgeModel);
        });

        $scope.$on('courseLoaded', function (event, course) {
            $scope.course = course;
            $scope.chapterModel.OCID = $scope.course.OCID;
            $scope.knowledgeModel.OCID = $scope.course.OCID;
            loadChapters($scope.chapterModel);
            loadKnowledges($scope.knowledgeModel);
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
            knowledgeService.Ken_Get(function (data) {
                var ken = data.d;
                ken.OCID = $scope.course.OCID; 
                ken.CourseID = $scope.course.CourseID; 
                ken.ChapterID = $scope.knowledge.chapter.ChapterID; 
                ken.Name = $scope.knowledge.name;
                ken.UpdateTime = new Date();
                knowledgeService.Ken_ADD(ken, function (data) {
                    $scope.$broadcast('knowledgeChanged', $scope.knowledge);
                });
            });
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

    $scope.$on('chapterChanged', function (event, chapters) {
        $scope.knowledge.chapters = chapters;
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

    var findById = function (chapterId) {
        var length = $scope.ocChapters.length;
        for (var i = 0; i < length; i++) {
            if ($scope.ocChapters[i].ChapterID === chapterId) {
                return $scope.ocChapters[i];
            }
        }
    }

    $scope.move = function (direction) {
        if (!$scope.selection) return;
        switch (direction) {
            case 1:
                chapterService.MoveLeft($scope.ocChapters, $scope.selection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.selection = findById($scope.selection.ChapterID);
                    $scope.childPicker = $scope.selection;
                    $scope.parentPicker = findById($scope.selection.ParentID);
                });
                break;
            case 2:
                chapterService.MoveRight($scope.ocChapters, $scope.selection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.selection = findById($scope.selection.ChapterID);
                    $scope.childPicker = $scope.selection;
                    $scope.parentPicker = findById($scope.selection.ParentID);
                });
                break;
            case 3:
                chapterService.MoveUp($scope.ocChapters, $scope.selection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.selection = findById($scope.selection.ChapterID);
                });
                break;
            default:
                chapterService.MoveDown($scope.ocChapters, $scope.selection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.selection = findById($scope.selection.ChapterID);
                });
                break;
        }
        $scope.$emit('chapterChanged', $scope.ocChapters);
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


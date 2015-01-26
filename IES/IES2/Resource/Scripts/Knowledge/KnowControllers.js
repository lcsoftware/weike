'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.knowledge.services',
    'app.resken.services',
    'app.assist.services',
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', 'knowledgeService', 'chapterService', 'assistService', 'resourceKenService',
    function ($scope, contentService, knowledgeService, chapterService, assistService, resourceKenService) {

        $scope.$emit('willResetCourse');

        $scope.course = {};

        $scope.ocChapters = [];
        $scope.ocKnowledges = [];
        $scope.requireMent = [];
        $scope.requireMents = {};

        $scope.selectKnowledge = {};

        assistService.Resource_Dict_Requirement_Get(function (data) {
            if (data.d.length > 0) {
                $scope.requireMents = data.d;
                $scope.requireMent = $scope.requireMents[0];
                console.log($scope.requireMents);
            }
        });

        var loadChapters = function (course) {
            var model = {};
            model.OCID = course.OCID;
            model.CouseID = course.CourseID;
            chapterService.Chapter_List(model, function (data) {
                $scope.ocChapters = data.d;
                if ($scope.ocChapters.length > 0) {
                    $scope.aKnowledge.chapter = $scope.ocChapters[0];
                }
            });
        }

        var loadKnowledges = function (course) { 
            var model = {};
            model.OCID = course.OCID;
            model.CouseID = course.CourseID; 
            knowledgeService.Ken_List(model, function (data) {
                $scope.ocKnowledges = data.d;
                $scope.aChapter.knowledges = data.d;
                if ($scope.ocKnowledges.length > 0) {
                    $scope.aChapter.knowledge = $scope.ocKnowledges[0];
                }
            });
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            loadChapters($scope.course);
            loadKnowledges($scope.course);
        });

        $scope.$on('courseLoaded', function (event, course) {
            $scope.course = course;
            loadChapters($scope.course);
            loadKnowledges($scope.course);
        });


        /// 添加知识点 
        $scope.aKnowledge = {};
        $scope.aKnowledge.name = '';
        $scope.aKnowledge.chapter = {};

        var ResourceKenAdd = function (ken) {
            var resKen = {
                ResourceID: ken.ChapterID,
                KenID: ken.KenID,
                Source: 'Chapter'
            };
            resourceKenService.ResourceKen_ADD(resKen);
        }

        var knowledgeSave = function (knowledge, callback) {
            var ken = {};
            ken.OCID = $scope.course.OCID;
            ken.CourseID = $scope.course.CourseID;
            ken.ChapterID = knowledge.chapter.ChapterID;
            ken.Requirement = $scope.requireMent.id;
            ken.Name = knowledge.name;
            ken.UpdateTime = new Date();
            knowledgeService.Ken_ADD(ken, function (data) {
                $scope.ocKnowledges.push(data.d); 
                var kenEntry = {
                    ChapterID: data.d.ChapterID,
                    KenID: data.d.KenID,
                    
                }
                ResourceKenAdd(kenEntry)
                if (callback) callback();
            });
        }

        $scope.aKnowledge.save = function (knowledge) {
            knowledgeSave(knowledge);
        }

        $scope.aKnowledge.saveNew = function (knowledge) {
            knowledgeSave(knowledge, function () {
                $scope.aKnowledge.name = '';
            });
        }

        $scope.aKnowledge.cancel = function () {
            $scope.aKnowledge = {};
            $scope.aKnowledge.name = '';
            $scope.aKnowledge.chapter = {};
        }

        /// end 添加知识点

        ///添加章节
        $scope.aChapter = {};
        $scope.aChapter.name = '';
        $scope.aChapter.knowledge = {};

        var chapterSave = function (chapter, callback)
        {
            var newChapter = {};
            newChapter.OCID = $scope.course.OCID;
            newChapter.CourseID = $scope.course.CourseID;
            newChapter.Title = chapter.name;
            chapterService.save(newChapter, function (data) {
                $scope.ocChapters.push(data.d);
                var resourceKen = {
                    ChapterID: data.d.ChapterID,
                    KenID: $scope.aChapter.knowledge.KenID
                }
                ResourceKenAdd(resourceKen);
                if (callback) callback();
            });
        }

        $scope.aChapter.save = function (chapter) {
            chapterSave(chapter);
        }
        $scope.aChapter.saveNew = function (chapter) {
            chapterSave(chapter, function () {
                $scope.aChapter.name = '';
            });
        }

        $scope.aChapter.cancel = function () {
            $scope.aChapter = {};
            $scope.aChapter.name = '';
            $scope.aChapter.knowledge = {};
        }
        /// end 添加章节 
    }]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'chapterService', function ($scope, chapterService) {

    $scope.canAdd = false;

    $scope.parentPicker = {};
    $scope.parentPicker.ChapterID = -1;

    $scope.childPicker = {};
    $scope.childPicker.ChapterID = -1;

    $scope.chapterSelection = null;
    $scope.knowSelection = { KenID: 0 };

    $scope.parentSelected = function (chapter) {
        $scope.parentPicker = chapter;
        $scope.chapterSelection = chapter;
        showAssociation($scope.chapterSelection, $scope.knowSelection);
    }

    $scope.childSelected = function (chapter) {
        $scope.childPicker = chapter;
        $scope.chapterSelection = chapter;
        showAssociation($scope.chapterSelection, $scope.knowSelection);
    } 

    $scope.knowSelected = function (knowledge) {
        $scope.knowSelection = knowledge;
        showAssociation($scope.chapterSelection, $scope.knowSelection);
    }

    ///显示关联内容
    var showAssociation = function (chapter, knowledge) {

    }

    ///添加章节
    $scope.addChapter = function () {
        $scope.canAdd = true;
    }

    ///添加章节输入框失去焦点
    $scope.onBlurAdd = function (title) {
        var newChapter = {};
        newChapter.OCID = $scope.course.OCID;
        newChapter.CourseID = $scope.course.CourseID;
        newChapter.Title = title;

        chapterService.Chapter_ADD($scope.ocChapters, newChapter, function (data) {
            if (data.d) {
                $scope.ocChapters.push(data.d);
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
        if (!$scope.chapterSelection) return;
        switch (direction) {
            case 1:
                chapterService.MoveLeft($scope.ocChapters, $scope.chapterSelection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.chapterSelection = findById($scope.chapterSelection.ChapterID);
                    $scope.childPicker = $scope.chapterSelection;
                    $scope.parentPicker = findById($scope.chapterSelection.ParentID);
                });
                break;
            case 2:
                chapterService.MoveRight($scope.ocChapters, $scope.chapterSelection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.chapterSelection = findById($scope.chapterSelection.ChapterID);
                    $scope.childPicker = $scope.chapterSelection;
                    $scope.parentPicker = findById($scope.chapterSelection.ParentID);
                });
                break;
            case 3:
                chapterService.MoveUp($scope.ocChapters, $scope.chapterSelection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.chapterSelection = findById($scope.chapterSelection.ChapterID);
                });
                break;
            default:
                chapterService.MoveDown($scope.ocChapters, $scope.chapterSelection, function (data) {
                    $scope.ocChapters = data.d;
                    $scope.chapterSelection = findById($scope.chapterSelection.ChapterID);
                });
                break;
        }
        $scope.$emit('chapterChanged', $scope.ocChapters);
    }

    $scope.delete = function () {
        if ($scope.chapterSelection) {
            chapterService.Chapter_Del($scope.chapterSelection, function (data) {
                if (data.d === true) {
                    var length = $scope.ocChapters.length;
                    for (var i = 0; i < length; i++) {
                        if ($scope.ocChapters[i].ChapterID === $scope.chapterSelection.ChapterID) {
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


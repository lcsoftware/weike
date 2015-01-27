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
        $scope.resourceKens = [];

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

        var loadResourceKens = function (course) {
            resourceKenService.ResourceKen_List_OCID(course.OCID, function (data) {
                $scope.resourceKens = data.d;
            });
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            loadChapters($scope.course);
            loadKnowledges($scope.course);
            loadResourceKens($scope.course);
        });

        $scope.$on('courseLoaded', function (event, course) {
            $scope.course = course;
            loadChapters($scope.course);
            loadKnowledges($scope.course);
            loadResourceKens($scope.course);
        });


        /// 添加知识点 
        $scope.aKnowledge = {};
        $scope.aKnowledge.name = '';
        $scope.aKnowledge.chapter = {};

        var ResourceKenAdd = function (chapterId, kenId) {
            var resKen = {
                ResourceID: chapterId,
                KenID: kenId,
                Source: 'Chapter'
            };
            resourceKenService.ResourceKen_ADD(resKen, function (data) {
                $scope.resourceKens.push(data.d);
                $scope.$broadcast('knowledgeChanged', data.d.KenID);
            });
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
                ResourceKenAdd(data.d.ChapterID, data.d.KenID)
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

        var chapterSave = function (chapter, callback) {
            var newChapter = {};
            newChapter.OCID = $scope.course.OCID;
            newChapter.CourseID = $scope.course.CourseID;
            newChapter.Title = chapter.name;
            chapterService.save(newChapter, function (data) {
                $scope.ocChapters.push(data.d);
                ResourceKenAdd(data.d.ChapterID, $scope.aChapter.knowledge.KenID)
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


        $scope.parentPicker = {};
        $scope.parentPicker.ChapterID = -1;

        $scope.childPicker = {};
        $scope.childPicker.ChapterID = -1;

        $scope.chapterSelection = null;
        $scope.knowSelection = { KenID: 0 };

        $scope.parentSelected = function (chapter) {
            $scope.parentPicker = chapter;
            $scope.chapterSelection = chapter;
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
        }

        $scope.childSelected = function (chapter) {
            $scope.childPicker = chapter;
            $scope.chapterSelection = chapter;
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
        }

        $scope.knowSelected = function (knowledge) {
            $scope.$broadcast('knowSelected', knowledge);
            $scope.knowSelection = knowledge; 
            $scope.$broadcast('knowledgeChanged', $scope.knowSelection.KenID);
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
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


        $scope.chapterFiles = [];
        $scope.chapterExercises = [];
        ///显示关联内容
        $scope.showAssociation = function (chapter, knowledge) {
            var chapterId = chapter ? chapter.ChapterID : 0;
            var createUserId = chapter ? chapter.CreateUserID : 0;
            var kenId = knowledge ? knowledge.KenID : 0;
            switch ($scope.association.selection) {
                case 1:
                    chapterService.Chapter_File_List(chapterId, createUserId, kenId, function (data) {
                        $scope.chapterFiles = data.d;
                    });
                    break;
                case 2:
                    chapterService.Chapter_File_List(chapterId, createUserId, kenId, function (data) {
                        $scope.chapterExercises = data.d;
                    });
                    break;
                default:
                    break;
            }
        }

    }]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'chapterService', function ($scope, chapterService) {

    $scope.canAdd = false;

    $scope.knowNoLimit = function () {
        $scope.knowSelection = { KenID: 0 };
        $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
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

}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'resourceKenService', function ($scope, resourceKenService) {

    $scope.knowChapters = [];

    $scope.$on('knowledgeChanged', function (event, kenId) {
        $scope.knowChapters.length = 0;
        var len1 = $scope.resourceKens.length;
        for (var i = 0; i < len1; i++) {
            if ($scope.resourceKens[i].KenID === kenId) {
                var length = $scope.ocChapters.length;
                for (var j = 0; j < length; j++) {
                    if ($scope.ocChapters[j].ChapterID === $scope.resourceKens[i].ResourceID)
                    {
                        $scope.knowChapters.push($scope.ocChapters[j]);
                        break;
                    }
                } 
            }
        }
    });

}]);


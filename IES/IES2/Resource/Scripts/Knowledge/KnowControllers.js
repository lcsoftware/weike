'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.knowledge.services',
    'app.resken.services',
    'app.assist.services',
]);

appKnow.controller('KnowledgeCtrl', ['$scope', '$state', 'contentService', 'knowledgeService', 'chapterService', 'assistService', 'resourceKenService',
    function ($scope, $state, contentService, knowledgeService, chapterService, assistService, resourceKenService) {

        $scope.$emit('willResetCourse');

        $scope.course = {};

        $scope.chapters = [];
        $scope.kens = [];

        $scope.requireMent = [];
        $scope.requireMents = {};

        $scope.resourceKens = [];

        $scope.kenSelection = {};

        ///按章节查询，同时显示知识点查询条件 
        $scope.kenDisable = true;

        assistService.Resource_Dict_Requirement_Get(function (data) {
            if (data.d.length > 0) {
                $scope.requireMents = data.d;
                $scope.requireMent = $scope.requireMents[0];
            }
        });

        var loadChapters = function (course) {
            var model = {};
            model.OCID = course.OCID;
            model.CouseID = course.CourseID;
            chapterService.Chapter_List(model, function (data) {
                $scope.chapters = data.d;
                if ($scope.chapters.length > 0) {
                    $scope.aKnowledge.chapter = $scope.chapters[0];
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
                if (data.d) {
                    $scope.resourceKens = data.d;
                }
            });
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            loadChapters($scope.course);
            loadKnowledges($scope.course);
            loadResourceKens($scope.course);
            $scope.$broadcast('knowledgeChanged', -1);
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

        ///习题共享 TODO
        $scope.shareExercise = function (exercise) {

        }
        /// <summary>
        /// 1判断题 ; 2单选题 ; 3 多选题 4填空题（客观）5填空题 ; 6连线题 ;7 排序题 ; 8分析题  9计算题   10问答题 ;
        ///11 翻译题  12听力训练  13写作  14阅读理解  15论述题 ;16 答题卡题型  17自定义题型
        /// </summary>
        ///编辑习题
        $scope.editExercise = function (exercise) {
            var param = { ExerciseID: exercise.ExerciseID };
            switch (exercise.ExerciseType) {
                case '18': //简答题
                    $state.go('exercise.shortanswer', param)
                    break;
                case '19': //名词解释
                    $state.go('exercise.noun', param)
                    break;
                case '12': //听力题
                    $state.go('exercise.listening', param)
                    break;
                case '10': //问答题
                    $state.go('exercise.quesanswer', param)
                    break;
                case '1': //判断题
                    $state.go('exercise.truefalse', param)
                    break;
                case '5': //填空题
                    $state.go('exercise.fillblank', param)
                    break;
                case '4': //填空客观题
                    $state.go('exercise.fillblank2', param)
                    break;
                case '6':  //连线题
                    $state.go('exercise.connection', param)
                    break;
                default:
                    break;
            }
        }

        ///习题删除
        $scope.deleteExercise = function (exercise) {
            var model = {
                KenID: $scope.knowSelection.KenID,
                ResourceID: exercise.ExerciseID,
                Source: 'Exercise'
            }
            resourceKenService.ResourceKen_Del(model, function (data) {
                if (data.d === true) {
                    var length = $scope.chapterExercises.length;
                    for (var i = 0; i < length; i++) {
                        if ($scope.chapterExercises[i].ExerciseID === exercise.ExerciseID) {
                            $scope.chapterExercises.splice(i, 1);
                            break;
                        }
                    }
                }
            });
        }

        $scope.parentPicker = {};
        $scope.parentPicker.ChapterID = -1;

        $scope.childPicker = {};
        $scope.childPicker.ChapterID = -1;

        $scope.chapterSelection = null;
        $scope.knowSelection = { KenID: 0 };

        $scope.parentChapterSelected = function (chapter) {
            $scope.parentPicker = chapter;
            $scope.chapterSelection = chapter;
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
        }

        $scope.childChapterSelected = function (chapter) {
            $scope.childPicker = chapter;
            $scope.chapterSelection = chapter;
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
        }

        $scope.chapterSelected = function (chapter) {
            $scope.chapterSelection = chapter;
            $scope.showAssociation($scope.chapterSelection, $scope.knowSelection);
            console.log($scope.chapterSelection);
        }

        $scope.knowSelected = function (knowledge) {
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

    $scope.$parent.knowHide = true;
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
        if (!$scope.selection) return;
        chapterService.Chapter_Move($scope.selection, direction, function (data) {
            if (data.d) {
                $scope.ocChapters = data.d;
            }
        });
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

    $scope.$parent.knowHide = false;
    $scope.knowChapters = [];

    $scope.$on('knowledgeChanged', function (event, kenId) {
        //if ($scope.knowSelection.KenID !== kenId) return;
        $scope.knowChapters.length = 0;
        var len1 = $scope.resourceKens.length;
        for (var i = 0; i < len1; i++) {
            if ($scope.resourceKens[i].KenID === kenId) {
                var length = $scope.ocChapters.length;
                for (var j = 0; j < length; j++) {
                    if ($scope.ocChapters[j].ChapterID === $scope.resourceKens[i].ResourceID) {
                        $scope.knowChapters.push($scope.ocChapters[j]);
                        break;
                    }
                }
            }
        }
    });

}]);


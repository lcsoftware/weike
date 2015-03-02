'use strict';

var appKnow = angular.module('app.ken.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.ken.services',
    'app.resken.services',
    'app.assist.services',
    'app.common.services'
]);

appKnow.controller('KenCtrl', ['$scope', '$state', '$stateParams', 'freezeService', 'tagService', 'contentService', 'kenService', 'chapterService', 'assistService', 'resourceKenService', 'exerciseService',
    function ($scope, $state, $stateParams, freezeService, tagService, contentService, kenService, chapterService, assistService, resourceKenService, exerciseService) {

        $scope.$emit('onWizardSwitch', true);

        $scope.$emit('willResetCourse');

        $scope.course = {};

        $scope.chapters = [];
        $scope.kens = [];
        ///用于添加章节下拉列表
        $scope.ocKens = [];
        $scope.kenSelection = {};
        $scope.resourceKens = [];
        $scope.shareRanges = [];

        $scope.requireMent = [];
        $scope.requireMents = {};

        ///按章节查询，同时显示知识点查询条件 
        $scope.kenDisable = true;

        ///关联 资料 习题 话题
        $scope.tab = 1;

        $scope.kenSelected = function (item) {
            $scope.kenSelection = item;
            $scope.$broadcast('requestQuery', $scope.kenSelection);
        }

        assistService.Resource_Dict_Requirement_Get(function (data) {
            if (data && data.length > 0) {
                $scope.requireMents = data;
                $scope.requireMent = $scope.requireMents[0];
            }
        });

        assistService.Resource_Dict_ShareRange_Get(function (data) {
            if (data) {
                $scope.shareRanges = angular.copy(data);
                var item = angular.copy($scope.shareRanges[0]);
                item.id = 0;
                item.name = '不限';
                $scope.shareRanges.insert(0, item);
            }
        });

        $scope.linkFiles = [];

        $scope.linkExercises = [];


        ///子页面请求查询数据
        $scope.$on('onRequestQuery', function (event, chapter, ken, from) {
            if (chapter.ChapterID && ken.KenID >= 0) {
                ken.UpdateTime = new Date();
                $scope.linkFiles.length = 0;
                $scope.linkExercises.length = 0;
                switch ($scope.tab) {
                    case 1:
                        if (from === 'fromChapter') {
                            chapterService.File_ChapterID_KenID_List(chapter, ken, function (data) {
                                $scope.linkFiles = data.d;
                            });
                        } else {
                            kenService.File_KenID_ChapterID_List(chapter, ken, function (data) {
                                $scope.linkFiles = data.d;
                            });
                        }
                        break;
                    case 2:
                        if (from === 'fromChapter') {
                            chapterService.Exercise_ChapterID_KenID_List(chapter, ken, function (data) {
                                $scope.linkExercises = data.d;
                            });
                        } else {
                            kenService.Exercise_KenID_ChapterID_List(chapter, ken, function (data) {
                                $scope.linkExercises = data.d;
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
        });

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.course = course;
            $scope.loadStart($scope.course);
        }); 

        $scope.$on('courseLoaded', function (event, course) {
            var freezeData = freezeService.getFreeze(tagService.KenListTag);
            if (freezeData) {
                $scope.course = freezeData.data.course;
                $scope.tab = freezeData.data.tab;
                $scope.$parent.course = $scope.course;
            } else {
                $scope.course = course;
            } 
        });


        $scope.tabChange = function (tab) {
            $scope.tab = tab;
            ///广播消息，通知子页面发起数据请求
            $scope.$broadcast('requestQuery', $scope.kenSelection);
        }

        ///保存ResourceKen 关联数据
        var ResourceKenAdd = function (chapterId, kenId) {
            var resKen = {
                ResourceID: chapterId,
                KenID: kenId,
                Source: 'Chapter'
            };
            resourceKenService.ResourceKen_ADD(resKen, function (data) {
                $scope.resourceKens.push(data.d);
            });
        }

        /// 添加知识点 
        $scope.ken = {};
        $scope.ken.name = '';
        $scope.ken.chapter = {};
        $scope.kenSave = function (ken, keeping) {
            var newData = {
                OCID: $scope.course.OCID,
                CourseID: $scope.course.CourseID,
                name: $scope.ken.name,
                ChapterID: $scope.ken.chapter.ChapterID,
                Requirement: $scope.requireMent.id,
                UpdateTime: new Date()
            };
            kenService.Ken_ADD(newData, function (data) {
                var resultKen = data.d;
                $scope.ocKens.push(resultKen);
                ResourceKenAdd(resultKen.ChapterID, resultKen.KenID);
                if (keeping) $scope.ken.name = '';
            });
        }

        /// end 添加知识点

        ///添加章节
        $scope.chapter = {};
        $scope.chapter.name = '';
        $scope.chapter.ken = {};

        $scope.chapterSave = function (chapter, keeping) {
            var newData = {
                OCID: $scope.course.OCID,
                CourseID: $scope.course.CourseID,
                name: chapter.ken.name,
                Title: chapter.name
            }
            chapterService.Chapter_ADD(newData, function (data) {
                var resultChapter = data.d;
                $scope.chapters.push(resultChapter);
                ResourceKenAdd(resultChapter.ChapterID, chapter.ken.KenID)
                if (keeping) $scope.chapter.name = '';
            });
        }
        /// end 添加章节 


        ///习题共享 TODO 
        $scope.$on('onShareExercise', function (event, exercise, range) {
            var ids = exercise.ExerciseID;
            exerciseService.Exercise_Batch_ShareRange(ids, range.id, function (data) {
                if (data.d === true) {
                    //filterChanged();
                } else {
                    ///TODO 统一提示框 加美化效果
                    alert('共享操作失败！');
                }
            });
        });
        ///显示删除提示框
        $scope.$on('onDeleteExercise', function (event, exercise) {
            $scope.exerciseItemDelete = exercise;
            $('#eConfirm').show();
        });

        $scope.exerciseItemDelete = {};
        ///确认删除
        $scope.$on('onDialogOk', function (event) {
            exerciseService.Exercise_Del($scope.exerciseItemDelete.ExerciseID, function (data) {
                var length = $scope.exercises.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.exercises[i].ExerciseID == $scope.exerciseItemDelete.ExerciseID) {
                        $scope.exercises.splice(i, 1);
                        break;
                    }
                }
            });
        });
     
        $scope.$on('onEditExercise', function (event, exercise) { 
            ///消息转发给子页面，由子页面来处理
            $scope.$broadcast('onKenExerciseEdit', exercise); 
        });

        /// <summary>
        /// 1判断题 ; 2单选题 ; 3 多选题 4填空题（客观）5填空题 ; 6连线题 ;7 排序题 ; 8分析题  9计算题   10问答题 ;
        ///11 翻译题  12听力训练  13写作  14阅读理解  15论述题 ;16 答题卡题型  17自定义题型
        /// </summary>
        ///编辑习题
        $scope.navEditExercise = function (exercise) {
            var param = { ocid: $scope.course.OCID, ExerciseID: exercise.ExerciseID };
            switch (exercise.ExerciseType) {
                case 18: //简答题
                    $state.go('exercise.shortanswer', param)
                    break;
                case 4: //名词解释
                    $state.go('exercise.noun', param)
                    break;
                case 12: //听力题
                    $state.go('exercise.listening', param)
                    break;
                case 17: //自定义题
                    $state.go('exercise.custom', param)
                    break;
                case 10: //问答题
                    $state.go('exercise.quesanswer', param)
                    break;
                case 13: //写作题
                    $state.go('exercise.quesanswer', param)
                    break;
                case 1: //判断题
                    $state.go('exercise.truefalse', param)
                    break;
                case 5: //填空题
                    $state.go('exercise.fillblank', param)
                    break;
                    //case 4: //填空客观题
                    //    $state.go('exercise.fillblank2', param)
                    //    break;
                case 6:  //连线题
                    $state.go('exercise.connection', param)
                    break;
                case 2: //单选题
                    $state.go('exercise.radio', param)
                    break;
                case 3: //多选题
                    $state.go('exercise.multiple', param)
                    break;
                case 11: //翻译题
                    $state.go('exercise.translation', param)
                    break;
                case 7: //排序题
                    $state.go('exercise.sorting', param)
                    break;
                case 8: //分析题
                    $state.go('exercise.analysis', param)
                    break;
                case 9: //计算题
                    $state.go('exercise.analysis', param)
                    break;
                case 14: //阅读理解题
                    $state.go('exercise.reading', param)
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

        ///初始化加载数据
        $scope.loadStart = function (course) {
            chapterService.Chapter_List({ OCID: course.OCID }, function (data) {
                $scope.chapters = data.d;
                if ($scope.chapters.length > 0) {
                    $scope.ken.chapter = $scope.chapters[0];
                }
            });
            kenService.Ken_List({ OCID: course.OCID }, function (data) {
                $scope.ocKens = angular.copy(data.d);
                if ($scope.ocKens.length > 0) {
                    $scope.chapter.ken = $scope.ocKens[0];
                }
            });
        }
    }]);

appKnow.controller('KenChapterCtrl', ['$scope', '$state', 'chapterService', 'kenService', 'tagService', 'freezeService',
    function ($scope, $state, chapterService, kenService, tagService, freezeService) {

        //$scope.$emit('willResetCourse', 'Ken');

        $scope.$parent.kenDisable = true;
        $scope.canAdd = false;
        $scope.enableEdit = false;
        $scope.title = '';

        $scope.lastSelection = {};
        $scope.parentChapter = {};
        $scope.childChpater = {};

        $scope.$on('courseLoaded', function (event, course) {
            var freezeData = freezeService.getFreeze(tagService.KenListTag);
            if (freezeData) {
                $scope.parentChapter = freezeData.data.parentChapter; 
                $scope.childChpater = freezeData.data.childChpater;
                $scope.lastSelection = freezeData.data.lastSelection;
                freezeService.unFreeze(tagService.KenListTag);
            } 
            $scope.loadStart($scope.course);
        });

        $scope.$on('onKenExerciseEdit', function (event, exercise) { 
            freezeService.freeze(tagService.UrlSourceTag, $state.current.name);
            freezeService.freeze(tagService.KenListTag, {
                course: $scope.course,
                tab: $scope.tab,
                parentChpater: $scope.parentChpater,
                childChapter: $scope.childChapter,
                lastSelection: $scope.lastSelection
            });
            $scope.navEditExercise(exercise); 
        });

        ///添加章节
        $scope.addChapter = function () {
            $scope.canAdd = true;
        }

        ///添加章节输入框失去焦点
        $scope.onBlur = function (title) {
            var newChapter = {
                OCID: $scope.course.OCID,
                CourseID: $scope.course.OCID,
                Title: title
            };
            chapterService.Chapter_ADD(newChapter, function (data) {
                if (data.d) {
                    $scope.$parent.chapters.push(data.d);
                    $scope.title = '';
                    $scope.canAdd = false;
                }
            });
        }

        $scope.chapterDblclick = function (chapter) {
            $scope.lastSelection = chapter;
            $scope.enableEdit = true;
        }

        $scope.onEdit = function (chapter) {
            $scope.enableEdit = false;
            chapterService.Chapter_Upd(chapter);
        }

        var Ken_FileFilter_ChapterID_List = function (chapter) {
            $scope.$parent.kens.length = 0;
            kenService.Ken_FileFilter_ChapterID_List(chapter, function (data) {
                $scope.$parent.kens = data.d;
                if ($scope.$parent.kens.length > 0) {
                    $scope.$parent.kenSelection = angular.copy($scope.chapter.ken);
                }
                $scope.$parent.kenSelection.KenID = 0;
                $scope.$parent.kenSelection.Name = '全部';
                $scope.$parent.kens.insert(0, $scope.kenSelection);
                $scope.$emit('onRequestQuery', $scope.lastSelection, $scope.kenSelection, 'fromChapter');
            });
        }

        var Ken_ExerciseFilter_ChapterID_List = function (chapter) {
            $scope.$parent.kens.length = 0;
            kenService.Ken_ExerciseFilter_ChapterID_List(chapter, function (data) {
                $scope.$parent.kens = data.d;
                if ($scope.$parent.kens.length > 0) {
                    $scope.$parent.kenSelection = angular.copy($scope.chapter.ken);
                }
                $scope.$parent.kenSelection.KenID = 0;
                $scope.$parent.kenSelection.Name = '全部';
                $scope.$parent.kens.insert(0, $scope.kenSelection);
                $scope.$emit('onRequestQuery', $scope.lastSelection, $scope.kenSelection, 'fromChapter');
            });
        }

        $scope.parentFocus = function (item) {
            $scope.parentChpater = item;
            $scope.lastSelection = item;
            switch ($scope.tab) {
                case 1:
                    Ken_FileFilter_ChapterID_List(item);
                    break;
                default:
                    Ken_ExerciseFilter_ChapterID_List(item);
                    break;
            }
        }

        $scope.childFocus = function (item) {
            $scope.childChpater = item;
            $scope.lastSelection = item;
            switch ($scope.tab) {
                case 1:
                    Ken_FileFilter_ChapterID_List(item);
                    break;
                default:
                    Ken_ExerciseFilter_ChapterID_List(item);
                    break;
            }
        }

        $scope.$watch('tab', function (tab) {
            switch (tab) {
                case 1:
                    Ken_FileFilter_ChapterID_List($scope.lastSelection);
                    break;
                default:
                    Ken_ExerciseFilter_ChapterID_List($scope.lastSelection);
                    break;
            }
        });


        /// 知识点被选中, Tab页面切换， 请求查询数据
        $scope.$on('requestQuery', function (event, ken) {
            $scope.$emit('onRequestQuery', $scope.lastSelection, ken, 'fromChapter');
        });

        $scope.move = function (direction) {
            if (!$scope.lastSelection) return;
            chapterService.Chapter_Move($scope.lastSelection, direction, function (data) {
                if (data.d) {
                    $scope.$parent.chapters = data.d;
                }
            });
        }

        $scope.delete = function () {
            if (!$scope.lastSelection.ChapterID) return;
            chapterService.Chapter_Del($scope.lastSelection, function (data) {
                if (data.d == true) {
                    var length = $scope.$parent.chapters.length;
                    for (var i = 0; i < length; i++) {
                        if ($scope.$parent.chapters[i].ChapterID === $scope.lastSelection.ChapterID) {
                            $scope.$parent.chapters.splice(i, 1);
                            break;
                        }
                    }
                }
            });
        }
    }]);

appKnow.controller('KenTopicCtrl', ['$scope', '$state', 'resourceKenService', 'chapterService', 'kenService', 'tagService', 'freezeService',
    function ($scope, $state, resourceKenService, chapterService, kenService, tagService, freezeService) {

        $scope.$parent.kenDisable = false;

        $scope.$parent.linkExercises.length = 0;
        $scope.$parent.linkFiles.length = 0;

        $scope.dataKen = {};
        $scope.dataChapter = {}; 

        $scope.$on('courseLoaded', function (event, course) {
            var freezeData = freezeService.getFreeze(tagService.KenListTag);
            if (freezeData) {
                $scope.dataKen = freezeData.data.dataKen;
                $scope.dataChapter = freezeData.data.dataChapter;
                $scope.$parent.dataChapters = freezeData.data.dataChapters;
                freezeService.unFreeze(tagService.KenListTag);
            }
            $scope.loadStart($scope.course);
        });

        $scope.$on('onKenExerciseEdit', function (event, exercise) {
            freezeService.freeze(tagService.UrlSourceTag, $state.current.name);
            freezeService.freeze(tagService.KenListTag, {
                course: $scope.course,
                tab: $scope.tab,
                dataKen: $scope.dataKen,
                dataChapter: $scope.dataChapter,
                dataChapters: $scope.dataChapters
            });
            $scope.navEditExercise(exercise);
        });

        $scope.$watch('dataKen', function (v) {
            $scope.know = v.Requirement === 1;
        });

        $scope.kenFocus = function (item) {
            $scope.dataKen = item;
            kenService.Chapter_KenID_List({ KenID: item.KenID, OCID: $scope.course.OCID }, function (data) {
                $scope.dataChapters = data.d;
                if ($scope.dataChapters.length > 0) {
                    $scope.dataChapter = angular.copy($scope.dataChapters[0]);
                    $scope.dataChapter.ChapterID = 0;
                    $scope.dataChapter.Title = '全部';
                    $scope.dataChapters.insert(0, $scope.dataChapter);
                    $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen, 'fromKen');
                }
            });
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.dataChapters.length = 0;
        });

        $scope.chapterFocus = function (item) {
            $scope.dataChapter = item;
            $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen, 'fromKen');
        }

        /// 知识点被选中, Tab页面切换， 请求查询数据
        $scope.$on('requestQuery', function (event, ken) {
            $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen, 'fromKen');
        });
    }]);


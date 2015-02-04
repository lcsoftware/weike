'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services',
    'app.chapter.services',
    'app.ken.services',
    'app.resken.services',
    'app.assist.services',
]);

appKnow.controller('KenCtrl', ['$scope', '$state', 'contentService', 'kenService', 'chapterService', 'assistService', 'resourceKenService',
    function ($scope, $state, contentService, kenService, chapterService, assistService, resourceKenService) {

        $scope.$emit('willResetCourse');

        $scope.course = {};

        $scope.chapters = [];
        $scope.kens = [];
        ///用于添加章节下拉列表
        $scope.ocKens = [];
        $scope.kenSelection = {};
        $scope.resourceKens = [];

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
            if (data.d && data.d.length > 0) {
                $scope.requireMents = data.d;
                $scope.requireMent = $scope.requireMents[0];
            }
        });

        $scope.linkFiles = [];

        $scope.linkExercises = [];


        ///子页面请求查询数据
        $scope.$on('onRequestQuery', function (event, chapter, ken) {
            if (chapter.ChapterID && ken.KenID) {
                switch ($scope.tab) {
                    case 1:
                        chapterService.Chapter_File_List(chapter.ChapterID, ken.KenID, function (data) {
                            $scope.linkFiles = data.d;
                        });
                        break;
                    case 2:
                        chapterService.Chapter_Exercise_List(chapter.ChapterID, ken.KenID, function (data) {
                            $scope.linkExercises = data.d;
                        });
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
            $scope.course = course;
            $scope.loadStart($scope.course);
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
                $scope.$broadcast('onKenChapterAdd', data.d);
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
                $scope.kens.push(resultKen);
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

                $scope.kens = data.d;
                if ($scope.kens.length > 0) {
                    $scope.kenSelection = angular.copy($scope.chapter.ken); 
                }
                $scope.kenSelection.KenID = 0;
                $scope.kenSelection.Name = '全部';
                $scope.kens.insert(0, $scope.kenSelection);
            });
        }
    }]);

appKnow.controller('KenChapterCtrl', ['$scope', 'chapterService', 'kenService', function ($scope, chapterService, kenService) {

    $scope.$parent.kenDisable = true;
    $scope.canAdd = false;
    $scope.title = '';

    $scope.lastSelection = {};
    $scope.parentChapter = {};
    $scope.childChpater = {};

    $scope.$parent.loadStart({ OCID: $scope.course.OCID });

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

    $scope.parentFocus = function (item) {
        $scope.parentChpater = item;
        $scope.lastSelection = item;

        chapterService.Chapter_Ken_List(item.ChapterID, function (data) {
            $scope.$parent.kens = data.d;
            if ($scope.$parent.kens.length > 0) {
                $scope.$parent.kenSelection = angular.copy($scope.chapter.ken);
            }
            $scope.$parent.kenSelection.KenID = 0;
            $scope.$parent.kenSelection.Name = '全部';
            $scope.$parent.kens.insert(0, $scope.kenSelection);
            $scope.$emit('onRequestQuery', $scope.lastSelection, $scope.kenSelection);
        });
    }

    $scope.childFocus = function (item) {
        $scope.childChpater = item;
        $scope.lastSelection = item;
        $scope.$emit('onRequestQuery', $scope.lastSelection, $scope.kenSelection);
    }

    /// 知识点被选中, Tab页面切换， 请求查询数据
    $scope.$on('requestQuery', function (event, ken) {
        $scope.$emit('onRequestQuery', $scope.lastSelection, ken);
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

appKnow.controller('KenTopicCtrl', ['$scope', 'resourceKenService', 'chapterService', 'kenService',
    function ($scope, resourceKenService, chapterService, kenService) {

        $scope.$parent.kenDisable = false;

        $scope.dataKen = {};
        $scope.dataChapter = {};

        $scope.dataKens = [];
        $scope.dataChapters = [];

        $scope.chapterKens = [];

        $scope.$parent.loadStart({ OCID: $scope.course.OCID });

        $scope.kenFocus = function (item) {
            $scope.dataKen = item;
            kenService.Ken_Chapter_List({ KenID: item.KenID }, function (data) {
                $scope.dataChapters = data.d;
                if ($scope.dataChapters.length > 0) {
                    $scope.dataChapter = $scope.dataChapters[0];
                    $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen);
                }
            });
        }

        $scope.$on('willCourseChanged', function (event, course) {
            $scope.dataChapters.length = 0;
        });

        $scope.chapterFocus = function (item) {
            $scope.dataChapter = item;
            $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen);
        }

        /// 知识点被选中, Tab页面切换， 请求查询数据
        $scope.$on('requestQuery', function (event, ken) {
            $scope.$emit('onRequestQuery', $scope.dataChapter, $scope.dataKen);
        });

        $scope.$on('onKenChapterAdd', function (event, data) {
        });

    }]);


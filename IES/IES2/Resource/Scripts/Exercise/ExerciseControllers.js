﻿'use strict';

var appExercise = angular.module('app.exercise.controllers', [
    'checklist-model',
    'app.assist.services',
    'app.exercise.services',
    'app.ken.services',
    'app.resken.services'
]);

appExercise.controller('ExerciseListCtrl', ['$scope', '$state', 'resourceKenService', 'exerciseService', 'contentService', 'kenService', 'assistService',
    function ($scope, $state, resourceKenService, exerciseService, contentService, kenService, assistService) {
        ///课程切换
        $scope.$on('willCourseChanged', function (event, course) {
            //console.log(course); 
        });
        ///课程加载完成
        $scope.$on('courseLoaded', function (course) {

        });

        contentService.OC_Get(function (data) {
            $scope.$parent.courses.length = 0;
            var course = angular.copy(data.d);
            course.OCID = -2;
            course.CourseID = course.OCID;
            course.Name = '共享习题';
            $scope.$parent.courses.insert(0, course);

            course = angular.copy(data.d);
            course.OCID = -1;
            course.CourseID = course.OCID;
            course.Name = '我的习题';
            $scope.$parent.courses.insert(0, course);
            $scope.$parent.course = course;

        });


        //习题列表
        $scope.exercises = [];

        //课程
        $scope.courses = [];

        //试题类型
        $scope.exerciseTypes = [];
        //难易程度
        $scope.difficulties = [];
        //范围
        $scope.shareRanges = [];
        //标签
        $scope.keys = [];
        //知识点
        $scope.kens = [];

        $scope.data = {};
        $scope.data.searchKey = '';
        $scope.data.course = {};
        $scope.data.exerciseType = {};
        $scope.data.difficult = {};
        //知识点
        $scope.data.ken = {};
        $scope.data.shareRange = {};
        //被选择的标签
        $scope.data.key = {};

        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                var course = angular.copy($scope.courses[0]);
                course.OCID = -1;
                course.CourseID = -1;
                course.Name = '不限';
                $scope.courses.insert(0, course);
                $scope.data.course = $scope.courses[0];
            }
        });

        assistService.Resource_Dict_ExerciseType_Get(function (data) {
            if (data) {
                $scope.exerciseTypes = angular.copy(data);
                var item = angular.copy($scope.exerciseTypes[0]);
                item.id = -1;
                item.name = '不限';
                $scope.exerciseTypes.insert(0, item);
                $scope.data.exerciseType = $scope.exerciseTypes[0];
            }
        });

        assistService.Resource_Dict_Diffcult_Get(function (data) {
            if (data) {
                $scope.difficulties = angular.copy(data);
                var item = angular.copy($scope.difficulties[0]);
                item.id = -1;
                item.name = '不限';
                $scope.difficulties.insert(0, item);
                $scope.data.difficult = $scope.difficulties[0];
            }
        });

        assistService.Resource_Dict_ShareRange_Get(function (data) {
            if (data) {
                $scope.shareRanges = angular.copy(data);
                var item = angular.copy($scope.shareRanges[0]);
                item.id = -1;
                item.name = '不限';
                $scope.shareRanges.insert(0, item);
                $scope.data.shareRange = $scope.shareRanges[0];
            }
        });

        resourceKenService.ResourceKen_List('', 'Exercise', 100, function (data) {
            if (data.d) {
                $scope.kens = data.d;
                $scope.data.ken = angular.copy($scope.kens[0]);
                $scope.data.ken.KenID = -1;
                $scope.data.ken.Name = '不限';
                $scope.kens.insert(0, $scope.data.ken);
            }
        });

        assistService.Resource_Key_List('', 'Exercise', 100, function (data) {
            if (data.d.length > 0) {
                $scope.keys = data.d;
                var item = angular.copy($scope.keys[0]);
                item.KeyID = -1;
                item.Name = '不限';
                $scope.keys.insert(0, item);
                $scope.data.key = $scope.keys[0];
            }
        });

        $scope.courseChanged = function (item) {
            $scope.data.course = item;
            ExerciseSearch(20, 1);
        }

        $scope.exerciseTypeChanged = function (item) {
            $scope.data.exerciseType = item;
            ExerciseSearch(20, 1);
        }

        $scope.difficultChanged = function (item) {
            $scope.data.difficult = item;
            ExerciseSearch(20, 1);
        }

        $scope.shareRangeChanged = function (item) {
            $scope.data.shareRange = item;
            ExerciseSearch(20, 1);
        }

        $scope.keyChanged = function (item) {
            $scope.data.key = item;
            ExerciseSearch(20, 1);
        }

        $scope.knowChanged = function (item) {
            $scope.data.knowledge = item;
            ExerciseSearch(20, 1);
        }

        var ExerciseSearch = function (pageSize, pageIndex) {
            var model = {
                Conten: $scope.data.searchKey,
                OCID: $scope.data.course.OCID,
                CourseID: $scope.data.course.CourseID,
                ExerciseType: $scope.data.exerciseType.id,
                Diffcult: $scope.data.difficult.id,
                Scope: -1,
                ShareRange: $scope.data.shareRange.id
            };

            exerciseService.Exercise_Search(model, $scope.data.key, pageSize, pageIndex, function (data) {
                $scope.exercises.length = 0;
                if (data.d) {
                    $scope.exercises = data.d;
                }
            });
        }
        ExerciseSearch(20, 1);
        $scope.search = function () {
            ExerciseSearch(20, 1);
        }

        ///习题共享 TODO
        $scope.shareExercise = function (exercise) {
        }
        ///习题删除
        $scope.deleteExercise = function (exercise) {
            exerciseService.Exercise_Del(exercise.ExerciseID, function (data) {
                var length = $scope.exercises.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.exercises[i].ExerciseID == exercise.ExerciseID) {
                        $scope.exercises.splice(i, 1);
                        break;
                    }
                }

            });
        }
        /// <summary>
        /// 1判断题 ; 2单选题 ; 3 多选题 4填空题（客观）5填空题 ; 6连线题 ;7 排序题 ; 8分析题  9计算题   10问答题 ;
        ///11 翻译题  12听力训练  13写作  14阅读理解  15论述题 ;16 答题卡题型  17自定义题型
        /// </summary>
        ///编辑习题
        $scope.editExercise = function (exercise) {
            var param = { ExerciseID: exercise.ExerciseID };
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
                case 2:
                    $state.go('exercise.radio', param)
                    break;
                case 3:
                    $state.go('exercise.multiple', param)
                    break;
                default:
                    break;
            }
        }

    }]);

appExercise.controller('ExerciseCtrl', ['$scope', '$state', '$stateParams', 'exerciseService', 'contentService', 'kenService', 'assistService', '$timeout',
    function ($scope, $state, $stateParams, exerciseService, contentService, kenService, assistService, $timeout) {
        //课程
        $scope.courses = [];
        //试题类型
        $scope.exerciseTypes = [];
        //难易程度
        $scope.difficulties = [];
        //范围
        $scope.ranges = [];
        //标签
        //$scope.keys = [];

        $scope.keySelection = {};
        $scope.scope = {};

        $scope.data = {};
        $scope.data.course = {};
        $scope.data.exerciseType = {};
        $scope.data.difficult = {};
        //知识点
        $scope.data.kens = [];
        //标签
        $scope.data.keys = [];
        //已经选择的范围
        $scope.data.scopes = [];
        //被选择的标签
        $scope.data.selectedKeys = [];

        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.data.course = $scope.courses[0];
            }
        });

        assistService.Resource_Dict_ExerciseType_Get(function (data) {
            if (data) {
                $scope.exerciseTypes = angular.copy(data);
                $scope.data.exerciseType = $scope.exerciseTypes[0];
            }
        });

        assistService.Resource_Dict_Diffcult_Get(function (data) {
            if (data) {
                $scope.difficulties = angular.copy(data);
                $scope.data.difficult = $scope.difficulties[0];
            }
        });

        assistService.Resource_Dict_Scope_Get(function (data) {
            if (data) $scope.scopes = angular.copy(data);
        });

        $scope.$watch('data.exerciseType', function (v) {
            var param = { ExerciseID: $scope.$stateParams.ExerciseID };
            switch (v.id) {
                case '18': //简答题
                    $state.go('exercise.shortanswer', param)
                    break;
                case '4': //名词解释
                    $state.go('exercise.noun', param)
                    break;
                case '12': //听力题
                    $state.go('exercise.listening', param)
                    break;
                case '10': //问答题
                    $state.go('exercise.quesanswer', param)
                    break;
                case '13': //写作题
                    $state.go('exercise.quesanswer', param)
                    break;
                case '1': //判断题
                    $state.go('exercise.truefalse', param)
                    break;
                case '5': //填空题
                    $state.go('exercise.fillblank', param)
                    break;
                    //case '4': //填空客观题
                    //    $state.go('exercise.fillblank2', param)
                    //    break;
                case '6':  //连线题
                    $state.go('exercise.connection', param)
                    break;
                case '2'://单选题
                    $state.go('exercise.radio', param)
                    break;
                case '3'://多选题
                    $state.go('exercise.multiple', param)
                    break;
                default:
                    break;
            }
        });

        $scope.addKey = function (exerciseKey) {
            $scope.data.keys.push({ Name: exerciseKey });
            $scope.exerciseKey = '';
        }

        $scope.addKen = function (exerciseKen) {
            $scope.data.kens.push({ Name: exerciseKen });
            $scope.exerciseKen = '';
        }

        $scope.removeKey = function (key) {
            var length = $scope.data.selectedKeys.length;
            for (var i = 0; i < length; i++) {
                if ($scope.data.selectedKeys[i].KeyID === key.KeyID) {
                    $scope.data.selectedKeys.splice(i, 1);
                    break;
                }
            }
        }

        $scope.removeKen = function (ken) {
            var length = $scope.data.kens.length;
            for (var i = 0; i < length; i++) {
                if ($scope.data.kens[i].KenID === ken.KenID) {
                    $scope.data.kens.splice(i, 1);
                    break;
                }
            }
        }

        $scope.submit = function () {
            $scope.$broadcast('willRequestSave', $scope.data);
        }

        $scope.preview = function () {
            $scope.$broadcast('willPreview', $scope.data);
        }

        var setCourse = function (OCID, courseID) {
            var length = $scope.courses.length;
            for (var i = 0; i < length; i++) {
                if ($scope.courses[i].OCID == OCID && $scope.courses[i].CourseID == courseID) {
                    $scope.data.course = $scope.courses[i];
                    return;
                }
            }
        }
        var setExerciseType = function (ExerciseType) {
            var length = $scope.exerciseTypes.length;
            for (var i = 0; i < length; i++) {
                if ($scope.exerciseTypes[i].id == ExerciseType) {
                    $scope.data.exerciseType = $scope.exerciseTypes[i];
                    return;
                }
            }
        }
        var setDifficult = function (Difficult) {
            var length = $scope.difficulties.length;
            for (var i = 0; i < length; i++) {
                if ($scope.difficulties[i].id == Difficult) {
                    $scope.data.difficult = $scope.difficulties[i];
                    return;
                }
            }
        }
        var setScope = function (Scope) {
            $scope.data.scopes.length = 0;
            var length = $scope.scopes.length;
            for (var i = 0; i < length; i++) {
                if ((parseInt($scope.scopes[i].id) & parseInt(Scope)) > 0) {
                    $scope.data.scopes.push($scope.scopes[i]);
                }
            }
        }

        var setKeys = function (keyList) {
            $scope.data.keys.length = 0;
        }

        $scope.willEdit = function (data) {
            setCourse(data.exercisecommon.exercise.OCID, data.exercisecommon.exercise.CourseID);
            setExerciseType(data.exercisecommon.exercise.ExerciseType);
            setDifficult(data.exercisecommon.exercise.Diffcult);
            setScope(data.exercisecommon.exercise.Scope);
            $scope.data.keys = data.exercisecommon.keylist;
            $scope.data.kens = data.exercisecommon.kenlist;

        }

        $scope.willTopBind = function (model, data) {
            //顶部关联项            
            model.exercisecommon.exercise.OCID = data.course.OCID
            model.exercisecommon.exercise.CourseID = data.course.CourseID;//课程编号
            model.exercisecommon.exercise.Diffcult = parseInt(data.difficult.id);//难度等级
            var scope = 0;
            for (var i = 0; i < data.scopes.length; i++) {
                scope += parseInt(data.scopes[i].id);
            }

            model.exercisecommon.exercise.Scope = scope;
            //key关键字
            model.exercisecommon.exercise.Keys = '';
            for (var i = 0; i < data.keys.length; i++) {
                model.exercisecommon.exercise.Keys += data.keys[i].Name + 'wshgkjqbwhfbxlfrh';
            }

            //ken知识点
            model.exercisecommon.exercise.Kens = '';
            for (var i = 0; i < data.kens.length; i++) {
                model.exercisecommon.exercise.Kens += data.kens[i].Name + 'wshgkjqbwhfbxlfrh';
            }

            model.exercisecommon.exercise.ExerciseType = data.exerciseType.id;

            model.exercisecommon.exercise.ExerciseTypeName = data.exerciseType.name;
        }
    }]);

//简答题
appExercise.controller('ShortAnswerCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {

    //$scope.$on('willExerciseChange', function (event, changeParam) {
    //});

    //$scope.$on('willRequestSave', function (event, data) {
    //    //顶部关联项            
    //    $scope.model.exercise.OCID = data.course.OCID
    //    $scope.model.exercise.CourseID = data.course.CourseID;//课程编号
    //    $scope.model.exercise.Diffcult = parseInt(data.difficult.id);//难度等级
    //    var scope = 0;
    //    for (var i = 0; i < data.rangeSelected.length; i++) {
    //        scope += parseInt(data.rangeSelected[i].id);
    //    }

    //    $scope.model.exercise.Scope = scope;
    //    //key关键字
    //    $scope.model.exercise.Keys = '';
    //    for (var i = 0; i < data.selectedKeys.length; i++) {
    //        $scope.model.exercise.Keys = data.selectedKeys[i].Name + 'wshgkjqbwhfbxlfrh';
    //    }

    //    //ken知识点
    //    $scope.model.exercise.Kens = '';
    //    for (var i = 0; i < data.knowledges.length; i++) {
    //        $scope.model.exercise.Kens = data.knowledges[i].Name + 'wshgkjqbwhfbxlfrh';
    //    }

    //    $scope.model.exercise.ExerciseType = data.exerciseType.id;

    //    exerciseService.Exercise_ADD($scope.model, function (data) {
    //        if (data.d) {
    //            alert('提交成功！');
    //            $state.go('content.exercise');
    //        }
    //    });
    //});

    //$scope.$on('willPreview', function (event, exerciseData) {

    //});

    //$scope.model = {};//ExerciseInfo对象

    //var answer = { IsCorrect: false, Conten: '' };
    //$scope.Attachment = {};//附件对象
    //$scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    //var init = function () {
    //    if ($scope.ExerciseID > -1) {
    //        exerciseService.ExerciseInfo_Get($scope.ExerciseID, function (data) {
    //            $scope.model = data.d;
    //            $scope.willEdit($scope.model);
    //        });
    //    } else {
    //        exerciseService.Exercise_Model_Info(function (data) {
    //            $scope.model = data.d;
    //            answer = { IsCorrect: false, Conten: '' };
    //            $scope.model.exercisechoicelist.push(answer);
    //        });
    //    }
    //}
    //$scope.isRandChange = function (IsRand) {
    //    $scope.model.exercisecommon.exercise.IsRand = !!IsRand;
    //}

    ////添加选项
    //$scope.AddAnswer = function () {
    //    answer = { IsCorrect: false, Conten: '' };
    //    $scope.model.exercisechoicelist.push(answer);
    //}
    ////删除选项
    //$scope.del = function (item) {
    //    for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
    //        if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
    //            $scope.model.exercisechoicelist.splice(i, 1);
    //        }
    //    }
    //}
    ////是否是正确答案
    //$scope.isCorrectChange = function (item) {
    //    item.IsCorrect = item.IsCorrect ? false : true;
    //}
    //init();
}]);

//听力题
appExercise.controller('ListeningCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.willTopBind($scope.model, data);
        //增加子单选题
        if ($scope.model.Children != null) {
            SplicingRadio($scope.model.Children);
            $scope.model.Children.exercisecommon.exercise.ExerciseType = 2;
            $scope.model.Children.exercisecommon.exercise.ExerciseTypeName = '单选题';
        }
        //增加子填空题
        if ($scope.model.Children.Children != null) {            
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseType = 18;
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseTypeName = '填空题';
        }

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Custom_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象

    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', IsCorrect: 0 };


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.ExerciseInfo_GetListen($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                //$scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.Children.exercisechoicelist.push(answer);
                $scope.model.Children.Children.exercisechoicelist = [];
                answer = { Conten: '' };
                $scope.model.Children.Children.exercisechoicelist.push(answer);
            });
        }
    }
    //添加选项
    $scope.AddAnswer = function () {
        answer = { Conten: '', IsCorrect: 0 };
        $scope.model.Children.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
            if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.Children.exercisechoicelist.splice(i, 1);
            }
        }
    }
    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
            $scope.model.Children.exercisechoicelist[i].IsCorrect = 0;
        }
        item.IsCorrect = item.IsCorrect ? 0 : 1;
    }

    //添加选项
    $scope.AddAnswerSmall = function () {
        answer = { Conten: '' };
        $scope.model.Children.Children.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.delSmall = function (item) {
        for (var i = 0; i < $scope.model.Children.Children.exercisechoicelist.length; i++) {
            if ($scope.model.Children.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.Children.Children.exercisechoicelist.splice(i, 1);
            }
        }
    }

    //拼接单选题选项
    var SplicingRadio = function (model) {
        model.exercisecommon.exercise.Content = "";
        if (model.exercisecommon.exercise.ExerciseID > 0) {
            for (var i = 0; i < model.exercisechoicelist.length; i++) {
                var answer = model.exercisechoicelist[i].Conten == null ? "" : model.exercisechoicelist[i].Conten;
                var childId = model.exercisechoicelist[i].ChoiceID == null ? 0 : model.exercisechoicelist[i].ChoiceID;
                var isCorrect = model.exercisechoicelist[i].IsCorrect == false ? 0 : 1;
                if ((i + 1) == model.exercisechoicelist.length) {
                    model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect;
                } else {
                    model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < model.exercisechoicelist.length; i++) {
                var answer = model.exercisechoicelist[i].Conten == null ? "" : model.exercisechoicelist[i].Conten;
                if ((i + 1) == model.exercisechoicelist.length) {
                    model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + model.exercisechoicelist[i].IsCorrect;
                } else {
                    model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + model.exercisechoicelist[i].IsCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
    }   

    init();
}]);

//问答题
appExercise.controller('QuesanswerCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Writing_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Writing_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);

                $scope.textarea = $scope.model.exercisecommon.exercise.Analysis != null ? 0 : 1;
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                $scope.textarea = 0;//切换试题解析和得分点
            });
        }
    }

    $scope.tabTextarea = function () {
        $scope.textarea = $scope.textarea == 1 ? 0 : 1;
    }
    init();
}]);

//名词解释
appExercise.controller('NounCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? "" : $scope.model.exercisechoicelist[i].Answer;
                var childId = $scope.model.exercisechoicelist[i].ExerciseID == null ? 0 : $scope.model.exercisechoicelist[i].ExerciseID;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + $scope.model.exercisecommon.exercise.Conten + "wshgkjqbwhfbxlfrh_c" + answer;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + $scope.model.exercisecommon.exercise.Conten + "wshgkjqbwhfbxlfrh_c" + answer + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? "" : $scope.model.exercisechoicelist[i].Answer;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + $scope.model.exercisecommon.exercise.Conten + "wshgkjqbwhfbxlfrh_c" + answer;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + $scope.model.exercisecommon.exercise.Conten + "wshgkjqbwhfbxlfrh_c" + answer + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Analysis_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID
    var answer = { Answer: '' };


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Analysis_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                if ($scope.model.exercisechoicelist.length == 0) {
                    $scope.model.exercisechoicelist.push(answer);
                }
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                $scope.model.exercisechoicelist.push(answer);
            });
        }

    }
    $scope.Attachment = {};//附件对象

    init();
}]);

//判断题
appExercise.controller('TruefalseCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Judge_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });
    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Judge_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                $scope.model.exercisecommon.exercise.Answer = '0';
            });
        }
    }
    init();

    $scope.answeChange = function (answer) {
        $scope.model.exercisecommon.exercise.Answer = answer == '0' ? '1' : '0';
    }
}]);

//填空题
appExercise.controller('FillBlankCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? String.fromCharCode(32) : $scope.model.exercisechoicelist[i].Answer;
                var childId = $scope.model.exercisechoicelist[i].ExerciseID == null ? 0 : $scope.model.exercisechoicelist[i].ExerciseID;
                var spare = $scope.model.exercisechoicelist[i].Spare == null ? String.fromCharCode(32) : $scope.model.exercisechoicelist[i].Spare;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + spare;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + spare + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? "" : $scope.model.exercisechoicelist[i].Answer;
                var spare = $scope.model.exercisechoicelist[i].Spare == null ? "" : $scope.model.exercisechoicelist[i].Spare;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer == '' ? answer.charCodeAt(32) : answer + "wshgkjqbwhfbxlfrh_c" + spare == '' ? spare.charCodeAt(32) : spare;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer == '' ? answer.charCodeAt(32) : answer + "wshgkjqbwhfbxlfrh_c" + spare == '' ? spare.charCodeAt(32) : spare + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_FillInBlanks_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象

    var answer = { Answer: '', Spare: '' };
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Analysis_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);

                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    var a = $scope.model.exercisechoicelist[i].Answer.split('wshgkjqbwhfbxlfrh_c');
                    $scope.model.exercisechoicelist[i].Answer = a[0];
                    $scope.model.exercisechoicelist[i].Spare = a[1];
                }
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Answer: '', Spare: '' };
                $scope.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.Add = function () {
        answer = { Answer: '', Spare: '' };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.Del = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist[i].Answer = '';
                    $scope.model.exercisechoicelist[i].Spare = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist.splice(i, 1);
                }
            }
        }

    }
    init();
}]);

//填空客观题
appExercise.controller('FillBlank2Ctrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            $scope.model.exercisechoicelist[i].Conten = $scope.model.exercisechoicelist[i].Conten +
                'wshgkjqbwhfbxlfrh' + $scope.model.exercisechoicelist[i].Spare;
        }
        $scope.$emit('willSave', $scope.model);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    var answer = { Conten: '', Spare: '' };
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.ExerciseInfo_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                //$scope.willEdit($scope.model);

                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    var a = $scope.model.exercisechoicelist[i].Conten.split('wshgkjqbwhfbxlfrh');
                    $scope.model.exercisechoicelist[i].Conten = a[0];
                    $scope.model.exercisechoicelist[i].Spare = a[1];
                }
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', Spare: '' };
                $scope.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.Add = function () {
        answer = { Conten: '', Spare: '' };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.Del = function (item) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.exercisechoicelist.splice(i, 1);
            }
        }
    }
    init();
}]);

//连线题
appExercise.controller('ConnectionCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        $scope.model.exercisechoicelist.length = 0;
        var choice = {};
        for (var i = 0; i < $scope.list.length; i++) {
            choice = { Conten: $scope.list[i].Conten, grou: $scope.list[i].grou };
            $scope.model.exercisechoicelist.push(choice);
            choice = { Conten: $scope.list[i].Answer, grou: $scope.list[i].grou };
            $scope.model.exercisechoicelist.push(choice);
        }
        for (var i = 0; i < $scope.ganraoList.length; i++) {
            $scope.model.exercisechoicelist.push($scope.ganraoList[i]);
        }
        $scope.$emit('willSave', $scope.model);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID
    $scope.ganraoList = [];//答案数组最终
    $scope.list = [];//答案数组起始
    var grou = 1;//连线分组
    var answer = {};

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.ExerciseInfo_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                //$scope.willEdit($scope.model);
                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    if ($scope.model.exercisechoicelist[i].Grou == 0) {
                        $scope.ganraoList.push($scope.model.exercisechoicelist[i]);
                    }
                }

                var choiceList = $scope.model.exercisechoicelist;
                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    if ($scope.model.exercisechoicelist[i].Grou == 0) continue;
                    if ($scope.model.exercisechoicelist[i].Grou > grou) grou += 1;
                    for (var n = 0; n < $scope.model.exercisechoicelist.length; n++) {
                        if ($scope.model.exercisechoicelist[i].Grou == choiceList[n].Grou) {
                            $scope.list.push({
                                Conten: choiceList[n].Conten,
                                Answer: choiceList[n + 1].Conten,
                                grou: choiceList[n].Grou
                            });
                            choiceList.splice(n, 1);
                        }
                    }
                }
                console.log($scope.list);
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', Answer: '', grou: grou };
                $scope.list.push(answer);
                answer = { Conten: '', grou: 0 };
                $scope.ganraoList.push(answer);
            });
        }
    }
    //添加干扰项
    $scope.AddInterference = function () {
        answer = { Conten: '', grou: 0 };
        $scope.ganraoList.push(answer);
    }

    //添加选项
    $scope.Add = function () {
        grou += 1;
        answer = { Conten: '', Answer: '', grou: grou };
        $scope.list.push(answer);
    }
    //删除选项
    $scope.Del = function (item) {
        for (var i = 0; i < $scope.list.length; i++) {
            if ($scope.list[i].$$hashKey == item.$$hashKey) {
                $scope.list.splice(i, 1);
            }
        }
    }
    init();
}]);

//单选题
appExercise.controller('RadioCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                var childId = $scope.model.exercisechoicelist[i].ChoiceID == null ? 0 : $scope.model.exercisechoicelist[i].ChoiceID;
                var isCorrect = $scope.model.exercisechoicelist[i].IsCorrect == false ? 0 : 1;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + $scope.model.exercisechoicelist[i].IsCorrect;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + $scope.model.exercisechoicelist[i].IsCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_MultipleChoice_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象

    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', IsCorrect: 0 };

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_MultipleChoice_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.exercisechoicelist = [];
                $scope.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.AddAnswer = function () {
        answer = { Conten: '', IsCorrect: 0 };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist.splice(i, 1);
                }
            }
        }
    }

    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {

            $scope.model.exercisechoicelist[i].IsCorrect = 0;
        }
        item.IsCorrect = item.IsCorrect ? 0 : 1;
    }

    init();
}]);

//多选题
appExercise.controller('MultipleCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                var childId = $scope.model.exercisechoicelist[i].ChoiceID == null ? 0 : $scope.model.exercisechoicelist[i].ChoiceID;
                var isCorrect = $scope.model.exercisechoicelist[i].IsCorrect == false ? 0 : 1;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + isCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + $scope.model.exercisechoicelist[i].IsCorrect;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + $scope.model.exercisechoicelist[i].IsCorrect + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_MultipleChoice_M_Edit(v, function (data) {
            if (data.d) {
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象

    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', IsCorrect: 0 };

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_MultipleChoice_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.exercisechoicelist = [];
                $scope.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.AddAnswer = function () {
        answer = { Conten: '', IsCorrect: 0 };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.exercisechoicelist.splice(i, 1);
                }
            }
        }
    }

    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        item.IsCorrect = item.IsCorrect ? 0 : 1;
    }

    init();
}]);


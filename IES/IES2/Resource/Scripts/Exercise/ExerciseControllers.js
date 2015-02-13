'use strict';

var appExercise = angular.module('app.exercise.controllers', [
    'checklist-model',
    'app.assist.services',
    'app.exercise.services',
    'app.ken.services',
    'app.res.services',
    'app.resken.services'
]);

appExercise.controller('PreviewCtrl', ['$scope', 'previewService', function ($scope, previewService) {
    $scope.exercise = previewService.exercise;
    $scope.$emit('onPreviewSwitch', true);
}]);

appExercise.controller('ExerciseListCtrl', ['$scope', '$state', 'resourceService', 'resourceKenService', 'exerciseService', 'contentService', 'kenService', 'assistService',
    function ($scope, $state, resourceService, resourceKenService, exerciseService, contentService, kenService, assistService) {

        $scope.pagesNum = 100; 
        var pageSize = 5;

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
                course.OCID = 0;
                course.CourseID = 0;
                course.Name = '不限';
                $scope.courses.insert(0, course);
                $scope.data.course = $scope.courses[0];
            }
        });

        assistService.Resource_Dict_ExerciseType_Get(function (data) {
            if (data) {
                $scope.exerciseTypes = angular.copy(data);
                var item = angular.copy($scope.exerciseTypes[0]);
                item.id = 0;
                item.name = '不限';
                $scope.exerciseTypes.insert(0, item);
                $scope.data.exerciseType = $scope.exerciseTypes[0];
            }
        });

        assistService.Resource_Dict_Diffcult_Get(function (data) {
            if (data) {
                $scope.difficulties = angular.copy(data);
                var item = angular.copy($scope.difficulties[0]);
                item.id = 0;
                item.name = '不限';
                $scope.difficulties.insert(0, item);
                $scope.data.difficult = $scope.difficulties[0];
            }
        });

        assistService.Resource_Dict_ShareRange_Get(function (data) {
            if (data) {
                $scope.shareRanges = angular.copy(data);
                var item = angular.copy($scope.shareRanges[0]);
                item.id = 0;
                item.name = '不限';
                $scope.shareRanges.insert(0, item);
                $scope.data.shareRange = $scope.shareRanges[0];
            }
        });

        resourceKenService.ResourceKen_List('', 'Exercise', 100, function (data) {
            if (data.d && data.d.length > 0) {
                $scope.kens = data.d;
                $scope.data.ken = angular.copy($scope.kens[0]);
                $scope.data.ken.KenID = 0;
                $scope.data.ken.Name = '不限';
                $scope.kens.insert(0, $scope.data.ken);
            }
        });

        assistService.Resource_Key_List('', 'Exercise', 100, function (data) {
            if (data.d.length > 0) {
                $scope.keys = data.d;
                var item = angular.copy($scope.keys[0]);
                item.KeyID = 0;
                item.Name = '不限';
                $scope.keys.insert(0, item);
                $scope.data.key = $scope.keys[0];
            }
        });


        $scope.courseChanged = function (item) {
            $scope.data.course = item;
            ExerciseSearch(pageSize, 1);
        }

        $scope.exerciseTypeChanged = function (item) {
            $scope.data.exerciseType = item;
            ExerciseSearch(pageSize, 1);
        }

        $scope.difficultChanged = function (item) {
            $scope.data.difficult = item;
            ExerciseSearch(pageSize, 1);
        }

        $scope.shareRangeChanged = function (item) {
            $scope.data.shareRange = item;
            ExerciseSearch(pageSize, 1);
        }

        $scope.keyChanged = function (item) {
            $scope.data.key = item;
            ExerciseSearch(pageSize, 1);
        }

        $scope.kenChanged = function (item) {
            $scope.data.ken = item;
            ExerciseSearch(pageSize, 1);
        }
        $scope.pagesNum = 1;



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
            var keys = $scope.data.key.KeyID === undefined || $scope.data.key.KeyID === -1 ? '' : $scope.data.key.Name;
            var kens = $scope.data.ken.KenID === undefined || $scope.data.ken.KenID === -1 ? '' : $scope.data.ken.Name;
            exerciseService.Exercise_Search(model, $scope.data.key, keys, kens, pageSize, pageIndex, function (data) {
                $scope.exercises.length = 0;
                $scope.pageNum = 1;
                if (data.d) {
                    $scope.exercises = data.d;
                    var rowsCount = $scope.exercises[0].RowsCount;
                    $scope.pageNum = Math.ceil(rowsCount / pageSize);
                }
            });
        }

        laypage({
            cont: $('#pager'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: $scope.pagesNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: '首页', //若不显示，设置false即可
            last: '尾页', //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                ExerciseSearch(pageSize, e.curr);
            }
        });
        ExerciseSearch(pageSize, 1);

        $scope.search = function () {
            ExerciseSearch(pageSize, 1);
        }

        $scope.checks = [];

        var buildIDs = function (checks) {
            var ids = '';
            var length = checks.length;
            for (var i = 0; i < length; i++) {
                ids += checks[i].ExerciseID + ',';
            }
            return ids.substr(0, ids.length - 1);
        }

        ///批量共享
        $scope.$on('onBatchShareRange', function (event, range) {
            var ids = buildIDs($scope.checks);
            exerciseService.Exercise_Batch_ShareRange(ids, range.id, function (data) {
                if (data.d === true) {
                    var length = $scope.checks.length;
                    for (var i = 0; i < length; i++) {
                        $scope.checks[i].ShareRange = range.id;
                    }
                    $scope.checks.length = 0;
                } else {
                    ///TODO 统一提示框 加美化效果
                    alert('批量共享操作失败！');
                }
            });
        });
        ///批量删除
        $scope.$on('onBatchRemove', function () {
            var ids = buildIDs($scope.checks);
            exerciseService.Exercise_Batch_Del(ids, function (data) {
                if (data.d === true) {
                    ExerciseSearch(pageSize, 1);
                    $scope.checks.length = 0;
                } else {
                    ///TODO 统一提示框 加美化效果
                    alert('批量删除操作失败！');
                }
            });

        });

        ///习题共享 TODO 
        $scope.$on('onShareExercise', function (event, exercise, range) {
            var ids = exercise.ExerciseID;
            exerciseService.Exercise_Batch_ShareRange(ids, range.id, function (data) {
                if (data.d === true) {
                    ExerciseSearch(pageSize, 1);
                } else {
                    ///TODO 统一提示框 加美化效果
                    alert('共享操作失败！');
                }
            });
        });

        $scope.$on('onDeleteExercise', function (event, exercise) {
            exerciseService.Exercise_Del(exercise.ExerciseID, function (data) {
                var length = $scope.exercises.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.exercises[i].ExerciseID == exercise.ExerciseID) {
                        $scope.exercises.splice(i, 1);
                        break;
                    }
                }
            });
        });

        /// <summary>
        /// 1判断题 ; 2单选题 ; 3 多选题 4填空题（客观）5填空题 ; 6连线题 ;7 排序题 ; 8分析题  9计算题   10问答题 ;
        ///11 翻译题  12听力训练  13写作  14阅读理解  15论述题 ;16 答题卡题型  17自定义题型
        /// </summary>
        ///编辑习题
        $scope.$on('onEditExercise', function (event, exercise) {
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
                default:
                    break;
            }
        });
    }]);

appExercise.controller('ExerciseCtrl', ['$scope', '$state', '$stateParams', 'exerciseService', 'contentService', 'previewService', 'resourceService', 'kenService', 'assistService', '$timeout',
    function ($scope, $state, $stateParams, exerciseService, contentService, previewService, resourceService, kenService, assistService, $timeout) {

        $scope.$emit('onPreviewSwitch', false);

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
                case '17': //自定义题
                    $state.go('exercise.custom', param)
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
                case '11': //翻译题
                    $state.go('exercise.translation', param)
                    break;
                case '7': //排序题
                    $state.go('exercise.sorting', param)
                    break;
                case '8': //分析题
                    $state.go('exercise.analysis', param)
                    break;
                case '9': //计算题
                    $state.go('exercise.analysis', param)
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
            var length = $scope.data.keys.length;
            for (var i = 0; i < length; i++) {
                if ($scope.data.keys[i].KeyID === key.KeyID) {
                    $scope.data.keys.splice(i, 1);
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

        $scope.attachmentList = [];

        $scope.data.exercise = {};

        $scope.preview = function () {
            $scope.$broadcast('onPreview');
        }


        $scope.$on('onBeginPreview', function (event, model) {
            model.exercisecommon.exercise.ExerciseType = $scope.data.exerciseType.id;
            $scope.data.exercise = model;
            previewService.exercise = angular.copy(model);
            $state.go('preview');
        });
        ///更新附件关联关系
        var attachmentSave = function (exerciseID) {
            var length = $scope.attachmentList.length;
            for (var i = 0; i < length; i++) {
                var model = $scope.attachmentList[i];
                if (model.SourceID <= 0) {
                    model.Source = 'Exercise';
                    model.SourceID = exerciseID;
                    exerciseService.Attachment_SourceID_Upd(model);
                }
            }
        }

        $scope.$on('onExerciseSaved', function (event, exerciseID) {
            attachmentSave(exerciseID);
        });

        ///删除附件
        $scope.removeAttachment = function (attachmentList, removedAttachmentObject) {
            var length = attachmentList.length;
            for (var i = 0; i < length; i++) {
                if (attachmentList[i].AttachmentID === removedAttachmentObject.AttachmentID) {
                    attachmentList.splice(i, 1);
                    break;
                }
            }
        }


        $scope.$on('onRemoveAttachment', function (event, attachment) {
            attachment.Updatetime = new Date();
            resourceService.Attachment_Del(attachment, function (data) {
                if (data.d) {
                    $scope.$broadcast('onRemoveFinishedAttachment', attachment);
                }
            });
        });

        /// 附件上传
        $scope.$on('onSuccessItem', function (event, fileItem, response, status, headers) {
            response.file.SourceID = $scope.model.exercisecommon.exercise.ExerciseID;
            $scope.attachmentList.push(response.file);
            attachmentSave($scope.model.exercisecommon.exercise.ExerciseID);
        });

        $scope.$on('onCompleteItem', function (event) {

        });
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
        //增加子多选题
        if ($scope.model.Children.Children != null) {
            SplicingRadio($scope.model.Children.Children);
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseType = 3;
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseTypeName = '多选题';
        }
        //增加子填空题
        if ($scope.model.Children.Children.Children != null) {
            $scope.model.Children.Children.Children.exercisecommon.exercise.ExerciseType = 18;
            $scope.model.Children.Children.Children.exercisecommon.exercise.ExerciseTypeName = '填空题';
        }


        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Custom_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
    });

    $scope.model = {};//ExerciseInfo对象

    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', IsCorrect: 0 };


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Custom_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.Children.exercisechoicelist.push(answer);
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.Children.Children.exercisechoicelist.push(answer);
                answer = { Conten: '' };
                $scope.model.Children.Children.Children.exercisechoicelist.push(answer);
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
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.exercisechoicelist.splice(i, 1);
                }
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

    //添加选项,多选
    $scope.AddAnswerMultiple = function () {
        answer = { Conten: '', IsCorrect: 0 };
        $scope.model.Children.Children.exercisechoicelist.push(answer);
    }
    //删除选项,多选
    $scope.delMultiple = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.Children.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.Children.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.Children.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.Children.exercisechoicelist.splice(i, 1);
                }
            }
        }
    }
    //是否是正确答案,多选
    $scope.isCorrectChangeMultiple = function (item) {
        item.IsCorrect = item.IsCorrect ? 0 : 1;
    }

    //拼接单选，多选题选项
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

//自定义题
appExercise.controller('CustomCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
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
        //增加子多选题
        if ($scope.model.Children.Children != null) {
            SplicingRadio($scope.model.Children.Children);
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseType = 3;
            $scope.model.Children.Children.exercisecommon.exercise.ExerciseTypeName = '多选题';
        }
        //增加子填空题
        if ($scope.model.Children.Children.Children != null) {
            $scope.model.Children.Children.Children.exercisecommon.exercise.ExerciseType = 18;
            $scope.model.Children.Children.Children.exercisecommon.exercise.ExerciseTypeName = '填空题';
        }


        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Custom_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
    });

    $scope.model = {};//ExerciseInfo对象

    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', IsCorrect: 0 };


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Custom_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.Children.exercisechoicelist.push(answer);
                answer = { Conten: '', IsCorrect: 0 };
                $scope.model.Children.Children.exercisechoicelist.push(answer);
                answer = { Conten: '' };
                $scope.model.Children.Children.Children.exercisechoicelist.push(answer);
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
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.exercisechoicelist.splice(i, 1);
                }
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

    //添加选项,多选
    $scope.AddAnswerMultiple = function () {
        answer = { Conten: '', IsCorrect: 0 };
        $scope.model.Children.Children.exercisechoicelist.push(answer);
    }
    //删除选项,多选
    $scope.delMultiple = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.Children.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.Children.exercisechoicelist[i].Conten = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.model.Children.Children.exercisechoicelist.length; i++) {
                if ($scope.model.Children.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.model.Children.Children.exercisechoicelist.splice(i, 1);
                }
            }
        }
    }
    //是否是正确答案,多选
    $scope.isCorrectChangeMultiple = function (item) {
        item.IsCorrect = item.IsCorrect ? 0 : 1;
    }

    //拼接单选，多选题选项
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
        var editor = EWEBEDITOR.Instances["editorInput"];
        $scope.model.exercisecommon.exercise.Conten = editor.getHTML();

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Writing_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        var editor = EWEBEDITOR.Instances["editorInput"];
        $scope.model.exercisecommon.exercise.Conten = editor.getHTML();
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.model = {};//ExerciseInfo对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
    });

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Writing_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
                $scope.editorText = $scope.model.exercisecommon.exercise.Conten;
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
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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
    $scope.$parent.attachmentList = [];
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.willTopBind($scope.model, data);
        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Judge_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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

    /// 附件上传成功
    $scope.$on('onSuccessItem', function (event, fileItem, response, status, headers) {
        $scope.$parent.attachmentList.push(response.file);
        response.file.SourceID = $scope.ExerciseID;
        $scope.$emit('willFileUploaded', fileItem, response.file);
    });

    $scope.$on('onCompleteItem', function (event) {

    });
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
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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
appExercise.controller('ConnectionCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        //将选项加到exercisechoicelist数组中
        $scope.model.exercisechoicelist.length = 0;
        var choice = {};
        for (var i = 0; i < $scope.list.length; i++) {
            choice = { Conten: $scope.list[i].Conten, grou: $scope.list[i].grou, ChoiceID: $scope.list[i].ChoiceID };
            $scope.model.exercisechoicelist.push(choice);
            choice = { Conten: $scope.list[i].Answer, grou: $scope.list[i].grou, ChoiceID: $scope.list[i].ChoiceID1 };
            $scope.model.exercisechoicelist.push(choice);
        }
        for (var i = 0; i < $scope.ganraoList.length; i++) {
            $scope.model.exercisechoicelist.push($scope.ganraoList[i]);
        }

        //将exercisechoicelist数组中的元素拼接成Content
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var conten = $scope.model.exercisechoicelist[i].Conten == null ? String.fromCharCode(32) : $scope.model.exercisechoicelist[i].Conten;
                var childId = $scope.model.exercisechoicelist[i].ChoiceID == null ? 0 : $scope.model.exercisechoicelist[i].ChoiceID;
                var grou = $scope.model.exercisechoicelist[i].grou == null ? 0 : $scope.model.exercisechoicelist[i].grou;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + conten + "wshgkjqbwhfbxlfrh_c" + grou;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + conten + "wshgkjqbwhfbxlfrh_c" + grou + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var conten = $scope.model.exercisechoicelist[i].Conten == null ? String.fromCharCode(32) : $scope.model.exercisechoicelist[i].Conten;
                var grou = $scope.model.exercisechoicelist[i].grou == null ? 0 : $scope.model.exercisechoicelist[i].grou;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + conten + "wshgkjqbwhfbxlfrh_c" + grou;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + conten + "wshgkjqbwhfbxlfrh_c" + grou + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Line_S_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.model.exercisecommon.exercise.ExerciseType = 6;
        $scope.model.exercisechoicelist.length = 0;
        var choice = {};
        for (var i = 0; i < $scope.list.length; i++) {
            choice = { Conten: $scope.list[i].Conten, Answer: $scope.list[i].Answer, grou: $scope.list[i].grou };
            $scope.model.exercisechoicelist.push(choice);
        }
        for (var i = 0; i < $scope.ganraoList.length; i++) {
            choice = { Conten: '', Answer: $scope.ganraoList[i].Conten, grou: $scope.ganraoList[i].grou };
            $scope.model.exercisechoicelist.push(choice);
        }
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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
            exerciseService.Exercise_MultipleChoice_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    if ($scope.model.exercisechoicelist[i].Grou == 0) {
                        $scope.ganraoList.push($scope.model.exercisechoicelist[i]);
                    }
                }

                var choiceList = $scope.model.exercisechoicelist;
                for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                    if ($scope.model.exercisechoicelist[i].Grou == 0) continue;
                    if ($scope.model.exercisechoicelist[i].Grou > grou) grou = $scope.model.exercisechoicelist[i].Grou;
                    for (var n = 0; n < $scope.model.exercisechoicelist.length; n++) {
                        if ($scope.model.exercisechoicelist[i].Grou == choiceList[n].Grou) {
                            $scope.list.push({
                                Conten: choiceList[n].Conten,
                                Answer: choiceList[n + 1].Conten,
                                grou: choiceList[n].Grou,
                                ChoiceID: choiceList[n].ChoiceID,
                                ChoiceID1: choiceList[n + 1].ChoiceID
                            });
                            choiceList.splice(n, 1);
                        }
                    }
                }
                console.log(grou);
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', Answer: '', grou: grou, ChoiceID: 0 };
                $scope.list.push(answer);
                answer = { Conten: '', grou: 0, ChoiceID: 0 };
                $scope.ganraoList.push(answer);
            });
        }
    }
    //添加干扰项
    $scope.AddInterference = function () {
        answer = { Conten: '', grou: 0, ChoiceID: 0 };
        $scope.ganraoList.push(answer);
    }

    //添加选项
    $scope.Add = function () {
        grou += 1;
        answer = { Conten: '', Answer: '', grou: grou, ChoiceID: 0 };
        $scope.list.push(answer);
    }
    //删除选项
    $scope.Del = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.list.length; i++) {
                if ($scope.list[i].$$hashKey == item.$$hashKey) {
                    $scope.list[i].Conten = '';
                    $scope.list[i].Answer = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.list.length; i++) {
                if ($scope.list[i].$$hashKey == item.$$hashKey) {
                    $scope.list.splice(i, 1);
                }
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
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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

//翻译题
appExercise.controller('TranslationCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });
    $scope.$on('willRequestSave', function (event, data) {
        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Writing_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
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

//排序题
appExercise.controller('SortingCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willRequestSave', function (event, data) {
        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? String.fromCharCode(32) : $scope.model.exercisechoicelist[i].Conten;
                var childId = $scope.model.exercisechoicelist[i].ChoiceID == null ? 0 : $scope.model.exercisechoicelist[i].ChoiceID;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + (i + 1);
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + answer + "wshgkjqbwhfbxlfrh_c" + (i + 1) + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var answer = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + (i + 1);
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + answer + "wshgkjqbwhfbxlfrh_c" + (i + 1) + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.model, data);

        var v = angular.toJson($scope.model);
        exerciseService.Exercise_Order_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });


    $scope.$on('onPreview', function (event) {
        $scope.model.exercisecommon.exercise.ExerciseType = 7;
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            $scope.model.exercisechoicelist[i].OrderNum = i + 1;
        }
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象    
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID
    var answer = { Conten: '', OrderNum: 0 };

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_MultipleChoice_Get($scope.ExerciseID, function (data) {
                $scope.model = data.d;
                $scope.willEdit($scope.model);
                //for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                //    var a = $scope.model.exercisechoicelist[i].Answer.split('wshgkjqbwhfbxlfrh_c');
                //    $scope.model.exercisechoicelist[i].Answer = a[0];
                //    $scope.model.exercisechoicelist[i].Spare = a[1];
                //}
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.model = data.d;
                answer = { Conten: '', OrderNum: 0 };
                $scope.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.Add = function () {
        answer = { Conten: '', OrderNum: 0 };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.Del = function (item) {
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
    init();
}]);

//分析题
appExercise.controller('AnalysisCtrl', ['$scope', 'exerciseService', '$stateParams', '$state', function ($scope, exerciseService, $stateParams, $state) {

    $scope.$on('onPreview', function (event) {
        $scope.$emit('onBeginPreview', $scope.model);
    });

    $scope.$on('willRequestSave', function (event, data) {
        var editor = EWEBEDITOR.Instances["editorInput"];
        $scope.model.exercisecommon.exercise.Conten = editor.getHTML();

        $scope.model.exercisecommon.exercise.Content = "";
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var conten = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? "" : $scope.model.exercisechoicelist[i].Answer;
                var childId = $scope.model.exercisechoicelist[i].ExerciseID == null ? 0 : $scope.model.exercisechoicelist[i].ExerciseID;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + conten + "wshgkjqbwhfbxlfrh_c" + answer;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    childId + "wshgkjqbwhfbxlfrh_b"
                    + conten + "wshgkjqbwhfbxlfrh_c" + answer + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }
        else {
            for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
                var conten = $scope.model.exercisechoicelist[i].Conten == null ? "" : $scope.model.exercisechoicelist[i].Conten;
                var answer = $scope.model.exercisechoicelist[i].Answer == null ? "" : $scope.model.exercisechoicelist[i].Answer;
                if ((i + 1) == $scope.model.exercisechoicelist.length) {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + conten + "wshgkjqbwhfbxlfrh_c" + answer;
                } else {
                    $scope.model.exercisecommon.exercise.Content +=
                    "0wshgkjqbwhfbxlfrh_b" + conten + "wshgkjqbwhfbxlfrh_c" + answer + "wshgkjqbwhfbxlfrh_a";
                }
            }
        }

        $scope.willTopBind($scope.$parent.model, data);

        var v = angular.toJson($scope.$parent.model);
        exerciseService.Exercise_Analysis_M_Edit(v, function (data) {
            if (data.d != null) {
                $scope.$emit('onExerciseSaved', data.d.exercisecommon.exercise.ExerciseID);
                alert('提交成功！');
                $state.go('content.exercise');
            }
        });
    });

    $scope.$on('onRemoveFinishedAttachment', function (event, attachment) {
        $scope.removeAttachment($scope.model.exercisecommon.attachmentlist, attachment);
    });

    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var answer = { Conten: '', Answer: '' };

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Analysis_Get($scope.ExerciseID, function (data) {
                $scope.$parent.model = data.d;
                $scope.willEdit($scope.$parent.model);
                $scope.editorText = $scope.$parent.model.exercisecommon.exercise.Conten;
            });
        } else {
            exerciseService.Exercise_Model_Info_Get(function (data) {
                $scope.$parent.model = data.d;
                $scope.$parent.model.exercisechoicelist.push(answer);
            });
        }
    }

    //添加选项
    $scope.Add = function () {
        answer = { Conten: '', Answer: '' };
        $scope.$parent.model.exercisechoicelist.push(answer);
    }

    //删除选项
    $scope.Del = function (item) {
        if ($scope.ExerciseID > 0) {
            for (var i = 0; i < $scope.$parent.model.exercisechoicelist.length; i++) {
                if ($scope.$parent.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.$parent.model.exercisechoicelist[i].Conten = '';
                    $scope.$parent.model.exercisechoicelist[i].Answer = '';
                }
            }
        } else {
            for (var i = 0; i < $scope.$parent.model.exercisechoicelist.length; i++) {
                if ($scope.$parent.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                    $scope.$parent.model.exercisechoicelist.splice(i, 1);
                }
            }
        }

    }

    init();
}]);
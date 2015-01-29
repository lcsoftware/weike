'use strict';

var appExercise = angular.module('app.exercise.controllers', [
    'checklist-model',
    'app.assist.services',
    'app.exercise.services',
    'app.resken.services'
]);

appExercise.controller('ExerciseListCtrl', ['$scope', '$state', 'resourceKenService', 'exerciseService', 'contentService', 'knowledgeService', 'assistService',
    function ($scope, $state, resourceKenService, exerciseService, contentService, knowledgeService, assistService) {
        ///课程切换
        $scope.$on('willCourseChanged', function (event, course) {
            //console.log(course); 
        });
        ///课程加载完成
        $scope.$on('courseLoaded', function (course) {
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
        });

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
        $scope.knowledges = [];

        $scope.data = {};
        $scope.data.course = {};
        $scope.data.exerciseType = {};
        $scope.data.difficult = {};
        //知识点
        $scope.data.knowledge = {};
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
            if (data.d) {
                $scope.exerciseTypes = data.d;
                var item = angular.copy($scope.exerciseTypes[0]);
                item.id = -1;
                item.name = '不限';
                $scope.exerciseTypes.insert(0, item);
                $scope.data.exerciseType = $scope.exerciseTypes[0];
            }
        });

        assistService.Resource_Dict_Diffcult_Get(function (data) {
            if (data.d) {
                $scope.difficulties = data.d;
                var item = angular.copy( $scope.difficulties[0]);
                item.id = -1;
                item.name = '不限';
                $scope.difficulties.insert(0, item);
                $scope.data.difficult = $scope.difficulties[0];
            }
        });

        assistService.Resource_Dict_ShareRange_Get(function (data) {
            if (data.d) {
                $scope.shareRanges = data.d;
                var item = angular.copy($scope.shareRanges[0]);
                item.id = -1;
                item.name = '不限';
                $scope.shareRanges.insert(0, item);
                $scope.data.shareRange = $scope.shareRanges[0];
            }
        });

        resourceKenService.ResourceKen_List('', 'Exercise', 100, function (data) {
            if (data.d) {
                $scope.knowledges = data.d;
                var item = angular.copy($scope.knowledges[0]);
                item.KenID = -1;
                item.Source = '不限';
                $scope.knowledges.insert(0, item);
                $scope.data.knowledge = $scope.knowledges[0];
            }
        });

        assistService.Resource_Key_List('', 'Exercise', 100, function (data) {
            if (data.d) {
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
        }

        $scope.exerciseTypeChanged = function (item) {
            $scope.data.exerciseType = item;
        }

        $scope.difficultChanged = function (item) {
            $scope.data.difficult = item;
        }

        $scope.shareRangeChanged = function (item) {
            $scope.data.shareRange = item;
        }

        $scope.keyChanged = function (item) {
            $scope.data.key = item;
        }

        $scope.knowChanged = function (item) {
            $scope.data.knowledge = item;
        }

        ///习题共享 TODO
        $scope.shareExercise = function (exercise) {

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

    }]);

appExercise.controller('ExerciseCtrl', ['$scope', '$state', 'exerciseService', 'contentService', 'knowledgeService', 'assistService',
    function ($scope, $state, exerciseService, contentService, knowledgeService, assistService) {
        //课程
        $scope.courses = [];
        //试题类型
        $scope.exerciseTypes = [];
        //难易程度
        $scope.difficulties = [];
        //范围
        $scope.ranges = [];
        //标签
        $scope.keys = [];

        $scope.keySelection = {};
        $scope.range = {};

        $scope.data = {};
        $scope.data.course = {};
        $scope.data.exerciseType = {};
        $scope.data.difficult = {};
        //知识点
        $scope.data.knowledges = [];
        //已经选择的范围
        $scope.data.rangeSelected = [];
        //被选择的标签
        $scope.data.selectedKeys = [];

        contentService.User_OC_List(function (data) {
            if (data.d) {
                $scope.courses = data.d;
                $scope.data.course = $scope.courses[0];
            }
        });

        assistService.Resource_Dict_ExerciseType_Get(function (data) {
            if (data.d) {
                $scope.exerciseTypes = data.d;
                $scope.data.exerciseType = $scope.exerciseTypes[0];
            }
        });

        assistService.Resource_Dict_Diffcult_Get(function (data) {
            if (data.d) {
                $scope.difficulties = data.d;
                $scope.data.difficult = $scope.difficulties[0];
            }
        });

        assistService.Resource_Dict_Scope_Get(function (data) {
            if (data.d) $scope.ranges = data.d;
        });

        $scope.$watch('data.course', function (v) {
            knowledgeService.Ken_List({ OCID: v.OCID }, function (data) {
                if (data.d) $scope.data.knowledges = data.d;
            });
            assistService.Key_List({ OCID: v.OCID }, function (data) {
                if (data.d) {
                    $scope.keys = data.d;
                    $scope.keySelection = $scope.keys[0];
                    $scope.data.selectedKeys.length = 0;
                }
            });
        });

        $scope.$watch('data.exerciseType', function (v) {
            var param = { ExerciseID: -1 };
            switch (v.id) {
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
        });

        $scope.addKey = function () {
            if ($scope.keySelection) {
                var length = $scope.data.selectedKeys.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.data.selectedKeys[i].KeyID === $scope.keySelection.KeyID) {
                        return;
                    }
                }
                $scope.data.selectedKeys.push($scope.keySelection);
            }
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

        $scope.removeKnow = function (knowledge) {
            var length = $scope.data.knowledges.length;
            for (var i = 0; i < length; i++) {
                if ($scope.data.knowledges[i].KenID === knowledge.KenID) {
                    $scope.data.knowledges.splice(i, 1);
                    break;
                }
            }
        }

        var findByRange = function (rangeId) {
            var length = $scope.data.rangeSelected.length;
            for (var i = 0; i < length; i++) {
                if ($scope.data.rangeSelected[i].id === rangeId) {
                    return i;
                }
            }
            return -1;
        }

        $scope.toggleRange = function (range) {
            var index = findByRange(range.id);
            if (index === -1) {
                $scope.data.rangeSelected.push(range);
            } else {
                $scope.data.rangeSelected.splice(index, 1);
            }
            console.log($scope.data.rangeSelected);
        }

        $scope.doChanged = function () {
            $scope.$broadcast('willExerciseChange', {});
        }

  

        $scope.submit = function () { 
            $scope.$broadcast('willSubmit');
        }

        $scope.preview = function () {
            $scope.$broadcast('willPreview', $scope.data);
        }

        $scope.$on('willSave', function (event, data) {
            //顶部关联项            
            data.exercisecommon.exercise.CourseID = $scope.data.course.CourseID;//课程编号
            data.exercisecommon.exercise.Diffcult = parseInt($scope.data.difficult.id);//难度等级
            var scope = 0;
            for (var i = 0; i < $scope.data.rangeSelected.length; i++) {
                scope += parseInt($scope.data.rangeSelected[i].id);
            }

            data.exercisecommon.exercise.Scope = scope;
            //key关键字
            for (var i = 0; i < $scope.data.selectedKeys.length; i++) {
                data.exercisecommon.keylist.push($scope.data.selectedKeys[i]);
            }
            //ken知识点
            for (var i = 0; i < $scope.data.knowledges.length; i++) {
                data.exercisecommon.kenlist.push($scope.data.knowledges[i]);
            }
            var v = angular.toJson(data);

            exerciseService.Exercise_ADD(v, function (data) {
                if (data.d) {
                    alert('提交成功！');                    
                }
            });
        })

    }]);

//简答题
appExercise.controller('ShortAnswerCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {

    $scope.$on('willExerciseChange', function (event, changeParam) {
    });
     
    $scope.$on('willSubmit', function (event) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event, exerciseData) {

    });

    $scope.model = {};//ExerciseInfo对象
    
    var answer = { IsCorrect: false, Conten: '' };
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                exerciseService.ExerciseInfo_Get($scope.ExerciseID, function (data) {
                    $scope.model = data.d;
                });
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { IsCorrect: false, Conten: '' };
                $scope.model.exercisechoicelist.push(answer);
                $scope.model.exercisecommon.exercise.ExerciseType = 10;//简答题
            });
        }
    }
    $scope.isRandChange = function (IsRand) {
        $scope.model.exercisecommon.exercise.IsRand = !!IsRand;
    }

    //添加选项
    $scope.AddAnswer = function () {
        answer = { IsCorrect: false, Conten: '' };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.exercisechoicelist.splice(i, 1);
            }
        }
    }
    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        item.IsCorrect = item.IsCorrect ? false : true;
    }
    init();
}]);

//听力题
appExercise.controller('ListeningCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象
    
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID
    
    var answer = { IsCorrect: false, Conten: '' };
    var conten = { Conten: '' };
    

    var init = function () {
        if ($scope.ExerciseID > -1) {

        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                answer = { IsCorrect: false, Conten: '' };
                $scope.model.exercisechoicelist.push(answer);
                $scope.model.exercisecommon.exercise.ExerciseType = 12;//简答题
                $scope.model.exercisecommon.exercise.ExerciseID = $scope.ExerciseID;

                
                $scope.model.Children.exercisechoicelist = [];
                conten = { Conten: '' };
                $scope.model.Children.exercisechoicelist.push(conten);
            });
        }       
    }

    $scope.isRandChange = function (IsRand) {
        $scope.model.exercisecommon.exercise.IsRand = !!IsRand;
    }

    //添加选项
    $scope.AddAnswer = function () {
        answer = { IsCorrect: false, Conten: '' };
        $scope.model.exercisechoicelist.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            if ($scope.model.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.exercisechoicelist.splice(i, 1);
            }
        }
    }

    //添加选项
    $scope.AddAnswerSmall = function () {
        conten = { Conten: '' };
        $scope.model.Children.exercisechoicelist.push(conten);
    }
    //删除选项
    $scope.delSmall = function (item) {
        for (var i = 0; i < $scope.model.Children.exercisechoicelist.length; i++) {
            if ($scope.model.Children.exercisechoicelist[i].$$hashKey == item.$$hashKey) {
                $scope.model.Children.exercisechoicelist.splice(i, 1);
            }
        }
    }
    
    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        item.IsCorrect = item.IsCorrect ? false : true;
    }

    init();
}]);

//问答题
appExercise.controller('QuesanswerCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event, exerciseData) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var init = function () {       
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 10;//问答题
            $scope.model.exercisecommon.exercise.ExerciseID = $scope.ExerciseID;
            $scope.textarea = 0;//切换试题解析和得分点
        });
    }    
    //切换解析和得分点
    $scope.tabTextarea = function () {
        $scope.textarea = $scope.textarea == 1 ? 0 : 1;
        $scope.model.exercisecommon.exercise.Analysis = $scope.textarea == 0 ? $scope.model.exercisecommon.exercise.Analysis : null;
        $scope.model.exercisecommon.exercise.ScorePoint = $scope.textarea == 1 ? $scope.model.exercisecommon.exercise.ScorePoint : null;
    }
    init();
}]);

//名词解释
appExercise.controller('NounCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.Exercises = [];//答案数组
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID

    var model = { Conten: '', Answer: '' };

    var init = function () {
        $scope.Exercises.push(model);
    }

    //添加选项
    $scope.Add = function () {

        $scope.Exercises.push(model);
    }
    //删除选项
    $scope.Del = function (item) {
        for (var i = 0; i < $scope.Exercises.length; i++) {
            if ($scope.Exercises[i].$$hashKey == item.$$hashKey) {
                $scope.Exercises.splice(i, 1);
            }
        }
    }
    $scope.Attachment = {};//附件对象

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 19;//名词解释            
        });
    }
    init();
}]);

//判断题
appExercise.controller('TruefalseCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });
    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    $scope.ExerciseID = parseInt($stateParams.ExerciseID);//习题ID


    var init = function () {
        if ($scope.ExerciseID > -1) {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                exerciseService.ExerciseInfo_Get($scope.ExerciseID, function (data) {
                    $scope.model = data.d;
                });
            });
        } else {
            exerciseService.Exercise_Model_Info(function (data) {
                $scope.model = data.d;
                $scope.model.exercisecommon.exercise = {};//Exercise对象
                $scope.model.exercisecommon.exercise.ExerciseType = 1;//判断题                
                $scope.model.exercisechoicelist = [];//答案数组
                $scope.ExerciseChoice = { IsCorrect: true };
                $scope.model.exercisechoicelist.push($scope.ExerciseChoice);
            });
        }
    }
    init();

    $scope.answeChange = function (answer) {
        $scope.model.exercisechoicelist[0].IsCorrect = !answer;
    }
}]);

//填空题
appExercise.controller('FillBlankCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    var answer = { Conten: '' };

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise.ExerciseType = 5;//填空题            
            answer = { Conten: '' };
            $scope.model.exercisechoicelist.push(answer);
        });
    }

    //添加选项
    $scope.Add = function () {
        answer = { Conten: '' };
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

//填空客观题
appExercise.controller('FillBlank2Ctrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        for (var i = 0; i < $scope.model.exercisechoicelist.length; i++) {
            $scope.model.exercisechoicelist[i].Conten = $scope.model.exercisechoicelist[i].Conten +
                ',' + $scope.model.exercisechoicelist[i].Spare;
        }
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    var answer = { Conten: '', Spare: '' };

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise.ExerciseType = 4;//填空客观题            
            answer = { Conten: '', Spare: '' };
            $scope.model.exercisechoicelist.push(answer);
        });
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
        var choice = {};
        for (var i = 0; i < $scope.list.length; i++) {
            choice = { Conten: $scope.list[i].Conten, grou: $scope.list[i].grou };
            $scope.model.exercisechoicelist.push(choice);
            choice = { Conten: $scope.list[i].Answer, grou: $scope.list[i].grou };
            $scope.model.exercisechoicelist.push(choice);
        }
        $scope.$emit('willSave', $scope.model);
        $scope.model = {};
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象


    var grou = 1;//连线分组
    var answer = {};

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 6;//连线题
            $scope.list = [];//答案数组起始
            $scope.model.exercisechoicelist = [];//答案数组最终
            answer = { Conten: '', Answer: '', grou: grou };
            $scope.list.push(answer);
            answer = { Conten: '', grou: 0 };
            $scope.model.exercisechoicelist.push(answer);
        });
    }
    //添加干扰项
    $scope.AddInterference = function () {
        answer = { Conten: '', grou: 0 };
        $scope.model.exercisechoicelist.push(answer);
        console.log($scope.model.exercisechoicelist);
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
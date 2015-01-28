'use strict';

var appExercise = angular.module('app.exercise.controllers', [
    'checklist-model',
    'app.exercise.services'
]);

appExercise.controller('ExerciseCtrl', ['$scope', 'exerciseService', 'contentService', 'knowledgeService',
    function ($scope, exerciseService, contentService, knowledgeService) {
        //课程
        $scope.courses = [];
        //试题类型
        $scope.exerciseTypes = [];
        //难易程度
        $scope.difficulties = [];
        //范围
        $scope.ranges = [];
        //标签
        $scope.lables = [];
        //知识点
        $scope.knowledges = [];

        $scope.course = {};
        $scope.exerciseType = {};
        $scope.difficult = {};
        $scope.range = {};
        $scope.rangeSelected = {
            Items: []
        };
        contentService.User_OC_List(function (data) {
            if (data.d) $scope.courses = data.d;
        }); 

        exerciseService.Resource_Dict_ExerciseType_Get(function (data) {
            if (data.d) $scope.exerciseTypes = data.d;
        });

//<<<<<<< HEAD
//    $scope.submit = function () {
//        $scope.$broadcast('willSubmit');
//    }

//    $scope.preview = function () {
//        $scope.$broadcast('willPreview');
//    }


//}]);
//=======
        exerciseService.Resource_Dict_Diffcult_Get(function (data) {
            if (data.d) $scope.difficulties = data.d;
        });

        exerciseService.Resource_Dict_Scope_Get(function (data) {
            if (data.d) $scope.ranges = data.d;
        });

        $scope.$watch('course', function(v){
            knowledgeService.Ken_List({ OCID: v.OCID }, function (data) {
                if (data.d) $scope.knowledges = data.d;
            });
        });

        $scope.doChanged = function () {
            $scope.$broadcast('willExerciseChange', {});
        }

        $scope.submit = function () {
            $scope.$broadcast('willSubmit');
        }
        
        $scope.preview = function () {
            $scope.$broadcast('willPreview');
        }

   
    }]);

//简答题
appExercise.controller('ShortAnswerCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {

    $scope.$on('willExerciseChange', function (event, changeParam) {
    });

    $scope.$on('willSubmit', function (event) {
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象
    $scope.model.exercisechoicelist = [];//答案数组
    var answer = { IsCorrect: false, Conten: '' };
    $scope.Attachment = {};//附件对象

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            answer = { IsCorrect: false, Conten: '' };
            $scope.model.exercisechoicelist.push(answer);
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 10;//简答题
            //$scope.model.exercisecommon.exercise.IsRand = false;//选是否项乱序
            //$scope.model.exercisecommon.exercise.Conten = '';//题干;
        });
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
        console.log($scope.model);
        console.log($scope.ExerciseAnswercards);
        console.log($scope.Contens);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象

    $scope.model.ExerciseType = 12;//听力题
    $scope.model.IsRand = false;//选是否项乱序
    $scope.ExerciseAnswercards = [];//答案数组

    $scope.Contens = [];//填空答案数组
    var answer = { IsCorrect: false, Conten: '' };
    var conten = { Conten: '' };

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.ExerciseAnswercards.push(answer);
            $scope.Contens.push(conten);
        });
    }

    $scope.isRandChange = function (IsRand) {
        $scope.model.IsRand = !IsRand;
    }

    //添加选项
    $scope.AddAnswer = function () {
        answer = { IsCorrect: false, Conten: '' };
        $scope.ExerciseAnswercards.push(answer);
    }
    //删除选项
    $scope.del = function (item) {
        for (var i = 0; i < $scope.ExerciseAnswercards.length; i++) {
            if ($scope.ExerciseAnswercards[i].$$hashKey == item.$$hashKey) {
                $scope.ExerciseAnswercards.splice(i, 1);
            }
        }
    }

    //添加选项，填空答案
    $scope.AddConten = function () {
        conten = { Conten: '' };
        $scope.Contens.push(conten);
    }
    //删除选项,填空答案
    $scope.delConten = function (item) {
        for (var i = 0; i < $scope.Contens.length; i++) {
            if ($scope.Contens[i].$$hashKey == item.$$hashKey) {
                $scope.Contens.splice(i, 1);
            }
        }
    }

    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        item.IsCorrect = item.IsCorrect == 1 ? 0 : 1;
    }

    init();
}]);

//问答题
appExercise.controller('QuesanswerCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//ExerciseInfo对象
    $scope.Attachment = {};//附件对象

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 10;//问答题
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
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.Attachment = {};//附件对象

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 14;//名词解释            
        });
    }
    init();
}]);

//判断题
appExercise.controller('TruefalseCtrl', ['$scope', 'exerciseService', '$stateParams', function ($scope, exerciseService, $stateParams) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });
    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象



    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
            $scope.model.exercisecommon.exercise.ExerciseType = 1;//判断题
            $scope.model.exercisechoicelist = [];//答案数组
            $scope.ExerciseChoice = { IsCorrect: true };
            $scope.model.exercisechoicelist.push($scope.ExerciseChoice);
        });
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
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    $scope.model.exercisechoicelist = [];//答案数组
    var answer = { Conten: '' };

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
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
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};//Exercise对象
    $scope.Attachment = {};//附件对象
    $scope.model.exercisechoicelist = [];//答案数组
    var answer = { Conten: '', Spare: '' };

    var init = function () {
        exerciseService.Exercise_Model_Info(function (data) {
            $scope.model = data.d;
            $scope.model.exercisecommon.exercise = {};//Exercise对象
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
        exerciseService.Exercise_ADD($scope.model, function (data) {
            if (data.d) {
                alert('提交成功！');
                init();
            }
        });
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
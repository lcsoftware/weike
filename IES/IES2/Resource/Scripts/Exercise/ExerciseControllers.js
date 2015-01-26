'use strict';

var appExercise = angular.module('app.exercise.controllers', [
    'app.exercise.services'
]);

appExercise.controller('ExerciseCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
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
appExercise.controller('ShortAnswerCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.ExerciseAnswercards);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 10;//简答题
    $scope.model.IsRand = 0;//选是否项乱序    
    $scope.ExerciseAnswercards = [];//答案数组

    $scope.isRandChange = function (IsRand) {
        $scope.model.IsRand = IsRand ? 1 : 0;
    }

    //添加选项
    $scope.AddAnswer = function () {
        var answer = {IsCorrect:0,Conten:''};
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
    //是否是正确答案
    $scope.isCorrectChange = function (item) {
        item.IsCorrect = item.IsCorrect == 1 ? 0 : 1;
    }
}]);

//听力题
appExercise.controller('ListeningCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.ExerciseAnswercards);
        console.log($scope.Contens);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 12;//听力题
    $scope.model.IsRand = 0;//选是否项乱序
    $scope.ExerciseAnswercards = [];//答案数组
    $scope.model.ChoiceNum = 0;//选项
    $scope.Contens = [];//填空答案数组

    $scope.isRandChange = function (IsRand) {
        $scope.model.IsRand = IsRand ? 1 : 0;
    }

    //添加选项
    $scope.AddAnswer = function () {
        var answer = { IsCorrect: 0, Conten: '' };
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
        var conten = { Conten: '' };
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
}]);

//问答题
appExercise.controller('QuesanswerCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//名词解释
appExercise.controller('NounCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.Exercises);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 14;//阅读理解
    $scope.Exercises = [];//答案数组
    
    //添加选项
    $scope.Add = function () {
        var model = { Conten: '', Answer: '' };
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
}]);

//判断题
appExercise.controller('TruefalseCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
    });

    $scope.$on('willPreview', function (event) {

    });
    $scope.model = {};
    $scope.model.ExerciseType = 1;//判断题
    $scope.model.answer = 1;//正确

    $scope.answeChange = function (answer) {
        $scope.model.answer = answer == 0 ? 1 : 0;
    }
}]);

//填空题
appExercise.controller('FillBlankCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.Exercises);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 5;//填空题
    $scope.Exercises = [];//答案数组

    //添加选项
    $scope.Add = function () {
        var model = { Answer: '' };
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
}]);

//填空客观题
appExercise.controller('FillBlank2Ctrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.Exercises);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 4;//填空客观题    
    $scope.Exercises = [];//答案数组

    //添加选项
    $scope.Add = function () {
        var model = { Conten: '', Answer: '' };
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
}]);

//连线题
appExercise.controller('ConnectionCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
    $scope.$on('willExerciseChange', function (event, changeParam) {

    });

    $scope.$on('willSubmit', function (event) {
        console.log($scope.model);
        console.log($scope.Exercises);
    });

    $scope.$on('willPreview', function (event) {

    });

    $scope.model = {};
    $scope.model.ExerciseType = 6;//连线题    
    $scope.Exercises = [];//答案数组

    //添加选项
    $scope.Add = function () {
        var model = { Conten: '', Answer: '' };
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
}]);
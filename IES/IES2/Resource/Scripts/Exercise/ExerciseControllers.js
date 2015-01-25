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
}]);
//简答题
appExercise.controller('ShortAnswerCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {
<<<<<<< HEAD
    $scope.$on('willExerciseChange', function (changeParam) {
        
=======
    $scope.$on('willExerciseChange', function (event, changeParam) {

>>>>>>> a38840aa68651bbf73d492e1362df917049b43a9
    });
    $scope.model = {};
    $scope.model.ExerciseType = 10;
    $scope.model.IsRand = 0;
    $scope.ExerciseAnswercards = [];//答案数组

    $scope.isRandChange = function (IsRand) {
        $scope.model.IsRand = IsRand ? 1 : 0;
        console.log($scope.model.IsRand);
    }

    //添加选项
    $scope.AddAnswer = function () {
        var answer = {};
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
}]);

//听力题
appExercise.controller('ListeningCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//问答题
appExercise.controller('QuesanswerCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//名词解释
appExercise.controller('NounCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//判断题
appExercise.controller('TruefalseCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//填空题
appExercise.controller('FillBlankCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//填空客观题
appExercise.controller('FillBlank2Ctrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);

//连线题
appExercise.controller('ConnectionCtrl', ['$scope', 'exerciseService', function ($scope, exerciseService) {

}]);
'use strict';
var DoHomeWorkModule = angular.module('app.test', []);
DoHomeWorkModule.controller('DoHomeWorkController', ['$scope', '$state', 'MarkingProviderUrl', function ($scope, $state, MarkingProviderUrl) {
    //获取试卷题目
    var PaperInfo_Get = function (paperid) {
        // var paperid = 11;
        var url = MarkingProviderUrl + "/PaperInfo_Get";
        var param = { PaperID: paperid };
        $scope.baseService.post(url, param, function (data) {
            $scope.paper = data.d.paper;
            $scope.papergrouplist = data.d.papergrouplist;
            $scope.exerciselist = data.d.exerciselist;
            $scope.ExerciseChoices = data.d.ExerciseChoices;


        });
    }

    //获取考试详细
    var Test_Get = function () {
        var testid = 34;
        var url = MarkingProviderUrl + "/Test_Get";
        var param = { TestID: testid };
        $scope.baseService.post(url, param, function (data) {
            $scope.test = data.d;
            if (data.d.ScaleType == 1) {
                $scope.ScaleTypeName = "百分制";
            } else if (data.d.ScaleType == 2) {
                $scope.ScaleTypeName = "通过制";
            }
            else if (data.d.ScaleType == 3) {
                $scope.ScaleTypeName = "等级制(中文)";
            }
            else if (data.d.ScaleType == 4) {
                $scope.ScaleTypeName = "等级制(英文)";
            }
            PaperInfo_Get($scope.test.PaperID);


        });

    }
    var Init = function () {
      
        Test_Get();
        //PaperInfo_Get();
    }
    Init();
}]);
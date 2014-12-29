'use strict';

var appPaper = angular.module('app.paper.controllers', ['app.paper.services']);

appPaper.controller('PaperListCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {
    $scope.$emit('onActived', 2);

    $scope.keyword = '';

    $scope.paperTypes = [];
    $scope.conditionPaperTypes = [];

    $scope.typeSelection = -1;
    $scope.createrSelection = -1; 

    $scope.$watch('typeSelection', function (newValue) {
        find(newValue, $scope.createrSelection);
    });

    $scope.$watch('createrSelection', function (newValue) {
        find($scope.typeSelection, newValue);
    });

    $scope.typeChanged = function (v) {
        $scope.typeSelection = v;
    }

    $scope.createrChanged = function (v) {
        $scope.createrSelection = v;
    }

    var find = function (typeSelection, createrSelection) { 
        console.log(typeSelection, createrSelection);
    }
    ///初始化试卷类型
    PaperService.getPaperTypes(function (data) {
        $scope.paperTypes = data;
        if ($scope.paperTypes.length > 0) {
            var item = {};
            angular.copy($scope.paperTypes[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.conditionPaperTypes.push(item);
            angular.forEach($scope.paperTypes, function (v) {
                $scope.conditionPaperTypes.push(v);
            });
        }
    });

}]); 
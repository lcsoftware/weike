'use strict';

var appPaper = angular.module('app.paper.controllers', ['app.paper.services']);

appPaper.controller('PaperListCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {
    $scope.$emit('onActived', 2);

    $scope.paperTypes = [];
    $scope.conditionPaperTypes = [];

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
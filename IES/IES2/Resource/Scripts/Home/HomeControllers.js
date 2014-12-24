'use strict';

var homeModule = angular.module('app.home.controllers', []);

homeModule.controller('HomeCtrl', ['$scope', '$state', function ($scope, $state) {
    $scope.toResource = function () {
        $state.go('content.resource');
    }
}]);

/// 错误处理
homeModule.controller('Error404Ctrl', ['$scope', function () {

}]);
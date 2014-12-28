'use strict';

var appResource = angular.module('app.resource.controllers', []);

appResource.controller('ResourceCtrl', ['$scope', function ($scope) {
    $scope.$emit('onActived', 1);

    $scope.actived = 2;

    $scope.$on("onActived", function (event, active) {
        $scope.actived = active;
    });
}]);


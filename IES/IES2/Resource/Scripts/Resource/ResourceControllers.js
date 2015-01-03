'use strict';

var appResource = angular.module('app.resource.controllers', []);

appResource.controller('ResourceCtrl', ['$scope', function ($scope) {
    $scope.$emit('onActived', 1);
}]);


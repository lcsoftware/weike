'use strict';

angular.module('app.content.controllers', [])
    .controller('ContentCtrl', ['$scope', function ($scope) {
        $scope.actived = 2;

        $scope.$on("onActived", function (event, active) {
            $scope.actived = active;
        });

    }]);


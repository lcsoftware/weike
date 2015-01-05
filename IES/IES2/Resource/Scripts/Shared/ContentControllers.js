'use strict';

angular.module('app.content.controllers', [])
    .controller('ContentCtrl', ['$scope', function ($scope) {
        $scope.actived = {};

        $scope.$on("onActived", function (event, active) {
            $scope.actived = active;
            console.log($scope.actived);
        });

    }]);


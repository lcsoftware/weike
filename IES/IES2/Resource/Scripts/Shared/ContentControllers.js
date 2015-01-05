'use strict';

angular.module('app.content.controllers', [])
    .controller('ContentCtrl', ['$scope', function ($scope) {
        $scope.actived = 'B21';

        $scope.$on("onActived", function (event, active) {
            $scope.actived = '\'' +  active + '\'';
            console.log($scope.actived);
        });

    }]);


'use strict';

angular.module('app.statCtrl', [])

    // Path: /
    .controller('statController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
    }]);


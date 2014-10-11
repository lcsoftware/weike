'use strict';

var aDirective = angular.module('app.directives', []);

aDirective.directive('appVersion', ['version', function (version) {
    return function (scope, elm, attrs) {
        elm.text(version);
    };
}]);
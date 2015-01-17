'use strict';

var directiveApp = angular.module('app.directives', []);

directiveApp.directive('appVersion', ['version', function (version) {
    return function (scope, elm, attrs) {
        elm.text(version);
    };
}]);

directiveApp.directive('dialogShow', function () {
    return {
        restrict: 'EA',
        scope: {
            dialogId: '@'
        },
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var elem = '#' + scope.dialogId;
                $(elem ).show();
            });
        }
    }
}); 
'use strict';

var directiveApp = angular.module('app.directives', []);

directiveApp.directive('appVersion', ['version', function (version) {
    return function (scope, elm, attrs) {
        elm.text(version);
    };
}]);

directiveApp.directive('onFinishRenderFilters', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
});

directiveApp.directive('showDialog', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', tanchu);
            //弹出层方法
            function tanchu() {
                var oHeight = $(document).height();
                var oScroll = $(window).scrollTop();
                $('.pop_bg').show().css('height', oHeight);
                $('.pop_600').show().css('top', oScroll + 200);
            }
        }
    }
});
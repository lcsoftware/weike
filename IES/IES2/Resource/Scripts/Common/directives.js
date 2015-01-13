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
        scope: {
            bgClass: '@',
            popClass: '@',
            cancelClass: '@',
        },
        link: function (scope, element, attrs) {
            element.bind('click', tanchu); 
            var cancelCls = '.' + scope.cancelClass;
            $(cancelCls).bind('click', function () {
                var bgCls = '.' + scope.bgClass;
                var popCls = '.' + scope.popClass;
                $(bgCls).hide();
                $(popCls).hide();
            }); 

            //弹出层方法
            function tanchu() {
                var oHeight = $(document).height();
                var oScroll = $(window).scrollTop();
                var bgCls = '.' + scope.bgClass;
                var popCls = '.' + scope.popClass; 
                $(bgCls).show().css('height', oHeight);
                $(popCls).show().css('top', oScroll + 200);
            }
        }
    }
}); 
'use strict';

var scoreApp = angular.module('app.controllers', ['app.services']);

// Path: /
scoreApp.controller('HomeController', ['$scope', '$location', 'auth', function ($scope, $location, auth) {
    $scope.$root.title = '成绩分析系统 | 首页';
    $scope.logout = function () {
        auth.logout();
        $location.path('/login').replace();
    }
    $scope.go = function (url) {
        $location.path(url);
    }
    $scope.auth = auth;
}]);

// Path: /login
scoreApp.controller('LoginController', [
    '$scope', '$location', '$window',
    'systemService', 'util','auth',
    function ($scope, $location, $window
        , systemService, util, auth) {

        $scope.$root.title = '成绩分析系统 | 登录';
        $scope.userName = 'system';
        $scope.password = '888';

        ///登录验证
        $scope.login = function (userName, password) {
            systemService.verify(userName, password, function (data) {
                if (data.d != null) {
                    auth.setUser(data.d);
                    systemService.initSystem(auth.getUser(), function (data) {
                        if (data) {
                            auth.setUser(auth.getUser());
                            $location.path('/').replace();
                        } else {
                            util.showAlert('登录提示', '您没有任何权限，不能登录！');
                        }
                    });
                } else {
                    util.showAlert('登录提示', '用户名/密码错误！');
                }
            });
        };
    }]);

// Path: /error/404
scoreApp.controller('Error404Controller', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'Error 404: Page Not Found';
}]);
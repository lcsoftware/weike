'use strict';

angular.module('app.controllers', ['app.utils'])

    // Path: /
    .controller('HomeController', ['$scope', '$location', 'menuService', function ($scope, $location, menuService) {
        $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
       
    }])

    // Path: /login
    .controller('LoginController', ['$scope', '$location', '$window', 'userService', 'dialogUtils',
        function ($scope, $location, $window, userService, dialogUtils) {
            $scope.$root.title = 'AngularJS SPA | Sign In';
            $scope.userName = 'system';
            $scope.password = '888'; 
            $scope.login = function (userName, password) {
                userService.verify(userName, password, function (data) {
                    if (data.d != null) {
                        $location.path('/').replace();
                    }
                    else {
                        dialogUtils.info('用户名密码错误，请重试！');
                    }
                })
            };
        }])

    // Path: /error/404
    .controller('Error404Ctrl', ['$scope', '$location', '$window', function ($scope, $location, $window) {
        $scope.$root.title = 'Error 404: Page Not Found';
    }]);
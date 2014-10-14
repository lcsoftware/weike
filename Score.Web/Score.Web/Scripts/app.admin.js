'use strict';

var aService = angular.module('app.admin', ['app.services', 'app.utils', 'ui.router']);

aService.controller('LoginController', ['userService', 'cookieService', 'dialogUtils', '$state',
    function (userService, cookieService, dialogUtils, $state) {

    $scope.verify = function (userName, pwd) {
        userService.verify(userName, pwd, function (data) {
            var user = data.d;
            if (user !== null) {
                cookieService.setUser(user);
                $state.go('home');
            } else {
                dialogUtils.info('用户名/密码错误，请重新输入！');
            }
        });
    }
}]);
'use strict';

var appAdmin = angular.module('app.admin', []);

// Path: /UserEdit  用户(组)维护
appAdmin.controller('UserEditController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  升留级处理
appAdmin.controller('RightQueryController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  转换为学籍成绩
appAdmin.controller('CJtoXJController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  从学籍成绩转换过来
appAdmin.controller('XJtoCJController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  数据备份与恢复
appAdmin.controller('DBbackupController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  学生编号导入
appAdmin.controller('LogUserController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  生成上传数据文件
appAdmin.controller('SendMailController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

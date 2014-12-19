'use strict';

var appModule = angular.module('app', [
    'ui.router',
    'app.filters',
    'app.directives',
    'app.services',
    'app.home',
    'app.user'
]);

appModule.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

        $stateProvider
            //index 
            .state('home', { url: '/', templateUrl: '/views/index', controller: 'HomeController' })

            //登录
            .state('login', { url: '/login', layout: 'basic', templateUrl: '/views/User/Login', controller: 'UserController' })

            //注册
            .state('register', { url: '/register', templateUrl: '/views/User/Register', controller: 'UserController' })

            //用户列表
            .state('userList', { url: '/userList', templateUrl: '/views/User/UserList', controller: 'UserListController' })

            .state('otherwise', {
                url: '*path',
                templateUrl: '/views/404',
                controller: 'Error404Ctrl'
            });

        $locationProvider.html5Mode(true);

    }])

    .run(['$templateCache', '$rootScope', '$state', '$stateParams','baseService',
        function ($templateCache, $rootScope, $state, $stateParams, baseService) {

            var view = angular.element('#ui-view');
            $templateCache.put(view.data('tmpl-url'), view.html());

            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
            $rootScope.baseService = baseService;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $rootScope.layout = toState.layout;
            });

            $rootScope.$on('$locationChangeStart', function (event) {
            })
        }]);
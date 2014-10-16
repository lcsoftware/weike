'use strict';

angular.module('app', [
    'ui.router',
    'app.filters',
    'app.services',
    'app.directives',
    'app.controllers'
])

    .config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {
 
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/views/index',
                controller: 'HomeController' 
            })
            .state('login', {
                url: '/login',
                layout: 'basic',
                templateUrl: '/views/login',
                controller: 'LoginController'
            })
            .state('otherwise', {
                url: '*path',
                templateUrl: '/views/404',
                controller: 'Error404Ctrl'
            });

        $locationProvider.html5Mode(true);

    }])

    .run(['$templateCache', '$rootScope', '$state', '$stateParams', '$location', 'userService','menuService',
        function ($templateCache, $rootScope, $state, $stateParams, $location, userService, menuService) {

        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.logout = function () {
            userService.logout(function (data) {
                $location.path('login').replace();
            });
        }

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            menuService.readMenu(function (data) {
                $rootScope.menus = JSON.parse(data.d);
            });

            userService.getUser(function (data) {
                if (data.d !== '') {
                    var user = JSON.parse(data.d);
                    $rootScope.name = user.Name;
                }
            })
            $rootScope.layout = toState.layout;
        });

        $rootScope.$on('$locationChangeStart', function (event) {
            userService.isAuthorized(function (data) {
                if (data.d === '') {
                    $location.path('/login').replace();
                }
            });
        })
    }]);
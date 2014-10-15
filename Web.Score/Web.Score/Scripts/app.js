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

    .run(['$templateCache', '$rootScope', '$state', '$stateParams', 'cookieService',
        function ($templateCache, $rootScope, $state, $stateParams, cookieService) {

        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            if (!cookieService.isAuthorized()) {
                $location.path('/login');
            }
            $rootScope.layout = toState.layout;
        });

        $rootScope.$on('$stateChangeSuccess', function (event, toState) {
            if (!cookieService.isAuthorized()) {
                $location.path('/login').replace();
            }
            $rootScope.layout = toState.layout;
        });
    }]);
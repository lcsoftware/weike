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

    .run(['$templateCache', '$rootScope', '$state', '$stateParams', '$location', 'userService',
        function ($templateCache, $rootScope, $state, $stateParams, $location, userService) {

        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
         
            $rootScope.layout = toState.layout;
        });

        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            if (toState.name !== 'login'){
                userService.isAuthorized(function (data) {
                    if (!data.d)
                    {
                        $location.path('/login').replace();
                    }
                });
            } 
            $rootScope.layout = toState.layout;
        });
    }]);
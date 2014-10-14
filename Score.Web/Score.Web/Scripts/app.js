'use strict';

var app = angular.module('app', [
    'ui.router',
    'app.filters',
    'app.services',
    'app.directives',
    'app.controllers'
]);

app.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {
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
}]);

app.run(['$templateCache', '$rootScope', '$state', '$stateParams','cookieService',
    function ($templateCache, $rootScope, $state, $stateParams, cookieService) {

        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            if (!cookieService.isAuthorize()) {
                $state.go('login');
            }
        });

        $rootScope.$on('$stateChangeSuccess', function (event, toState) {

            $rootScope.layout = toState.layout;
        });
    }]);
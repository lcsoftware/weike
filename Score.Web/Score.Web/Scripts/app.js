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
            controller: 'HomeCtrl'

        })
        .state('about', {
            url: '/about',
            templateUrl: '/views/about',
            controller: 'AboutCtrl'
        })
        .state('login', {
            url: '/login',
            layout: 'basic',
            templateUrl: '/views/login',
            controller: 'LoginCtrl'
        })
        .state('otherwise', {
            url: '*path',
            templateUrl: '/views/404',
            controller: 'Error404Ctrl'
        });

    $locationProvider.html5Mode(true);

}]);

app.run(['$templateCache', '$rootScope', '$state', '$stateParams',
    function ($templateCache, $rootScope, $state, $stateParams) {

        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeSuccess', function (event, toState) {

            $rootScope.layout = toState.layout;
        });
    }]);
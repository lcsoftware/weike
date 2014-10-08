'use strict';

var app = angular.module('app', [
    'ui.router', 
    'app.filters',
    'app.services',
    'app.directives',
    'app.controllers',
    'app.system',
    'ui.tree'
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
        .state('system', {
            url: '/usergroup',
            templateUrl: '/views/System/user',
            controller: 'userController'
        });
    
        //.state('otherwise', {
        //    url: '*path',
        //    templateUrl: '/views/404',
        //    controller: 'Error404Ctrl'
        //});

    $locationProvider.html5Mode(true);

}])

app.run([
    '$templateCache',
    '$rootScope',
    '$state',
    '$stateParams',
    '$location',
    'auth',
    'systemService',
    function ($templateCache, $rootScope, $state, $stateParams, $location, auth, systemService) {
        var view = angular.element('#ui-view');
        $templateCache.put(view.data('tmpl-url'), view.html());

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

        $rootScope.$on('$stateChangeSuccess', function (event, toState) {
            if (!auth.isAuthorized())
            {
                $location.path('/login');
            } 
            $rootScope.layout = toState.layout;
        });

        $rootScope.$on('$stateChangeStart', function (event, toState) {
            $rootScope.layout = toState.layout;
        });

        $rootScope.$on('$locationChangeStart', function (event) {
            console.log('$locationChangeStart');
            //if (auth.menus == null || auth.menus.length == 0) {
            //    if (auth.isAuthorized())
            //    {
            //        systemService.initSystem(auth.getUser(), function () {
            //            console.log('page refreshed!');
            //        });
            //    }
            //}
        });
    }]);
//var myApp = angular.module("myApp", ['ui.router']);
 
//myApp.config(function ($stateProvider, $urlRouterProvider) {
 
//    $urlRouterProvider.when("", "/top");
 
//    $stateProvider
//       .state("top", {
//           url: "/top",
//           templateUrl: "top.html"
//       })
//       .state("top.Page1", {
//           url:"/Page1",
//           templateUrl: "Page1.html"
//       })
//       .state("top.Page2", {
//           url:"/Page2",
//           templateUrl: "Page2.html"
//       })
//       .state("top.Page3", {
//           url:"/Page3",
//           templateUrl: "Page3.html"
//       })
//        .state("top.Page1.Page1", {
//            url: "/Page1/Page1_1",
//            templateUrl: "Page1_1.html"
//        })

//    ;
//});
angular.module('myApp', ['ui.state'])
myApp.config(['$stateProvider', function ($stateProvider) {
        var home = {
                 name: 'home',
            url: '/',
            views:
                {
                    '':{
                        template: 'Hello {{name}}',
                        controller: ['$scope', function ($scope) { $scope.name = "World"; }]},
                    'foo': { template: 'bar'}
                };
        var details = {
            name: 'settings.details',
            parent: settings,
            url: '',
            views: {
                '': {   templateUrl: 'settings.details.html'},
                'hint': {   template: 'edit your details!' }
            };
        var quotes = {
            name: 'settings.quotes',
            parent: settings,
            url: '/quotes',
            views: {
                '': {
                    templateUrl: 'settings.quotes.html'
                },
                'hint': {
                    template: 'edit your quotes!'
                }
            };
        $stateProvider.state(home);
    }])



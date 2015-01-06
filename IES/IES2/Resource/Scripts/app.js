'use strict';

var appModule = angular.module('app', [
    'ui.router',
    'app.filters',
    'app.directives',
    'app.common.services',
    'app.home.controllers', 
    'app.user.controllers',
    'app.content.controllers',
    'app.resource.controllers',
    'app.paper.controllers',
    'app.knowledge.controllers',
    'app.exercise.controllers'
]);

appModule.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

    $stateProvider
        //index 
        .state('home', { url: '/', templateUrl: '/views/index', controller: 'HomeCtrl' }) 
        //登录
        .state('login', { url: '/login', layout: 'basic', templateUrl: '/views/User/Login', controller: 'UserCtrl' })
        //内容区
        .state('content', { url: '/content', abstract:true, templateUrl: '/views/Shared/Content', controller: 'ContentCtrl' })
        //我的资料
        .state('content.resource', { url: '/resource', templateUrl: '/views/Resource/ResourceList', controller: 'ResourceCtrl' })
        //试卷
        .state('content.paper', { url: '/paper', templateUrl: '/views/Paper/PaperList', controller: 'PaperListCtrl' })
        //新增智能试卷
        .state('content.newsmart', { url: '/paper', templateUrl: '/views/Paper/PaperList', controller: 'PaperListCtrl' })
        //新增智能试卷
        .state('content.addsmart', { url: '/addsmart', templateUrl: '/views/Paper/AddSmart', controller: 'PaperSmartCtrl' })
        //新增自测型试卷
        .state('content.addtest', { url: '/addtest', templateUrl: '/views/Paper/AddTest', controller: 'PaperTestCtrl' })
        //新增答题卡试卷
        .state('content.addsheet', { url: '/addsheet', templateUrl: '/views/Paper/AddSheet', controller: 'PaperSheetCtrl' })
        //习题
        .state('content.exercise', { url: '/exercise', templateUrl: '/views/Exercise/ExerciseList', controller: 'ExerciseListCtrl' })
        //知识点
        .state('content.ken', { url: '/Knowledge', templateUrl: '/views/Knowledge/KnowledgeList', controller: 'KnowledgeListCtrl' })


        .state('otherwise', {
            url: '*path',
            templateUrl: '/views/404',
            controller: 'Error404Ctrl'
        }); 

    $locationProvider.html5Mode(true);
}]);

appModule.run(['$templateCache', '$rootScope', '$state', '$stateParams', 'httpService',
        function ($templateCache, $rootScope, $state, $stateParams, httpService) {

            var view = angular.element('#ui-view');
            $templateCache.put(view.data('tmpl-url'), view.html());

            $rootScope.title = 'IES';
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.httpService = httpService;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $rootScope.layout = toState.layout;
            });

            $rootScope.$on('$locationChangeStart', function (event) {
            })
        }]);
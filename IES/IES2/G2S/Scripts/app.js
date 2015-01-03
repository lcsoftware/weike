'use strict';

var appModule = angular.module('app', [
    'ui.router',
    'app.filters',
    'app.directives',
    'app.services',
    'app.home',
    'app.user',
    'app.site',
    'app.oc.team',
    'app.oc.class',
    'app.fc',
    'app.courselive.score'

]);
// 'app.oc.team'
appModule.config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

    $stateProvider
        //index 
        .state('home', { url: '/', templateUrl: '/views/index', controller: 'HomeController' })

        //登录
        .state('login', { url: '/login', layout: 'basic', templateUrl: '/views/User/Login', controller: 'UserController' })

        //注册
        .state('register', { url: '/register', templateUrl: '/views/User/Register', controller: 'UserController' })

        //用户管理-教学团队
        .state('Teamindex', { url: 'OC/Team/index', templateUrl: '/views/OC/Team/index', controller: 'TeamController' })
        .state('classlist', { url: 'OC/Class/index', templateUrl: '/views/OC/Class/index', controller: 'ClassController' })

        //.state('detail.ExpenseList', {
        //    url: '/ExpenseList',
        //    templateUrl: '/views/Finance/ExpenseList',
        //    controller: 'ExpenseListCtrl'
        //}) topicdetail

        .state('forumindex', { url: '/CourseLive/Forum/index', templateUrl: '/views/CourseLive/Forum/index', controller: 'forumcontrol' })

        .state('topicdetail', { url: '/CourseLive/Forum/topicdetail', templateUrl: '/views/CourseLive/Forum/topicdetail', controller: 'forumcontrol' })

        .state('Site', { url: '/OC/Site/index', templateUrl: '/views/OC/Site/index', controller: 'SiteController' })

        //成绩类别管理
        .state('scoretype', { url: '/CourseLive/Score/ScoreType', templateUrl: '/views/CourseLive/Score/ScoreType', controller: 'ScoreTypeCtrl' })
        //成绩类别权重设置
        .state('scoreweight', { url: '/CourseLive/Score/ScoreWeight', templateUrl: '/views/CourseLive/Score/ScoreWeight', controller: 'ScoreWeightCtrl' })
        .state('scoremanageinfo', { url: '/CourseLive/Score/ScoreManageInfo', templateUrl: '/views/CourseLive/Score/ScoreManageInfo', controller: 'ScoreManageCtrl' })
        .state('scorewith', { url: '/CourseLive/Score/ScoreWith', templateUrl: '/views/CourseLive/Score/ScoreWith', controller: 'ScoreManageCtrl' })
        //翻转课堂首页
        .state('FC', { url: '/OC/FC/index', templateUrl: '/views/OC/FC/index', controller: 'FCController' })

        //用户列表
        .state('userList', { url: '/userList', templateUrl: '/views/User/UserList', controller: 'UserListController' })

        .state('otherwise', {
            url: '*path',
            templateUrl: '/views/404',
            controller: 'Error404Ctrl'
        });

    $locationProvider.html5Mode(true);

}])

    .run(['$templateCache', '$rootScope', '$state', '$stateParams', 'baseService',
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
'use strict';

var appModule = angular.module('app', [
    'ui.router',
    'app.filters',
    'app.directives',
    'app.custom.directives',
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

        //习题列表
        .state('content.exercise', { url: '/exercise', templateUrl: '/views/Exercise/ExerciseList', controller: 'ExerciseListCtrl' })        

        //试题
        .state('exercise', { url: '/exercise', abstract: true, templateUrl: '/views/Shared/Exercise', controller: 'ExerciseCtrl' })
        //简答题
        .state('exercise.shortanswer', { url: '/shortanswer/:ExerciseID', templateUrl: '/views/Exercise/ShortAnswer', controller: 'ShortAnswerCtrl' })
        //听力题
        .state('exercise.listening', { url: '/listening/:ExerciseID', templateUrl: '/views/Exercise/Listening', controller: 'ListeningCtrl' })
        //问答题
        .state('exercise.quesanswer', { url: '/quesanswer/:ExerciseID', templateUrl: '/views/Exercise/QuesAnswer', controller: 'QuesanswerCtrl' })
        //名词解释
        .state('exercise.noun', { url: '/noun/:ExerciseID', templateUrl: '/views/Exercise/Noun', controller: 'NounCtrl' })
        //判断题
        .state('exercise.truefalse', { url: '/truefalse/:ExerciseID', templateUrl: '/views/Exercise/TrueFalse', controller: 'TruefalseCtrl' })
        //填空题
        .state('exercise.fillblank', { url: '/fileblank/:ExerciseID', templateUrl: '/views/Exercise/FillBlank', controller: 'FillBlankCtrl' })
        //填空客观题
        .state('exercise.fillblank2', { url: '/fileblank2/:ExerciseID', templateUrl: '/views/Exercise/FillBlank2', controller: 'FillBlank2Ctrl' })
        //连线题
        .state('exercise.connection', { url: '/connection/:ExerciseID', templateUrl: '/views/Exercise/Connection', controller: 'ConnectionCtrl' })


        //试卷
        .state('content.paper', { url: '/paper', templateUrl: '/views/Paper/PaperList', controller: 'PaperListCtrl' })
        //新增智能试卷
        .state('content.newsmart', { url: '/paper', templateUrl: '/views/Paper/PaperList', controller: 'PaperListCtrl' })
        //新增智能试卷
        .state('content.addsmart', { url: '/paper/smart', templateUrl: '/views/Paper/AddSmart', controller: 'PaperSmartCtrl' })
        //新增自测型试卷
        .state('content.addtest', { url: '/paper/test', templateUrl: '/views/Paper/AddTest', controller: 'PaperTestCtrl' })
        //新增答题卡试卷
        .state('content.addsheet', { url: '/paper/sheet', templateUrl: '/views/Paper/AddSheet', controller: 'PaperSheetCtrl' })

           

        //知识点 
        .state('content.ken', { url: '/ken', templateUrl: '/views/Ken/Ken', controller: 'KenCtrl' })
        //知识点 按章节
        .state('content.ken.chapter', { url: '/chapter', templateUrl: '/views/Ken/KenChapter', controller: 'KenChapterCtrl' })
        //知识点 按知识点
        .state('content.ken.topic', { url: '/topic', templateUrl: '/views/Ken/KenTopic', controller: 'KenTopicCtrl' })
 
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

            $rootScope.appTitle = 'IES';
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.httpService = httpService;

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $rootScope.layout = toState.layout;
            });

            $rootScope.$on('$locationChangeStart', function (event) {
            })
        }]);
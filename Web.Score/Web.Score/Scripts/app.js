'use strict';

angular.module('app', [
    'ui.router',
    'treeControl',
    'angularFileUpload',
    'ui.tree',
    'app.utils',
    'app.filters',
    'app.services',
    'app.school',
    'app.queryService',
    'app.directives',
    'app.controllers',
    'app.admin',
    'app.query',
    'app.stat',
    'app.analysis'
])

    .config(['$stateProvider', '$locationProvider', function ($stateProvider, $locationProvider) {

        $stateProvider
            //index 
            .state('home', { url: '/', templateUrl: '/views/index', controller: 'HomeController' })
            //登录
            .state('login', { url: '/login', layout: 'basic', templateUrl: '/views/login', controller: 'LoginController' })

            /**************************系统管理***********************/
            .state('admin', { url: '/admin', abstract: true, templateUrl: '/views/admin/main' })
            //用户(组)维护
            .state('admin.nSysMenu0', { url: '/UserEdit', templateUrl: '/views/admin/UserEdit', controller: 'UserEditController' })
            //用户组管理
            .state('admin.nSysMenu1', { url: '/GroupEdit', templateUrl: '/views/admin/GroupEdit', controller: 'GroupEditController' })
            //权限编辑
            .state('admin.nSysMenu3', { url: '/AuthEdit', templateUrl: '/views/admin/AuthEdit', controller: 'AuthEditController' })
            //权限查询
            .state('admin.nSysMenu2', { url: '/AuthView', templateUrl: '/views/admin/AuthView', controller: 'AuthViewController' })
            //升留级处理
            .state('admin.nSysMenu5', { url: '/StayGrade', templateUrl: '/views/admin/StayGrade', controller: 'StayGradeController' })
            //转换为学籍成绩
            .state('admin.nSysMenu6', { url: '/CJtoXJ', templateUrl: '/views/admin/CJtoXJ', controller: 'CJtoXJController' })
            //从学籍成绩转换过来
            .state('admin.nSysMenu8', { url: '/XJtoCJ', templateUrl: '/views/admin/XJtoCJ', controller: 'XJtoCJController' })
            //学生编号导入
            .state('admin.nSysMenu9', { url: '/StudentImport', templateUrl: '/views/admin/StudentImport', controller: 'StdImportController' })
            //生成上传数据文件
            .state('nSendMail', { url: '/SendMail', templateUrl: '/views/admin/SendMail', controller: 'SendMailController' })
            //修改口令
            .state('nChangePwd', { url: '/ChangePwd', templateUrl: '/views/admin/ChangePwd', controller: 'ChangePwdController' })
            /**************************End 系统管理***********************/

            /**************************在线查询***********************/
            .state('query', { url: '/query', abstract: true, templateUrl: '/views/admin/main' })
            //任课教师查询
            .state('query.nQuery0', { url: '/TeacherQuery', templateUrl: '/views/query/TeacherQuery', controller: 'TeacherQueryController' })
            //班主任查询
            .state('query.nQuery1', { url: '/BTeacherQuery', templateUrl: '/views/query/BTeacherQuery', controller: 'BTeacherQueryController' })
            //年级领导查询
            .state('query.nQuery2', { url: '/GradeManager', templateUrl: '/views/query/GradeManager', controller: 'GradeManagerController' })
            //教务员查询
            .state('query.nQuery3', { url: '/SchoolManagerQuery', templateUrl: '/views/query/SchoolManagerQuery', controller: 'SchoolManagerQueryController' })
            //校领导查询
            .state('query.nQuery4', { url: '/SchoolHead', templateUrl: '/views/query/SchoolHead', controller: 'SchoolHeadController' })
            //常用查询
            .state('query.nQuery5', { url: '/AdvQuery', templateUrl: '/views/query/AdvQuery', controller: 'AdvQueryController' })
            //组合查询
            .state('query.nQuery6', { url: '/AdvQuery1', templateUrl: '/views/query/AdvQuery1', controller: 'AdvQuery1Controller' })
            /**************************End 在线查询***********************/

            /**************************教师学生统计***********************/
            .state('stat', { url: '/userStat', abstract: true, templateUrl: '/views/admin/main' })
            //教学情况报表(不分班)
            .state('stat.Stat01', { url: '/TeacherRep1', templateUrl: '/views/stat/TeacherRep1', controller: 'TeacherRep1Controller' })
            //教学情况报表(分班)
            .state('stat.Stat02', { url: '/TeacherRep2', templateUrl: '/views/stat/TeacherRep2', controller: 'TeacherRep2Controller' })
            //教学情况比较图表(历次)
            .state('stat.Stat03', { url: '/TeacherStyle', templateUrl: '/views/stat/TeacherStyle', controller: 'TeacherStyleController' })
            //教学情况比较图2(历次)
            .state('stat.Stat04', { url: '/TeacherStyle1', templateUrl: '/views/stat/TeacherStyle1', controller: 'TeacherStyle1Controller' })
            //横向纵向比较
            .state('stat.Stat05', { url: '/TeacherPJ', templateUrl: '/views/stat/TeacherPJ', controller: 'TeacherPJController' })
            //考试情况统计
            .state('stat.Stat07', { url: '/StudentStat', templateUrl: '/views/stat/StudentStat', controller: 'StudentStatController' })
            //年级排名趋势图
            .state('stat.Stat08', { url: '/GradeOrder', templateUrl: '/views/stat/GradeOrder', controller: 'GradeOrderController' })
            //语数外排名曲线
            .state('stat.Stat09', { url: '/CourseOrder', templateUrl: '/views/stat/CourseOrder', controller: 'CourseOrderController' })
            //打印成绩单
            .state('stat.Stat10', { url: '/PrintScore', templateUrl: '/views/stat/PrintScore', controller: 'PrintScoreController' })
            //打印学生名条
            .state('stat.Stat11', { url: '/StudentMT', templateUrl: '/views/stat/StudentMT', controller: 'StudentMTController' })

            /**************************End 教师学生统计***********************/


            /**************************年级班级统计***********************/
            
            //年级等级分布图
            .state('stat.Stat12', { url: '/GradeStyle', templateUrl: '/views/stat/GradeStyle', controller: 'GradeStyleController' })
            //班级间比较
            .state('stat.Stat13', { url: '/GradeClassComp', templateUrl: '/views/stat/GradeClassComp', controller: 'GradeClassCompController' })
            //年级排名
            .state('stat.Stat14', { url: '/GradeOrders', templateUrl: '/views/stat/GradeOrders', controller: 'GradeOrdersController' })
            //年级学科成绩正态
            .state('stat.Stat15', { url: '/GradeScore', templateUrl: '/views/stat/GradeScore', controller: 'GradeScoreController' })
            //班级细目成绩报表-未完成
            .state('stat.Stat16', { url: '/GradeMinutia', templateUrl: '/views/stat/GradeMinutia', controller: 'GradeMinutiaController' })
            //年级成绩统计(并表)
            .state('stat.Stat17', { url: '/Gradestat', templateUrl: '/views/stat/Gradestat', controller: 'GradestatController' })
            //学生成绩纵向比较
            .state('stat.Stat18', { url: '/GradeStdPJ', templateUrl: '/views/stat/GradeStdPJ', controller: 'GradeStdPJController' })
            //考试统计分析
            .state('stat.Stat20', { url: '/ClassStat', templateUrl: '/views/stat/ExamStat', controller: 'ExamStatController' })
            //学科相关分析
            .state('stat.Stat21', { url: '/ClassCourse', templateUrl: '/views/stat/ClassCourse', controller: 'ClassCourseController' })
            //学科成绩清单
            .state('stat.Stat22', { url: '/ClassScore', templateUrl: '/views/stat/ClassScore', controller: 'ClassScoreController' })
            //细目成绩清单
            .state('stat.Stat23', { url: '/ClassMinutia', templateUrl: '/views/stat/ClassMinutia', controller: 'ClassMinutiaController' })
            //班级学期总评
            .state('stat.Stat24', { url: '/ClassAmin', templateUrl: '/views/stat/ClassAmin', controller: 'ClassAminController' })
            //班级排名
            .state('stat.Stat25', { url: '/ClassOrder', templateUrl: '/views/stat/ClassOrder', controller: 'ClassOrderController' })
            //多学科成绩报表
            .state('stat.Stat26', { url: '/ClassRep', templateUrl: '/views/stat/ClassRep', controller: 'ClassRepController' })
            //学生多门清单
            .state('stat.Stat27', { url: '/ClassND', templateUrl: '/views/stat/ClassND', controller: 'ClassNDController' })
            /**************************End 年级班级统计***********************/

            /**************************统计分析***********************/
            .state('analyze', { url: '/analyze', abstract: true, templateUrl: '/views/admin/main' })
            //超级型数据处理
            .state('analyze.Sum0', { url: '/Super', templateUrl: '/views/Analysis/AnalyzeSuper', controller: 'AnalyzeSuperController' })
            //统一型数据处理
            .state('analyze.Sum1', { url: '/common', templateUrl: '/views/Analysis/AnalyzeCommon', controller: 'AnalyzeCommonController' })
            //细目分析
            .state('analyze.Sum2', { url: '/MinutiaAnalyse', templateUrl: '/views/Analysis/MinutiaAnalyse', controller: 'MinutiaAnalyseController' })
            //高三选课排名
            .state('analyze.Sum3', { url: '/ThirdOrder', templateUrl: '/views/Analysis/ThirdOrder', controller: 'ThirdOrderController' })
            /**************************End 统计分析***********************/

            .state('otherwise', {
                url: '*path',
                templateUrl: '/views/404',
                controller: 'Error404Ctrl'
            });

        $locationProvider.html5Mode(true);

    }])

    .run(['$templateCache', '$rootScope', '$state', '$stateParams', '$location', 'menuService',
        'dialogUtils', 'softname', 'baseService', 'userService', 'utilService', 'schoolService',
        'constService', 'queryService', 'chartService',
        function ($templateCache, $rootScope, $state, $stateParams, $location, menuService,
            dialogUtils, softname, baseService, userService, utilService, schoolService,
            constService, queryService, chartService) {

            var view = angular.element('#ui-view');
            $templateCache.put(view.data('tmpl-url'), view.html());

            $rootScope.$location = $location;

            $rootScope.user = {};
            $rootScope.baseService = baseService;
            $rootScope.utilService = utilService;
            $rootScope.constService = constService;
            $rootScope.dialogUtils = dialogUtils;
            $rootScope.softname = softname;
            $rootScope.userService = userService;
            $rootScope.schoolService = schoolService;
            $rootScope.queryService = queryService;
            $rootScope.chartService = chartService;
            $rootScope.schoolService.loadSchool();

            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.logout = function () {
                userService.logout(function (data) {
                    $location.path('login').replace();
                });
            }

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $rootScope.layout = toState.layout;
                userService.getUser(function (user) {
                    if (user !== null) {
                        $rootScope.user = user;
                        menuService.getUserFuncs($rootScope.user.TeacherID, function (data) {
                            $rootScope.menus = data.d;
                        });
                    }
                });
            });

            $rootScope.$on('$locationChangeStart', function (event) {
                userService.isAuthorized(function (data) {
                    if (data.d === '') {
                        $location.path('/login').replace();
                    }
                });
            })
        }]);
'use strict';

angular.module('app', [
    'ui.router',
    'treeControl',
    'angularFileUpload',
    'ui.tree',
    'app.filters',
    'app.services',
    'app.school',
    'app.queryService',
    'app.directives',
    'app.controllers',
    'app.admin',
    'app.query',
    'app.stat'
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
            .state('n_Teacher_Rep1', { url: '/TeacherRep1', templateUrl: '/views/TeacherRep1', controller: 'TeacherRep1Controller' })
            //教学情况报表(分班)
            .state('n_Teacher_Rep2', { url: '/TeacherRep2', templateUrl: '/views/TeacherRep2', controller: 'TeacherRep2Controller' })
            //教学情况比较图表(历次)
            .state('n_Teacher_Style', { url: '/TeacherStyle', templateUrl: '/views/TeacherStyle', controller: 'TeacherStyleController' })
            //教学情况比较图2(历次)
            .state('n_Teacher_Style1', { url: '/TeacherStyle1', templateUrl: '/views/TeacherStyle1', controller: 'TeacherStyle1Controller' })
            //横向纵向比较
            .state('n_Teacher_PJ', { url: '/TeacherPJ', templateUrl: '/views/TeacherPJ', controller: 'TeacherPJController' })
            //考试情况统计
            .state('stat.Stat07', { url: '/StudentStat', templateUrl: '/views/stat/StudentStat', controller: 'StudentStatController' })
            //年级排名趋势图
            .state('n_Student_style', { url: '/StudentStyle', templateUrl: '/views/stat/StudentStyle', controller: 'StudentStyleController' })
            //语数外排名曲线
            .state('n_Student_YSW', { url: '/StudentYSW', templateUrl: '/views/StudentYSW', controller: 'StudentYSWController' })
            //打印成绩单
            .state('n_Student_Score', { url: '/StudentScore', templateUrl: '/views/StudentScore', controller: 'StudentScoreController' })
            //打印学生名条
            .state('n_Student_MT', { url: '/StudentMT', templateUrl: '/views/StudentMT', controller: 'StudentMTController' })

            /**************************End 教师学生统计***********************/


            /**************************年级班级统计***********************/
            .state('GradeClass', { url: '/GradeClass', abstract: true, templateUrl: '/views/admin/main' })
            //年级等级分布图
            .state('n_Grade_Style', { url: '/GradeStyle', templateUrl: '/views/GradeStyle', controller: 'GradeStyleController' })
            //班级间比较
            .state('n_Grade_ClassComp', { url: '/GradeClassComp', templateUrl: '/views/GradeClassComp', controller: 'GradeClassCompController' })
            //年级排名
            .state('n_Grade_Order', { url: '/GradeOrder', templateUrl: '/views/GradeOrder', controller: 'GradeOrderController' })
            //年级学科成绩正态
            .state('n_Grade_Score', { url: '/GradeScore', templateUrl: '/views/GradeScore', controller: 'GradeScoreController' })
            //班级细目成绩报表
            .state('n_Grade_Minutia', { url: '/GradeMinutia', templateUrl: '/views/GradeMinutia', controller: 'GradeMinutiaController' })
            //年级成绩统计(并表)
            .state('n_Gradestat', { url: '/Gradestat', templateUrl: '/views/Gradestat', controller: 'GradestatController' })
            //学生成绩纵向比较
            .state('n_Grade_StdPJ', { url: '/GradeStdPJ', templateUrl: '/views/GradeStdPJ', controller: 'GradeStdPJController' })
            //考试统计分析
            .state('n_Class_Stat', { url: '/ClassStat', templateUrl: '/views/ClassStat', controller: 'ClassStatController' })
            //学科相关分析
            .state('n_Class_Course', { url: '/ClassCourse', templateUrl: '/views/ClassCourse', controller: 'ClassCourseController' })
            //学科成绩清单
            .state('n_Class_Score', { url: '/ClassScore', templateUrl: '/views/ClassScore', controller: 'ClassScoreController' })
            //细目成绩清单
            .state('n_Class_Minutia', { url: '/ClassMinutia', templateUrl: '/views/ClassMinutia', controller: 'ClassMinutiaController' })
            //班级学期总评
            .state('n_Class_Amin', { url: '/ClassAmin', templateUrl: '/views/ClassAmin', controller: 'ClassAminController' })
            //班级排名
            .state('n_Class_Order', { url: '/ClassOrder', templateUrl: '/views/ClassOrder', controller: 'ClassOrderController' })
            //多学科成绩报表
            .state('n_Class_Rep', { url: '/ClassRep', templateUrl: '/views/ClassRep', controller: 'ClassRepController' })
            //学生多门清单
            .state('n_Class_ND', { url: '/ClassND', templateUrl: '/views/ClassND', controller: 'ClassNDController' })
            /**************************End 年级班级统计***********************/

            /**************************统计分析***********************/

            //统一型数据处理
            .state('nshujuchuli1', { url: '/Shujuchuli1', templateUrl: '/views/Shujuchuli1', controller: 'Shujuchuli1Controller' })
            //超级型数据处理
            .state('nshujuchuli2', { url: '/Shujuchuli2', templateUrl: '/views/Shujuchuli2', controller: 'Shujuchuli2Controller' })
            //细目分析
            .state('nMinutiaAnalyse', { url: '/MinutiaAnalyse', templateUrl: '/views/MinutiaAnalyse', controller: 'MinutiaAnalyseController' })
            //高三选课排名
            .state('nThirdOrder', { url: '/ThirdOrder', templateUrl: '/views/ThirdOrder', controller: 'ThirdOrderController' })
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
        'constService','queryService', 'chartService',
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

            var initMenu = function (user) {
                var url = "/DataProvider/Admin.aspx/GetMenus";
                var param = null;
                var funcsPromise = baseService.postPromise(url, param);
                url = "/DataProvider/Admin.aspx/GetUserFuncs";
                param = { teacher: user.TeacherID };
                var userFuncsPromise = baseService.postPromise(url, param);
                baseService.runPromises({
                    allFuncs: funcsPromise,
                    userFuncs: userFuncsPromise
                }, function (results) {
                    if (results.allFuncs.d != null) {
                        $rootScope.menus = results.allFuncs.d;
                        console.log($rootScope.menus);
                        angular.forEach($rootScope.menus, function(menu){
                            if (menu.FuncID !== 1701) {
                                var visibleFuncs = results.userFuncs.d;
                                var length = visibleFuncs.length;
                                for (var i = 0; i < length; i++) {
                                    var visibleFunc = visibleFuncs[i];
                                    if (menu.FuncID === visibleFunc.FuncID) {
                                        menu.Visibled = true;
                                        break;
                                    }
                                }
                            } else {
                                menu.Visibled = true;
                            }
                        }); 
                    }
                })
            }

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                $rootScope.layout = toState.layout;
                userService.getUser(function (user) {
                    if (user !== null) {
                        $rootScope.user = user;
                        //menuService.getMenus(function (data) {
                        //    $rootScope.menus = data.d;
                        //});
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
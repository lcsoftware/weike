'use strict';

var analysis = angular.module('app.analysis', []);

analysis.controller('AnalyzeSuperController', ['$scope', 'appUtils', function ($scope, appUtils) {
    var moduleName = '数据处理 - 超级型';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName

    $scope.appUtils = appUtils;
    $scope.schoolName = $scope.schoolService.school.SchoolName;
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    month = month.length === 1 ? '0' + month : month;
    day = day.length === 1 ? '0' + day : day;
    $scope.now = year + '-' + month + '-' + day;

    $scope.ValueA1 = 15;
    $scope.ValueB1 = 25;
    $scope.ValueC1 = 30;
    $scope.ValueD1 = 10;
    $scope.ValueE1 = 20;

    $scope.ValueA2 = 0;
    $scope.ValueB2 = 0;
    $scope.ValueC2 = 0;
    $scope.ValueD2 = 0;
    $scope.ValueE2 = 0;

    $scope.cksr = 0;
    $scope.cbDC = 0;
    //统计结果
    $scope.data1 = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeClasses = [];
    $scope.GradeCourses = [];
    $scope.TestLogins = [];
    $scope.ExamMethods = $scope.constService.TestTypes;


    $scope.classChecks = [];
    $scope.courseChecks = [];

    $scope.otherChecks = [];

    $scope.SettingChecks = [];
    $scope.Settings = [
      { code: 1, name: '百分比' },
      { code: 2, name: '分值' }
    ]
    $scope.conditionData.Setting = $scope.Settings[0];

    $scope.OutItems = [
      { code: 1, name: '原始分' },
      { code: 2, name: 'Z分' }
    ]
    $scope.conditionData.OutItem = $scope.OutItems[0];

    $scope.base = [];
    $scope.data1 = [];
    $scope.data2 = [];

    $scope.haveStat = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        $scope.GradeCourses.length = 0;
        if (!$scope.conditionData.MicYear) return;
        if ($scope.conditionData.TestLogin) {
            var testLogin = $scope.conditionData.TestLogin;
            var url = "/DataProvider/Analyze.aspx/GetCourses";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode, testLogin: testLogin };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }

        $scope.GradeClasses.length = 0;
        var url1 = "/DataProvider/Util.aspx/GetGradeClass";
        var param1 = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
        $scope.baseService.post(url1, param1, function (data) {
            $scope.GradeClasses = data.d;
        });
    });

    $scope.$watch('conditionData.TestLogin', function (testLogin) {
        $scope.GradeCourses.length = 0;
        if ($scope.conditionData.TestLogin) {
            var gradeCode = $scope.conditionData.GradeCode;
            var url = "/DataProvider/Analyze.aspx/GetCourses";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode, testLogin: testLogin };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    $scope.$watch('conditionData.TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if ($scope.conditionData.TestType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = null;
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, $scope.conditionData.TestType.code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });

    var statBase = function () {

        var valueA = $scope.conditionData.Setting.code === 1 ? $scope.ValueA1 : $scope.ValueA2;
        var valueB = $scope.conditionData.Setting.code === 1 ? $scope.ValueB1 : $scope.ValueB2;
        var valueC = $scope.conditionData.Setting.code === 1 ? $scope.ValueC1 : $scope.ValueC2;
        var valueD = $scope.conditionData.Setting.code === 1 ? $scope.ValueD1 : $scope.ValueD2;
        var valueE = $scope.conditionData.Setting.code === 1 ? $scope.ValueE1 : $scope.ValueE2;
        var strLevel = '';
        var strLevel1 = '';

        if ($scope.cbDC === 1) {
            if (valueB === 100) {
                strLevel = 'A';
                strLevel1 = 'B';
            } else if (valueC === 100) {
                strLevel = 'B';
                strLevel1 = 'C';
            } else if (valueD === 100) {
                strLevel = 'C';
                strLevel1 = 'D';
            } else {
                strLevel = 'D';
                strLevel1 = 'E';
            }
        }

        $scope.data1.length = 0;

        var url = "/DataProvider/Analyze.aspx/AnalyzeSuper";
        var param = {
            micYear: $scope.conditionData.MicYear.MicYear,
            gradeCode: $scope.conditionData.GradeCode,
            gradeClasses: $scope.classChecks,
            gradeCourses: $scope.courseChecks,
            testType: $scope.conditionData.TestType,
            testLogin: $scope.conditionData.TestLogin,
            outItem: $scope.conditionData.OutItem.code,
            cksr: $scope.cksr,
            cbDC: $scope.cbDC,
            setting: $scope.conditionData.Setting,
            valueA: valueA,
            valueB: valueB,
            valueC: valueC,
            valueD: valueD,
            valueE: valueE,
            strlevel: strLevel,
            strlevel1: strLevel1
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.data1 = angular.fromJson(data.d[0].Message);
            }
        });
    }

    $scope.stat = function () {
        if (!$scope.conditionData.MicYear) {
            $scope.dialogUtils.info('请选择学年');
            return;
        }
        if (!$scope.conditionData.GradeCode) {
            $scope.dialogUtils.info('请选择年级！');
            return;
        }
        if (!$scope.conditionData.TestType) {
            $scope.dialogUtils.info('请选择考试类型');
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        if ($scope.classChecks.length == 0) {
            $scope.dialogUtils.info('请选择班级！');
            return;
        }
        if ($scope.courseChecks.length == 0) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }
        statBase();
    }
}]);

analysis.controller('AnalyzeCommonController', ['$scope', 'appUtils', function ($scope, appUtils) {
    var moduleName = '数据处理 - 统一型';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName

    $scope.appUtils = appUtils;
    $scope.schoolName = $scope.schoolService.school.SchoolName;
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    month = month.length === 1 ? '0' + month : month;
    day = day.length === 1 ? '0' + day : day;
    $scope.now = year + '-' + month + '-' + day;

    //统计结果
    $scope.data1 = [];

    $scope.conditionData = {};
    $scope.GradeCode = null;

    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.TestLogins = [];

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.$watch('conditionData.MicYear', function (micYear) {
        if (micYear) {
            var url = "/DataProvider/Analyze.aspx/GetTestLoginByYear";
            var param = { micYear: micYear.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestLogins = angular.fromJson(data.d);
            });
        }
    });

    $scope.$watch('conditionData.TestLogin', function (testLogin) {
        if (testLogin) {
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Analyze.aspx/GetCoursesByTestLogin";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, testLogin: testLogin };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });

            url = "/DataProvider/Analyze.aspx/GetGradeByTestNo";
            param = { micYear: $scope.conditionData.MicYear.MicYear, testLogin: testLogin };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCode = data.d;
            });
        }
    });

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var testLogin = $scope.conditionData.TestLogin;
        var gradeCode = $scope.GradeCode;

        var url = "/DataProvider/Analyze.aspx/AnalyzeCommon";
        var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode, testLogin: testLogin, courses: $scope.GradeCourses };
        $scope.baseService.post(url, param, function (data) {
            $scope.data1 = data.d;
        });
    }

    $scope.stat = function () {
        if (!$scope.conditionData.MicYear) {
            $scope.dialogUtils.info('请选择学年');
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        statBase();
    }
}]);

//细目分析
analysis.controller('MinutiaAnalyseController', ['$scope', function ($scope) {
    var moduleName = '细目分析';
    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.Grades = [];
    $scope.Students = [];
    $scope.GradeCourses = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];

    //获得学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });
    //获得所有年级
    $scope.utilService.GetGradeAll(function (data) {
        $scope.GradeCodes = data.d;
    });
    //绑定考试类型，考试号
    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });
    //根据年级获得班级
    $scope.$watch('GradeCode', function (gradecode) {
        $scope.Grades.length = 0;
        if (gradecode) {
            //获得班级
            $scope.queryService.GetGradeByGradeNo($scope.MicYear.MicYear, gradecode.GradeNo, function (data) {
                if (data.d != '') {
                    $scope.Grades = JSON.parse(data.d);
                }
            });
        }
    });
    //根据班级获得学生
    $scope.$watch('GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if (gradeClass) {
            //根据班级获得学生
            $scope.utilService.GetStudentsByGrade($scope.MicYear.MicYear, gradeClass.classNo, function (data) {
                $scope.Students = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradecode) {
        //获得课程
        $scope.GradeCourses.length = 0;
        if (gradecode) {
            $scope.utilService.GetGradeCourse($scope.MicYear.MicYear, gradecode, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType && $scope.MicYear) {
            var micYear = $scope.MicYear;
            var gradeCode = $scope.GradeCode ? $scope.GradeCode.GradeNo : '';
            var gradeCourse = $scope.GradeCourse ? $scope.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });

}]);

//高三选课排名
analysis.controller('ThirdOrderController', ['$scope', function ($scope) {

}]);
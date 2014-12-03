'use strict';

var stat = angular.module('app.stat', ['checklist-model']);

// Path: /stat.Stat07
stat.controller('StudentStatController', ['$scope', function ($scope) {
    var moduleName = '考试情况统计';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    //统计结果
    $scope.base = {};
    $scope.chartOptions = [];
    $scope.data = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];

    $scope.GradeClasses = [];
    $scope.Students = [];

    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.ScoreTypes = $scope.constService.ScoreTypes;
    $scope.ScoreSorts = $scope.constService.ScoreSorts;

    $scope.haveStat = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });

            url = "/DataProvider/Util.aspx/GetGradeCourse";
            param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    $scope.$watch('conditionData.GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if ($scope.conditionData.MicYear) {
            var url = "/DataProvider/Util.aspx/GetStudent";
            var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.Students = data.d;
            });
        }
    });

    $scope.$watch('conditionData.GradeCourse', function (gradeCourse) {
        $scope.TestTypes.length = 0;
        if ($scope.conditionData.MicYear && $scope.conditionData.GradeCode) {
            var url = "/DataProvider/Util.aspx/GetTestTypeByCourse";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCourse: gradeCourse, gradeCode: $scope.conditionData.GradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = $scope.conditionData.GradeCourse ? $scope.conditionData.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    var chart1 = {};
    var chart2 = {};
    var chart3 = {};

    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });
    $scope.chartService.chartCreate('main2', function (data) {
        chart2 = data;
    });
    $scope.chartService.chartCreate('main3', function (data) {
        chart3 = data;
    });

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;

        var url = "/DataProvider/Statistic.aspx/GetStat07Base";
        var param = { micYear: micYear.MicYear, testNo: testNo, gradeCourse: gradeCourse, gradeClass: gradeClass, student: student };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.base = angular.fromJson(data.d)[0];
            }
        });
    }

    var statCharts = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;
        var scoreType = $scope.conditionData.ScoreType.code;

        var url = "/DataProvider/Statistic.aspx/GetStat07Charts";
        var param = { micYear: micYear, testNo: testNo, gradeCourse: gradeCourse, gradeClass: gradeClass, student: student, scoreType: scoreType };
        $scope.baseService.post(url, param, function (data) {
            $scope.chartService.changeOption(chart1, data.d[0]);
            $scope.chartService.changeOption(chart2, data.d[1]);
            $scope.chartService.changeOption(chart3, data.d[2]);
        });
    }

    var statData = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;

        var url = "/DataProvider/Statistic.aspx/GetStat07Data";
        var param = { micYear: micYear, gradeCourse: gradeCourse, gradeClass: gradeClass, student: student };
        $scope.baseService.post(url, param, function (data) {
            $scope.data = angular.fromJson(data.d);
            $scope.haveStat = true;
        });
    }

    $scope.stat = function () {

        if (!$scope.conditionData.Student) {
            $scope.dialogUtils.info('请选择学生！');
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info('请选择考试号！');
            return;
        }

        statBase();
        statCharts();
        statData();
    }


}]);

// Path: /stat.Stat08
stat.controller('GradeOrderController', ['$scope', '$sce', function ($scope, $sce) {
    var moduleName = '年级排名趋势';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    //统计结果
    $scope.base = {};
    $scope.chartOptions = [];
    $scope.data = [];

    $scope.conditionData = {};
    $scope.conditionData.checkValue = "0";

    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeClasses = [];
    $scope.Students = [];

    $scope.haveStat = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.MicYear', function () {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            $scope.userService.getUser(function (user) {
                var url = "/DataProvider/Util.aspx/GetClassByScope";
                var param = { academicYear: $scope.conditionData.MicYear.MicYear, teacher: user.TeacherID };
                $scope.baseService.post(url, param, function (data) {
                    $scope.GradeClasses = data.d;
                });
            });
            var url = "/DataProvider/Util.aspx/GetCourseByYear";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    $scope.$watch('conditionData.GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if ($scope.conditionData.MicYear) {
            var url = "/DataProvider/Util.aspx/GetStudent";
            var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.Students = data.d;
            });
        }
    });


    var chart1 = {};

    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    var statCharts = function () {

        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;
        var checkValue = $scope.conditionData.checkValue;

        var url = "/DataProvider/Statistic.aspx/GetStat08Charts";
        var param = { micYear: micYear, gradeCourse: gradeCourse, gradeClass: gradeClass, student: student, checkValue: checkValue };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                var length = data.d.length;
                for (var i = 0; i < length; i++) {
                    var resultEntry = data.d[i];
                    if (resultEntry.Code == 0) {
                        $scope.chartService.changeOption(chart1, resultEntry.Message);
                    } else if (resultEntry.Code == 1) {
                        $scope.data = angular.fromJson(resultEntry.Message);
                    }
                }
            } else {
                $scope.dialogUtils.info('您选择的条件下无数据，不能生成的图表！');
            }
        });
    }


    $scope.stat = function () {
        if (!$scope.conditionData.GradeClass) {
            $scope.dialogUtils.info('请选择班级！');
            return;
        }
        if (!$scope.conditionData.Student) {
            $scope.dialogUtils.info('请选择学生！');
            return;
        }
        statCharts();
        $scope.haveStat = true;
    }
}]);

//Path: /stat.Stat09
stat.controller('CourseOrderController', ['$scope', 'appUtils', function ($scope, appUtils) {
    var moduleName = '语数外总分比较';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.appUtils = appUtils;
    //统计结果
    $scope.base = {};
    $scope.chartOptions = [];
    $scope.data = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];

    $scope.GradeClasses = [];
    $scope.Students = [];

    $scope.courseChecks = [];
    $scope.otherChecks = [];

    $scope.haveStat = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });

            url = "/DataProvider/Util.aspx/GetGradeCourse";
            param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    $scope.$watch('conditionData.GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if ($scope.conditionData.MicYear) {
            var url = "/DataProvider/Util.aspx/GetStudent";
            var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.Students = data.d;
            });
        }
    });


    var chart1 = {};

    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var scoreOption = $scope.conditionData.ScoreOption;

        var url = "/DataProvider/Statistic.aspx/GetStat20Base";
        var param = { micYear: micYear.MicYear, testNo: testNo, gradeCourse: gradeCourse, gradeClass: gradeClass, scoreOption: scoreOption };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.base = angular.fromJson(data.d)[0];
            }
        });
    }

    var statCharts = function () {

        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeCode = $scope.conditionData.GradeCode;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;

        var length = $scope.otherChecks.length;
        var otherCheckValue = length == 0 ? -1 : 0;
        for (var i = 0; i < length; i++) {
            otherCheckValue += $scope.otherChecks[i];
        }
        otherCheckValue = otherCheckValue === -1 ? 0 : otherCheckValue;

        var url = "/DataProvider/Statistic.aspx/GetStat09Charts";
        var param = { micYear: micYear, gradeCode: gradeCode, gradeClass: gradeClass, student: student, courseChecks: $scope.courseChecks, otherCheckValue: otherCheckValue };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                var length = data.d.length;
                for (var i = 0; i < length; i++) {
                    var resultEntry = data.d[i];
                    if (resultEntry.Code == 0) {
                        $scope.chartService.changeOption(chart1, resultEntry.Message);
                    } else if (resultEntry.Code == 1) {
                        $scope.data = angular.fromJson(resultEntry.Message);
                    }
                }
            } else {
                $scope.dialogUtils.info('您选择的条件下无数据，不能生成的图表！');
            }
        });
    }


    $scope.stat = function () {
        if (!$scope.courseChecks.length === 0) {
            $scope.dialogUtils.info('请选择课程！');
            return;
        }
        if (!$scope.conditionData.Student) {
            $scope.dialogUtils.info('请选择学生！');
            return;
        }
        statCharts();
        $scope.haveStat = true;
    }
}]);

//Path: /stat.Stat10
stat.controller('PrintScoreController', ['$scope', 'appUtils', function ($scope, appUtils) {
    var moduleName = '打印成绩单';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.appUtils = appUtils;
    $scope.schoolName = $scope.schoolService.school.SchoolName;
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    month = month.length === 1 ? '0' + month : month;
    day = day.length === 1 ? '0' + day : day;
    $scope.now = year + '-' + month + '-' + day;

    $scope.appUtils = appUtils;
    //统计结果
    $scope.base = {};
    $scope.chartOptions = [];
    $scope.data1 = [];
    $scope.data2 = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];

    $scope.GradeClasses = [];

    $scope.Students = [];
    $scope.studentChecks = [];

    $scope.Semesters = $scope.constService.Semesters;
    $scope.PrintMethods = $scope.constService.PrintMethods;
    $scope.ExamMethods = $scope.constService.TestTypes;
    $scope.TestLogins = [];
    $scope.courseChecks = [];
    $scope.otherChecks = [];

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
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });
        }
        changeTestLogin();
    });

    $scope.$watch('conditionData.GradeClass', function (gradeClass) {
        $scope.Students.length = 0;
        if ($scope.conditionData.PrintMethod && $scope.conditionData.PrintMethod.code === 1) {
            if ($scope.conditionData.MicYear) {
                var url = "/DataProvider/Util.aspx/GetStudent";
                var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
                $scope.baseService.post(url, param, function (data) {
                    $scope.Students = data.d;
                });
            }
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        changeTestLogin();
    });

    var changeTestLogin = function () {
        $scope.TestLogins.length = 0;
        if ($scope.conditionData.TestType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = null;
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, $scope.conditionData.TestType.code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    }

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var gradeCode = $scope.conditionData.GradeCode;
        var gradeClass = $scope.conditionData.GradeClass;
        var studentChecks = $scope.studentChecks;
        var testType = $scope.conditionData.TestType;
        var testNo = $scope.conditionData.TestLogin;
        var printMethod = $scope.conditionData.PrintMethod;
        var semester = $scope.conditionData.Semester;

        $scope.base.length = 0;
        $scope.data1.length = 0;
        $scope.data2.length = 0;

        var url = "/DataProvider/Statistic.aspx/GetStat10Base";
        var param = { micYear: micYear.MicYear, testLogin: testNo, studentChecks: studentChecks };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.base = angular.fromJson(data.d[0].Message);
            }
        });

        if ($scope.conditionData.PrintMethod.code === 1) {
            url = "/DataProvider/Statistic.aspx/GetStat10Data1";
            param = {
                micYear: micYear.MicYear,
                gradeCode: gradeCode,
                gradeClass: gradeClass,
                studentChecks: studentChecks,
                testType: testType,
                testLogin: testNo,
                printMethod: printMethod,
                semester: semester.code
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d !== null) {
                    $scope.data1 = angular.fromJson(data.d[0].Message);
                    $scope.testTime = data.d[1].Message;
                }
            });
        } else if ($scope.conditionData.PrintMethod.code === 2) {
            url = "/DataProvider/Statistic.aspx/GetStat10Data2";
            param = {
                micYear: micYear.MicYear,
                gradeClass: gradeClass,
                testLogin: testNo,
                semester: semester.code
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d !== null) {
                    $scope.data2 = angular.fromJson(data.d[0].Message);
                    $scope.testTime = data.d[1].Message;
                }
            });
        }
    }

    $scope.stat = function () {
        if (!$scope.conditionData.Semester) {
            $scope.dialogUtils.info('请选择学期');
            return;
        }
        if (!$scope.conditionData.PrintMethod) {
            $scope.dialogUtils.info('请选择打印方式');
            return;
        }
        if (!$scope.conditionData.GradeCode) {
            $scope.dialogUtils.info('请选择年级！');
            return;
        }
        if (!$scope.conditionData.GradeClass) {
            $scope.dialogUtils.info('请选择班级！');
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        statBase();
    }
}]);


//Path: /stat.Stat11
stat.controller('StudentMTController', ['$scope', function ($scope) {
    var moduleName = '打印学生名条';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    //统计结果
    $scope.data1 = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeClasses = [];

    $scope.Students = [];

    $scope.data1 = [];

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });
        }
    });

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var gradeCode = $scope.conditionData.GradeCode === undefined ? null : $scope.conditionData.GradeCode;
        var gradeClass = $scope.conditionData.GradeClass === undefined ? null : $scope.conditionData.GradeClass;

        $scope.data1.length = 0;

        if (micYear) {
            var url = "/DataProvider/Statistic.aspx/GetStat11Data1";
            var param = {
                micYear: micYear.MicYear,
                gradeCode: gradeCode,
                gradeClass: gradeClass
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d !== null) {
                    $scope.data1 = angular.fromJson(data.d[0].Message);
                }
            });
        }
    }

    $scope.stat = function () {
        if (!$scope.conditionData.MicYear) {
            $scope.dialogUtils.info('请选择学年');
            return;
        }
        statBase();
    }
}]);


// Path: /stat.Stat19
stat.controller('ExamStatController', ['$scope', function ($scope) {
    var moduleName = '考试统计分析';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    //统计结果
    $scope.base = {};
    $scope.chartOptions = [];
    $scope.data = [];

    $scope.conditionData = {};

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];

    $scope.GradeClasses = [];

    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.ScoreTypes = $scope.constService.ScoreTypes;
    $scope.ExamMethods = $scope.constService.ExamMethods;

    $scope.ScoreOptions = [
        { code: 1, name: '仅在籍生' },
        { code: 2, name: '跨学年(无平时)' }
    ];

    $scope.haveStat = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        if ($scope.conditionData.MicYear) {
            $scope.GradeClasses.length = 0;
            $scope.GradeCourses.length = 0;
            var url = "/DataProvider/Util.aspx/GetGradeClass";
            var param = { academicYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeClasses = data.d;
            });

            url = "/DataProvider/Util.aspx/GetGradeCourse";
            param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCode: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });
    //$scope.$watch('conditionData.GradeClass', function (gradeClass) {
    //    $scope.Students.length = 0;
    //    if ($scope.conditionData.MicYear) {
    //        var url = "/DataProvider/Util.aspx/GetStudent";
    //        var param = { academicyear: $scope.conditionData.MicYear.MicYear, classcode: gradeClass.ClassNo };
    //        $scope.baseService.post(url, param, function (data) {
    //            $scope.Students = data.d;
    //        });
    //    }
    //});

    $scope.$watch('conditionData.GradeCourse', function (gradeCourse) {
        $scope.TestTypes.length = 0;
        if ($scope.conditionData.MicYear && $scope.conditionData.GradeCode) {
            var url = "/DataProvider/Util.aspx/GetTestTypeByCourse";
            var param = { micYear: $scope.conditionData.MicYear.MicYear, gradeCourse: gradeCourse, gradeCode: $scope.conditionData.GradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = $scope.conditionData.GradeCourse ? $scope.conditionData.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });

    var chart1 = {};
    var chart2 = {};
    var chart3 = {};

    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });
    $scope.chartService.chartCreate('main2', function (data) {
        chart2 = data;
    });
    $scope.chartService.chartCreate('main3', function (data) {
        chart3 = data;
    });


    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var scoreOption = $scope.conditionData.ScoreOption;

        var url = "/DataProvider/Statistic.aspx/GetStat20Base";
        var param = { micYear: micYear.MicYear, testNo: testNo, gradeCourse: gradeCourse, gradeClass: gradeClass, scoreOption: scoreOption };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.base = angular.fromJson(data.d)[0];
            }
        });
    }

    var statCharts = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeCode = $scope.conditionData.GradeCode;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;
        var scoreType = $scope.conditionData.ScoreType.code;
        var scoreOption = $scope.conditionData.ScoreOption.code;

        var url = "/DataProvider/Statistic.aspx/GetStat20Charts";
        var param = { micYear: micYear, testNo: testNo, gradeCode: gradeCode, gradeCourse: gradeCourse, gradeClass: gradeClass, scoreType: scoreType, scoreOption: scoreOption };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.chartService.changeOption(chart1, data.d[0]);
                delete data.d[1].yAxis;
                $scope.chartService.changeOption(chart2, data.d[1]);
                $scope.chartService.changeOption(chart3, data.d[2]);
            } else {
                $scope.dialogUtils.info('您选择的条件下无数据，不能生成的图表！');
            }
        });
    }

    var statData = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeCode = $scope.conditionData.GradeCode;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;
        var scoreType = $scope.conditionData.ScoreType.code;
        var scoreOption = $scope.conditionData.ScoreOption.code;

        var url = "/DataProvider/Statistic.aspx/GetStat20GradeData1";
        var param = { micYear: micYear, testNo: testNo, gradeCode: gradeCode, gradeCourse: gradeCourse, gradeClass: gradeClass };
        $scope.baseService.post(url, param, function (data) {
            $scope.data1 = angular.fromJson(data.d);
            $scope.haveStat = true;
        });

        url = "/DataProvider/Statistic.aspx/GetStat20GradeData2";
        param = { micYear: micYear, testNo: testNo, gradeCode: gradeCode, gradeCourse: gradeCourse, gradeClass: gradeClass, scoreType: scoreType, scoreOption: scoreOption };
        $scope.baseService.post(url, param, function (data) {
            $scope.data2 = angular.fromJson(data.d);
        });
    }

    $scope.stat = function () {
        if (!$scope.conditionData.GradeClass) {
            $scope.dialogUtils.info('请选择班级！');
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info('请选择考试号！');
            return;
        }

        statBase();
        statCharts();
        statData();
        $scope.haveStat = true;
    }
}]);


stat.controller('TeacherRep1Controller', ['$scope', function ($scope) {
    var moduleName = '教师教学情况报表(不分班统计)';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TeacherAnalysis = [];
    $scope.base = {};

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得所有年级
        $scope.utilService.GetGradeAll(function (data) {
            $scope.GradeCodes = data.d;
            //获得当前年级
            var url = "/DataProvider/Statistic.aspx/GetCurrentGrade";
            var param = { micyear: $scope.MicYear.MicYear, teacherId: $scope.user.TeacherID };
            $scope.baseService.post(url, param, function (data) {
                var rs = angular.fromJson(data.d);
                $scope.GradeCode = findGradeNo($scope.GradeCodes, rs[0].GradeNo);
                //获取当前课程
                var url = "/DataProvider/Statistic.aspx/GeCurrentCourse";
                var param = { micyear: $scope.MicYear.MicYear, gradeNo: rs[0].GradeNo };
                $scope.baseService.post(url, param, function (data) {
                    $scope.GradeCourses = data.d;
                    //获得当前考试号
                    bindTest();
                });
            });
        });
    });
    var bindTest = function () {
        var url = "/DataProvider/Statistic.aspx/GetCurrentTestNo";
        var param = {
            gradeNo: $scope.GradeCode == null ? null : $scope.GradeCode.GradeNo,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse == null ? null : $scope.GradeCourse.CourseCode
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestLogins = data.d;
        });
    }
    //监控课程，改变考试号
    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.TestLogins.length = 0;
        if (gradeCourse) {
            bindTest();
        }
    });
    //查询按钮方法
    $scope.query = function () {
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherAnalysisGrade";
        var param = {
            gradeNo: $scope.GradeCode,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse,
            testNo: $scope.TestNo.TestNo,
            ck: $scope.stuZaiJi == null ? null : $scope.stuZaiJi
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TeacherAnalysis = angular.fromJson(data.d);
            $scope.base = angular.fromJson(data.d)[0];
            $scope.utilService.closeBg();
        });
    }

    var findGradeNo = function (values, GradeNo) {
        var length = values.length;
        for (var i = 0; i < length; i++) {
            if (parseInt(values[i].GradeNo) == parseInt(GradeNo)) {
                return values[i];
            }
        }
    }
    var check = function () {
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return false;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }
        return true;
    }

}]);

stat.controller('TeacherRep2Controller', ['$scope', function ($scope) {
    var moduleName = '教师教学情况报表(分班统计)';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TeacherAnalysis = [];
    $scope.base = {};

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得所有年级
        $scope.utilService.GetGradeAll(function (data) {
            $scope.GradeCodes = data.d;
            //获得当前年级
            var url = "/DataProvider/Statistic.aspx/GetCurrentGrade";
            var param = { micyear: $scope.MicYear.MicYear, teacherId: $scope.user.TeacherID };
            $scope.baseService.post(url, param, function (data) {
                var rs = angular.fromJson(data.d);
                $scope.GradeCode = findGradeNo($scope.GradeCodes, rs[0].GradeNo);
                //获取当前课程
                var url = "/DataProvider/Statistic.aspx/GeCurrentCourse";
                var param = { micyear: $scope.MicYear.MicYear, gradeNo: rs[0].GradeNo };
                $scope.baseService.post(url, param, function (data) {
                    $scope.GradeCourses = data.d;
                    //获得当前考试号
                    bindTest();
                });
            });
        });
    });
    var bindTest = function () {
        var url = "/DataProvider/Statistic.aspx/GetCurrentTestNo";
        var param = {
            gradeNo: $scope.GradeCode == null ? null : $scope.GradeCode.GradeNo,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse == null ? null : $scope.GradeCourse.CourseCode
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestLogins = data.d;
        });
    }
    //监控课程，改变考试号
    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.TestLogins.length = 0;
        if (gradeCourse) {
            bindTest();
        }
    });
    //查询按钮方法
    $scope.query = function () {
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherAnalysisClass";
        var param = {
            gradeNo: $scope.GradeCode,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse,
            testNo: $scope.TestNo.TestNo,
            ck: $scope.stuZaiJi == null ? null : $scope.stuZaiJi
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TeacherAnalysis = angular.fromJson(data.d);
            $scope.base = angular.fromJson(data.d)[0];
            $scope.utilService.closeBg();
        });
    }

    var findGradeNo = function (values, GradeNo) {
        var length = values.length;
        for (var i = 0; i < length; i++) {
            if (parseInt(values[i].GradeNo) == parseInt(GradeNo)) {
                return values[i];
            }
        }
    }
    var check = function () {
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return false;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }
        return true;
    }

}]);

//教师教学情况比较图表（历次）
stat.controller('TeacherStyleController', ['$scope', function ($scope) {
    var moduleName = '教师教学情况比较图表（历次）';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.Teachers = [];

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        //根据学年，获取课程        
        $scope.utilService.GetCourse($scope.MicYear, function (data) {
            $scope.GradeCourses = data.d;
        });
    });


    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.GradeCodes.length = 0;
        $scope.Teachers.length = 0;
        if (gradeCourse) {
            //根据课程，获取年级
            var url = "/DataProvider/Statistic.aspx/GetGradeCodeByGradeNo";
            var param = { micyear: $scope.MicYear, gradeCourse: gradeCourse };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.Teachers.length = 0;
        if (gradeCode) {
            //根据年级，获得老师
            var url = "/DataProvider/Statistic.aspx/GetTeachers";
            var param = { micyear: $scope.MicYear, courseCode: $scope.GradeCourse, gradeNo: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.Teachers = data.d;
            });
        }
    });
    $scope.teacherCheck = [];
    $scope.teacherChange = function (teacher) {
        if ($.inArray(teacher.$parent.teacher, $scope.teacherCheck) < 0) {
            $scope.teacherCheck.push(teacher.$parent.teacher);
        } else {
            $scope.teacherCheck.splice($.inArray(teacher.$parent.teacher, $scope.teacherCheck), 1);
        }
    }
    $scope.NumScore = 0;
    $scope.query = function () {
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherStyle";
        var param = {
            micyear: $scope.MicYear,
            gradeNo: $scope.GradeCode,
            courseCode: $scope.GradeCourse,
            only: $scope.ScoreOnly == null ? false : $scope.ScoreOnly,
            year: $scope.ScoreYear == null ? false : $scope.ScoreYear,
            numScore: $scope.NumScore,
            teacher: $scope.teacherCheck
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d[0].xAxis.data.length > 0) {
                $scope.chartService.changeOption(chart1, data.d[0]);
            }
            $scope.utilService.closeBg();
        });
    }

    function check() {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.teacherCheck.length <= 0) {
            $scope.dialogUtils.info('请选择教师');
            return false;
        }
        return true;
    }
}]);

//教师教学情况比较图2(历次)
stat.controller('TeacherStyle1Controller', ['$scope', function ($scope) {
    var moduleName = '教师教学情况比较图2(历次)';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.Teachers = [];

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        $scope.starYear = $scope.AcademicYears[0];
        $scope.endYear = $scope.AcademicYears[$scope.AcademicYears.length - 1];
        //根据学年，获取课程        
        $scope.utilService.GetCourse($scope.MicYear, function (data) {
            $scope.GradeCourses = data.d;
        });
    });



    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.GradeCodes.length = 0;
        $scope.Teachers.length = 0;
        if (gradeCourse) {
            //根据课程，获取年级
            var url = "/DataProvider/Statistic.aspx/GetGradeCodeByGradeNo";
            var param = { micyear: $scope.MicYear, gradeCourse: gradeCourse };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.Teachers.length = 0;
        if (gradeCode) {
            //根据年级，获得老师
            var url = "/DataProvider/Statistic.aspx/GetTeachers";
            var param = { micyear: $scope.MicYear, courseCode: $scope.GradeCourse, gradeNo: gradeCode };
            $scope.baseService.post(url, param, function (data) {
                $scope.Teachers = data.d;
            });
        }
    });
    $scope.teacherCheck = [];
    $scope.teacherChange = function (teacher) {
        if ($.inArray(teacher.$parent.teacher, $scope.teacherCheck) < 0) {
            $scope.teacherCheck.push(teacher.$parent.teacher);
        } else {
            $scope.teacherCheck.splice($.inArray(teacher.$parent.teacher, $scope.teacherCheck), 1);
        }
    }
    $scope.NumScore = 0;
    $scope.query = function () {
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherStyle1";
        var param = {
            micyear: $scope.MicYear,
            gradeNo: $scope.GradeCode,
            courseCode: $scope.GradeCourse,
            numScore: $scope.NumScore,
            starYear: $scope.starYear.MicYear,
            endYear: $scope.endYear.MicYear,
            teacher: $scope.teacherCheck
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.chartService.changeOption(chart1, data.d[0]);
            $scope.utilService.closeBg();
        });
    }

    function check() {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.teacher == "") {
            $scope.dialogUtils.info('请选择教师');
            return false;
        }
        return true;
    }
}]);

//教师横向纵向比较图
stat.controller('TeacherPJController', ['$scope', function ($scope) {
    var moduleName = '教师横向纵向比较图';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TestNos = [];
    $scope.TestNosJZ = [];
    $scope.TestNosXZ = [];

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });
    //根据学年，获取课程
    var url = "/DataProvider/Util.aspx/GetCourseCodeAll";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.GradeCourses = data.d;
    });

    $scope.$watch('MicYear', function (micyear) {
        $scope.MicYearJZ = $scope.MicYear;
        $scope.MicYearXZ = $scope.MicYear;
    });

    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.GradeCodes.length = 0;
        if (gradeCourse) {
            //根据课程，获取年级
            var url = "/DataProvider/Statistic.aspx/GetGradeCodeByGradeNo";
            var param = { micyear: $scope.MicYear, gradeCourse: gradeCourse };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.TestNos.length = 0;
        $scope.TestNosJZ.length = 0;
        $scope.TestNosXZ.length = 0;
        if (gradeCode) {
            //根据年级，获得考试号
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNos = data.d;
            });
            //基准
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo - $scope.MicYear.MicYear + $scope.MicYearJZ.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNosJZ = data.d;
            });
            //选择
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo - $scope.MicYear.MicYear + $scope.MicYearXZ.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNosXZ = data.d;
            });
        }
    });
    $scope.teacherCheck = [];
    $scope.teacherChange = function (teacher) {
        if ($.inArray(teacher.$parent.teacher, $scope.teacherCheck) < 0) {
            $scope.teacherCheck.push(teacher.$parent.teacher);
        } else {
            $scope.teacherCheck.splice($.inArray(teacher.$parent.teacher, $scope.teacherCheck), 1);
        }
    }
    $scope.NumScore = 0;
    $scope.query = function () {
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherPJ";
        var param = {
            courseCode: $scope.GradeCourse,
            micYearJZ: $scope.MicYearJZ,
            micYearXZ: $scope.MicYearXZ == null ? null : $scope.MicYearXZ.MicYear,
            testNoJZ: $scope.TestNoJZ,
            testNoXZ: $scope.TestNoXZ == null ? null : $scope.TestNoXZ.TestNo,
            micyear: $scope.MicYear,
            gradeCode: $scope.GradeCode,
            testNo: $scope.TestNo,
            numScore: $scope.NumScore
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.chartService.changeOption(chart1, data.d[0]);
            $scope.utilService.closeBg();
        });
    }

    function check() {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试');
            return false;
        }
        if ($scope.MicYearJZ == null) {
            $scope.dialogUtils.info('请选择基准学年');
            return false;
        }
        if ($scope.TestNoJZ == null) {
            $scope.dialogUtils.info('请选择基准考试');
            return false;
        }
        return true;
    }
}]);

//年级等级分布图
stat.controller('GradeStyleController', ['$scope', function ($scope) {
    var moduleName = '年级等级分布图';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TestNos = [];
    $scope.TestTypes = [];

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        //根据学年，获取课程        
        $scope.utilService.GetCourse($scope.MicYear, function (data) {
            $scope.GradeCourses = data.d;
        });
    });

    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.GradeCodes.length = 0;
        if (gradeCourse) {
            //根据课程，获取年级
            var url = "/DataProvider/Statistic.aspx/GetGradeCodeByGradeNo";
            var param = { micyear: $scope.MicYear, gradeCourse: gradeCourse };
            $scope.baseService.post(url, param, function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.TestTypes.length = 0;
        if (gradeCode) {
            //绑定考试类型
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestNos.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.GradeCode.GradeNo, '', testType.Code, function (data) {
                $scope.TestNos = data.d;
            });
        }
    });

    $scope.query = function () {
        if (!check()) return;
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeStyle";
        var param = {
            micYear: $scope.MicYear,
            gradeCourse: $scope.GradeCourse,
            gradeCode: $scope.GradeCode,
            testNo: $scope.TestNo
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.chartService.changeOption(chart1, data.d[0]);
            $scope.utilService.closeBg();
        });
    }
    function check() {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }
        return true;
    }
}]);

//班级间比较
stat.controller('GradeClassCompController', ['$scope', function ($scope) {
    var moduleName = '班级间比较';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.Grades = [];
    $scope.testShow = false;
    $scope.NumScore = 0;
    $scope.Kaoshi = 1;

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        //根据学年，获取课程        
        $scope.utilService.GetCourse($scope.MicYear, function (data) {
            $scope.GradeCourses = data.d;
        });
    });

    //根据课程，获取年级
    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.Grades.length = 0;
        if (gradeCode) {
            //获得班级
            var url = "/DataProvider/Statistic.aspx/GetGradeByGradeNo";
            var param = { micyear: $scope.MicYear.MicYear, gradeNo: gradeCode.GradeNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.Grades = data.d;
                $scope.testShow = true;
            });
        }
    });

    $scope.classes = [];
    $scope.gradeChange = function (gradeClass) {
        if ($.inArray(gradeClass.$parent.grade, $scope.classes) < 0) {
            $scope.classes.push(gradeClass.$parent.grade);
        } else {
            $scope.classes.splice($.inArray(gradeClass.$parent.grade, $scope.classes), 1);
        }
    }
    $scope.query = function () {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        if ($scope.classes.length <= 0) {
            $scope.dialogUtils.info('请选择班级');
            return;
        }
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeClassComp";
        var param = {
            micyear: $scope.MicYear,
            courseCode: $scope.GradeCourse,
            gradeNo: $scope.GradeCode,
            only: $scope.ScoreOnly == null ? false : $scope.ScoreOnly,
            year: $scope.ScoreYear == null ? false : $scope.ScoreYear,
            numScore: $scope.NumScore,
            Kaoshi: $scope.Kaoshi,
            gradeClass: $scope.classes
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d[0].xAxis.data.length > 0) {
                $scope.chartService.changeOption(chart1, data.d[0]);
            }
            $scope.utilService.closeBg();
        });
    }
    $scope.query1 = function () {
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        if ($scope.classes.length <= 0) {
            $scope.dialogUtils.info('请选择班级');
            return;
        }
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeClassCompNum";
        var param = {
            micyear: $scope.MicYear,
            gradeNo: $scope.GradeCode,
            numScore: $scope.NumScore,
            Kaoshi: $scope.Kaoshi,
            gradeClass: $scope.classes
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d[0].xAxis.data.length > 0) {
                $scope.chartService.changeOption(chart1, data.d[0]);
            }
            $scope.utilService.closeBg();
        });
    }
}]);

//年级排名
stat.controller('GradeOrdersController', ['$scope', function ($scope) {
    var moduleName = '年级排名';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.Semester = 1;
    $scope.TestTypes = [];
    $scope.TestNos = [];

    $scope.ColumnsName = [];
    $scope.Items = [];

    //获得当前学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });
    //获取年级
    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.GradeCourses.length = 0;
        $scope.TestTypes.length = 0;
        if (gradeCode) {
            //绑定课程
            $scope.utilService.GetGradeCourse($scope.MicYear.MicYear, gradeCode, function (data) {
                $scope.GradeCourses = data.d;
            });
            //绑定考试类型
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });

    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestNos.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.GradeCode.GradeNo, '', testType.Code, function (data) {
                $scope.TestNos = data.d;
            });
        }
    });

    $scope.courses = [];
    $scope.courseChange = function (courseCode) {
        if ($.inArray(courseCode.$parent.course, $scope.courses) < 0) {
            $scope.courses.push(courseCode.$parent.course);
        } else {
            $scope.courses.splice($.inArray(courseCode.$parent.course, $scope.courses), 1);
        }
    }
    $scope.query = function () {        
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        if ($scope.TestType == null) {
            $scope.dialogUtils.info('请选择考试类型');
            return;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        if ($scope.courses.length <= 0) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }

        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeOrders";
        var param = {
            micYear: $scope.MicYear,
            gradeCourse: $scope.courses,
            gradeCode: $scope.GradeCode,
            testNo: $scope.TestNo,
            semester: $scope.Semester
        };
        $scope.baseService.post(url, param, function (data) {
            var length = data.d.length;
            for (var i = 0; i < length; i++) {
                var resultEntry = data.d[i];
                if (resultEntry.Code == 0) {
                    $scope.Items = angular.fromJson(resultEntry.Message);
                } else if (resultEntry.Code == 1) {
                    $scope.ColumnsName = angular.fromJson(resultEntry.Message);
                }
            }


            var rs = "<table class='table table-striped table-bordered' style='width:100%'><thead><tr style='background-color:#808080'>";
            var len = count($scope.ColumnsName[0]);
            for (var i = 0; i < len; i++) {
                rs += "<th style='text-align:center'>" + $scope.ColumnsName[0][i] + "</th>";
            }
            rs += "</tr></thead>";
            for (var n = 0; n < $scope.Items.length; n++) {
                rs += "<tr>";
                len = count($scope.Items[n]);
                for (var m in $scope.Items[n]) {
                    rs += "<td>" + $scope.Items[n][m] + "</td>";
                }
                rs += "</tr>";
            }
            rs += "</table>";
            $('#data').html(rs);
            $scope.utilService.closeBg();
        });
        function count(o) {
            var t = typeof o;
            if (t == 'string') {
                return o.length;
            } else if (t == 'object') {
                var n = 0;
                for (var i in o) {
                    n++;
                }
                return n;
            }
            return false;
        }
    }
}]);

//年级学科成绩正态
stat.controller('GradeScoreController', ['$scope', function ($scope) {
    var moduleName = '年级学科成绩正态';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.Grades = [];
    $scope.nianji = false;
    $scope.banji = false;
    $scope.type = 0;
    $scope.TestTypes = [];
    $scope.TestNos = [];

    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });
    //获取年级
    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });
    
    $scope.$watch('type', function (type) {
        if(type == 0)
        {
            $scope.nianji = true;
            $scope.banji = false;
            
        }else
        {
            $scope.nianji = false;
            $scope.banji = true;            
        }
    })

    //获得当前学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        //根据学年，获取课程
        $scope.utilService.GetCourse($scope.MicYear, function (data) {
            $scope.GradeCourses = data.d;
        });
        //根据学年，获得班级
        $scope.utilService.GetGradeClassByYear($scope.MicYear, function (data) {
            $scope.Grades = data.d;
        });
    });
    
    $scope.$watch('GradeCourse', function (gradecourse) {
        $scope.TestTypes.length = 0;
        if (gradecourse) {
            //绑定考试类型
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });
    $scope.$watch('TestType', function (testType) {
        $scope.TestNos.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            var gradeNo = $scope.GradeCode == null ? "" : $scope.GradeCode.GradeNo;
            var gradeClass = $scope.GradeClass == null ? "" : $scope.GradeClass.classno;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, gradeNo, gradeClass, testType.Code, function (data) {
                $scope.TestNos = data.d;
            });
        }
    });
    
    $scope.query = function () {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }
        if ($scope.type == 0) {
            if ($scope.GradeCode == null) {
                $scope.dialogUtils.info('请选择年级');
                return;
            }
        }
        if ($scope.type == 1) {
            if ($scope.GradeClass == null) {
                $scope.dialogUtils.info('请选择班级');
                return;
            }            
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeScore";
        var param = {
            micYear: $scope.MicYear,
            gradeCourse: $scope.GradeCourse,
            gradeCode: $scope.GradeCode == null ? null : $scope.GradeCode,
            gradeClass: $scope.GradeClass == null ? null : $scope.GradeClass,
            type: $scope.type,
            testNo: $scope.TestNo
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== null) {
                $scope.chartService.changeOption(chart1, data.d[0]);
            } else {
                $scope.dialogUtils.info('您选择的条件下无数据，不能生成的图表！');
            }
            $scope.utilService.closeBg();
        });
    }
}]);

//班级细目成绩报表
stat.controller('GradeMinutiaController', ['$scope', function ($scope) {
    var moduleName = '班级细目成绩报表';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TeacherAnalysis = [];
    $scope.base = {};

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得所有年级
        $scope.utilService.GetGradeAll(function (data) {
            $scope.GradeCodes = data.d;
            //获得当前年级
            var url = "/DataProvider/Statistic.aspx/GetCurrentGrade";
            var param = { micyear: $scope.MicYear.MicYear, teacherId: $scope.user.TeacherID };
            $scope.baseService.post(url, param, function (data) {
                var rs = angular.fromJson(data.d);
                $scope.GradeCode = findGradeNo($scope.GradeCodes, rs[0].GradeNo);
                //获取当前课程
                var url = "/DataProvider/Statistic.aspx/GeCurrentCourse";
                var param = { micyear: $scope.MicYear.MicYear, gradeNo: rs[0].GradeNo };
                $scope.baseService.post(url, param, function (data) {
                    $scope.GradeCourses = data.d;
                    //获得当前考试号
                    bindTest();
                });
            });
        });
    });
    var bindTest = function () {
        var url = "/DataProvider/Statistic.aspx/GetCurrentTestNo";
        var param = {
            gradeNo: $scope.GradeCode == null ? null : $scope.GradeCode.GradeNo,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse == null ? null : $scope.GradeCourse.CourseCode
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestLogins = data.d;
        });
    }
    //监控课程，改变考试号
    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.TestLogins.length = 0;
        if (gradeCourse) {
            bindTest();
        }
    });
    //查询按钮方法
    $scope.query = function () {
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherAnalysisGrade";
        var param = {
            gradeNo: $scope.GradeCode,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse,
            testNo: $scope.TestNo.TestNo,
            ck: $scope.stuZaiJi == null ? null : $scope.stuZaiJi
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TeacherAnalysis = angular.fromJson(data.d);
            $scope.base = angular.fromJson(data.d)[0];
            $scope.utilService.closeBg();
        });
    }

    var findGradeNo = function (values, GradeNo) {
        var length = values.length;
        for (var i = 0; i < length; i++) {
            if (parseInt(values[i].GradeNo) == parseInt(GradeNo)) {
                return values[i];
            }
        }
    }
    var check = function () {
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return false;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }
        return true;
    }

}]);

//年级成绩统计(并表)
stat.controller('GradestatController', ['$scope', function ($scope) {
    var moduleName = '年级成绩统计(并表)';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TestTypes = [];
    $scope.TestNos = [];


    //获得当前学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });
    //获取年级
    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.GradeCourses.length = 0;
        $scope.TestTypes.length = 0;
        if (gradeCode) {
            //绑定课程
            $scope.utilService.GetGradeCourse($scope.MicYear.MicYear, gradeCode, function (data) {
                $scope.GradeCourses = data.d;
            });
            //绑定考试类型
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    });

    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestNos.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.GradeCode.GradeNo, '', testType.Code, function (data) {
                $scope.TestNos = data.d;
            });
        }
    });

    $scope.courses = [];
    $scope.courseChange = function (courseCode) {
        if ($.inArray(courseCode.$parent.course, $scope.courses) < 0) {
            $scope.courses.push(courseCode.$parent.course);
        } else {
            $scope.courses.splice($.inArray(courseCode.$parent.course, $scope.courses), 1);
        }
    }
    $scope.query = function () {
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        if ($scope.TestType == null) {
            $scope.dialogUtils.info('请选择考试类型');
            return;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return;
        }
        if ($scope.courses.length <= 0) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }

        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradestat";
        var param = {
            micYear: $scope.MicYear,
            gradeCourse: $scope.courses,
            gradeCode: $scope.GradeCode,
            testNo: $scope.TestNo,
            only: $scope.only == null ? false : $scope.only
        };
        $scope.baseService.post(url, param, function (data) {
            var length = data.d.length;
            for (var i = 0; i < length; i++) {
                var resultEntry = data.d[i];
                if (resultEntry.Code == 0) {
                    $scope.Items = angular.fromJson(resultEntry.Message);
                } else if (resultEntry.Code == 1) {
                    $scope.ColumnsName = angular.fromJson(resultEntry.Message);
                }
            }


            var rs = "<table class='table table-striped table-bordered' style='width:100%'><thead><tr style='background-color:#808080'>";
            var len = count($scope.ColumnsName[0]);
            for (var i = 0; i < len; i++) {
                rs += "<th style='text-align:center'>" + $scope.ColumnsName[0][i] + "</th>";
            }
            rs += "</tr></thead>";
            for (var n = 0; n < $scope.Items.length; n++) {
                rs += "<tr>";
                len = count($scope.Items[n]);
                for (var m in $scope.Items[n]) {
                    rs += "<td>" + $scope.Items[n][m] + "</td>";
                }
                rs += "</tr>";
            }
            rs += "</table>";
            $('#data').html(rs);
            $scope.utilService.closeBg();
        });
        function count(o) {
            var t = typeof o;
            if (t == 'string') {
                return o.length;
            } else if (t == 'object') {
                var n = 0;
                for (var i in o) {
                    n++;
                }
                return n;
            }
            return false;
        }
        
    }
}]);

//学生成绩纵向比较
stat.controller('GradeStdPJController', ['$scope', 'pageService', function ($scope, pageService) {
    var moduleName = '学生成绩纵向比较';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TestNos = [];
    $scope.TestNosJZ = [];
    $scope.TestNosXZ = [];
    $scope.items = [];

    $scope.pageService = pageService;
    $scope.pageService.reset();

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];        
        //获取课程
        var url = "/DataProvider/Util.aspx/GetCourseCodeAll";
        var param = {};
        $scope.baseService.post(url, param, function (data) {
            $scope.GradeCourses = data.d;
        });
    });

    $scope.$watch('MicYear', function (micyear) {
        $scope.MicYearJZ = $scope.MicYear;
        $scope.MicYearXZ = $scope.MicYear;
    });

    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.GradeCodes.length = 0;
        if (gradeCourse) {
            //获取年级
            $scope.utilService.GetGradeCodes(function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    });
    $scope.$watch('GradeCode', function (gradeCode) {
        $scope.TestNos.length = 0;
        $scope.TestNosJZ.length = 0;
        $scope.TestNosXZ.length = 0;
        if (gradeCode) {
            //根据年级，获得考试号
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNos = data.d;
            });
            //基准
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo - $scope.MicYear.MicYear + $scope.MicYearJZ.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNosJZ = data.d;
            });
            //选择
            var url = "/DataProvider/Statistic.aspx/GetTestNoByCourse";
            var param = { micYear: $scope.MicYear, gradeCode: gradeCode.GradeNo - $scope.MicYear.MicYear + $scope.MicYearXZ.MicYear };
            $scope.baseService.post(url, param, function (data) {
                $scope.TestNosXZ = data.d;
            });
        }
    });
    
    $scope.query = function () {
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetGradeStdPJ";
        var param = {
            courseCode: $scope.GradeCourse,
            micYearJZ: $scope.MicYearJZ,
            micYearXZ: $scope.MicYearXZ == null ? null : $scope.MicYearXZ.MicYear,
            testNoJZ: $scope.TestNoJZ,
            testNoXZ: $scope.TestNoXZ == null ? null : $scope.TestNoXZ.TestNo,
            micyear: $scope.MicYear,
            gradeCode: $scope.GradeCode,
            testNo: $scope.TestNo
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != "")
            {
                $scope.rsTitle = $scope.GradeCode.GradeBriefName + $scope.GradeCourse.FullName + '成绩学生三次考试比较报表';
                $scope.items = angular.fromJson(data.d);
                $scope.pageService.init($scope.items, 20);
            }
            else
            {
                $scope.rsTitle = '你有可能未录入成绩或教师信息，或者未做教师任课安排！';
                $scope.pageService.data.length = 0;
            }
            $scope.utilService.closeBg();
        });
    }

    function check() {
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试');
            return false;
        }
        if ($scope.MicYearJZ == null) {
            $scope.dialogUtils.info('请选择基准学年');
            return false;
        }
        if ($scope.TestNoJZ == null) {
            $scope.dialogUtils.info('请选择基准考试');
            return false;
        }
        return true;
    }
}]);

//学科相关分析
stat.controller('ClassCourseController', ['$scope', 'pageService', function ($scope, pageService) {
    var moduleName = '学科相关分析';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.GradeCourses1 = [];
    $scope.GradeCourses2 = [];
    $scope.Grades = [];
    $scope.GradeCodes = [];
    $scope.TestNos = [];
    $scope.TestTypes = [];
    $scope.type = 1;
    var chart1 = {};
    $scope.chartService.chartCreate('main1', function (data) {
        chart1 = data;
    });

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
        //根据学年获得班级
        $scope.utilService.GetGradeClassByYear($scope.MicYear, function (data) {
            $scope.Grades = data.d;
        });
        //根据学年获得课程
        $scope.utilService.GetCourseByYear($scope.MicYear.MicYear, function (data) {
            $scope.GradeCourses1 = data.d;
            $scope.GradeCourses2 = data.d;
        });
        //绑定考试类型
        $scope.utilService.GetTestType(function (data) {
            $scope.TestTypes = data.d;
        });
        //获取年级
        $scope.utilService.GetGradeCodes(function (data) {
            $scope.GradeCodes = data.d;
        });
    });    
    
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestNos.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, '', '', testType.Code, function (data) {
                $scope.TestNos = data.d;
            });
        }
    });

    $scope.query = function () {
        if (!check()) return;
        chart1 = {};
        $scope.chartService.chartCreate('main1', function (data) {
            chart1 = data;
        });
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetClassCourse";
        var param = {
            micyear: $scope.MicYear,
            gradeCode: $scope.GradeCode == null ? null : $scope.GradeCode,
            gradeClass: $scope.GradeClass == null ? null : $scope.GradeClass,
            gradeCourse1: $scope.GradeCourse1,
            gradeCourse2: $scope.GradeCourse2,
            testNo: $scope.TestNo,
            type:$scope.type
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d[0].xAxis.data.length > 0) {
                $scope.chartService.changeOption(chart1, data.d[0]);
            }
            $scope.utilService.closeBg();
        });
    }

    function check() {
        if ($scope.GradeCourse1 == null) {
            $scope.dialogUtils.info('请选择第一门课程');
            return false;
        }
        if ($scope.GradeCourse2 == null) {
            $scope.dialogUtils.info('请选择第二门课程');
            return false;
        }
        if ($scope.type == 0) {
            if ($scope.GradeCode == null) {
                $scope.dialogUtils.info('请选择年级');
                return false;
            }
        } else{
            if ($scope.GradeClass == null) {
                $scope.dialogUtils.info('请选择班级');
                return false;
            }
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }        
        return true;
    }
}]);

//学科成绩清单
stat.controller('ClassScoreController', ['$scope', function ($scope) {
    var moduleName = '学科成绩清单';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];
    $scope.TeacherAnalysis = [];
    $scope.base = {};

    //获得当前学年
    var url = "/DataProvider/Statistic.aspx/GetCurrentYear";
    var param = {};
    $scope.baseService.post(url, param, function (data) {
        $scope.AcademicYears = data.d;
        $scope.MicYear = $scope.AcademicYears[0];
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得所有年级
        $scope.utilService.GetGradeAll(function (data) {
            $scope.GradeCodes = data.d;
            //获得当前年级
            var url = "/DataProvider/Statistic.aspx/GetCurrentGrade";
            var param = { micyear: $scope.MicYear.MicYear, teacherId: $scope.user.TeacherID };
            $scope.baseService.post(url, param, function (data) {
                var rs = angular.fromJson(data.d);
                $scope.GradeCode = findGradeNo($scope.GradeCodes, rs[0].GradeNo);
                //获取当前课程
                var url = "/DataProvider/Statistic.aspx/GeCurrentCourse";
                var param = { micyear: $scope.MicYear.MicYear, gradeNo: rs[0].GradeNo };
                $scope.baseService.post(url, param, function (data) {
                    $scope.GradeCourses = data.d;
                    //获得当前考试号
                    bindTest();
                });
            });
        });
    });
    var bindTest = function () {
        var url = "/DataProvider/Statistic.aspx/GetCurrentTestNo";
        var param = {
            gradeNo: $scope.GradeCode == null ? null : $scope.GradeCode.GradeNo,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse == null ? null : $scope.GradeCourse.CourseCode
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestLogins = data.d;
        });
    }
    //监控课程，改变考试号
    $scope.$watch('GradeCourse', function (gradeCourse) {
        $scope.TestLogins.length = 0;
        if (gradeCourse) {
            bindTest();
        }
    });
    //查询按钮方法
    $scope.query = function () {
        if (!check()) return;
        $scope.utilService.showBg();
        var url = "/DataProvider/Statistic.aspx/GetTeacherAnalysisGrade";
        var param = {
            gradeNo: $scope.GradeCode,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse,
            testNo: $scope.TestNo.TestNo,
            ck: $scope.stuZaiJi == null ? null : $scope.stuZaiJi
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TeacherAnalysis = angular.fromJson(data.d);
            $scope.base = angular.fromJson(data.d)[0];
            $scope.utilService.closeBg();
        });
    }

    var findGradeNo = function (values, GradeNo) {
        var length = values.length;
        for (var i = 0; i < length; i++) {
            if (parseInt(values[i].GradeNo) == parseInt(GradeNo)) {
                return values[i];
            }
        }
    }
    var check = function () {
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return false;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return false;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return false;
        }
        if ($scope.TestNo == null) {
            $scope.dialogUtils.info('请选择考试号');
            return false;
        }
        return true;
    }

}]);

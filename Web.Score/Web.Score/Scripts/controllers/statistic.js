'use strict';

var stat = angular.module('app.stat', []);

// Path: /
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



    var statChart1 = function () {

    }

    var chart1 = {};
    var chart2 = {};
    var chart3 = {};

    $scope.chartService.chartCreate('main1', function (data) { chart1 = data; });
    $scope.chartService.chartCreate('main2', function (data) { chart2 = data; });
    $scope.chartService.chartCreate('main3', function (data) { chart3 = data; });

    var statBase = function () {
        var micYear = $scope.conditionData.MicYear;
        var testNo = $scope.conditionData.TestLogin;
        var gradeCourse = $scope.conditionData.GradeCourse;
        var gradeClass = $scope.conditionData.GradeClass;
        var student = $scope.conditionData.Student;

        var url = "/DataProvider/Statistic.aspx/GetStatBase";
        var param = { micYear: micYear.MicYear, testNo: testNo, gradeCourse: gradeCourse, gradeClass: gradeClass, student: student };
        $scope.baseService.post(url, param, function (data) {
            $scope.base = angular.fromJson(data.d);
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
        });
    }

    $scope.stat = function () {
        statBase();
        statCharts();
        //statData();
    }

    $scope.changeOption = function () {
        var legend = { legend: { data: ['蒸发量1', '降水量'] } };
        var xAxis = {
            xAxis: [
                {
                    type: 'category',
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                }
            ]
        };
        var series = {
            series: [
                {
                    name: '蒸发量1',
                    type: 'line',
                    data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3]
                },
                {
                    name: '降水量',
                    type: 'line',
                    data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3]
                }
            ]
        };
        $scope.chartService.refresh(chart1, legend, xAxis, series);
    }

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
}]);


stat.controller('TeacherRep1Controller', ['$scope', function ($scope) {
    var moduleName = '教师教学情况报表(不分班统计)';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.GradeCourses = [];
    $scope.GradeCodes = [];

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

    var findGradeNo = function (values, GradeNo) {
        var length = values.length;
        for (var i = 0; i < length; i++) {
            if (parseInt(values[i].GradeNo) == parseInt(GradeNo)) {
                return values[i];
            }
        }
    }

    $scope.query = function () {
        var url = "/DataProvider/Statistic.aspx/GetTeacherAnalysis";
        var param = {
            gradeNo: $scope.GradeCode,
            micyear: $scope.MicYear.MicYear,
            courseCode: $scope.GradeCourse,
            testNo: $scope.TestNo.TestNo,
            ck: $scope.stuZaiJi == null ? null : $scope.stuZaiJi
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestLogins = data.d;
        });
    }
}]);
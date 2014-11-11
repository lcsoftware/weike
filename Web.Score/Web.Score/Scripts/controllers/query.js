'use strict';

var appQuery = angular.module('app.query', ['checklist-model']);

// Path: /
appQuery.controller('TeacherQueryController', ['$scope', 'pageService', function ($scope, pageService) {
    $scope.$root.title = $scope.softname + ' | 任课教师查询';
    $scope.$root.moduleName = '任课教师查询';
    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];
    $scope.Students = [];
    $scope.studentsGrid = [];
    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.testShow = false;

    $scope.pageService = pageService;
    $scope.pageService.reset();

    $scope.userService.getUser(function (data) {
        $scope.user = data;
    });

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });




    var load = function () {
        var good = 0.75;
        var fail = 0.6
        //成绩列表
        $scope.queryService.GetQueryTeacher($scope.MicYear.MicYear, $scope.user.TeacherID, $scope.GradeCourse.CourseCode, $scope.GradeCode.GradeNo, $scope.TestType, $scope.TestNo, $scope.student, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            if ($scope.studentsGrid.length > 0) {
                $scope.pageService.init($scope.studentsGrid, 10);
                $scope.maxNumScore = $scope.utilService.getMax($scope.studentsGrid);
                $scope.minNumScore = $scope.utilService.getMin($scope.studentsGrid);
                $scope.aveNumScore = $scope.utilService.getAve($scope.studentsGrid);
                $scope.couNumScore = $scope.studentsGrid.length;
                $scope.goodNumScore = $scope.utilService.getGood($scope.studentsGrid, good);
                $scope.failNumScore = $scope.utilService.getFail($scope.studentsGrid, fail);                
            } else {
                $scope.pageService.data.length = 0;
                $scope.pageService.pages.length = 0;
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
            }
        });
        $scope.testShow = true;
        //绑定考试类型，考试号
        if ($scope.TestTypes.length <= 0) {
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    }

    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.queryService.GetTestNo($scope.MicYear.MicYear, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });

    $scope.query = function () {
        $scope.student = $scope.utilService.getlist($('input[name=selected]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        if ($scope.GradeCourse == null) {
            $scope.dialogUtils.info('请选择课程');
            return;
        }
        if ($scope.GradeCode == null) {
            $scope.dialogUtils.info('请选择年级');
            return;
        }
        load();
    }


    $scope.$watch('MicYear', function (micyear) {
        $scope.GradeCourses = null;
        if (micyear) {
            $scope.queryService.GetGradeCourseByTeacherId($scope.MicYear.MicYear, $scope.user.TeacherID, function (data) {
                $scope.GradeCourses = data.d;
            });
            $scope.queryService.GetGradeCodeByTeacherId($scope.MicYear.MicYear, $scope.user.TeacherID, function (data) {
                $scope.GradeCodes = data.d;
            });
        }
    })
    $scope.$watch('GradeCode', function (classcode) {
        $scope.Students = null;
        if (classcode) {
            $scope.utilService.GetStudent($scope.MicYear.MicYear, classcode.GradeNo, function (data) {
                $scope.Students = data.d;
            });
        }
    });   
    
}]);

// Path: /
appQuery.controller('BTeacherQueryController', ['$scope', 'pageService', function ($scope, pageService) {
    $scope.$root.title = $scope.softname + ' | 班主任查询';
    $scope.$root.moduleName = '班主任查询';    
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];
    $scope.GradeCourses = [];
    $scope.Students = [];
    $scope.testShow = false;
    //获得学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });
    //绑定考试类型，考试号
    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });
    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得班主任年级
        $scope.queryService.GetScope($scope.user.TeacherID, function (data) {
            var rs = JSON.parse(data.d);
            $scope.teacherScope = rs[0].scope.substring(0, 2);
            $scope.classCode = rs[0].scope;
            $scope.queryService.GetBCourse($scope.teacherScope, function (data) {
                $scope.GradeCourses = data.d;
            });
        });
    });
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.queryService.GetTestNo($scope.MicYear.MicYear, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    $scope.$watch('MicYear', function (micyear) {
        $scope.Students.length = 0;
        if (micyear) {
            $scope.testShow = true;
            $scope.utilService.GetStudent($scope.MicYear.MicYear, $scope.classCode, function (data) {
                $scope.Students = data.d;
            });
        }
    });
    $scope.query = function () {
        $scope.student = $scope.utilService.getlist($('input[name=selected]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        
    }
}]);


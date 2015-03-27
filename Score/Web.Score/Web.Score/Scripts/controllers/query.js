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
    //获得学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    var load = function () {
        var good = 0.75;
        var fail = 0.6
        $scope.utilService.showBg();
        //成绩列表
        $scope.queryService.GetQueryTeacher($scope.MicYear.MicYear, $scope.user.TeacherID, $scope.GradeCourse.CourseCode, $scope.GradeCode.GradeNo, $scope.TestType, $scope.TestNo, $scope.student,null, function (data) {
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
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
                $scope.aveNumScore = 0;
                $scope.couNumScore = 0;
                $scope.goodNumScore = 0;
                $scope.failNumScore = 0;
            }
            $scope.utilService.closeBg();
        });
        $scope.testShow = true;
        //绑定考试类型，考试号
        if ($scope.TestTypes.length <= 0) {
            $scope.utilService.GetTestType(function (data) {
                $scope.TestTypes = data.d;
            });
        }
    }
    //获得考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.GradeCode.GradeNo.substring(0, 2), '', testType.Code, function (data) {
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

    $scope.order = function (orderNum) {
        $scope.utilService.showBg();
        $scope.queryService.GetQueryTeacher($scope.MicYear.MicYear, $scope.user.TeacherID, $scope.GradeCourse.CourseCode, $scope.GradeCode.GradeNo, $scope.TestType, $scope.TestNo, $scope.student, orderNum, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            $scope.pageService.init($scope.studentsGrid, 10);
            $scope.utilService.closeBg();
        });
    }

    var cAll = 0;
    $scope.checkALl = function () {
        if (cAll == 0) {
            $("input[name=selected]").attr("checked", true);
            cAll = 1;
        } else {
            $("input[name=selected]").attr("checked", false);
            cAll = 0;
        }
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
    $scope.pageService = pageService;
    $scope.pageService.reset();

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
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.teacherScope, '', testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    $scope.$watch('MicYear', function (micyear) {
        $scope.Students.length = 0;
        if (micyear) {
            $scope.utilService.GetStudent($scope.MicYear.MicYear, $scope.classCode, function (data) {
                $scope.Students = data.d;
            });
            $scope.testShow = true;
        }
    });
    $scope.query = function () {
        $scope.student = $scope.utilService.getlist($('input[name=selected]'));
        $scope.courseCode = $scope.utilService.getlist($('input[name=grades]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        load();
    }
    var load = function () {
        var good = 0.75;
        var fail = 0.6
        $scope.utilService.showBg();
        //成绩列表
        $scope.queryService.GetQueryBTeacher($scope.MicYear.MicYear, $scope.courseCode, $scope.classCode, $scope.TestType, $scope.TestNo, $scope.student, null, function (data) {
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
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
                $scope.aveNumScore = 0;
                $scope.couNumScore = 0;
                $scope.goodNumScore = 0;
                $scope.failNumScore = 0;
            }
            $scope.utilService.closeBg();
            $scope.testShow = true;
        });
    }
    $scope.order = function (orderNum) {
        $scope.utilService.showBg();
        $scope.queryService.GetQueryBTeacher($scope.MicYear.MicYear, $scope.courseCode, $scope.classCode, $scope.TestType, $scope.TestNo, $scope.student, orderNum, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            $scope.pageService.init($scope.studentsGrid, 10);
            $scope.utilService.closeBg();
        });
    }
}]);

// Path: /
appQuery.controller('GradeManagerController', ['$scope', 'pageService', function ($scope, pageService) {
    $scope.$root.title = $scope.softname + ' | 年级领导查询';
    $scope.$root.moduleName = '年级领导查询';
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];
    $scope.GradeCourses = [];
    $scope.Grades = [];
    $scope.Students = [];
    $scope.testShow = false;
    $scope.orderNum = 0;
    $scope.pageService = pageService;
    $scope.pageService.reset();

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
        //获得年级领导年级
        $scope.queryService.GetGradeScope($scope.user.TeacherID, function (data) {
            if (data.d == '') return;
            var rs = JSON.parse(data.d);
            //$scope.classCode = rs[0].scope;
            if (rs.length === 0) {
                $scope.classCode = '';
            } else $scope.classCode = rs[0].scope;
            //根据年级获得课程
            //$scope.queryService.GetBCourse($scope.classCode, function (data) {
            //    $scope.GradeCourses = data.d;
            //});
        });
    });
    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.classCode, '', testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    $scope.$watch('MicYear', function (micyear) {
        $scope.Grades.length = 0;
        $scope.Students.length = 0;
        if (micyear) {
            //获得班级
            //$scope.queryService.GetGradeByGradeNo($scope.MicYear.MicYear, $scope.classCode, function (data) {
            //    if (data.d != '') {
            //        $scope.Grades = JSON.parse(data.d);
            //    }
            //});
            //$scope.testShow = true;
        }
    });
    $scope.query = function () {
        $scope.gradeCode = $scope.utilService.getlist($('input[name=grades]'));
        $scope.courseCode = $scope.utilService.getlist($('input[name=courses]'));
        $scope.student = $scope.utilService.getlist($('input[name=students]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        $scope.testShow = true;
        load();
    }
    var load = function () {
        var good = 0.75;
        var fail = 0.6
        $scope.utilService.showBg();
        //根据班级获得学生
        if ($scope.Students.length <= 0) {
            $scope.utilService.GetStudents(function (data) {
                $scope.Students = data.d;
            });
        }

        //成绩列表
        $scope.queryService.GetQueryGradeManager($scope.MicYear.MicYear, $scope.courseCode, $scope.classCode, $scope.TestType, $scope.TestNo, $scope.gradeCode, $scope.stu, $scope.orderNum, function (data) {
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
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
                $scope.aveNumScore = 0;
                $scope.couNumScore = 0;
                $scope.goodNumScore = 0;
                $scope.failNumScore = 0;
            }
            $scope.utilService.closeBg();
            $scope.testShow = true;
        });
    }
    $scope.order = function (orderNum) {
        $scope.utilService.showBg();
        $scope.gradeCode = $scope.utilService.getlist($('input[name=grades]'));
        $scope.courseCode = $scope.utilService.getlist($('input[name=courses]'));
        $scope.student = $scope.utilService.getlist($('input[name=students]'));
        $scope.queryService.GetQueryGradeManager($scope.MicYear.MicYear, $scope.courseCode, $scope.classCode, $scope.TestType, $scope.TestNo, $scope.gradeCode, $scope.stu, orderNum, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            $scope.pageService.init($scope.studentsGrid, 10);
            $scope.utilService.closeBg();
        });
    }
    //$scope.bindStudents = function () {
    //    var classNo = $scope.utilService.getlist($('input[name=grades]'));
    //    $scope.Students.length = 0;
    //    if (classNo) {
    //        //根据班级获得学生
    //        $scope.utilService.GetStudentsByGrade($scope.MicYear.MicYear, classNo, function (data) {
    //            $scope.Students = data.d;
    //        });
    //    }
    //}
}]);

// Path: /
appQuery.controller('SchoolManagerQueryController', ['$scope', 'pageService', function ($scope, pageService) {
    $scope.$root.title = $scope.softname + ' | 教务员查询';
    $scope.$root.moduleName = '教务员查询';
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];
    $scope.GradeCourses = [];
    $scope.Grades = [];
    $scope.GradeCodes = [];
    $scope.Students = [];
    $scope.Teachers = [];
    $scope.testShow = false;
    $scope.orderNum = 0;
    $scope.pageService = pageService;
    $scope.pageService.reset();

    //获得学年
    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });
    //获得所有年级
    $scope.utilService.GetGradeAll(function (data) {
        $scope.GradeCodes = data.d;
    });
    //绑定考试类型
    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });
    //获得所有课程
    $scope.utilService.GetCourseCodeAll(function (data) {
        $scope.GradeCourses = data.d;
    });
    //获得所有学生    
    $scope.utilService.GetStudents(function (data) {
        $scope.Students = data.d;
    });
    //获得所有教师    
    $scope.utilService.GerTeacherAll(function (data) {
        $scope.Teachers = data.d;
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得年级领导年级
        $scope.queryService.GetGradeScope($scope.user.TeacherID, function (data) {
            if (data.d == '') return;
            var rs = JSON.parse(data.d);
            if (rs.length === 0) {
                $scope.classCode = '';
            } else $scope.classCode = rs[0].scope;
        });
    });
    $scope.$watch('MicYear', function (micyear) {
        if (micyear)
            $scope.testShow = true;
    });

    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.classCode, '', testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
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
    $scope.query = function () {
        $scope.classNo = $scope.utilService.getlist($('input[name=grades]'));
        $scope.courseCode = $scope.utilService.getlist($('input[name=courses]'));
        $scope.student = $scope.utilService.getlist($('input[name=students]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        load();
    }
    var load = function () {
        var good = 0.75;
        var fail = 0.6
        $scope.utilService.showBg();
        //成绩列表
        $scope.queryService.GetQuerySchoolManager($scope.MicYear.MicYear, $scope.courseCode, $scope.GradeCode, $scope.TestType, $scope.TestNo, $scope.classNo, $scope.stu, $scope.teacher, $scope.orderNum, function (data) {
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
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
                $scope.aveNumScore = 0;
                $scope.couNumScore = 0;
                $scope.goodNumScore = 0;
                $scope.failNumScore = 0;
            }
            $scope.utilService.closeBg();
            $scope.testShow = true;
        });
    }
    $scope.order = function (orderNum) {
        $scope.utilService.showBg();
        $scope.queryService.GetQuerySchoolManager($scope.MicYear.MicYear, $scope.courseCode, $scope.GradeCode, $scope.TestType, $scope.TestNo, $scope.classNo, $scope.stu, $scope.teacher, $scope.orderNum, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            $scope.pageService.init($scope.studentsGrid, 10);
            $scope.utilService.closeBg();
        });
    }
    $scope.bindStudents = function () {
        var classNo = $scope.utilService.getlist($('input[name=grades]'));
        $scope.Students.length = 0;
        if (classNo) {
            //根据班级获得学生
            $scope.utilService.GetStudentsByGrade($scope.MicYear.MicYear, classNo, function (data) {
                $scope.Students = data.d;
            });
        }
        else {
            //获得所有学生
            if ($scope.Students.length <= 0) {
                $scope.utilService.GetStudents(function (data) {
                    $scope.Students = data.d;
                });
            }
        }
    }
}]);

// Path: /
appQuery.controller('SchoolHeadController', ['$scope', 'pageService', function ($scope, pageService) {
    $scope.$root.title = $scope.softname + ' | 校领导查询';
    $scope.$root.moduleName = '校领导查询';
    $scope.AcademicYears = [];
    $scope.TestLogins = [];
    $scope.TestTypes = [];
    $scope.GradeCourses = [];
    $scope.Grades = [];
    $scope.GradeCodes = [];
    $scope.Students = [];
    $scope.Teachers = [];
    $scope.testShow = false;
    $scope.orderNum = 0;
    $scope.pageService = pageService;
    $scope.pageService.reset();

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
    //获得所有课程
    $scope.utilService.GetCourseCodeAll(function (data) {
        $scope.GradeCourses = data.d;
    });
    //获得所有学生    
    $scope.utilService.GetStudents(function (data) {
        $scope.Students = data.d;
    });
    //获得所有教师    
    $scope.utilService.GerTeacherAll(function (data) {
        $scope.Teachers = data.d;
    });

    //获得用户id
    $scope.userService.getUser(function (data) {
        $scope.user = data;
        //获得年级领导年级
        $scope.queryService.GetGradeScope($scope.user.TeacherID, function (data) {
            if (data.d == '') return;
            var rs = JSON.parse(data.d);
            if (rs.length > 0) {
                $scope.classCode = rs[0].scope;
            } else $scope.classCode = '';

        });
    });
    $scope.$watch('MicYear', function (micyear) {
        if (micyear)
            $scope.testShow = true;
    });

    //监控考试类型，绑定考试号
    $scope.$watch('TestType', function (testType) {
        $scope.TestLogins.length = 0;
        if (testType != null) {
            if (testType.Code == null) $scope.TestType = null;
            $scope.utilService.GetTestLogin($scope.MicYear.MicYear, $scope.classCode, '', testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
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
    $scope.query = function () {
        $scope.classNo = $scope.utilService.getlist($('input[name=grades]'));
        $scope.courseCode = $scope.utilService.getlist($('input[name=courses]'));
        $scope.student = $scope.utilService.getlist($('input[name=students]'));
        if ($scope.MicYear == null) {
            $scope.dialogUtils.info('请选择学年/学期');
            return;
        }
        load();
    }
    var load = function () {
        var good = 0.75;
        var fail = 0.6
        $scope.utilService.showBg();
        //成绩列表
        $scope.queryService.GetQuerySchoolManager($scope.MicYear.MicYear, $scope.courseCode, $scope.GradeCode, $scope.TestType, $scope.TestNo, $scope.classNo, $scope.stu, $scope.teacher, $scope.orderNum, function (data) {
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
                $scope.maxNumScore = 0;
                $scope.minNumScore = 0;
                $scope.aveNumScore = 0;
                $scope.couNumScore = 0;
                $scope.goodNumScore = 0;
                $scope.failNumScore = 0;
            }
            $scope.utilService.closeBg();
            $scope.testShow = true;
        });
    }
    $scope.order = function (orderNum) {
        $scope.utilService.showBg();
        $scope.queryService.GetQuerySchoolManager($scope.MicYear.MicYear, $scope.courseCode, $scope.GradeCode, $scope.TestType, $scope.TestNo, $scope.classNo, $scope.stu, $scope.teacher, $scope.orderNum, function (data) {
            $scope.studentsGrid = JSON.parse(data.d);
            $scope.pageService.init($scope.studentsGrid, 10);
            $scope.utilService.closeBg();
        });
    }
    $scope.bindStudents = function () {
        var classNo = $scope.utilService.getlist($('input[name=grades]'));
        $scope.Students.length = 0;
        if (classNo) {
            //根据班级获得学生
            $scope.utilService.GetStudentsByGrade($scope.MicYear.MicYear, classNo, function (data) {
                $scope.Students = data.d;
            });
        }
        else {
            //获得所有学生
            if ($scope.Students.length <= 0) {
                $scope.utilService.GetStudents(function (data) {
                    $scope.Students = data.d;
                });
            }
        }
    }
}]);
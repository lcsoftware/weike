'use strict';

var appAdmin = angular.module('app.admin', ['ui.tree', 'checklist-model', 'angularFileUpload']);

// Path: /UserEdit  用户(组)维护
appAdmin.controller('UserEditController', ['$scope', function ($scope) {

    $scope.$root.title = $scope.softname + ' | 用户(组)维护';
    $scope.$root.moduleName = '用户(组)管理';

    $scope.opt = {
        nodeChildren: "children",
        dirSelectable: false
    };
    $scope.query = "";
    $scope.userForm = {};
    $scope.userGroups = [];
    $scope.UserGroupEntity = {};

    $scope.Nations = [];
    $scope.Politics = [];
    $scope.ResidenceTypes = [];
    $scope.userNation = {};

    $scope.schoolName = $scope.schoolService.school.SchoolName;

    $scope.showSelected = function (node) {
        $scope.tpl = node.UserOrGroup === '0' ? 'group.html' : 'user.html';
        $scope.UserGroupEntity = node;
    }
    var init = function () {
        $scope.userGroups = [];
        $scope.userService.buildGroupUserTree(function (groupAndUsers, users) {
            var group = { Name: '用户组', UserOrGroup: -1, Children: [] };
            var user = { Name: '所有用户', UserOrGroup: -2, Children: [] };
            if (groupAndUsers !== null) {
                var length = groupAndUsers.length;
                for (var i = 0; i < length; i++) {
                    group.Children.push(groupAndUsers[i]);
                }
            }
            if (users !== null) {
                var length = users.length;
                for (var i = 0; i < length; i++) {
                    users[i].Children = [];
                    user.Children.push(users[i]);
                }
            }
            $scope.userGroups.push(group);
            $scope.userGroups.push(user);
        });
    }

    init();

    //民族
    $scope.utilService.GetNations(function (data) {
        if (data.d !== null) {
            $scope.Nations = data.d;
            if ($scope.Nations.length > 0) {
                $scope.userForm.userNation = $scope.Nations[0];
            }
        }
    });
    //政治面貌
    $scope.utilService.GetPolitics(function (data) {
        if (data.d !== null) {
            $scope.Politics = data.d;
            if ($scope.Politics.length > 0) {
                $scope.userForm.userPolitic = $scope.Politics[1];
            }
        }
    });
    //户口类别
    $scope.utilService.GetResidenceType(function (data) {
        if (data.d !== null) {
            $scope.ResidenceTypes = data.d;
            if ($scope.ResidenceTypes.length > 0) {
                $scope.userForm.userResident = $scope.ResidenceTypes[0];
            }
        }
    });

    $scope.addUserGroup = function (category) {
        $scope.tpl = category === 0 ? 'group.html' : 'user.html';
        $scope.userService.addUserGroup(category === 0 ? '0' : '1', function (data) {
            $scope.UserGroupEntity = data.d;
        });
    }

    $scope.removeUserGroup = function (userGroup) {
        $scope.dialogUtils.confirm('你真的要删除吗？', function () {
            $scope.userService.removeUserGroup(userGroup, function (data) {
                $scope.dialogUtils.info(data.d.Context);
                if (data.d.State === 1) {
                    init();
                }
            })
        });
    }

    $scope.editUserGroup = function (userGroup) {
        $scope.UserGroupEntity = userGroup;
        if ($scope.UserGroupEntity.UserOrGroup === '1') {
            $scope.UserGroupEntity.ConfirmPwd = $scope.UserGroupEntity.Password;
            $scope.tpl = 'user.html';
            $scope.userForm.userNation = $scope.utilService.locate($scope.Nations, 'NationNo', $scope.UserGroupEntity.NationNo);
            $scope.userForm.userResident = $scope.utilService.locate($scope.ResidenceTypes, 'ResidenceType', $scope.UserGroupEntity.ResidentNo);
            $scope.userForm.userPolitic = $scope.utilService.locate($scope.Politics, 'PoliticsCode', $scope.UserGroupEntity.PoliticCode);
        } else {
            $scope.tpl = 'group.html';
        }
    }

    $scope.saveUserGroup = function () {
        if ($scope.UserGroupEntity.UserOrGroup === '1') {
            if ($scope.UserGroupEntity.Password === undefined || !$scope.UserGroupEntity.ConfirmPwd === undefined) {
                dialogUtils.info('必须输入密码！');
                return;
            }
            if ($scope.UserGroupEntity.ConfirmPwd !== $scope.UserGroupEntity.Password) {
                $scope.UserGroupEntity.Password = '';
                $scope.UserGroupEntity.ConfirmPwd = '';
                dialogUtils.info('两次密码输入不一致，请重新输入！');
                return;
            }
            $scope.UserGroupEntity.NationNo = $scope.userForm.userNation.NationNo;
            $scope.UserGroupEntity.PoliticCode = $scope.userForm.userPolitic.PoliticsCode;
            $scope.UserGroupEntity.ResidentNo = $scope.userForm.userResident.ResidenceType;
        }
        var isAdd = $scope.UserGroupEntity.TeacherID === '-1';
        $scope.userService.saveUserGroup($scope.UserGroupEntity, function (data) {
            if (data.d === -1) {
                dialogUtils.info("数据库中已经存在名称为" + $scope.UserGroupEntity.Name + "的用户或组");
            } else {
                if (data.d === 1 && isAdd) {
                    if ($scope.UserGroupEntity.UserOrGroup === '0') {
                        $scope.userGroups[0].Children.push($scope.UserGroupEntity);
                    } else {
                        $scope.userGroups[1].Children.push($scope.UserGroupEntity);
                    }
                }
                $scope.dialogUtils.info(data.d === 0 ? '用户(组)操作失败，请重试！' : '用户(组)操作成功');
                init();
            }
        });
    }
}]);

// Path: /UserEdit  用户(组)维护
appAdmin.controller('GroupEditController', ['$scope', function ($scope) {
    $scope.$root.moduleName = '用户组管理';

    $scope.Groups = [];
    $scope.selectedGroup = {};

    $scope.allUsers = [];
    $scope.usersOfGroup = [];

    $scope.userService.getUserGroups('0', function (data) {
        if (data.d !== null) {
            $scope.Groups = [];
            angular.extend($scope.Groups, data.d);
        }
    });

    $scope.userService.getAllUsers(function (data) {
        if (data.d !== null) {
            $scope.allUsers = data.d;
        }
    });

    $scope.groupChanged = function (userGroup) {
        $scope.userService.getUsersOfGroup(userGroup.TeacherID, function (data) {
            $scope.usersOfGroup.length = 0;
            if (data.d !== null) {
                var length = data.d.length;
                for (var i = 0; i < length; i++) {
                    $scope.usersOfGroup.push(data.d[i].TeacherID);
                }
            }
        });
    }

    $scope.doChanged = function (userCheck) {
        var groupID = $scope.selectedGroup.TeacherID;
        var teacher = userCheck.$parent.user.TeacherID;
        if (groupID === undefined) {
            userCheck.checked = !userCheck.checked;
            $scope.dialogUtils.info('请选择用户组！');
            return;
        }
        if (userCheck.checked) {
            $scope.userService.joinGroup(teacher, groupID);
        } else {
            $scope.userService.leaveGroup(teacher, groupID);
        }
    }

}]);

// Path: /AuthView 权限查询 
appAdmin.controller('AuthViewController', ['$scope', 'menuService', function ($scope, menuService) {
    $scope.$root.moduleName = '权限查询';

    $scope.userFuncs = [];
    $scope.userGroups = [];
    $scope.selectedUserGroup = {};

    $scope.userService.getAllUserGroups(function (data) {
        if (data.d !== null) {
            $scope.userGroups = data.d;
        }
    });

    $scope.userGroupChanged = function (userGroup) {
        $scope.userFuncs.length = 0;
        $scope.userService.getUserAuths(userGroup.TeacherID, function (data) {
            if (data.d !== null) {
                $scope.userFuncs = data.d;
            }
        });
    }
}]);

// Path: /AuthEdit 权限编辑
appAdmin.controller('AuthEditController', ['$scope', 'menuService', function ($scope, menuService) {
    var moduleName = '权限编辑';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.allFuncs = [];
    $scope.userGroups = [];
    $scope.selectedTeacher = {};

    $scope.userService.buildGroupUserTree(function (groupAndUsers, users) {
        var group = { Name: '用户组', UserOrGroup: -1, Children: [] };
        var user = { Name: '所有用户', UserOrGroup: -2, Children: [] };
        if (groupAndUsers !== null) {
            var length = groupAndUsers.length;
            for (var i = 0; i < length; i++) {
                group.Children.push(groupAndUsers[i]);
            }
        }
        if (users !== null) {
            var length = users.length;
            for (var i = 0; i < length; i++) {
                users[i].Children = [];
                user.Children.push(users[i]);
            }
        }
        $scope.userGroups.push(group);
        $scope.userGroups.push(user);
    });

    $scope.userService.getFuncTree(function (data) {
        if (data.d !== null) {
            $scope.allFuncs.push(data.d);
        }
    });

    //清除功能树状态
    var ClearFuncState = function (treeFunc) {
        treeFunc.Kind = 0;
        angular.forEach(treeFunc.Children, function (v) {
            ClearFuncState(v);
        });
    }

    //var BindFunc = function (userFunc, treeFunc) {
    //    if (treeFunc.FuncID === userFunc.FuncID) {
    //        treeFunc.Kind = userFunc.GroupID === "-1" ? 2 : 1;
    //    }
    //    if (treeFunc.Children.length > 0) {
    //        var length = treeFunc.Children.length;
    //        for (var i = 0; i < length; i++) {
    //            BindFunc(userFunc, treeFunc.Children[i]);
    //        }
    //    }
    //}

    var BindFuncs = function (userFuncs, treeFunc) {

        if (treeFunc.FuncID === userFuncs.FuncID) {
            treeFunc.Kind = userFuncs.Kind;
        }
        angular.forEach(userFuncs.Children, function (userFunc) {
            var length = treeFunc.Children.length;
            for (var i = 0; i < length; i++) {
                BindFuncs(userFunc, treeFunc.Children[i]);
            }
        });
    }

    $scope.authView = function (teacher) {
        ClearFuncState($scope.allFuncs[0]);
        var sysKeep = teacher.TeacherID.substr(10, 4);
        if (sysKeep === '1001' || sysKeep === '0001' || sysKeep === '0888' || sysKeep === '0002') {
            $scope.dialogUtils.info('系统保留用户(组),不允许进行权限编辑');
            return;
        }

        $scope.selectedTeacher.Selected = false;
        $scope.selectedTeacher = teacher;
        menuService.getUserFuncs(teacher.TeacherID, function (data) {
            if (data.d !== null) {
                var userFuncs = data.d;
                BindFuncs(userFuncs, $scope.allFuncs[0]);
            }
            teacher.Selected = true;
        });
    }


    var findParent = function (treeFunc, func) {
        if (treeFunc.Parent === func.FuncID) return func;
        var length = func.Children.length;
        for (var i = 0; i < length; i++) {
            if (findParent(treeFunc, func.Children[i]))
                return func.Children[i];
        }
    }

    $scope.assignFunc = function (treeFunc) {
        if ($scope.selectedTeacher === null) {
            $scope.dialogUtils.info('请选择需要授予权限的用户(组)');
            return;
        }
        var v = treeFunc;
        if (treeFunc.Kind == 0) {
            grant(treeFunc);
        } else {
            revoke(treeFunc);
        }
    }

    var grant = function (treeFunc) {
        if (treeFunc.FuncID > 0) {
            var p = findParent(treeFunc, $scope.allFuncs[0]);
            if (p !== null && p.Kind == 0) {
                var context = '请先授予 <' + p.Description + '> 功能';
                $scope.dialogUtils.info(context);
                return;
            }
        }
        var rtype = treeFunc.FuncID === 0 ? 1 : 0;
        $scope.userService.grant($scope.selectedTeacher.TeacherID, treeFunc.FuncID, rtype, 2, function (data) {
            if (data.d > 0) {
                treeFunc.Kind = 2;
            }
        });
    }

    var revoke = function (treeFunc) {
        if (treeFunc.Kind === 1) {
            $scope.dialogUtils.info('权限从组中继承不能撤消!');
            return;
        }

        $scope.userService.revoke($scope.selectedTeacher.TeacherID, treeFunc, function (data) {
            if (data.d > 0) treeFunc.Kind = 0;
        });
    }
}]);

// Path: /StayGrade  升留级处理
appAdmin.controller('StayGradeController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    var moduleName = '升留级处理';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.grades = [];
    $scope.keeps = [];

    $scope.schoolService.loadGradeClass($scope.schoolService.school.AcademicYear, function (data) {
        $scope.grades = data.d;
    })

    var findStudent = function (student) {
    }

    $scope.keep = function (student) {
        student.Keep = !student.Keep;
        $scope.keeps.push(student);
    }

    $scope.upgrade = function (student) {
        student.Keep = !student.Keep;
        var tmpKeeps = [];
        var length = $scope.keeps.length;
        for (var i = 0; i < length; i++) {
            var st = $scope.keeps[i];
            if (st.StudentId != student.StudentId) {
                tmpKeeps.push(st);
            }
        }
        $scope.keeps.length = 0;
        $scope.keeps = tmpKeeps;
    }

    $scope.upDown = function () {
        var year = $scope.schoolService.school.AcademicYear;
        $scope.schoolService.upDown(year, $scope.keeps, $scope.grades, function (data) {
            if (data.d == 1) {
                $scope.schoolService.loadSchool();
                $scope.dialogUtils.info('升留级执行完毕！');
            }
        });
    }
}]);

// Path: /CJtoXJ  转换为学籍成绩
appAdmin.controller('CJtoXJController', ['$scope', 'schoolProviderUrl', 'pageService', function ($scope, schoolProviderUrl, pageService) {
    var moduleName = '转换为学籍成绩';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.pageService = pageService;
    $scope.pageService.reset();

    $scope.conditionData = {};
    $scope.sumDecEntry = {};
    $scope.studentScores = [];

    $scope.AcademicYears = [];
    $scope.GradeCodes = [];
    $scope.GradeCourses = [];
    $scope.TestTypes = [];
    $scope.TestLogins = [];
    $scope.ScoreSorts = $scope.constService.ScoreSorts;

    $scope.canTryOk = false;

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.utilService.GetTestType(function (data) {
        $scope.TestTypes = data.d;
    });

    $scope.$watch('conditionData.GradeCode', function (gradeCode) {
        $scope.GradeCourses = null;
        if (gradeCode) {
            $scope.utilService.GetGradeCourse($scope.conditionData.MicYear.MicYear, gradeCode, function (data) {
                $scope.GradeCourses = data.d;
            });
        }
    });

    $scope.$watch('conditionData.TestType', function (testType) {
        if (testType && $scope.conditionData.MicYear) {
            var micYear = $scope.conditionData.MicYear;
            var gradeCode = $scope.conditionData.GradeCode ? $scope.conditionData.GradeCode.GradeNo : '';
            var gradeCourse = $scope.conditionData.GradeCourse ? $scope.conditionData.GradeCourse.CourseCode : '';
            $scope.utilService.GetTestLogin(micYear.MicYear, gradeCode, gradeCourse, testType.Code, function (data) {
                $scope.TestLogins = data.d;
            });
        }
    });
    //试算
    $scope.tryCalculate = function (cv, kv) {
        if (!$scope.conditionData.MicYear) {
            $scope.dialogUtils.info("请选择学年");
            return;
        }
        if (!$scope.conditionData.GradeCode) {
            $scope.dialogUtils.info("请选择年级");
            return;
        }
        if (!$scope.conditionData.GradeCourse) {
            $scope.dialogUtils.info("请选择课程");
            return;
        }
        if (!$scope.conditionData.TestType) {
            $scope.dialogUtils.info("请选择考试类型");
            return;
        }
        if (cv < 30 || cv > 90) {
            $scope.dialogUtils.info('平移量不能太小或太大！');
            return;
        }

        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeNo = $scope.conditionData.GradeCode.GradeNo;
        var courseCode = $scope.conditionData.GradeCourse.CourseCode;
        var testType = $scope.conditionData.TestType.Code;
        $scope.schoolService.tryCalculate(micYear, gradeNo, courseCode, testType, function (data) {
            var result = data.d;
            if (result === -1) {
                $scope.dialogUtils.info('您选择的考试无人参加');
                return;
            }

            if (result === -2) {
                $scope.dialogUtils.info('您对本次考试还未进行统计！');
                return;
            }

            if (result === -3) {
                $scope.dialogUtils.info('在您统计本次考试后有可能有新成绩录入，请再次统计！');
                return;
            }
            var info = "有标准分的人数与总人数不一致！相差" + result.toString() + " 人，您继续吗?";
            $scope.dialogUtils.confirm(info,
                function () {
                    var cv = $scope.cValue;
                    var kv = $scope.kValue;
                    $scope.schoolService.tryCalculateAgain(cv, kv, micYear, gradeNo, courseCode, testType, function (data) {
                        $scope.kValue = data.d;
                        $scope.canTryOk = true;
                    });
                },
                function () { });
        });
    }

    //试算确定
    $scope.tryOk = function () {
        var cv = $scope.cValue;
        var kv = $scope.kValue;
        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeNo = $scope.conditionData.GradeCode.GradeNo;
        var courseCode = $scope.conditionData.GradeCourse.CourseCode;
        var testType = $scope.conditionData.TestType.Code;
        $scope.schoolService.tryOk(cv, kv, micYear, gradeNo, courseCode, testType, function (data) {
            var result = data.d;
            if (result == -1) {
                $scope.dialogUtils.info('您设置K值不能大于试算的K值！');
                return;
            }
            $scope.canTryOk = false;
            $scope.kValue = '';
        });
    }
    //查看转换前数据
    $scope.viewOriginData = function () {
        if (!$scope.conditionData.MicYear) {
            $scope.dialogUtils.info("请选择学年");
            return;
        }
        if (!$scope.conditionData.GradeCode) {
            $scope.dialogUtils.info("请选择年级");
            return;
        }
        if (!$scope.conditionData.GradeCourse) {
            $scope.dialogUtils.info("请选择课程");
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info("请选择考试号");
            return;
        }
        var micYear = $scope.conditionData.MicYear.MicYear;
        var semester = $scope.schoolService.school.Semester;
        var gradeNo = $scope.conditionData.GradeCode.GradeNo;
        var courseCode = $scope.conditionData.GradeCourse.CourseCode;
        var testType = $scope.conditionData.TestType.Code;
        var testNo = $scope.conditionData.TestLogin.TestLoginNo;

        var url = schoolProviderUrl + '/ViewOriginData';
        var param = {
            micYear: micYear,
            semester: $scope.schoolService.school.Semester,
            gradeNo: gradeNo,
            courseCode: courseCode,
            testType: testType,
            testNo: testNo
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.pageService.init(data.d, 10);
        });

        url = schoolProviderUrl + '/SumDec';
        param = {
            micYear: micYear,
            semester: $scope.schoolService.school.Semester,
            gradeNo: gradeNo,
            courseCode: courseCode,
            testType: testType,
            testNo: testNo
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.sumDecEntry = data.d;
        });
    }
    //转入学籍系统
    $scope.convertToXJ = function () {
        if (!$scope.conditionData.GradeCode) {
            $scope.dialogUtils.info("请选择年级");
            return;
        }
        if (!$scope.conditionData.GradeCourse) {
            $scope.dialogUtils.info("请选择课程");
            return;
        }
        if (!$scope.conditionData.TestLogin) {
            $scope.dialogUtils.info("请选择考试号");
            return;
        }
        if (!$scope.conditionData.ScoreSort) {
            $scope.dialogUtils.info("请选择将哪种成绩转换？");
            return;
        }
        var micYear = $scope.conditionData.MicYear.MicYear;
        var gradeNo = $scope.conditionData.GradeCode.GradeNo;
        var courseCode = $scope.conditionData.GradeCourse.CourseCode;
        var testType = $scope.conditionData.TestType.Code;
        var testNo = $scope.conditionData.TestLogin.TestLoginNo;
        var scoreSort = $scope.conditionData.ScoreSort.code;
        var teachOption = $scope.teacherOption || false;
        var url = schoolProviderUrl + '/ConvertToXJ';
        var param = {
            micYear: micYear,
            semester: $scope.schoolService.school.Semester,
            gradeNo: gradeNo,
            courseCode: courseCode,
            testType: testType,
            testNo: testNo,
            scoreSort: scoreSort,
            ckTeacherOp: $scope.teacherOption === '1'
        };
        $scope.baseService.post(url, param, function (data) {
            $scope.dialogUtils.info('转换操作完成！');
        });
    }

    $scope.exportDisabled = function () {
        if (!$scope.conditionData.TestType) return true;

        if ($scope.conditionData.TestType.Code === 0) {
            return !$scope.conditionData.MicYear ||
                    !$scope.conditionData.GradeCode ||
                    !$scope.conditionData.GradeCourse ||
                    !$scope.conditionData.ScoreSort ||
                    !$scope.schoolService.school.Semester;

        } else {
            return !$scope.conditionData.MicYear ||
                   !$scope.conditionData.GradeCode ||
                   !$scope.conditionData.GradeCourse ||
                    !$scope.conditionData.ScoreSort ||
                   !$scope.conditionData.TestLogin;
        }

        return true;
    }

    ///导出Excel
    $scope.exportToExcel = function () {
        var micYear = $scope.conditionData.MicYear.MicYear;
        var semester = $scope.school.Semester;
        var gradeNo = $scope.conditionData.GradeCode.GradeNo;
        var courseCode = $scope.conditionData.GradeCourse.CourseCode;
        var testType = $scope.conditionData.TestType.Code;
        var testNo = $scope.conditionData.TestLogin.TestLoginNo;
        var scoreSort = $scope.conditionData.ScoreSort.code;

        var url = "/DataProvider/Down.aspx?type=2";
        url += "&micYear=" + micYear;
        url += "&semester=" + semester;
        url += "&gradeNo=" + gradeNo;
        url += "&courseCode=" + courseCode;
        url += "&testType=" + testType;
        url += "&testNo=" + testNo;
        url += "&scoreSort=" + scoreSort;
        return url;
    }

}]);

// Path: /UserEdit  从学籍成绩转换过来
appAdmin.controller('XJtoCJController', ['$scope', 'schoolProviderUrl', 'pageService', function ($scope, schoolProviderUrl, pageService) {
    var moduleName = '从学籍转换过来';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.conditionData = {};
    $scope.GradeCodes = [];
    $scope.AcademicYears = [];
    $scope.Terms = $scope.constService.Terms;
    $scope.selectedAll = 0;

    $scope.pageService = pageService;
    $scope.pageService.reset();

    $scope.utilService.GetGradeCodes(function (data) {
        $scope.GradeCodes = data.d;
    });

    $scope.baseService.post("/DataProvider/School.aspx/CanSelectAll", null, function (data) {
        $scope.canSelectAll = data.d > 0;
    });

    $scope.utilService.GetAcademicYears(function (data) {
        $scope.AcademicYears = data.d;
    });

    $scope.viewData = function () {
        var url = "/DataProvider/School.aspx/ViewData";
        var param = { micYear: $scope.conditionData.MicYear.MicYear, chkAll: $scope.selectedAll, cbTestType: $scope.conditionData.Terms.Code };
        $scope.baseService.post(url, param, function (data) {
            if (data.d !== '') {
                $scope.pageService.init(angular.fromJson(data.d), 10);
            } else {
                $scope.pageService.reset();
            }
        });
    }

    $scope.convertToCJ = function () {

        if (!$scope.conditionData.MicYear || !$scope.conditionData.Terms) {
            $scope.dialogUtils.info('请选择学年及考试！');
            return;
        }

        var url = "/DataProvider/School.aspx/ConvertToCJ";

        var param = {
            micYear: $scope.conditionData.MicYear.MicYear,
            chkAll: $scope.selectedAll,
            cbTestType: $scope.conditionData.Terms.Code,
            canContinue: false
        };

        $scope.baseService.post(url, param, function (data) {
            if (data.d === -1) {
                $scope.dialogUtils.confirm('发现本学年已经登记过考试，您要继续吗?', function () {
                    param.canContinue = true;
                    $scope.baseService.post(url, param, function (data) {
                        $scope.dialogUtils.info('转入本系统操作完成！');
                    });
                });

            } else {
                $scope.dialogUtils.info('转入本系统操作完成！');
            }
        });
    }
}]);

// Path: /StudentImport 学生编号导入
appAdmin.controller('StdImportController', ['$scope', 'FileUploader', 'uploadService', function ($scope, FileUploader, uploadService) {
    var moduleName = '学生编号导入';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;
    $scope.students = [];

    $scope.fileUploader = uploadService.create({ type: 1 }, function (fileItem, response, status, headers) {
        $scope.students.length = 0;
        if (response === '-1') {
            $scope.dialogUtils.info('未发现可导入的学生数据');
        } else {
            $scope.students = angular.fromJson(response);
        }
    });

    $scope.fileChanged = function () {
        if ($scope.fileUploader.queue.length === 2) {
            $scope.fileUploader.queue = $scope.fileUploader.queue.slice(-1);
        }
        if ($scope.fileUploader.queue.length > 0) {
            $scope.fileUploader.queue[0].upload();
        }
    }

    $scope.writeToDb = function () {
        if ($scope.students && $scope.students.length > 0) {
            var micYear = $scope.schoolService.school.AcademicYear;
            var url = '/DataProvider/School.aspx/WriteToDb';
            var param = { micYear: micYear, students: $scope.students };
            $scope.baseService.post(url, param, function (data) {
                switch (data.d)
                {
                    case -1:
                        $scope.dialogUtils.info('系统发现您的Excel学年与系统设置不一样!');
                        break;
                    default:
                        $scope.dialogUtils.info('学生编号导入完成！');
                        break;
                } 
            });
        }
    }
}]);

// Path: /ChangePwd 修改口令
appAdmin.controller('ChangePwdController', ['$scope', '$location', 'softname', 'userService', 'dialogUtils',
        function ($scope, $location, softname, userService, dialogUtils) {
            $scope.$root.title = softname + ' | 修改口令';

            $scope.userName = '';
            $scope.teacherID = '';
            $scope.status = "0";

            userService.getUser(function (user) {
                $scope.userName = user.Name;
                $scope.teacherID = user.TeacherID;
            });

            $scope.changePwd = function () {
                if ($scope.newPwd == undefined || $scope.newPwd.length < 3) {
                    dialogUtils.info('密码长度必须介于3 至 10之间！');
                } else if ($scope.newPwd !== '' && $scope.confirmPwd !== $scope.newPwd) {
                    dialogUtils.info('两次输入口令输入不一致！');
                    $scope.newPwd = '';
                    $scope.confirmPwd = '';
                } else {
                    userService.changePwd($scope.teacherID, $scope.oldPwd, $scope.newPwd, parseInt($scope.status),
                        function (data) {
                            if (data.d == 0) {
                                dialogUtils.info('原口令输入错误');
                            } else {
                                $scope.oldPwd = '';
                                $scope.newPwd = '';
                                $scope.confirmPwd = '';
                                dialogUtils.info('口令修改成功');
                            }
                        });
                }
            }
        }]);
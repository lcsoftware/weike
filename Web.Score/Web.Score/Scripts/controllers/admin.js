'use strict';

var appAdmin = angular.module('app.admin', ['ui.tree', 'checklist-model']);

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

    $scope.schoolName = $scope.schoolService.schoolInfo.SchoolName;

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


    $scope.changed = function () {
        console.log($scope.query);
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

    //导出用户清单 
    $scope.ExportExcel = function () {
        var url = '/DataProvider/Export.aspx/ExportUserGroup';
        var param = { schoolName: $scope.schoolService.schoolInfo.SchoolName };
        $scope.baseService.post(url, param, function (data) { 
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
appAdmin.controller('AuthViewController', ['$scope', function ($scope) {
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

// Path: /AuthView 权限编辑
appAdmin.controller('AuthEditController', ['$scope', function ($scope) {
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

    var BindFunc = function (userFunc, treeFunc) {
        if (treeFunc.FuncID === userFunc.FuncID) {
            treeFunc.Kind = userFunc.GroupID === "-1" ? 2 : 1; 
        }
        if (treeFunc.Children.length > 0) {
            var length = treeFunc.Children.length;
            for (var i = 0; i < length; i++) {
                BindFunc(userFunc, treeFunc.Children[i]);
            }
        }
    } 

    var BindFuncs = function (userFuncs, allFuncs) {
        var length = userFuncs.length;
        for (var i = 0; i < length; i++) {
            var userFunc = userFuncs[i];
            var count = allFuncs.length;
            BindFunc(userFunc, allFuncs[0]);
        }
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
        $scope.userService.getUserFuncs(teacher, function (data) {
            if (data.d !== null) {
                var userFuncs = data.d;
                BindFuncs(userFuncs, $scope.allFuncs);
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
        var p = findParent(treeFunc, $scope.allFuncs[0]);
        if (p !== null && p.Kind == 0) {
            var context = '请先授予 <' + p.Description + '> 功能';
            $scope.dialogUtils.info(context);
            return;
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

// Path: /UserEdit  升留级处理
appAdmin.controller('StayGradeController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    var moduleName = '升留级处理';
    $scope.$root.moduleName = moduleName;
    $scope.$root.title = $scope.softname + ' | ' + moduleName;

    $scope.treedata = [{ "label": "New Node A", "id": "id A", "children": [{ "label": "New Node A", "id": "id A", "children": [] }] }];
    $scope.students = [];
    $scope.numberOfPeople = 22;
    $scope.clsName = "初三一班";

    $scope.expandedNodes = [$scope.treedata[0]]

    $scope.treeOptions = {
        nodeChildren: "children",
        dirSelectable: true,
        injectClasses: {
            ul: "a1",
            li: "a2",
            liSelected: "a7",
            iExpanded: "a3",
            iCollapsed: "a4",
            iLeaf: "a5",
            label: "a6",
            labelSelected: "a8"
        }
    }
    }]);

// Path: /UserEdit  转换为学籍成绩
appAdmin.controller('CJtoXJController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  从学籍成绩转换过来
appAdmin.controller('XJtoCJController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  数据备份与恢复
appAdmin.controller('DBbackupController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  学生编号导入
appAdmin.controller('LogUserController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
}]);

// Path: /UserEdit  生成上传数据文件
appAdmin.controller('SendMailController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
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
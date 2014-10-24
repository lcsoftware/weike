'use strict';

var appAdmin = angular.module('app.admin', ['ui.tree', 'checklist-model']);

// Path: /UserEdit  用户(组)维护
appAdmin.controller('UserEditController', ['$scope', function ($scope) {

        $scope.$root.title = softname + ' | 用户(组)维护';
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
                $scope.userForm.userNation = utilService.locate($scope.Nations, 'NationNo', $scope.UserGroupEntity.NationNo);
                $scope.userForm.userResident = utilService.locate($scope.ResidenceTypes, 'ResidenceType', $scope.UserGroupEntity.ResidentNo);
                $scope.userForm.userPolitic = utilService.locate($scope.Politics, 'PoliticsCode', $scope.UserGroupEntity.PoliticCode);
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

// Path: /AuthView 权限查询 
appAdmin.controller('AuthEditController', ['$scope', function ($scope) {
    $scope.$root.moduleName = '权限编辑';

    $scope.allFuncs = [];
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

    $scope.userService.getFuncTree(function (data) {
        if (data.d !== null) {
            $scope.allFuncs.push(data.d);
        }
    }); 
}]);

// Path: /UserEdit  升留级处理
appAdmin.controller('RightQueryController', ['$scope', '$location', '$window', function ($scope, $location, $window) {
    $scope.$root.title = 'AngularJS SPA Template for Visual Studio';
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
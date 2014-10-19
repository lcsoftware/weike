'use strict';

var appAdmin = angular.module('app.admin', ['ui.tree']);

// Path: /UserEdit  用户(组)维护
appAdmin.controller('UserEditController', ['$scope', '$location', '$window', 'softname', 'userService',
    function ($scope, $location, $window, softname, userService) {

        $scope.$root.title = softname + ' | 用户(组)维护';

        $scope.opt = {
            nodeChildren: "children",
            dirSelectable: false
        };
        $scope.query = "";
        $scope.userGroups = [];
        $scope.UserGroupEntity = {};

        $scope.showSelected = function (node) {
            console.log(node);
            optionType = -1;
            $scope.tpl = node.UserOrGroup === '0' ? 'group.html' : 'user.html';
            $scope.UserGroupEntity = node;
            userService.getGroupUsers($scope.UserGroupEntity.TeacherID, function (data) {
                if (data.d !== null) {
                    $scope.groupUsers = data.d;
                }
            });
        }

        //userService.getUserGroup(function (data) {
        //    if (data.d !== null) {
        //        var userGroups = data.d;
        //        var group = { Name: '用户组', UserOrGroup: -1, children: [] };
        //        var user = { Name: '用户', UserOrGroup: -1, children: [] };
        //        var length = userGroups.length;
        //        for (var i = 0; i < length; i++) {
        //            var userGroup = userGroups[i];
        //            if (userGroup.UserOrGroup === '0') {
        //                group.children.push(userGroup);
        //            } else {
        //                user.children.push(userGroup);
        //            }
        //        }
        //        $scope.userGroups.push(group);
        //        $scope.userGroups.push(user);
        //    }
        //});
        //var addUserToGroup = function (userGroup) {
        //    userService.getGroupUsers(userGroup.TeacherID, function (data) {
        //        if (data.d !== null) {
        //            userGroup.children.push(data.d);
        //        }
        //    });
        //}

        userService.buildGroupUserTree(function (groupAndUsers, users) {
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

        $scope.addUserGroup = function (userGroup) { 
            console.log(userGroup);
        }

        $scope.removeUserGroup = function (userGroup) {
            console.log(userGroup);
        }

        $scope.editUserGroup = function (userGroup) {
            console.log(userGroup);
            $scope.tpl = userGroup.UserOrGroup === '0' ? 'group.html' : 'user.html';
            $scope.UserGroupEntity = userGroup; 
        }

        $scope.changed = function () {
            console.log($scope.query);
        }

        $scope.saveUserGroup = function () {
            userService.saveUserGroup($scope.UserGroupEntity, function (data) {
                dialogUtils.info(data.d === 0 ? '添加用户(组)失败，请重试！' : '添加用户(组)成功');
            });
        } 
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
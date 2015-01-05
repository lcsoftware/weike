'use strict';

var userModule = angular.module('app.user', []);

//兄弟页面传值
userModule.factory('instance', function () {
    return {};
});

userModule.controller('UserController', ['$scope', '$state', 'userProviderUrl', function ($scope, $state, userProviderUrl) {
    $scope.userName = 'test';
    $scope.password = '123';
    ///登录验证
    $scope.login = function (userName, password) {
        var url = userProviderUrl + "/Login";
        var param = { userName: userName, password: password };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === 1) {
                $state.go('home');
            } else {
                alert('用户名密码错误');
            }
        });
    }

    $scope.register = function (userName, password, confirm) {
        if (password !== confirm) {
            alert('两次密码输入不一致！');
            return;
        }
        var url = userProviderUrl + "/Register";
        var param = { userName: userName, password: password };
        $scope.baseService.post(url, param, function (data) {
            console.log(data.d);
            alert('注册成功');
            $state.go('home');
        });
    }
}]);

userModule.controller('UserListController', ['$scope', '$state', 'userProviderUrl', function ($scope, $state, userProviderUrl) {

    var getList = function (groupId) {
        var url = userProviderUrl + "/GetUserList";
        var userGroup = { Id: groupId };
        var param = { userGroup: userGroup }; 
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('该用户组未发现有效用户！');
            } else {
                console.log(data.d);
                $scope.userList = data.d;
            }
        });
    } 

    getList(1001);
}]);

userModule.controller("UserPermissionController", ['$scope', '$state', '$stateParams', 'UserPermission', 'instance', function ($scope, $state, $stateParams, UserPermission, instance) {
    $scope.title = "这里是标题";
    $scope.text = "这里是内容哇。。。"
    $scope.test = "教学管理";
    $scope.CheckedTitle = "333";
    $scope.ngselect = function (title) {
        $scope.CheckedTitle = title;
    }

     var GetUserMemu = function () {
         var url = UserPermission + "/Menu_Top_List";
        var param = {  };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('该用户没有权限！');
            } else {
                $scope.UserMemu = data.d;
              
            }
        });
      
     }
     GetUserMemu();
     $scope.MemuChecked = function (memuname) {
         $(".active").removeClass("active");
         if (!memuname.isshow) {
             memuname.isActive = true;
             memuname.isshow = true;
         } else {
             memuname.isActive = false;
             memuname.isshow = false;
         }
         instance.name = memuname.menu.MenuID;
         //debugger;
         //$scope.$broadcast('recall', memuname.menu.MenuID);
     }


   
}]);


userModule.controller('UserPermissionLeftController', function ($scope, $rootScope, instance) {

    $rootScope.name = instance.name;
    $scope.$watch('name', function () {
        add();
    })

    var add = function () {
        $scope.name = instance.name;
    };
   
});







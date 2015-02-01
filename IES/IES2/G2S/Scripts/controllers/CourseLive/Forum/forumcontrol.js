var forumModule = angular.module('app.forum', []);

forumModule.controller('ForumCtrl', ['$scope', '$state', 'forumProviderUrl', function ($scope, $state, forumProviderUrl) {
    $scope.Forum_HotUser_Item = [];  //牛人
    $scope.ForumType_Item = [];//论坛版块
    $scope.Title = "创建讨论模块";
    $scope.flag = false;
    $scope.ForumType = {
        OCID: 1,
        CourseID: 1,
        Title: "",
        IsEssence: false,
        Brief: "",
        IsPublic: true,
        UserID: 4
    };
    //牛人列表
    $scope.Forum_HotUser_List = function () {
        var url = forumProviderUrl + "/Forum_HotUser_List";
        var para = { OCID: 1, Top: 10 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.Forum_HotUser_Item = data.d;
            }
        });
    }

    //版块列表
    $scope.ForumType_List = function () {
        var url = forumProviderUrl + "/ForumType_List";
        var para = { ft: { OCID: 1, UserID: 4 } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType_Item = data.d;
            }
        });
    }
    //新增版块
    $scope.ForumType_ADD = function () {
        var url = forumProviderUrl + "/ForumType_ADD";
        var para = { model: $scope.ForumType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType = {};//清空
                $scope.ForumType_Item.push(data.d);
            }
        });
    }

    //删除讨论模块
    $scope.ForumType_Del = function (m) {
        var url = forumProviderUrl + "/ForumType_Del";
        var para = { model: m };
        if (!confirm("确定要删除吗?")) { return; }
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null || data.d === false) {

            } else {
                //从数组中移除
                for (var i = 0; i < $scope.ForumType_Item.length; i++) {
                    if ($scope.ForumType_Item[i].ForumTypeID == m.ForumTypeID) {
                        $scope.ForumType_Item.splice(i, 1);
                    }
                }
            }
        });
    }

    //编辑讨论模块
    $scope.ForumType_Upd = function () {
        var temp = $scope.ForumType;
        var url = forumProviderUrl + "/ForumType_Upd";
        var para = { model: $scope.ForumType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null || data.d === false) {
                $scope.ForumType = temp; //不成功还原
            } else {
                alert("编辑成功!");
            }
        });
    }

    //编辑或新增
    $scope.AddOrEdit = function () {
        $scope.flag ? $scope.ForumType_ADD() : $scope.ForumType_Upd();
    }
    //显示 创建/编辑讨论模块
    $scope.EditOrAdd = function (flag, m) {
        $scope.Title = flag ? "创建讨论模块" : "编辑讨论模块";
        $scope.flag = flag ? true : false;
        $scope.ForumType = flag ? $scope.ForumType : m;
    }

    $scope.Forum_HotUser_List();
    $scope.ForumType_List();
}]);
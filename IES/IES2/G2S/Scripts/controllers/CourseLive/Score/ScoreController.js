var ScoreModel = angular.module("app.courselive.score", []);


//成绩类别Ctrl
ScoreModel.controller("ScoreTypeCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.OCID = 1;
    $scope.ScoreTypeItem = [];
    $scope.Name = "物理实践";
    $scope.ParentID = 1;
    $scope.ScoreType = {
        OCID: $scope.OCID,
        Name: $scope.Name,
        ParentID: $scope.ParentID
    };

    $scope.ScoreType_List = function () {
        var url = scoreProviderUrl + "/ScoreType_List";
        var para = { OCID: $scope.OCID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.ScoreTypeItem = data.d;
            }
        });
    }

    $scope.ScoreType_Add = function () {
        var url = scoreProviderUrl + "/ScoreType_Add";
        var para = { scoreType: $scope.ScoreType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("新增成功!");
                $scope.ScoreTypeItem.push(data.d);
            }
        });
    }

    $scope.ScoreType_Del = function () {
        var url = scoreProviderUrl + "/ScoreType_Del";
        var para = { scoreTypeID: 50 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("删除成功!");
                //$scope.ScoreTypeItem.push(data.d);
            }
        });
    }
    $scope.ScoreType_Name_Upd = function () {
        var url = scoreProviderUrl + "/ScoreType_Name_Upd";
        var para = { scoreTypeID: 50 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功!!");
                //$scope.ScoreTypeItem.push(data.d);
            }
        });
    }

    $scope.ScoreType_Status_Upd = function () {
        var url = scoreProviderUrl + "/ScoreType_Status_Upd";
        var para = { scoreTypeID: 50 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功!!");
                //$scope.ScoreTypeItem.push(data.d);
            }
        });
    }

    $scope.ScoreType_List();
}]);


//成绩权重Ctrl
ScoreModel.controller("ScoreWeightCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.ScoreWeight = {
        Name: "尽快尽快", Power: 0
    };

    $scope.ScoreWeightItem = [];
    $scope.ScoreWeight_List = function () {
        var url = scoreProviderUrl + "/ScoreWeight_List";
        var para = { OCID: 1, teachingClassID: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert("暂无数据");
            } else {
                $scope.ScoreWeightItem = data.d;
            }
        });
    }

    $scope.ScoreWeight_Power_Upd = function () {
        var url = scoreProviderUrl + "/ScoreWeight_Power_Upd";
        var para = {};
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert("暂无数据");
            } else {
                //$scope.ScoreWeight_Item = data.d;
            }
        });
    }
    $scope.ScoreWeight_List();
}]);

//成绩管理Ctrl
ScoreModel.controller("ScoreManageCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.ScoreManageInfo = {
        OCID: 1, TermID: -1, Name: '', ScoreTypeID: -1
    };
    $scope.ScoreManageInfoItem = [];
    $scope.ScoreManageInfo_List = function () {
        var url = scoreProviderUrl + "/ScoreManageInfo_List";
        var para = { smi: $scope.ScoreManageInfo, PageIndex: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ScoreManageInfoItem = data.d;
            }
        });
    }
    $scope.ScoreManageInfo_List();
}]);


//成绩详细Ctrl
ScoreModel.controller("ScoreManageCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.ScoreManageInfo = {
        OCID: 1, TermID: -1, Name: '', ScoreTypeID: -1
    };
    $scope.ScoreManageInfoItem = [];
    $scope.ScoreManageInfo_List = function () {
        var url = scoreProviderUrl + "/ScoreManageInfo_List";
        var para = { smi: $scope.ScoreManageInfo, PageIndex: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ScoreManageInfoItem = data.d;
            }
        });
    }
    $scope.ScoreManageInfo_List();
}]);
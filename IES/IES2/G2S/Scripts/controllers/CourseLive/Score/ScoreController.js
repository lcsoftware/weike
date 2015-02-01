var ScoreModel = angular.module("app.courselive.score", []);


//成绩类别Ctrl
ScoreModel.controller("ScoreTypeCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.ScoreTypeItem = [];
    $scope.show = false;
    $scope.EditShow = false;
    $scope.ScoreType = {
        ScoreTypeID: 0,
        OCID: 0,
        Name: '',
        ParentID: 0,
        Status: 0
    };

    $scope.ScoreType_List = function () {
        var url = scoreProviderUrl + "/ScoreType_List";
        var para = { OCID: 1 };
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
                data.d.Status = 1;
                $scope.ScoreTypeItem.push(data.d);
                $scope.show = false;
            }
        });
    }

    $scope.ScoreType_Del = function (item) {
        $scope.ScoreType = item;
        var url = scoreProviderUrl + "/ScoreType_Del";
        var para = { scoreType: $scope.ScoreType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("删除成功!");
                $scope.ScoreType_List();
            }
        });
    }
    $scope.ScoreType_Name_Upd = function () {
        var url = scoreProviderUrl + "/ScoreType_Name_Upd";
        var para = { scoreType: $scope.ScoreType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
                //alert("修改成功!!");
                $scope.EditShow = false;
            }
        });
    }

    $scope.ScoreType_Status_Upd = function (item) {
        item.Status = item.Status == 1 ? 0 : 1;
        var url = scoreProviderUrl + "/ScoreType_Status_Upd";
        var para = { scoreType: item };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                alert("修改成功!!");
                if (item.ParentID == 0) {
                    $scope.ScoreType_List();
                }
            }
        });
    }
    //新增
    $scope.showBox = function (item) {
        $scope.ScoreType.Name = "";
        $scope.ScoreType.ParentID = item.ScoreTypeID;
        $scope.show = true;
    }

    //编辑
    $scope.EditShowBox = function (item) {
        $scope.ScoreType = item;
        $scope.EditShow = true;
    }
    $scope.ScoreType_List();
}]);


//成绩权重Ctrl
ScoreModel.controller("ScoreWeightCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.ScoreWeight = {
        Name: "", Power: 0
    };
    $scope.Count = 0.0;

    $scope.ScoreWeightItem = [];
    $scope.ScoreWeight_List = function () {
        var url = scoreProviderUrl + "/ScoreWeight_List";
        var para = { OCID: 1, teachingClassID: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert("暂无数据");
            } else {
                $scope.ScoreWeightItem = data.d;
                $scope.TotalPower();
            }
        });
    }

    $scope.ScoreWeight_Power_Upd = function (item) {
        $scope.ScoreWeight = item;
        $scope.ScoreWeight.Power = $scope.ScoreWeight.Power == "" ? 0 : $scope.ScoreWeight.Power;
        $scope.TotalPower();
        //if ($scope.Count > 100) {
        //    //alert("不允许超过100");
        //    //$scope.ScoreWeight_List();
        //    return;
        //}
        var url = scoreProviderUrl + "/ScoreWeight_Power_Upd";
        var para = { sw: $scope.ScoreWeight };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {

            }
        });
    }
    $scope.ScoreWeight_JoinNum_Upd = function (item) {
        $scope.ScoreWeight = item;
        $scope.ScoreWeight.JoinNum = $scope.ScoreWeight.JoinNum == "" ? 0 : $scope.ScoreWeight.JoinNum;
        var url = scoreProviderUrl + "/ScoreWeight_JoinNum_Upd";
        var para = { sw: $scope.ScoreWeight };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {

            }
        });
    }

    $scope.TotalPower = function () {
        $scope.Count = 0.0;
        for (var i = 0; i < $scope.ScoreWeightItem.length; i++) {
            $scope.Count += parseFloat($scope.ScoreWeightItem[i].Power);
        }
    }

    $scope.btnOK = function () {
        $scope.TotalPower();
        if ($scope.Count > 100 || $scope.Count < 100) {
            alert("加权必须为100");
        }
    }
    $scope.btnCancel = function () {
        $scope.TotalPower();
        if ($scope.Count > 100 || $scope.Count < 100) {
            if (true && confirm("加权必须为100,确定要关闭吗?")) {
                alert(1);
            }
        }
    }

    $scope.ScoreWeight_List();
}]);

//成绩管理Ctrl
ScoreModel.controller("ScoreManageCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.PageIndex = 1;
    $scope.PageSize = 20;
    $scope.selected = { ScoreTypeID: -1 };

    $scope.ScoreTypeItem = []
    $scope.ScoreManageInfoItem = [];
    $scope.ScoreManageInfo = {
        OCID: 1, TeachingClassID: -1, Name: '', ScoreTypeID: -1
    };

    $scope.ScoreType_List = function () {
        var url = scoreProviderUrl + "/ScoreType_List";
        var para = { OCID: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.ScoreTypeItem = data.d;
            }
        });
    }

    $scope.ScoreManageInfo_List = function () {
        $scope.ScoreManageInfo.ScoreTypeID = ($scope.selected == "" || $scope.selected == null) ? -1 : $scope.selected.ScoreTypeID;
        $scope.ScoreManageInfoItem = [];
        var url = scoreProviderUrl + "/ScoreManageInfo_List";
        var para = { smi: $scope.ScoreManageInfo, PageIndex: $scope.PageIndex, PageSize: $scope.PageSize };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ScoreManageInfoItem = data.d;
            }
        });
    }
    laypage({
        cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
        pages: 100, //总页数
        skip: true, //是否开启跳页
        skin: '#AF0000', //选中的颜色
        groups: 5,//连续显示分页数
        first: '首页', //若不显示，设置false即可
        last: '尾页', //若不显示，设置false即可
        jump: function (e) { //触发分页后的回调
            $scope.PageIndex = e.curr;
            alert($scope.PageIndex);
            $scope.ScoreManageInfo_List();
        }
    });

    $scope.ScoreType_List();
}]);


//成绩详细Ctrl
ScoreModel.controller("ScoreWithCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.PageIndex = 1;
    $scope.PageSize = 20;
    $scope.ScoreWithInfo = {
        TestID: 1, UserName: '', ClassID: -1, TeachingClassID: -1
    };
    $scope.ScoreWithInfoItem = [];
    $scope.ScoreWithInfo_List = function () {
        var url = scoreProviderUrl + "/ScoreWithInfo_List";
        var para = { swi: $scope.ScoreWithInfo, PageIndex: $scope.PageIndex, PageSize: $scope.PageSize };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ScoreWithInfoItem = data.d;
            }
        });
    }
    $scope.ScoreWithInfo_List();
}]);

//成绩分析
ScoreModel.controller("ScoreAnalyseCtrl", ["$scope", "$state", "scoreProviderUrl", function ($scope, $state, scoreProviderUrl) {
    $scope.Scorejl = 0;
    $scope.ScoreType = [
        { ScoreTypeID: 1, Name: "平时成绩", ParentID: 0, Count: 3 },
        { ScoreTypeID: 5, Name: "作业", ParentID: 1, Count: 0 },
        { ScoreTypeID: 6, Name: "考试", ParentID: 1, Count: 0 },
        { ScoreTypeID: 7, Name: "Mooc", ParentID: 1, Count: 0 },
        { ScoreTypeID: 9, Name: "期中考试", ParentID: 0, Count: 0 },
        { ScoreTypeID: 2, Name: "论文", ParentID: 0, Count: 1 },
        { ScoreTypeID: 8, Name: "论文实践", ParentID: 2, Count: 0 },
        { ScoreTypeID: 3, Name: "期末考试", ParentID: 0, Count: 0 }
    ];
    $scope.ScoreAnalyseItem = [
        {
            UserNo: 1,
            UserName: '小白',
            Score: [{ ScoreTypeID: 5, Score: 90 }, { ScoreTypeID: 6, Score: 81 }, { ScoreTypeID: 7, Score: 87 }, { ScoreTypeID: 8, Score: 90 }, { ScoreTypeID: 9, Score: 81 }, { ScoreTypeID: 3, Score: 87 }]
        },
        {
            UserNo: 2,
            UserName: '小黑',
            Score: [{ ScoreTypeID: 5, Score: 90 }, { ScoreTypeID: 7, Score: 81 }, { ScoreTypeID: 6, Score: 87 }, { ScoreTypeID: 8, Score: 90 }, { ScoreTypeID: 9, Score: 81 }, { ScoreTypeID: 3, Score: 87 }]
        },
        {
            UserNo: 3,
            UserName: '小花',
            Score: [{ ScoreTypeID: 6, Score: 90 }, { ScoreTypeID: 5, Score: 81 }, { ScoreTypeID: 7, Score: 87 }, { ScoreTypeID: 8, Score: 90 }, { ScoreTypeID: 9, Score: 81 }, { ScoreTypeID: 3, Score: 85 }]
        },
        {
            UserNo: 4,
            UserName: '大黄',
            Score: [{ ScoreTypeID: 6, Score: 90 }, { ScoreTypeID: 5, Score: 81 }, { ScoreTypeID: 7, Score: 87 }, { ScoreTypeID: 8, Score: 90 }, { ScoreTypeID: 9, Score: 81 }, { ScoreTypeID: 3, Score: 85 }]
        }
    ];
}]);

ScoreModel.directive("hello", function () {
    return {
        restrict: "AE",
        replace: true,
        template: "<a href='http://www.baidu.com'>百度</a>",
        link: function (scope, element, attrs) {

        }
    }
});
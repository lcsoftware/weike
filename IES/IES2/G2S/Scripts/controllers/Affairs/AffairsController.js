'use strict';

var affairsModule = angular.module('app.affairs', []);

affairsModule.controller('AffairsController', ['$scope', '$state', 'affairsProviderUrl', function ($scope, $state, affairsProviderUrl) {
    $scope.ocID = '1';
    $scope.PageSize = 10;
    $scope.PagesNum = 10;
    $scope.Affairs = {
        OCID: $scope.ocID,
        AffairIDs: '',
        DictID: 0,
        Status: 0
    };

    $scope.Dict = {
        Source: '事务审核'
    };

    //获取事务类型列表
    var GetDictList = function () {
        var url = affairsProviderUrl + "/Dict_List";
        var param = {
            model: $scope.Dict
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.DictList = data.d;
            }
        });
    }
    GetDictList();
    $scope.DictSelectChanged=function()
    {
        if ($scope.Affairs.DictID == null)
        {
            $scope.Affairs.DictID = 0;
        }
        AffairsPages($scope.PagesNum);
    }
    //获取申请审核列表
    var GetAffairsList = function (PageIndex) {
        var url = affairsProviderUrl + "/Affairs_List";
        var param = {
            model: $scope.Affairs,
            PageIndex: PageIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
                $scope.PageIndex = 1;//
            } else {
                $scope.AffairsList = data.d;
                $scope.PagesNum = Math.ceil(data.d[0].rowscount / $scope.PageSize);
            }
        });
    }

    //请审核列表分页
    var AffairsPages = function (PageNum) {
        laypage({
            cont: $('#AffairsPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: false, //若不显示，设置false即可
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                //$scope.PageSearchStudentIndex = e.curr;  //为作用域外变量赋值一定要加上$scope.$apply(); 才能实现双向绑定
                GetAffairsList(e.curr);
            }
        });

    }

    AffairsPages($scope.PagesNum);

    $scope.CheckAll = false;
    ///全选
    $scope.SelectAll = function () {
        $scope.CheckAll = !$scope.CheckAll;
        for (var i = 0; i < $scope.AffairsList.length; i++) {
            $scope.AffairsList[i].IsSelected = $scope.CheckAll;
        }

    }
    ///单选
    $scope.SelectSingle = function () {
        $scope.CheckAll = false;
        $scope.$apply();
    }
    //操作
    //单对象操作
    $scope.StatusEdit = function (ocaffairs, StatusType) {
        $scope.Affairs.AffairIDs = "";
        $scope.Affairs.AffairID = ocaffairs.AffairID;
        $scope.Affairs.Status = StatusType;
        var url = affairsProviderUrl + "/OCAffairs_Status_Upd";
        var param = {
            model: $scope.Affairs
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                return false;
            } else {
                ocaffairs.Status = StatusType;
            }
        });


    }
    //批量操作
    $scope.BatchEdit = function (StatusType) {
        $scope.Affairs.AffairIDs = "";
        $scope.Affairs.AffairID = "-1";
        for (var i = 0; i < $scope.AffairsList.length; i++) {
            if ($scope.AffairsList[i].IsSelected == true) {
                $scope.Affairs.AffairIDs += $scope.AffairsList[i].AffairID + ',';
                //$scope.AffairsList[i].Status = StatusType;
            }
        }
        if ($scope.Affairs.AffairIDs != "") {
            $scope.Affairs.Status = StatusType;

            var url = affairsProviderUrl + "/OCAffairs_Beach_Upd";
            var param = {
                model: $scope.Affairs
            };
            //console.log($scope.Affairs.AffairIDs);
            $scope.baseService.post(url, param, function (data) {
                if (data.d === null) {
                    return false;
                } else {
                    for (var i = 0; i < $scope.AffairsList.length; i++) {
                        if ($scope.AffairsList[i].IsSelected == true) {
                            //$scope.Affairs.AffairIDs = $scope.AffairsList[i].AffairID + ',';
                            $scope.AffairsList[i].Status = StatusType;
                        }
                    }
                    $scope.$apply();
                }
            });
        }
        else {
            alert("至少选择一条记录！");
        }
    }
 


}]);



'use strict';

var teamModule = angular.module('app.oc.class', []);

teamModule.controller('ClassController', ['$scope', '$state', 'classProviderUrl', function ($scope, $state, classProviderUrl) {
    $scope.ocID = '1';
    $scope.showRegistStudent = false;//是否显示
    var GetClassList = function (OCID) {
        var url = classProviderUrl + "/ClassList";
        var param = { OCID: OCID,TeamID: 1,Searchkey: '' ,IsHistroy: 0 };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                //console.log(data.d);
                $scope.OCClassInfo = data.d;
            }
        });
    }
    var GetRegStudentList = function (OCID) {
        var url = classProviderUrl + "/RegStudentList";
        var param = { OCID: OCID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                //console.log(data.d);
                $scope.RegStudentInfo = data.d;
            }
        });
    }
    var GetRegClassList = function (OCID) {
        var url = classProviderUrl + "/OCClassList";
        var param = { OCID: OCID,TeachingClassName: "", IsHistroy: false, ClassType: 1, UserID: '', PageIndex: 1, PageSize: 20  };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                console.log(data.d);
                $scope.RegClassInfo = data.d;
            }
        });
    }
    GetClassList($scope.ocID);
    GetRegStudentList($scope.ocID);
    GetRegClassList($scope.ocID);
}]);


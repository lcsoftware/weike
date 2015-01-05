'use strict';

var teamModule = angular.module('app.oc.team', []);

teamModule.controller('TeamController', ['$scope', '$state', 'teamProviderUrl', function ($scope, $state, teamProviderUrl) {
    $scope.ocID = '1';
    $scope.showAddTeacher = false;//添加主讲教师是否显示
    $scope.showDialog = false;  //是否显示搜索框
    var GetTeamList = function (OCID) {
        var url = teamProviderUrl + "/GetTeamList";
        var param = { OCID: OCID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                //console.log(data.d);
                $scope.TeamInfo = data.d;
            }
        });
    }
    GetTeamList($scope.ocID);
    $scope.showaddteacher = function showaddteacher() {
        $scope.showAddTeacher = !$scope.showAddTeacher;
    }
    $scope.hidaddteacher = function hidaddteacher() {
        $scope.showAddTeacher = false;
    }
    $scope.showdialog = function showdialog() {
        $scope.showDialog = true;
    }

    $scope.DelTeam = function DelTeam(OcTeam) {
        var url1 = teamProviderUrl + "/OCTeam_Del";
        var params = { OCID: OcTeam.OCID, UserID: OcTeam.UserID };
        //alert(url1);
        $scope.baseService.post(url1, params, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                console.log(data.d);
                GetTeamList($scope.ocID);
            }
        });

      
    }
}]);


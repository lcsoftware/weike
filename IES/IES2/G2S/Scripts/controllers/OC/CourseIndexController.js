'use strict';

var courseindexModule = angular.module('app.oc', []);

teamModule.controller('CourseIndexController', ['$scope', '$state', 'courseindexProviderUrl', function ($scope, $state, courseindexProviderUrl) {
    $scope.ocID = '10000001';
    $scope.showAddTeacher = false;//添加主讲教师是否显示
    $scope.PageSize = 10;
    $scope.TeacherList = null;
    $scope.OcTeamFunctionInfo = null;
    $scope.SearchUserType = false;//false  主讲教师搜索 true 教学团队用户搜索

    //获取教学团队列表
    var GetTeamList = function (OCID) {
        var url = courseindexProviderUrl + "/OCTeam_List";
        var param = { OCID: OCID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                //console.log(data.d);
                $scope.TeamList = data.d;
                $scope.$apply();
            }
        });
    }
    GetTeamList($scope.ocID);
    //编辑主讲教师简介
    $scope.EditTeamBrief = function (modal) {
        var url = courseindexProviderUrl + "/OCTeam_Brief_Upd";
        var param = {
            octeam: modal
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {

            }
        });
    }


    //begin控制添加主讲教师是否显示
    $scope.showaddteacher = function showaddteacher() {
        $scope.showAddTeacher = !$scope.showAddTeacher;
    }
    $scope.hidaddteacher = function hidaddteacher() {
        $scope.showAddTeacher = false;
    }
    //end控制添加主讲教师是否显示

    //删除教学团队成员
    $scope.DelTeam = function (OcTeam) {
        if (confirm("是否删除教学团队成员")) {
            var url1 = courseindexProviderUrl + "/OCTeam_Del";
            var params = {
                motal: OcTeam
            };
            $scope.baseService.post(url1, params, function (data) {
                if (data.d === null) {
                    alert('暂无数据！');
                } else {
                    for (var i = 0; i < $scope.TeamList.length; i++) {
                        if ($scope.TeamList[i].TeamID == OcTeam.TeamID) {
                            $scope.TeamList.splice(i, 1);
                            break;
                        }

                    }
                }
            });
        }
    }
    //


    //begin锁定教学团队
    $scope.LockedTeam = function (OcTeam) {
        $scope.UpLockedOcTeam = OcTeam;
        if (OcTeam.IsLocked) {
            $('#myUnLockedModal').modal("show");
        }
        else {
            $('#myLockedModal').modal("show");
        }
    }
    $scope.LockedSure = function () {
        var url1 = courseindexProviderUrl + "/OCTeam_IsLocked_Upd";
        var params = {
            octeam: $scope.UpLockedOcTeam
        };
        $scope.baseService.post(url1, params, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.UpLockedOcTeam.IsLocked = !$scope.UpLockedOcTeam.IsLocked;
            }
        });
    }
    //end锁定教学团队

    //begin添加主讲教师
    //站内用户搜索分页
    $scope.TeacherModel = {
        Key: "",
        UserID: -1
    };
    var TeamPages = function (PageNum) {
        laypage({
            cont: $('#TeamPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: false, //若不显示，设置false即可
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                //为作用域外变量赋值一定要加上$scope.$apply(); 才能实现双向绑定
                GetTeacherList(e.curr);
            }
        });

    }
    ///搜索获取站内用户列表
    var GetTeacherList = function (pageIndex) {
        if ($scope.TeacherModel.Key == "请输入关键字") {
            $scope.TeacherModel.Key = "";
        }
        var url = courseindexProviderUrl + "/Teacher_List";
        var param = {
            teacher: $scope.TeacherModel,
            pageindex: pageIndex,
            pagesize: $scope.PageSize
        };

        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.TeacherList = data.d;
                $scope.TeamNum = data.d[0].rowscount;
            }
        });
    }
    $scope.SearchUserList = function () {
        $scope.TeamNum = 100;
        GetTeacherList(1);
        TeamPages(Math.ceil($scope.TeamNum / $scope.PageSize));
    }

    ///获取要添加的主讲教师信息
    var GetTeam = function (UserID) {
        var url = courseindexProviderUrl + "/TeacherInfo";
        var param = {
            UserID: UserID
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCTeamNew = data.d;
            }
        });
    }

    ///获取将要添加的主讲教师信息
    $scope.GetSelectUser = function () {
        var UserID = $scope.TeacherModel.UserID;
        $('#myAddTeacherModal').modal("hide");
        if ($scope.SearchUserType) {
            GetOcTeamFunctionInfo($scope.ocID, UserID);
            $('#myAddTeamModal').modal("show");
        }
        else {
            GetTeam(UserID);
        }
    }

    $scope.ShowSelectUser = function (Type) {
        $scope.SearchUserType = Type;
        if (Type) {
            if (!$scope.EditOrAdd) {
                $('#myAddTeacherModal').modal("show");
                $('#myAddTeamModal').modal("hide");
            }
        }
        else {
            $('#myAddTeacherModal').modal("show");
        }
    }
    $scope.CloseDialog = function () {
        if ($scope.SearchUserType) {
            if (!$scope.EditOrAdd) {
                $('#myAddTeacherModal').modal("hide");
                $('#myAddTeamModal').modal("show");
            }
        }
    }

    //添加主讲教师
    $scope.AddTeamUser = function () {
        $scope.OCTeamNew.OcTeam.OCID = $scope.ocID;
        $scope.OCTeamNew.OcTeam.Role = 2;
        $scope.OCTeamNew.OcTeam.Status = 2;
        var url = courseindexProviderUrl + "/OCTeam_ADD";
        var param = {
            octeam: $scope.OCTeamNew.OcTeam
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                //$scope.OCTeamNew = null;

                //GetTeamList($scope.ocID);
            }
        });
    }

    //end添加主讲教师

    //begin用户权限管理
    $scope.FunctionCheckedAll = {
        ClassAll: false,
        ModuleAll: false
    }
    //begin按班级授权操作
    $scope.ckb_Class_All = function () {
        $scope.FunctionCheckedAll.ClassAll = !$scope.FunctionCheckedAll.ClassAll;
        for (var i = 0; i < $scope.OcTeamFunctionInfo.OcTeamFunctionClass.length; i++) {
            $scope.OcTeamFunctionInfo.OcTeamFunctionClass[i].IsSelected = $scope.FunctionCheckedAll.ClassAll;
        }
    }
    $scope.ckb_Class_Single = function () {
        $scope.FunctionCheckedAll.ClassAll = false;
    }
    //end按班级授权操作
    //begin按功能授权操作
    $scope.ckb_Module_All = function () {
        $scope.FunctionCheckedAll.ModuleAll = !$scope.FunctionCheckedAll.ModuleAll;
        for (var i = 0; i < $scope.OcTeamFunctionInfo.OcTeamFunctionModule.length; i++) {
            $scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].IsSelected = $scope.FunctionCheckedAll.ModuleAll;
        }
    }
    $scope.ckb_Module_Single = function (functionModuleModel) {
        $scope.FunctionCheckedAll.ModuleAll = false;
        //var IsSelected = !functionModuleModel.IsSelected;
        //var ModuleID = functionModuleModel.ModuleID;
        //var ParentID = functionModuleModel.ParentID;
        for (var i = 0; i < $scope.OcTeamFunctionInfo.OcTeamFunctionModule.length; i++) {

            //一级节点不可选
            if ($scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].ParentID == "0") {
                $scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].IsSelected = false;
            }
            //父级取消选中
            if ($scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].ModuleID == functionModuleModel.ParentID) {
                $scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].IsSelected = false;
            }
            //子集全选或者不选
            if ($scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].ParentID == functionModuleModel.ModuleID) {
                $scope.OcTeamFunctionInfo.OcTeamFunctionModule[i].IsSelected = !functionModuleModel.IsSelected;
            }

        }

    }

    //end按功能授权操作


    $scope.EditOrAdd = false; //false  新增 true 编辑
    var GetOcTeamFunctionInfo = function (OCID, UserID) {
        var url = courseindexProviderUrl + "/GetOcTeamFunctionInfo";
        var param = { OCID: OCID, UserID: UserID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OcTeamFunctionInfo = data.d;
            }
        });
    }

    $scope.AddOcTeamFunctionInfo = function () {
        $scope.EditOrAdd = false;
        $('#myAddTeamModal').modal("show");
        $scope.OcTeamFunctionInfo = null;
        $scope.EditOcTeam = null;
    }

    $scope.EditOcTeam = null;

    $scope.EditOcTeamFunctionInfo = function (modal) {
        $scope.EditOrAdd = true;
        $scope.EditOcTeam = modal;
        $('#myAddTeamModal').modal("show");
        GetOcTeamFunctionInfo($scope.ocID, modal.UserID);
    }
    //添加教学团队
    $scope.SaveOcTeamFunctionInfo = function () {
        var url = courseindexProviderUrl + "/OCTeam_Class_Function_Save";
        var param = {
            octeamfunctioninfo: $scope.OcTeamFunctionInfo
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OcTeamFunctionInfo = null;
                if (!$scope.EditOrAdd) {
                    GetTeamList($scope.ocID);
                }
                else {
                    $scope.EditOcTeam.ClassCount = data.d.ClassCount;
                    $scope.EditOcTeam.FunctionCount = data.d.FunctionCount;
                    $scope.$apply();
                }

            }
        });

    }
    //end用户权限管理


}]);
teamModule.filter('listFilter3', function () {
    return function (arr, ope, num) {
        if (arr == null || arr == '') {
            return;
        }
        return arr.filter(function (item) {
            if (ope == '=') {
                return item.ParentID == num;
            }
        });
    }
});

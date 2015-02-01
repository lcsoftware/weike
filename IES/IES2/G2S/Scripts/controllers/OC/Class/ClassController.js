
'use strict';

var classModule = angular.module('app.oc.class', []);

classModule.controller('ClassController', ['$scope', '$state', 'classProviderUrl', function ($scope, $state, classProviderUrl) {
    $scope.ocID = '1';
    $scope.OCClass = {
        Key: "请输入‘班级’ ‘学生姓名’关键字",
        OCID: $scope.ocID,
        IsHistroy: false,
        TermID: -1,
        OCClassID: -1
    };

    $scope.PageSize = 20;
    $scope.PageIndex = 1;//
    $scope.PagesNum = 10;
    $scope.PageStudentIndex = 1;//教学班学生列表
    $scope.PagesStudentNum = 10;//教学班学生列表

    $scope.OCClassStudentList = null;//教学班学生列表
    $scope.Class_Upd = null;//待更新的教学班信息

    $scope.Class_Details = null;//教学班详细信息

    var GetClassList = function () {

        if ($scope.OCClass.Key == "请输入‘班级’ ‘学生姓名’关键字")
            $scope.OCClass.Key = "";
        var url = classProviderUrl + "/ClassList";
        var param = {
            model: $scope.OCClass,
            PageIndex: $scope.PageIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
                $scope.PageIndex = 1;//
            } else {
                //console.log(data.d);
                $scope.OCClassList = data.d;
                //alert(data.d[0].RowsCount);
                $scope.PagesNum = Math.ceil(data.d[0].RowsCount / $scope.PageSize);
            }
        });
    }
    //GetClassList();

    $scope.SearchClassList = function () {
        $scope.PageIndex = 1;
        GetClassList();
    }

    $scope.RegNum_Upd = function (model) {
        $scope.OCClass_Upd = model;
        $('#myRegNumModal').modal("show");
    }
    ///修改注册码
    $scope.OCClass_RegNum_Upd = function () {
        var url = classProviderUrl + "/OCClass_RegNum_Upd";

        $scope.OCClass_Upd.StartDate = "2014-01-01";
        $scope.OCClass_Upd.EndDate = "2014-01-01";
        $scope.OCClass_Upd.RecruitStartDate = "2014-01-01";
        $scope.OCClass_Upd.RecruitEndDate = "2014-01-01";
        $scope.OCClass_Upd.CreateTime = "2014-01-01";
        var param = {
            model: $scope.OCClass_Upd
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功");
                $scope.OCClass_Upd = null;
            }
        });

    }

    ///设为结业
    $scope.OCClass_IsHistroy_Upd = function (model) {
        var url = classProviderUrl + "/OCClass_IsHistroy_Upd";
        model.StartDate = "2014-01-01";
        model.EndDate = "2014-01-01";
        model.RecruitStartDate = "2014-01-01";
        model.RecruitEndDate = "2014-01-01";
        model.CreateTime = "2014-01-01";
        var param = {
            model: model
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功");
                GetClassList();
            }
        });
    }

    //删除教学班
    $scope.OCClass_Del = function (model) {
        model.StartDate = "2014-01-01";
        model.EndDate = "2014-01-01";
        model.RecruitStartDate = "2014-01-01";
        model.RecruitEndDate = "2014-01-01";
        model.CreateTime = "2014-01-01";
        var url = classProviderUrl + "/OCClass_Del";
        var param = {
            model: model
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功");
                GetClassList();
            }
        });
    }

    ///教学班切换
    $scope.ShowHistroyOCClass = function (model) {
        if ($scope.OCClass.IsHistroy != model) {
            $scope.OCClass.IsHistroy = model;
            $scope.PageIndex = 1;
            GetClassList();
        }
    }



    //查看详细
    $scope.ShowClassDetails = function () {
        $('#myDetailsModal').modal("show");
        $scope.PageStudentIndex = 1;
    }

    var GetClassStudentDetails = function () {
        var url = classProviderUrl + "/OCClassStudent_List";
        var param = {
            occlassid: $scope.Class_Details,
            PageIndex: $scope.PageStudentIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;
                $scope.PagesStudentNum = Math.ceil(data.d[0].RowsCount / $scope.PageSize);
            }
        });
    }

    //教学班全部学生信息
    $scope.OCClassStudent_List = function (model) {
        $('#myDetailsModal').modal("show");
        model.StartDate = "2014-01-01";
        model.EndDate = "2014-01-01";
        model.RecruitStartDate = "2014-01-01";
        model.RecruitEndDate = "2014-01-01";
        model.CreateTime = "2014-01-01";
        $scope.Class_Details = model;
        GetClassStudentDetails();
    }

    //编辑教学班
    $scope.OCClass_Edit = function (model) {
        alert(model.OCClassID);
        //导航到添加页面
        ///TODO
    }

    //添加教学班
    $scope.OCClass_ADD = function () {
        //$scope.OCClass
        //导航到添加页面
        ///TODO
    }

    //导出全部教学班学生信息
    $scope.OCClass_InputOutAll = function (model) {

    }

    laypage({
        cont: $('#ClassPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
        pages: $scope.PagesNum, //总页数
        skip: true, //是否开启跳页
        skin: '#374760', //选中的颜色
        groups: 3,//连续显示分页数
        first: '首页', //若不显示，设置false即可
        last: '尾页', //若不显示，设置false即可
        jump: function (e) { //触发分页后的回调
            $scope.PageIndex = e.curr;
            GetClassList();

        }
    });


    laypage({
        cont: $('#StudentPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
        pages: $scope.PagesStudentNum, //总页数
        skip: true, //是否开启跳页
        skin: '#374760', //选中的颜色
        groups: 3,//连续显示分页数
        first: '首页', //若不显示，设置false即可
        last: '尾页', //若不显示，设置false即可
        jump: function (e) { //触发分页后的回调
            $scope.PageStudentIndex = e.curr;
            GetClassStudentDetails();
        }
    });





}]);

classModule.controller('AddClassController', ['$scope', '$state', 'classProviderUrl', function ($scope, $state, classProviderUrl) {
    $scope.OCClass = null;
    $scope.OCClassTeachertList = null;
    $scope.OCClassStudentList = null;
    $scope.PageSize = 5;
    $scope.OCClass = {
        OCID: 1,
        OCClassID: 23
    };
    $scope.OCClassTemp = {
        OCID: 1,
        OCClassID: 23
    };

    $scope.OCTeamTeacher = {
        OCID: 1,
        OCClassID: 23,
        Role: -1
    };
    //获取编辑教学班
    var OCClass_Get = function () {
        if ($scope.OCClassTemp.OCClassID != -1) {
            var url = classProviderUrl + "/OCClass_Get";
            var param = {
                occlass: $scope.OCClassTemp
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d === null) {
                    alert('暂无数据！');
                } else {
                    $scope.OCClass = data.d;
                }
            });
        }
    }
    OCClass_Get();
    //获取编辑教学班授课教师
    var OCTeam_Dropdown_List = function () {
        var url = classProviderUrl + "/OCTeam_Dropdown_List";
        var param = {
            occlass: $scope.OCTeamTeacher
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassTeachertList = data.d;
                console.log($scope.OCClassTeachertList);
            }
        });
    }
    OCTeam_Dropdown_List();
    //获取编辑教学班学生信息
    var OCClassStudent_List = function () {
        if ($scope.OCClassTemp.OCClassID != -1) {
            var url = classProviderUrl + "/OCClassStudent_List";
            var param = {
                occlass: $scope.OCClassTemp,
                PageIndex: 1,
                PageSize: 99999
            };
            $scope.baseService.post(url, param, function (data) {
                if (data.d === null) {
                    alert('暂无数据！');
                } else {

                    $scope.OCClassStudentList = data.d;
                    $scope.OCClassStudentIndex = 1;
                    OCClassStudentPages(Math.ceil(data.d[0].RowsCount / $scope.PageSize));
                    $scope.$apply();
                }
            });
        }
    }
    OCClassStudent_List();
    //教学班学生分页信息信息






    //授课起止日期
    laydate({
        elem: '#start_date', //目标元素。由于laydate.js封装了一个轻量级的选择器引擎，因此elem还允许你传入class、tag但必须按照这种方式 '#id .class'
        event: 'focus', //响应事件。如果没有传入event，则按照默认的click
        festival: true, //显示节日
        istime: true,
        format: "YYYY-MM-DD", //日期格式
        choose: function (datas) { //选择日期完毕的回调
            $scope.OCClass.StartDate = datas;
            $scope.$apply();
        }
    });
    //授课起止日期
    laydate({
        elem: '#end_date', //目标元素。由于laydate.js封装了一个轻量级的选择器引擎，因此elem还允许你传入class、tag但必须按照这种方式 '#id .class'
        event: 'focus', //响应事件。如果没有传入event，则按照默认的click
        festival: true, //显示节日
        istime: true,
        format: "YYYY-MM-DD", //日期格式
        choose: function (datas) { //选择日期完毕的回调
            $scope.OCClass.EndDate = datas;
            $scope.$apply();
        }

    });

    laydate.skin('molv')//墨绿皮肤

    //begin-------------------------------------添加学生

    $scope.SearchType = true;//按行政班添加  false按学生添加
    $scope.ClassModel = {
        Key: "",
        ClassID: -1
    }
    $scope.SearchModel = {
        Key: ""
    }

    $scope.ShowAddModal = function () {
        $('#addStudentModal').modal("show");
    }
    $scope.addSearchType = function (fType) {
        $scope.SearchType = fType;
    }
    ///########################################按学生添加
    //搜索添加学生
    $scope.OCClassStudentSearchList = null;//全部搜索结果

    $scope.PageSearchStudentIndex = 1;
    $scope.OCClassStudentIndex = 1;
    $scope.PageSearchClassIndex = 1;

    //搜索学生
    $scope.SearchStudent = function () {
        $scope.OCClassStudentSearchList = null;
        OCClass_Student_List();
    }
    //搜索学生
    var OCClass_Student_List = function () {
        var url = classProviderUrl + "/OCClass_Student_List";
        var param = {
            occlassid: $scope.SearchModel,
            PageIndex: 1,
            PageSize: 99999
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentSearchList = data.d;
                $scope.pagesNum = Math.ceil(data.d[0].RowsCount / $scope.PageSize);
                StudentPages($scope.pagesNum);
                $scope.$apply();
            }
        });
    }
    //选择搜索结果
    $scope.SelectSelectedStudent = function () {
        var url = classProviderUrl + "/SelectSelectedStudent_List";
        var param = {
            occlassstudentsearchlist: $scope.OCClassStudentSearchList,//选择学生列表
            occlassstudentlist: $scope.OCClassStudentList//已选择学生列表
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;

                $('#addStudentModal').modal("hide");
                $scope.OCClassStudentIndex = 1;
                OCClassStudentPages(Math.ceil(data.d[0].RowsCount / $scope.PageSize));
                $scope.$apply();
            }
        });

    }



    //begin#弹出页全选操作
    //设置全选按钮
    $scope.SelectedType = {
        SelectAll: false,
        SelectPage: false,
        pageIndex: $scope.PageSearchStudentIndex
    };
    ///全选
    $scope.SelectAll = function () {
        $scope.SelectedType.SelectAll = !$scope.SelectedType.SelectAll;
        $scope.$apply();
        var url = classProviderUrl + "/SelectAll_List";
        var param = {
            occlassstudentsearchlist: $scope.OCClassStudentSearchList,//选择学生列表
            isselectall: $scope.SelectedType.SelectAll
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentSearchList = data.d;

            }
        });


    }
    //全选当前页
    $scope.SelectPage = function () {
        $scope.SelectedType.SelectPage = !$scope.SelectedType.SelectPage;
        $scope.$apply();
        var url = classProviderUrl + "/Select_Page_List";
        var param = {
            occlassstudentsearchlist: $scope.OCClassStudentSearchList,//选择学生列表
            isselectall: $scope.SelectedType.SelectPage,
            PageIndex: $scope.PageSearchStudentIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentSearchList = data.d;

            }
        });
    }
    //eng#弹出页操作

    //begin#编辑页面全选操作
    //设置全选按钮
    $scope.OCClassSelectedType = {
        SelectAll: false,
        SelectPage: false
    };
    ///全选
    $scope.OCClassSelectAll = function () {
        $scope.OCClassSelectedType.SelectAll = !$scope.OCClassSelectedType.SelectAll;
        $scope.$apply();
        var url = classProviderUrl + "/SelectAll_List";
        var param = {
            occlassstudentsearchlist: $scope.OCClassStudentList,//选择学生列表
            isselectall: $scope.OCClassSelectedType.SelectAll
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;
            }
        });


    }
    //全选当前页
    $scope.OCClassSelectPage = function () {
        $scope.OCClassSelectedType.SelectPage = !$scope.OCClassSelectedType.SelectPage;
        $scope.$apply();
        var url = classProviderUrl + "/Select_Page_List";
        var param = {
            occlassstudentsearchlist: $scope.OCClassStudentList,//选择学生列表
            isselectall: $scope.OCClassSelectedType.SelectPage,
            PageIndex: $scope.OCClassStudentIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;
            }
        });
    }
    //eng#编辑页面全选操作



    //选中的行政班学生
    //var ClassStudent_List = function () {
    //    var url = classProviderUrl + "/ClassStudent_List";
    //    var param = {
    //        occlass: $scope.ClassModel,//选中的行政班
    //        occlassstudentlist: $scope.OCClassStudentList,//已选择学生列表
    //        PageIndex: 1,
    //        PageSize: 9999
    //    };
    //    $scope.baseService.post(url, param, function (data) {
    //        if (data.d === null) {
    //            alert('暂无数据！');
    //        } else {
    //            $scope.OCClassStudentList = data.d;
    //            $('#addStudentModal').modal("hide");
    //        }
    //    });
    //}


    //######---分页
    //教学班学生分页
    var OCClassStudentPages = function (PageNum) {
        laypage({
            cont: $('#OCClassStudentPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: false, //若不显示，设置false即可
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                $scope.OCClassStudentIndex = e.curr;  //为作用域外变量赋值一定要加上$scope.$apply(); 才能实现双向绑定
                $scope.$apply();
            }
        });

    }

    //学生搜索
    var StudentPages = function (PageNum) {
        laypage({
            cont: $('#SearchStuPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: false, //若不显示，设置false即可
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                $scope.PageSearchStudentIndex = e.curr;  //为作用域外变量赋值一定要加上$scope.$apply(); 才能实现双向绑定
                $scope.$apply();
            }
        });

    }

    //教学班搜索
    var SClassPages = function (PageNum) {
        laypage({
            cont: $('#ClassPage'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageNum, //总页数
            skip: true, //是否开启跳页
            skin: '#374760', //选中的颜色
            groups: 3,//连续显示分页数
            first: false, //若不显示，设置false即可
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                //$scope.PageSearchStudentIndex = e.curr;  //为作用域外变量赋值一定要加上$scope.$apply(); 才能实现双向绑定
                Class_List(e.curr);
            }
        });

    }
    //######---

    ///##############################################

    //begin##############################################按行政班添加 搜索行政班列表
    //行政班列表
    var Class_List = function (PageIndex) {
        if ($scope.ClassModel.Key == "请输入 '行政班名称' 关键字") {
            $scope.ClassModel.Key == "";
        }
        var url = classProviderUrl + "/Class_List";
        var param = {
            model: $scope.ClassModel,
            PageIndex: PageIndex,
            PageSize: $scope.PageSize
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.Class_List = data.d;
                $scope.ClassNum = data.d[0].rowscount;
            }
        });

    }
    $scope.SearchClass = function () {
        $scope.ClassNum = 100;
        Class_List(1);
        SClassPages(Math.ceil($scope.ClassNum / $scope.PageSize));
    }
    //选择行政班学生
    $scope.SelectClassStudent = function () {
        if ($scope.ClassModel.ClassID > 0) {
            ClassStudent_List();
        }
        else {
            alert("请选择行政班！");
        }
    }

    //通过行政班添加学生
    var ClassStudent_List = function () {
        var url = classProviderUrl + "/ClassStudent_List";
        var param = {
            occlass: $scope.ClassModel,//选中的行政班
            occlassstudentlist: $scope.OCClassStudentList,//已选择学生列表
            PageIndex: 1,
            PageSize: 9999
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;

                $('#addStudentModal').modal("hide");
                $scope.OCClassStudentIndex = 1;
                OCClassStudentPages(Math.ceil(data.d[0].RowsCount / $scope.PageSize));
                $scope.$apply();
            }
        });
    }

    //----------------------------------------------
    //保存行政班信息
    $scope.OCClass_Edit = function () {
        var url = classProviderUrl + "/OCClass_Edit";
        $scope.OCClass.RecruitStartDate = "2014-01-01";
        $scope.OCClass.RecruitEndDate = "2014-01-01";
        $scope.OCClass.CreateTime = "2014-01-01";
        var param = {
            occlass: $scope.OCClass,
            octeamdropdownlist: $scope.OCClassTeachertList,
            occlassstudent: $scope.OCClassStudentList
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                alert("修改成功");
            }
        });
    }

    //end##############################################按行政班添加 搜索行政班列表


    //end-------------------------------------添加学生










    //删除学生
    $scope.DeleteStudent = function (occlassstudent) {
        var url = classProviderUrl + "/DeleteStudent";
        var param = {
            occlassstudent: occlassstudent,
            occlassstudentlist: $scope.OCClassStudentList
        };
        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;
            }
        });
    }

    //批量删除
    $scope.BatchDeleteStudent = function () {
        var url = classProviderUrl + "/BatchDeleteStudent";
        var param = {
            occlassstudentlist: $scope.OCClassStudentList
        };

        $scope.baseService.post(url, param, function (data) {
            if (data.d === null) {
                alert('暂无数据！');
            } else {
                $scope.OCClassStudentList = data.d;
                console.log($scope.OCClassStudentList);
            }
        });
    }


}]);



classModule.filter('listFilter', function () {
    return function (arr, ope, num, size) {
        if (arr == null || arr == '') {
            return;
        }
        return arr.filter(function (item) {
            if (ope == '>') {
                return item.rownum > num;
            }
            if (ope == '=') {
                return item.rownum == num;
            }
            if (ope == '<') {
                return item.rownum < num;
            }
            if (ope == 'between') {
                return item.rownum <= num * size && item.rownum >= ((num - 1) * size + 1);
            }

        });
    }
});

classModule.filter('listFilter2', function () {
    return function (arr, ope, num) {
        if (arr == null || arr == '') {
            return;
        }
        return arr.filter(function (item) {
            if (ope == '>') {
                return item.IsSelected > num;
            }
            if (ope == '=') {
                return item.IsSelected == num;
            }
            if (ope == '<') {
                return item.IsSelected < num;
            }

        });
    }
});


'use strict';
var TestModule = angular.module('app.mooc', []);
TestModule.controller('TestController', ['$scope', '$state', 'testProviderUrl', function ($scope, $state, testProviderUrl) {
    //模块切换
    $scope.CheckTestCutflag = 0;
    $scope.CheckStatusflag = 0;
    $scope.CheckTypeflag = 0;
    $scope.CheckIsSendflag = 2;
    $scope.CheckTestCut = function (type) {
        if (type == 1) {
            $scope.CheckTestCutflag = 0;
            if ($scope.NotCheck_List == null) {
                TestUser_NotCheck_List();
            }
        } else {
            $scope.CheckTestCutflag = 1;
            $scope.filtrate_NotCheck();
        }
    }

    //未批阅列表
    var TestUser_NotCheck_List = function () {
        var ocid = 1;
        var userid = 2;
        var url = testProviderUrl + "/TestUser_NotCheck_List";
        var param = { OCID: ocid, UserID: userid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d.length > 0) {
                $scope.NotCheck_List = data.d;
                $scope.OCNotCheckCount = data.d.length;
            } else {
                $scope.OCNotCheckCount = 0;
            }
        });
        $scope.$on('ngColumnGet', function (ngRepeatFinishedEvent) {
            $('.course_data').each(function () {
                $(this).find('tr:odd').css('background', '#f2f2f2');
            })
            $('.paper_data tr').hover(function () {
                $(this).find('.topic_icon').show();
            }, function () {
                $(this).find('.topic_icon').hide();
            })
            $('.topic_table tr').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).removeClass('active');
            })
            $('.paper_table tr').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).removeClass('active');
            })
            $('.guanbi').live('click', function () {
                $(this).parent().slideUp();
            })
            $('.batch_list li').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).removeClass('active');
            })
        });
    }

    //筛选弹出筛选框
    $scope.filtrate_NotCheck = function () {
        $(".filter_box").slideDown();
        var name = '';
        var ocid = 1;
        //var userid = 2;
        var type = 1;
        var Issend = 0;
        var UpdateTime = "";
        var pagesize = 20;
        var pageindex = 1;
        var url = testProviderUrl + "/TestInfo_List";
        var param = { Name: name, OCID: ocid, Type: type, IsSend: Issend, UpdateTime: UpdateTime, PageSize: pagesize, PageIndex: pageindex };
        $scope.baseService.post(url, param, function (data) {
            $scope.TestList = data.d;
        });

        $scope.$on('onColumnGetTest', function (ngRepeatFinishedEvent) {
            for (var i = 0; i < $scope.TestList.length; i++) {
                $G2S.Pie("div_" + $scope.TestList[i].test.TestID, $scope.TestList[i].MarkingPercent, "#008573");
               var data = {
                    title: "分数分布",
                    desc: { formatter: '人', name: '人数' },
                    legend: ["百分制"],
                    key: $scope.TestList[i].DSocreSectionKey,
                    value: $scope.TestList[i].DSocreSectionValue
                }
               $G2S.Line("line_chart_" + $scope.TestList[i].test.TestID, data);
            }
        });

    }

    //批阅
    $scope.Marking = function (id,userid) {
        alert(id + "---" + userid);
    }
    //新增作业考试 页面跳转
    $scope.skipAdd = function (type) {
        //type 1作业 2考试
        if (type == 1) {
            // window.location.href = "HomeWorkAdd";
            window.open("HomeWorkAdd");
        } else {
            //window.location.href = "TestAdd";
            window.open("TestAdd");
        }
    }
    TestUser_NotCheck_List();
}]);




TestModule.directive('onColumnGetTest', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('onColumnGetTest');
                });
            }
        }
    };
});

TestModule.controller('TestAddController',['$scope', '$state', 'testProviderUrl', function ($scope, $state, testProviderUrl) {
    $scope.OCID = 1;
    $scope.sc = 0;            //选择学生数
    var StudentCountAll = 0;  //学生总数 
    var ClassIDs = "";       //选中的班级字符串 ”,“ 分割
    
    $scope.UserIDs = "";

    //加载全部班级
    var TeachingClassList_Get = function()
    {
        
        var url = testProviderUrl + "/TeachingClass_Owner_List";
        var param = { OCID: $scope.OCID }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.TeachingClassList = data.d;
            }
        });
    }

    TeachingClassList_Get();

    //班级选中加载学生列表
    $scope.StudentList_TeachingClassID_Get = function (ClassID,UserCount,cx)
    {
        ClassIDs = "";
        //获取选中的班级字符串
        $(".TeachingClass_CHX").each(function () {
            if(this.checked)
            {
                ClassIDs += this.id + ",";
                
            }
        })


        //选中学生总数计算
        if (cx) {
            $scope.sc = $scope.sc + UserCount;
        }
        else {
            $scope.sc = $scope.sc - UserCount;
        }

        if ($scope.sc < 101) {  //超过一百学生学生列表不在添加，防止页面卡死
            //加载学生列表
            var url = testProviderUrl + "/Student_List";
            var param = { TeachingClassIDs: ClassIDs }
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.StudentList = data.d;
                }
            });
        }

    }

    $scope.Div_Confirm_Click = function()
    {
        var str = "";  //作业对象字符串
        
        
        $(".TeachingClass_CHX").each(function () {
            var cid = this.id;
            var isbreak = false;
            var strclass = "";
            if (this.checked) {
                str += cid + "@";
                $("." + cid).each(function () {
                    //this.attributes["cid"].nodeValue   获取标签自定义属性值
                    if (this.checked ) {
                        strclass += this.id + ",";
                    } else {
                        isbreak = true;
                    }
                });
                if (isbreak) {
                    str += strclass.substr(0, strclass.length - 1);
                }
                str += ";";
            }
        });

        alert(str);
        
    }

    $scope.Student_Change = function (stuchx,classid) {
        if(!stuchx)
        {
            $scope.sc = $scope.sc - 1;
        }else
        {
            $scope.sc = $scope.sc + 1;
        }
    }
}]);




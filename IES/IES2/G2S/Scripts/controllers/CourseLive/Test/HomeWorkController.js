'use strict';
var HomeWorkModule = angular.module('app.homework', []);
HomeWorkModule.controller('HomeWorkController', ['$scope', '$state', 'testProviderUrl', function ($scope, $state, testProviderUrl) {
    $scope.TestID = 0;
    $scope.OCID = 1;
    $scope.CourseID = 0;
    $scope.sc = 0;            //选择学生数
    var StudentCountAll = 0;  //学生总数 
    var ClassIDs = "";       //选中的班级字符串 ”,“ 分割
    $scope.frmoEditor1 = ""; //线下作业内容
    $scope.frmoEditor2 = ""; //线下作业参考答案
    $scope.Student_ZP_Disabled = true;  //学生自评状态true：禁用  false：启用
    var ScoreSource = 0 //成绩评定计算
    $scope.TS = true;   //教师评分勾选状态
    $scope.SH = false;  //学生互评勾选状态
    $scope.SZ = false;  //学生自评勾选状态
    $scope.ExerciseCount = 0; //习题总数
    $scope.TotalScore = 0;  //总分
    $scope.ExerciseTypeID = 0; //习题类型ID  用于题库选题
    $scope.DiffcultID = 0; //难度id  用于题库选题
    $scope.ExerciseTypeID_ZNXT = 0; //习题类型ID  用于智能出题
    $scope.DiffcultID_ZNXT = 0; //难度id  用于智能出题
    $scope.SeaCon = ""; //搜索关键字  用于题库选题
    $scope.PageCount = 10000; //每页显示行数  用于题库选题 默认显示全部
    $scope.KeyID = 0;  //标签id   用于题库选题
    $scope.KenID = 0;  //知识点id   用于题库选题
    $scope.PaperCon = "" //试卷搜索内容  用于试卷搜索
    $scope.PaperType = -1  //试卷类型  用于试卷搜索 
    $scope.PaperPageCount = 10000  //试卷显示数目  用于试卷搜索 默认显示全部 
    $scope.Paper_Set = null;   //选中的试卷   用于试卷搜索
    $scope.gv = 0  //试卷分组id
    $scope.ZNZJ = true;  //智能组卷显示状态
    $scope.TKXT = true;  //题库出题显示状态
    $scope.ZDSJ = true;  //制定试卷显示状态
    $scope.DTKCT = true;  //答题卡出题显示状态
    $scope.XXZY = true;  //线下作业显示状态
    $scope.NEXT = true; //新增部分显示状态
    $scope.ExerciseCount_ZNZJ = 0;  //智能组卷习题总数
    $scope.ExerciseType_Q = new Array();  //只能组卷已选择习题类型

    

    //初始加载
    var Test_Get = function () {
        var url = testProviderUrl + "/Test_Get";
        var param = { TestID: $scope.TestID }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.Test = data.d;
            }
            if ($scope.Test.DelayScoreDiscount == 0) {
                $scope.Test.DelayScoreDiscount = 10;
                $scope.Test.Delay = 2;
            } else {
                if ($scope.Test.ScoreSource == 7) {
                    $scope.TS = true;   //教师评分勾选状态
                    $scope.SH = true;  //学生互评勾选状态
                    $scope.SZ = true;  //学生自评勾选状态
                    $scope.Student_ZP_Disabled = false;
                }

                if ($scope.Test.ScoreSource == 3) {
                    $scope.TS = true;   //教师评分勾选状态
                    $scope.SH = true;  //学生互评勾选状态
                    $scope.SZ = false;  //学生自评勾选状态
                    $scope.Student_ZP_Disabled = false;
                }
            }
        });

    }
    //成绩类型
    var TestScaleType_Get = function () {
        var url = testProviderUrl + "/TestScaleType_Get";
        var param = "";
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.TestScaleTypeList = data.d;
            }
        });
    }

    //试卷信息
    var PaperGroupList = function(paperid)
    {
        var url = testProviderUrl + "/PaperDefineInfo_Get";
        var param = { PaperID: paperid };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.PaperDefineInfo = data.d;
            }
        });
    }

    Test_Get();
    TestScaleType_Get();
    
    if ($scope.Test == null) {
        PaperGroupList(0);
    } else {
        PaperGroupList($scope.Test.PaperID);
    }
    

    //保存
    $scope.Save_Click = function () {
        $scope.Test.ScoreSource = ScoreSourceCount();
        //debugger;
        if ($scope.Test.Name == "" || $scope.Test.Name == null) {
            alert("请输入作业名称。");
            return;
        }
        if ($scope.Test.ChapterName == "" || $scope.Test.ChapterName == null) {
            alert("请输入章节名称。");
            return;
        }
        //if ($scope.Test.ScoreTypeID == 0) {
        //    alert("请选择成绩类型。");
        //    return;
        //}
        if ($scope.Test.ScoreSource == 0) {
            alert("请选择成绩评定对象。");
            return;
        }
        if (isDate($scope.Test.StartDate) == "") {
            alert("请选择开始时间。");
            return;
        }
        if (isDate($scope.Test.EndDate) == "") {
            alert("请选择结束时间。");
            return;
        }
        if ($scope.Test.ScoreSource > 1) {
            if ($scope.Test.StudentCheckNum < 0) {
                alert("学生每人评阅份数不能为负。");
                return;
            }
            if ($scope.Test.LostScoreDiscount < 0) {
                alert("缺评总分减少百分比不能为负。");
                return;
            }
            if (isDate($scope.Test.EndCheckTime) == "") {
                alert("请输入评阅截止时间。");
                return;
            }
        }
        if ($scope.sc < 1) {
            alert("请选择参与对象。");
            return;
        }
        $scope.Test.UpdateTime = new Date();
        var IDS = Div_Confirm_Click();
        var url = testProviderUrl + "/Test_Add_Update";
        var param = {
            Test: $scope.Test,
            PaperDefineInfo: $scope.PaperDefineInfo,
            OCID: $scope.OCID,
            CourseID: $scope.CourseID,
            IDS: IDS,
            content: $scope.frmoEditor1,
            answer: $scope.frmoEditor2
        }
        $scope.baseService.post(url, param, function (data) {
            //debugger;
            if (data.d != null) {
                $scope.TestID = data.d;
                alert("添加成功！");
            } else {
                alert("添加失败！");
            }

        });

    }

    //点击“学生互评”激活“学生自评”
    $scope.StudentScoreSource_change = function (v) {
        if (v)
            $scope.Student_ZP_Disabled = false;
        else
            $scope.Student_ZP_Disabled = true;
    }

    //计算成绩评定
    var ScoreSourceCount = function () {
        ScoreSource = 0;
        $(".ScoreSource").each(function () {
            if (this.checked) {
                ScoreSource = ScoreSource + parseInt(this.value);
            }
        });
        return ScoreSource;
    }

    //线下作业弹出 确定
    $scope.XXHomeWork_Click = function () {
        $scope.frmoEditor1 = document.getElementById('frmoEditor1').contentWindow.getHTML();
        $scope.frmoEditor2 = document.getElementById('frmoEditor2').contentWindow.getHTML();
    }

    //线下作业弹出
    $scope.XXHomeWork_Eject = function () {
        $scope.Test.BuildMode = 4; //1选择试卷   2 智能选题     3 答题卡    4 附件型作业  5第三方批阅
        $scope.ExerciseCount = 1;  //线下作业默认1道题
        $scope.TotalScore = 100;  //线下作业默认100分

        $scope.ZNZJ = false;
        $scope.TKXT = false;
        $scope.ZDSJ = false;
        $scope.DTKCT = false;
        $scope.XXZY = false;
        $scope.NEXT = false;
    }

    //加载全部班级
    $scope.TeachingClassList_Get = function () {
        var url = testProviderUrl + "/TeachingClass_Owner_List";
        var param = { OCID: $scope.OCID }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.TeachingClassList = data.d;
            }
        });
    }

    //班级选中加载学生列表
    $scope.StudentList_TeachingClassID_Get = function (ClassID, UserCount, cx) {
        ClassIDs = "";
        //获取选中的班级字符串
        $(".TeachingClass_CHX").each(function () {
            if (this.checked) {
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

    //返回作业对象字符串
    var Div_Confirm_Click = function () {
        var str = "";  //作业对象字符串

        //循环班级列表
        $(".TeachingClass_CHX").each(function () {
            var cid = this.id;  //班级id
            var isbreak = false;  //循环学生列表时判断是否整个班级的学生都选中
            var strclass = "";  //班级选中学生字符串
            //班级是否选中
            if (this.checked) {
                str += cid + "@";  //拼接班级id
                //循环选中班级下的学生
                $("." + cid).each(function () {
                    //this.attributes["cid"].nodeValue   获取标签自定义属性值
                    //选中的学生加入临时选中学生字符串，如果发现有未选中的那么这个字符串需要拼接到作业对象字符串，负责不拼接
                    if (this.checked) {
                        strclass += this.id + ",";
                    } else {
                        isbreak = true;
                    }
                });
                //班级中学生如果全部选中学生id字符串为空，否则加上选中的学生id字符串
                if (isbreak) {
                    str += strclass.substr(0, strclass.length - 1);
                }
                str += ";"; //一个班级循环完毕，拼上结束字符串“;”
            }
        });
        return str;
    }

    //学生选中改变选中的学生数
    $scope.Student_Change = function (stuchx, classid) {
        if (!stuchx) {
            $scope.sc = $scope.sc - 1;
        } else {
            $scope.sc = $scope.sc + 1;
        }
    }

    //题型列表
    var Resource_Dict_ExerciseType_List = function () {
        var url = testProviderUrl + "/Resource_Dict_ExerciseType_List";
        var param = "";
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.ExerciseTypeList = data.d;
            }
        });
    }

    //难度系数
    var Resource_Dict_Diffcult_List = function () {
        var url = testProviderUrl + "/Resource_Dict_Diffcult_List";
        var param = "";
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.DiffcultList = data.d;
            }
        });
    }

    //知识点列表
    var Ken_List = function () {
        var url = testProviderUrl + "/Ken_List";
        var param = { OCID: $scope.OCID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.KenList = data.d;
            }
        });
    }

    //标签列表
    var Key_List = function () {
        var url = testProviderUrl + "/Key_List";
        var param = "";
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.KeyList = data.d;
            }
        });
    }



    //题库选题初始加载
    $scope.Init_Exercise = function (gv) {
        Resource_Dict_ExerciseType_List();
        Resource_Dict_Diffcult_List();
        Ken_List();
        Exercise_List();
        $scope.Test.BuildMode = 2;  //1选择试卷   2 智能选题     3 答题卡    4 附件型作业  5第三方批阅
        $scope.gv = gv;

        $scope.ZNZJ = true;
        $scope.TKXT = true;
        $scope.ZDSJ = false;
        $scope.DTKCT = false;
        $scope.XXZY = false;
        $scope.NEXT = true;
    }

    //标签选择
    $scope.Key_Click = function (keyid) {
        $scope.keyid = keyid;
        Exercise_List();
    }

    //知识点选择
    $scope.Ken_Click = function (kenid) {

        $scope.KenID = kenid;
        Exercise_List();
    }

    //习题筛选
    var Exercise_List = function () {
        //debugger;
        var url = testProviderUrl + "/Exercise_Search";
        var param = { PageSize: $scope.PageCount, PageIndex: 1, Searchkey: $scope.SeaCon, OCID: $scope.OCID, CourseID: $scope.CourseID, EXType: $scope.ExerciseTypeID, Diffcult: $scope.DiffcultID, keyID: $scope.KenID };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.ExerciseList = data.d;
                //debugger;
                //已选择的习题列表中不显示
                for (var i = 0; i < $scope.PaperDefineInfo.exerciselist.length; i++) {
                    haveSameNum_Exercise($scope.PaperDefineInfo.exerciselist[i].ExerciseID);
                }
            }
        });
    }

    //习题去除重复习题
    var haveSameNum_Exercise = function(id)
    {
        for (var i = 0; i < $scope.ExerciseList.length; i++) {
            if ($scope.ExerciseList[i].ExerciseID == id) {
                $scope.ExerciseList.splice(i, 1);
            }
        }
    }

    //习题全选
    $scope.Change_AllExercise = function (ch) {
        if (ch) {
            $(".chk_exercise").each(function () {
                this.checked = true;
            })
        } else {
            $(".chk_exercise").each(function () {
                this.checked = false;
            })
        }
    }


    //新增习题弹出框隐藏
    $scope.LoginCancel = function () {
        $(".loading").css("display", "none");
        $(".loading-mask").css("display", "none");
    }
    //新增习题弹出框显示
    $scope.ExerciseADD_Click = function () {
        $(".loading").css("display", "block");
        $(".loading-mask").css("display", "block");
    }
    //新增习题完成
    $scope.ExerciseADD_Save = function () {
        Exercise_List();
        $(".loading").css("display", "none");
        $(".loading-mask").css("display", "none");

    }

    //搜索
    $scope.Sea_Click = function () {
        Exercise_List();
    }

    //题库选题保存(参数为paperinfo下，分组集合的坐标)
    $scope.Exercise_Save_Click = function () {
        $scope.ExerciseSaveList = new Array();  //声明一个空集合
        //循环所有习题
        for (var i = 0; i < $scope.ExerciseList.length; i++) {
            var isChecked = false;  //判断习题是否被选中
            //判断是否被选中
            $(".chk_exercise").each(function () {
                if (this.checked && this.id == $scope.ExerciseList[i].ExerciseID) {
                    isChecked = true;
                }
            });
            //把选中的习题加入集合
            if (isChecked) {
                $scope.ExerciseSaveList.splice(0, 0, $scope.ExerciseList[i]);
            }
        }
        //debugger;
        if ($scope.PaperDefineInfo.exerciselist == null) {
            $scope.PaperDefineInfo.exerciselist = new Array();
        }
        //把习题内容放入paperinfo
        for (var i = 0; i < $scope.ExerciseSaveList.length; i++) {
            var item = $scope.ExerciseSaveList[i];
            
            var pe = {
                Conten: item.Conten,
                Kens: item.Kens,
                Keys: item.Keys,
                Diffcult: item.Diffcult,
                IsRand: item.IsRand,
                ExerciseTypeName: item.ExerciseTypeName,
                ExerciseType: item.ExerciseType,
                PaperExerciseID: 0,
                PaperID: 0,
                ParentExerciseID: 0,
                PaperTacticID: 0,
                Score: 0.00,
                Orde: 0,
                ExerciseID: item.ExerciseID,
                PaperGroupID: $scope.gv,
            }

            
            $scope.PaperDefineInfo.exerciselist.splice(0, 0, pe);
        }
        
    }



    //统一设置分
    $scope.ScoreAll_Click = function (groupID) {
        var v = $("#ScoreAll_"+groupID).val();
        $(".Score_Text_"+groupID).each(function () {
            this.value = v;
        })
        CountScore();
    }

    //计算题库选题选中的习题总分和总数  放入grouplist
    var ExerciseCountScore = function () {
        
    }

    //弹出框取消
    $scope.ESC = function()
    {
        $scope.ZNZJ = true;
        $scope.TKXT = true;
        $scope.ZDSJ = true;
        $scope.DTKCT = true;
        $scope.XXZY = true;
        $scope.NEXT = true;
        if ($scope.PaperDefineInfo.exerciselist != null)
        {
            $scope.ZNZJ = true;
            $scope.TKXT = true;
            $scope.ZDSJ = false;
            $scope.DTKCT = false;
            $scope.XXZY = false;
            $scope.NEXT = true;
        }
        
    }


    //创建下一部分
    $scope.Next_Click = function()
    {
        
        //debugger;
        var grouplist = $scope.PaperDefineInfo.papergrouplist;
        var gl = grouplist.length;  
        var lsid = grouplist[gl - 1].GroupID - 1;  //创建一个临时id  临时id必定小于1   当小于1时后台执行新增大于1执行更新 
        if (lsid > 0)  //临时id如果大于0  那么给他值为0
        { 
            lsid = 0;
        }

        var Group = {
            GroupID: lsid,
            GroupName: "第" + (gl+1) + "部分",
            Orde: gl + 1,
            PaperID: 0,
            Brief: "",
            Timelimit:0
        }
        $scope.PaperDefineInfo.papergrouplist.splice(gl, 0, Group); //添加一个对象

        if ($scope.PaperDefineInfo.papergrouplist.length > 1) {
            $scope.XXZY = false;
            $scope.ZDSJ = false;
        }
    }

    //删除习题
    $scope.Exercise_Del_Click = function(ExerciseID)
    {
        for (var i = 0; i < $scope.PaperDefineInfo.exerciselist.length; i++) {
            if ($scope.PaperDefineInfo.exerciselist[i].ExerciseID == ExerciseID)
            {
                $scope.PaperDefineInfo.exerciselist.splice(i, 1);
            }
        }
    }

    //删除一个分组  参数为数组位置
    $scope.Group_Del_Click = function(v)
    {
        //debugger;
        var gl = $scope.PaperDefineInfo.papergrouplist.length;  //分组个数
        var group = $scope.PaperDefineInfo.papergrouplist[v];  //要删除的分组
        if (gl > 1 )
        {
            //删除分组下的习题
            if ($scope.PaperDefineInfo.exerciselist != null) {
                var el = $scope.PaperDefineInfo.exerciselist.length;  //所有习题总数
                var n = el;  //中间变量
                for (var i = 0; i < el; i++) {
                    var exercise = $scope.PaperDefineInfo.exerciselist[i + n - el];  //计算对应习题对象
                    if (exercise.PaperGroupID == group.GroupID) {  
                        $scope.PaperDefineInfo.exerciselist.splice(i + n - el, 1);  //删除当前分组下习题
                        n = n - 1;  
                    }
                }
            }
            $scope.PaperDefineInfo.papergrouplist.splice(v, 1);  //删除分组
        }else
        {
            alert("至少要保留一部分。");
        }

        if ($scope.PaperDefineInfo.papergrouplist.length == 1) {
            $scope.XXZY = true;
            $scope.ZDSJ = true;
            $scope.ZNZJ = true;
        }
    }

    //试卷类型
    var PaperType_List = function () {
        var url = testProviderUrl + "/Resource_Dict_PaperType_List";
        var param = "";
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.PaperTypeList = data.d;
            }
        });
    }

    //试卷list
    var Paper_List = function () {
        var url = testProviderUrl + "/Paper_Search";
        var param = { Searchkey: $scope.PaperCon, OCID: $scope.OCID, CourseID: $scope.CourseID, Type: $scope.PaperType }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.PaperList = data.d;
            }
        });
    }

    //试卷弹出框加载
    $scope.Init_Paper = function () {
        PaperType_List();
        Paper_List();

        $scope.ZNZJ = false;
        $scope.TKXT = false;
        $scope.ZDSJ = false;
        $scope.DTKCT = false;
        $scope.XXZY = false;
        $scope.NEXT = false;
    }

    //试卷搜索
    $scope.Paper_Search = function () {
        Paper_List();
    }

    //试卷列表点击单选，把试卷对象传回来保存
    $scope.Paper_Save_Click = function () {
        //debugger;
        $scope.Paper_Set = th;
    }

    //知识点列题 列表
    var ZNZJ_Ken_List = function () {
        $("#EC_" + $scope.ZNZJ_SelectGroupID).html(0);  //习题总数默认给0
        var url = testProviderUrl + "/Ken_ExerciseCount_List";
        var param = { OCID: $scope.OCID, ExerciseType: $scope.ExerciseTypeID_ZNXT, Diffcult: $scope.DiffcultID_ZNXT }
        $scope.baseService.post(url, param, function (data) {
            //debugger;
            if (data.d != null) {
                $scope.KenExerciseCountList = data.d;
                //$scope.ExerciseCount_ZNZJ = 0;
                //for (var i in $scope.KenExerciseCountList) {
                //    $scope.ExerciseCount_ZNZJ += $scope.KenExerciseCountList[i].ExerciseCount;
                //}
            }
        });
    }

    //章节列题 列表
    var ZNZJ_Chapter_List = function () {
        $("#EC_" + $scope.ZNZJ_SelectGroupID).html(0);  //习题总数默认给0
        var url = testProviderUrl + "/Chapter_ExerciseCount_List";
        var param = { OCID: $scope.OCID, ExerciseType: $scope.ExerciseTypeID_ZNXT, Diffcult: $scope.DiffcultID_ZNXT }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.ChapterExerciseCountList = data.d;
                //$scope.ExerciseCount_ZNZJ = 0;
                //for (var i in $scope.ChapterExerciseCountList) {
                //    $scope.ExerciseCount_ZNZJ += $scope.ChapterExerciseCountList[i].ExerciseCount;
                //}
            }
        });
    }

    //智能组卷click
    $scope.ZNZJ_Click = function (groupid)
    {
        //debugger;
        $scope.Test.BuildMode = 2;
        $scope.ZNZJ_SelectGroupID = groupid;   //智能组卷当前操作的分组id
        $scope.ZNZJ_Show = true;
        if ($scope.DiffcultList == null) {
            Resource_Dict_Diffcult_List();
        }
        if ($scope.ExerciseTypeList == null)
        {
            Resource_Dict_ExerciseType_List();
        }
        $scope.KenExerciseCountList = null;
        $scope.ChapterExerciseCountList = null;
    }

    $scope.ZNZJ_ExerciseTypeID_Change =function(v)
    {
        $scope.ExerciseTypeID_ZNXT = v;
    }

    $scope.ZNZJ_DiffcultID_Change = function (v) {
        $scope.DiffcultID_ZNXT = v;
    }

    //智能组卷  知识点列题
    $scope.ZNZJ_Ken_Click = function()
    {
       
        $scope.ZNZJ_Type = "Ken";
        $scope.ChapterExerciseCountList = null;
        ZNZJ_Ken_List();
        //alert($scope.ExerciseCount_ZNZJ);
    }

    //智能组卷  章节列题
    $scope.ZNZJ_Chapter_Click = function ()
    {
        $scope.ZNZJ_Type = "Chapter";
        $scope.KenExerciseCountList = null;
        ZNZJ_Chapter_List();
        //alert($scope.ExerciseCount_ZNZJ);
    }

    $scope.ZNZJ_ADD_Click = function(groupid)
    {
        $scope.Tactic_EditOrAdd = "Add";
        if ($scope.PaperDefineInfo.papertacticlist == null) {
            $scope.PaperDefineInfo.papertacticlist = new Array();
        }
        for (var i in $scope.ChapterExerciseCountList) {
            var PT = {
                PaperTacticID: 0,
                PaperID: 0,
                GroupID: groupid,
                ExerciseType: $scope.ExerciseTypeID_ZNXT,
                Num: 0,
                ScorePer: 0,
                KenID: 0,
                Diffcult: $scope.DiffcultID_ZNXT,
                ChapterID: 0,
                KenName: "",
                ChapterName: "",
                ExerciseCount: 0   //习题总数，补充属性

            }
            var Chapter = $scope.ChapterExerciseCountList[i];
            PT.ScorePer = Chapter.Scoreper;
            PT.ChapterID = Chapter.ChapterID;
            PT.Num = Chapter.Num;
            PT.ChapterName = Chapter.Title;
            PT.ExerciseCount = Chapter.ExerciseCount;
            var index_tactic = 0;
            if ($scope.PaperDefineInfo.papertacticlist != null) {
                index_tactic = $scope.PaperDefineInfo.papertacticlist.length;
            }
            $scope.PaperDefineInfo.papertacticlist.splice(index_tactic, 0, PT);
        }

        for (var i in $scope.KenExerciseCountList) {
            var PT = {
                PaperTacticID: 0,
                PaperID: 0,
                GroupID: groupid,
                ExerciseType: $scope.ExerciseTypeID_ZNXT,
                Num: 0,
                ScorePer: 0,
                KenID: 0,
                Diffcult: $scope.DiffcultID_ZNXT,
                ChapterID: 0,
                KenName: "",
                ChapterName :"",
                ExerciseCount:0  //习题总数，补充属性
            }
            var Ken = $scope.KenExerciseCountList[i];
            PT.ScorePer = Ken.Scoreper;
            PT.KenID = Ken.KenID
            PT.Num = Ken.Num;
            PT.KenName = Ken.Name;
            PT.ExerciseCount = Ken.ExerciseCount;
            var index_tactic = 0;
            if ($scope.PaperDefineInfo.papertacticlist != null)
            {
                index_tactic = $scope.PaperDefineInfo.papertacticlist.length;
            }
            $scope.PaperDefineInfo.papertacticlist.splice(index_tactic, 0, PT);
        }

        ZNZJ_ExerciseType_Filter();
        $scope.ChapterExerciseCountList = null;
        $scope.KenExerciseCountList = null;
        $scope.ZNZJ_Type = "";
        ZNZJ_ExerciseInfo_CCS();
    }

    //智能组卷 筛选出选中的题型
    var ZNZJ_ExerciseType_Filter = function()
    {
        //debugger;
        for (var i in $scope.PaperDefineInfo.papertacticlist) {
            ZNXT_haveSameNum_ExerciseType($scope.PaperDefineInfo.papertacticlist[i]);
        }
    }

    //智能组卷 题型去除已选择的
    var ZNXT_haveSameNum_ExerciseType = function (obj) {
        for (var i in $scope.ExerciseTypeList) {
            if ($scope.ExerciseTypeList[i].id == obj.ExerciseType) {
                ZNXT_ExerciseInfo($scope.ExerciseTypeList[i].id, $scope.ExerciseTypeList[i].name, obj.Diffcult, obj.KenID, obj.ChapterID, obj.GroupID,0,0,0);
                $scope.ExerciseTypeList.splice(i, 1);
            }
        }
    }

    //只能选题选中的习题类型和其他基本信息  用于循环已选择的智能组卷习题类别
    var ZNXT_ExerciseInfo = function (ExerciseTypeID, ExerciseTypeName, DiffcultID, KenID, ChapterID,GroupID,ExerciseCount,ExerciseCountChecked,ExerciseCountScore)
    {
        var ZE = {
            ExerciseTypeID: ExerciseTypeID,
            ExerciseTypeName: ExerciseTypeName,
            DiffcultID: DiffcultID,
            KenID: KenID,
            ChapterID: ChapterID,
            GroupID: GroupID,
            ExerciseCount:ExerciseCount,  //习题总数
            ExerciseCountChecked:ExerciseCountChecked,  //抽提数
            ExerciseCountScore: ExerciseCountScore  //分数总和
        }
        //debugger;
        $scope.ExerciseType_Q.splice(0, 0, ZE);
    }

    //智能组卷的习题编辑
    $scope.ZNZJ_Exercise_Edit = function()
    {
        $scope.Tactic_EditOrAdd = "Edit";
    }

    $scope.ZNZJ_Exercise_Confirm = function()
    {
        $scope.Tactic_EditOrAdd = "Add";
    }

    //智能组卷的习题删除
    $scope.ZNZJ_Exercise_Del = function(exerciseTypeID)
    {
        //debugger;
        var ptl = 0;
        if ($scope.PaperDefineInfo.papertacticlist != null) {
            ptl  = $scope.PaperDefineInfo.papertacticlist.length;
        }
        var n = ptl;

        for (var i = 0; i < ptl; i++) {
            var tactic = $scope.PaperDefineInfo.papertacticlist[i + n - ptl];
            if(tactic.ExerciseType == exerciseTypeID)
            {
                $scope.PaperDefineInfo.papertacticlist.splice(i + n - ptl, 1);
                n = n - 1;
            }
        }
        

        for (var i in $scope.ExerciseType_Q) {
            var tactic = $scope.ExerciseType_Q[i];
            if (tactic.ExerciseTypeID == exerciseTypeID) {
                var ET = {
                    id:tactic.ExerciseTypeID,
                    name: tactic.ExerciseTypeName,
                    nameen:"",
                    source: "",
                    ceshi:""
                }
                $scope.ExerciseTypeList.splice(0, 0, ET);
                $scope.ExerciseType_Q.splice(i, 1);
            }
        }
    }

    //计算知识点或者章节下的习题总数
    $scope.$on('ngExerciseCountSelect', function (ngRepeatFinishedEvent) {
        var ec = 0;
        for (var i in $scope.KenExerciseCountList) {
            var ken_ec = $scope.KenExerciseCountList[i].ExerciseCount;
            ec = ec + ken_ec;
        }
        for (var i in $scope.ChapterExerciseCountList) {
            var Chapter_ec = $scope.ChapterExerciseCountList[i].ExerciseCount;
            ec = ec + Chapter_ec;
        }

        $("#EC_" + $scope.ZNZJ_SelectGroupID).html(ec);
        //$scope.ZNZJ_ExerciseCount = ec;
    });

    //计算各个题型下的习题总数，抽提数，分值总数
    var ZNZJ_ExerciseInfo_CCS = function()
    {
        for (var i in $scope.ExerciseType_Q) {
            var obj1 = $scope.ExerciseType_Q[i];
            for(var x in $scope.PaperDefineInfo.papertacticlist)
            {
                var obj2 = $scope.PaperDefineInfo.papertacticlist[x];
                if(obj1.ExerciseTypeID == obj2.ExerciseType)
                {
                    //debugger;
                    obj1.ExerciseCount = obj1.ExerciseCount + obj2.ExerciseCount;
                    obj1.ExerciseCountChecked = obj1.ExerciseCountChecked + parseInt(obj2.Num);
                    obj1.ExerciseCountScore = obj1.ExerciseCountScore + (parseInt(obj2.Num) * obj2.ScorePer);
                    
                }
            }

            for(var j in $scope.PaperDefineInfo.papergrouplist)
            {
                var obj3 = $scope.PaperDefineInfo.papergrouplist[j];
                if(obj3.GroupID == obj1.GroupID)
                {
                    obj3.ExerciseCount = obj3.ExerciseCount + obj1.ExerciseCountChecked;
                    obj3.ExerciseScore = obj3.ExerciseScore + obj1.ExerciseCountScore;
                }
            }
        }


    }


    //开始时间
    var start = {
        elem: '#start',
        format: 'YYYY-MM-DD hh:mm:ss',
        min: laydate.now(), //设定最小日期为当前日期
        max: '2099-06-16 00:00:00', //最大日期
        istime: true,
        istoday: false,
        choose: function (datas) {
            end.min = datas; //开始日选好后，重置结束日的最小日期
            end.start = datas; //将结束日的初始值设定为开始日
            $scope.Test.StartDate = datas;
        }
    };
    //结束时间
    var end = {
        elem: '#end',
        format: 'YYYY-MM-DD hh:mm:ss',
        min: laydate.now(),
        max: '2099-06-16 00:00:00',
        istime: true,
        istoday: false,
        choose: function (datas) {
            start.max = datas; //结束日选好后，重置开始日的最大日期
            $scope.Test.EndDate = datas;
        }
    };
    //评阅截止时间
    var endCheckTime = {
        elem: '#endCheckTime',
        format: 'YYYY-MM-DD hh:mm:ss',
        min: laydate.now(), //设定最小日期为当前日期
        max: '2099-06-16 00:00:00', //最大日期
        istime: true,
        istoday: false,
        choose: function (datas) {
            end.max = datas;
            $scope.Test.EndCheckTime = datas;
        }
    };
    //debugger;
    laydate(start);
    laydate(end);
    laydate(endCheckTime);

    //判断日期是否为空
    var isDate = function (v) {
        if (v == undefined) { return ""; }
        var re = /-?\d+/;
        var m = re.exec(v);
        if (parseInt(m) < 0) { return ""; }
    }


}]);


HomeWorkModule.directive('onExerciseCountSelect', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngExerciseCountSelect');
                });
            }
        }
    };
});


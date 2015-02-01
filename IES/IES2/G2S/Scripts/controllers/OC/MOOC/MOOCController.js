/// <reference path="../../../../Views/CourseLive/Test/HomeWorkAdd.cshtml" />

var moocModule = angular.module('app.mooc', []);



//MOOC设置页面
moocModule.controller('MOOCController', ['$scope', '$filter', '$state', 'moocProviderUrl', function ($scope, $filter, $state, moocProviderUrl, dateFormatAll) {

    $scope.MOOCName = "《国际英语》";
    $scope.OCID = 1; //1选中课程目录；2选中课程资料
    $scope.type = 1; //1选中课程目录；2选中课程资料
    $scope.Step = 1; //1进阶式教程设计；2见面课设计；3发布与招生
    $scope.Chapter = null;  //当前操作的章节信息
    $scope.ChapterEnter = null;  //回车添加章节时候的信息
    $scope.ChapterList = null; //章节的集合
    $scope.OCMoocFileList = null; //资料集合
    $scope.OcMooc = null;   //MOOC基本信息
    $scope.OCMoocOfflineList = null; //见面课集合
    $scope.OCMoocOffline = null;     //当前操作的见面课
    $scope.Data = 1; //1本次见面课的目的 2教师要点说明 3学生组织说明 4评价与成绩说明 5资源配套说明 6考核方式
    $scope.Incomplete = 0; //没有准备好的资料数
    $scope.ClassList = null; //招生列表
    $scope.OCMoocLiveDiscussList = null; //章节讨论列表
    $scope.OCMoocLiveInit = 0; //章节讨论计数
    $scope.OCMoocRecruitInfoList = null;        //MOOC招生列表
    $scope.OCMoocRecruitInfoList_His = null;    //MOOC历史招生列表
    $scope.OCMoocRecruitStatus = 1;   //默认选中进行中招生
    $scope.OCMoocTotal = null;   //MOOC统计信息

    var bindBodyKeyUp;
    $scope.$watch('$viewContentLoaded', function () {
        bindBodyKeyUp = function () {
            dieBodyKeyUp();
            $('.course_detail').keyup(function (e) {
                var keyNumber = e.keyCode;
                switch (keyNumber) {
                    case 13:
                        Chapter_Enter_Add();    //回车添加新章节
                        break;
                }
            });
        }

        function dieBodyKeyUp() {
            $('.course_detail').unbind('keyup');
        }
        bindBodyKeyUp();
    });



    //设计向导-步骤
    $scope.ChangeStep = function (step) {
        $scope.Step = step;
        if (step == 2 && $scope.OCMoocOfflineList == null) //首次进入见面课设计，读取列表
        {
            var url = moocProviderUrl + "/OCMoocOffline_List";
            var param = { OCID: $scope.OCID }
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.OCMoocOfflineList = data.d;
                }
            });
        }

        if (step == 3) //首次进入见面课设计，读取列表
        {
            if ($scope.OCMoocOfflineList == null) {
                var url = moocProviderUrl + "/OCMoocOffline_List";
                var param = { OCID: $scope.OCID }
                $scope.baseService.post(url, param, function (data) {
                    if (data.d != null) {
                        $scope.OCMoocOfflineList = data.d;
                    }
                });
            }

            $scope.CompleteCheck();

            if ($scope.OCMoocRecruitInfo == null) {
                var url = moocProviderUrl + "/OCMoocRecruit_List";
                var param = { OCID: $scope.OCID, IsHistroy: 0 }
                $scope.baseService.post(url, param, function (data) {
                    if (data.d != null) {

                    }
                });
            }

        }
    }

    $scope.OCMoocSave_Step1 = function (next) {
        
        if ($scope.type == 1) {
            for (var i = 0; i < $scope.ChapterList.length; i++) {
                var url = moocProviderUrl + "/Chapter_Upd";
                var param = { model: $scope.ChapterList[i] };
                $scope.baseService.post(url, param, function (data) {
                   
                });
            }
        }
        else if ($scope.type == 2) {
            for (var i = 0; i < $scope.OCMoocFileList.length; i++) {
                $scope.OCMoocFileList[i].Timelimit = parseInt($scope.OCMoocFileList[i].TimelimitMin * 60);
                $scope.OCMoocFileList[i].UploadTime = $filter('dateFormatAll')($scope.OCMoocFileList[i].UploadTime);
                var url = moocProviderUrl + "/OCMoocFile_Edit";
                var param = { model: $scope.OCMoocFileList[i] };
                $scope.baseService.post(url, param, function (data) {
                  
                });
            }
        }

        if (next == 1) {
            alert("保存成功");
        }
        else {
            $scope.Step = 2;
        }
    }

    $scope.CompleteCheck = function () { //完整度检测
        $scope.Incomplete = 0
        for (var i = 0; i < $scope.ChapterList.length; i++) {
            if ($scope.ChapterList[i].ParentID == 0) {  //下列项只需检测章
                if ($scope.ChapterList[i].PlanDay == null || parseInt($scope.ChapterList[i].PlanDay) < 1) $scope.Incomplete++;
                if ($scope.ChapterList[i].MinHour == null || parseInt($scope.ChapterList[i].MinHour) < 1) $scope.Incomplete++;
                if ($scope.ChapterList[i].TestNum == null || parseInt($scope.ChapterList[i].TestNum) < 1) $scope.Incomplete++;
            }
            else {                                      //下列项只需检测节
                if ($scope.ChapterList[i].FileNum == null || parseInt($scope.ChapterList[i].FileNum) < 1) $scope.Incomplete++;

            }
            if ($scope.ChapterList[i].Title == null || $scope.ChapterList[i].Title == "") $scope.Incomplete++;
        }

        DataTotal();

    }


    var DataTotal = function ()
    {
        if ($scope.OCMoocFileList == null) {
            var url = moocProviderUrl + "/OCMoocFile_List";
            var param = { OCID: $scope.OCID, ChapterID: -1 }
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.OCMoocFileList = data.d;
                    for (var i = 0; i < $scope.OCMoocFileList.length; i++) {
                        $scope.OCMoocFileList[i].TimelimitMin = round($scope.OCMoocFileList[i].Timelimit / 60, 2);
                    }
                    DataStatistics();
                }
            });
        }
        else {
            DataStatistics();
        }
    }

    var DataStatistics = function () {
        $scope.OCMoocTotal = new Object();
        $scope.OCMoocTotal.FileNum = 0;
        $scope.OCMoocTotal.VideoNum = 0;
        $scope.OCMoocTotal.TestNum = 0;
        $scope.OCMoocTotal.TopicNum = 0;
        $scope.OCMoocTotal.KenNum = 0;
        $scope.OCMoocTotal.PlanDay = 0;
        $scope.OCMoocTotal.MinHour = 0;

        for (var i = 0; i < $scope.ChapterList.length; i++) {
            if ($scope.ChapterList[i].ParentID == 0) {
                var Chapter = $scope.ChapterList[i];
                Chapter.FileNumTotal = 0;
                Chapter.VideoNumTotal = 0;
                Chapter.TestNumTotal = Chapter.TestNum == ""? 0 : Chapter.TestNum; //章上面也有测试，其它为0是章上面本身不绑定那些信息
                Chapter.KenNumTotal = Chapter.KenNum == "" ? 0 : Chapter.KenNum;

                for (var j = 0; j < $scope.ChapterList.length; j++) {
                    if ($scope.ChapterList[j].ParentID == Chapter.ChapterID) {
                        var ChapterChild=$scope.ChapterList[j];
                        Chapter.TestNumTotal += ChapterChild.TestNum;  //章测试统计
                        Chapter.KenNumTotal += ChapterChild.KenNum;
                        for (var k = 0; k < $scope.OCMoocFileList.length; k++) {
                            if ($scope.OCMoocFileList[k].ChapterID == ChapterChild.ChapterID)
                            {
                                if ($scope.OCMoocFileList[k].FileType == 1) {   //视频文件
                                    Chapter.VideoNumTotal++;
                                }
                                else {      //其它类型
                                    Chapter.FileNumTotal++;
                                }
                            }
                        }
                        
                    }
                }
                $scope.OCMoocTotal.FileNum += Chapter.FileNumTotal;
                $scope.OCMoocTotal.VideoNum += Chapter.VideoNumTotal;
                $scope.OCMoocTotal.TestNum += Chapter.TestNumTotal;
                $scope.OCMoocTotal.TopicNum += Chapter.TopicNum;
                $scope.OCMoocTotal.KenNum += Chapter.KenNumTotal;
                $scope.OCMoocTotal.PlanDay += Chapter.PlanDay;
                $scope.OCMoocTotal.MinHour += Chapter.MinHour;
            }
        }
    }


    //查看结果菜单
    $scope.ViewCheckResult = function () {
        $('#div_CompleteCheck').modal('show');

    }




    var OCMoocInfo_Get = function () { //绑定MOOC基本信息
        var url = moocProviderUrl + "/OCMoocInfo_Get";
        var param = { OCID: $scope.OCID }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.OcMooc = data.d.OcMooc;
                $scope.ChapterList = data.d.ChapterList;
            }
        });
    }



    //显示章节讨论详细
    $scope.PartDiscussion = function (Chapter) {
        if ($scope.OCMoocLiveDiscussList == null) //首次进入，加载讨论列表
        {
            var url = moocProviderUrl + "/OCMoocLiveDiscuss_List";
            var param = { ChapterID: -1 }
            $scope.baseService.post(url, param, function (data) {
                if (data.d != null) {
                    $scope.OCMoocLiveDiscussList = data.d;
                }
            });
        }
        $scope.Chapter = Chapter;
        $('#div_Discussion').modal('show');
    }



    //保存章节讨论新增
    $scope.OCMoocLiveDiscuss_Add = function () {
        $scope.OCMoocLiveInit++;
        var OCMoocLiveModel = new Object();
        OCMoocLiveModel.MoocLiveID = -1;
        OCMoocLiveModel.OCID = $scope.OCID;
        OCMoocLiveModel.TopicID = -1;
        OCMoocLiveModel.ForumTitle = "";
        OCMoocLiveModel.ChapterID = $scope.Chapter.ChapterID;
        OCMoocLiveModel.IsMust = 0;
        OCMoocLiveModel.IsDiscuss = 1;
        OCMoocLiveModel.active = true;
        OCMoocLiveModel.edit = true;//edit=1 则为编辑状态
        OCMoocLiveModel.count = $scope.OCMoocLiveInit; //计数，删除用
        $scope.OCMoocLiveDiscussList[$scope.OCMoocLiveDiscussList.length] = OCMoocLiveModel;
    }

    //章节讨论更新
    $scope.OCMoocLiveDiscuss_Upd = function () {
        for (var i = 0; i < $scope.OCMoocLiveDiscussList.length; i++) {
            if ($scope.OCMoocLiveDiscussList[i].ChapterID == $scope.Chapter.ChapterID) //保存当前章节的
            {
                if ($scope.OCMoocLiveDiscussList[i].edit) {
                    var url = moocProviderUrl + "/OCMoocLiveDiscuss_Edit";
                    var param = { model: $scope.OCMoocLiveDiscussList[i] }
                    $scope.baseService.post(url, param, function (data) {
                        if (data.d != null) {
                            $scope.OCMoocLiveDiscussList[i].edit = false;
                            $scope.OCMoocLiveDiscussList[i].MoocLiveID = data.d.MoocLiveID;
                        }
                    });
                }
            }
        }
        $('#div_Discussion').modal('hide');
    }

    //章节讨论删除
    $scope.OCMoocLiveDiscuss_Del = function (OCMoocLive) {
        if (OCMoocLive.MoocLiveID > 0)  //直接从库里删除
        {

        }
        else if (OCMoocLive.MoocLiveID == -1) //数据还没入库,直接移除就可以 
        {
            for (var i = 0; i < $scope.OCMoocLiveDiscussList.length; i++) {
                if ($scope.OCMoocLiveDiscussList[i].count == OCMoocLive.count) { //MoocLiveID=-1 数据还没入库,直接移除就可以
                    $scope.OCMoocLiveDiscussList.splice(i, 1);
                }
            }
        }
    }


    //显示知识点
    $scope.PartKnowledge = function (Chapter) {
        $scope.Chapter = Chapter;
        $('#div_KnowledgePoint').modal('show');
    }

    //章节启用状态
    $scope.PartStatus = function (Chapter) {
        $scope.Chapter = Chapter;
        if (Chapter.MoocStatus == 0) { //需要启用该节
            //后面的话加一些判断:是否符合启用条件
            Chapter.MoocStatus = 1;
        }
        else if (Chapter.MoocStatus == 1) //需要禁用该节
        {
            Chapter.MoocStatus = 0;
        }
        //$scope.Chapter_Upd();
    }



    //加载课程资料列表
    $scope.PartFile = function (Chapter) {
        if ($scope.OCMoocFileList == null) //首次进入，加载课程资料列表
        {
            OCMoocFile_List();
        }
        $scope.Chapter = Chapter;
        $('#ChapterFile').modal('show');
    }

    //置资料必读选读权限
    $scope.OCMoocFileStatus = function (OCMoocFile) {
        if (OCMoocFile.IsMust == 0) {
            OCMoocFile.IsMust = 1;
        }
        else if (OCMoocFile.IsMust == 1) {
            OCMoocFile.IsMust = 0;
        }
    }



    //显示章详细信息
    $scope.ChapterDetail = function (Chapter) {
        $scope.Chapter = Chapter;
        $('#div_Chapter').modal('show');
    }

    //显示节详细信息
    $scope.PartDetail = function (Chapter) {
        $scope.Chapter = Chapter;
        $('#div_Part').modal('show');
    }

    //章节保存
    $scope.Chapter_Upd = function () {
        var url = moocProviderUrl + "/Chapter_Upd";
        var param = { model: $scope.Chapter }
        $scope.baseService.post(url, param, function (data) {

        });
    }



    //章节标题保存
    $scope.Chapter_Title_Upd = function (Chapter) {
        Chapter.active = false;
        $scope.Chapter = Chapter;
        $scope.Chapter_Upd();
    }


    var Chapter_Enter_Add = function () //按回车键时候触发
    {
        Chapter_Add($scope.ChapterEnter.ParentID);
    }

    var Chapter_Add = function (ParentID) {
        var ChapterModel = new Object();
        ChapterModel.OCID = $scope.OCID;
        ChapterModel.ChapterID = -1;
        ChapterModel.Title = "";
        ChapterModel.Brief = "";
        ChapterModel.PlanDay = 0;
        ChapterModel.MinHour = 0;
        ChapterModel.MoocStatus = 0;
        ChapterModel.ParentID = ParentID;

        var url = moocProviderUrl + "/Chapter_Add";
        var param = { model: ChapterModel }
        var promise = $scope.baseService.postPromise(url, param);
        promise.then(function (data) {  // 调用承诺API获取数据 .resolve  
            Chapter_Add_Back(data.d);
        }).then(function () {  // 调用承诺API获取数据 .resolve  

        });
    }

    var Chapter_Add_Back = function (ChapterModel) {
        if (ChapterModel != null) {
            $scope.ChapterList[$scope.ChapterList.length] = ChapterModel;
            if (ChapterModel.ParentID == 0) {
                Chapter_Add(ChapterModel.ChapterID)
            }
        }
    }

    $scope.Chapter_MouseOver = function (Chapter) //按回车键时候触发
    {
        $scope.ChapterEnter = Chapter;
        Chapter.ShowOperation = true;
    }

    $scope.Chapter_MouseLeave = function (Chapter) //按回车键时候触发
    {
        //$scope.ChapterEnter = Chapter;
        Chapter.ShowOperation = false;
    }

    //章节删除
    $scope.Chapter_Del = function (Chapter) {
        if (confirm("是否确定删除？")) {
            var url = moocProviderUrl + "/Chapter_Del";
            var param = { model: Chapter }
            $scope.baseService.post(url, param, function (data) {
                for (var i = 0; i < $scope.ChapterList.length; i++) {
                    if ($scope.ChapterList[i].ChapterID == Chapter.ChapterID) {
                        $scope.ChapterList.splice(i, 1);
                    }
                }
            });
        }
    }

    //章节移动 type 1:上移 2:下移
    $scope.Chapter_Move = function (Chapter, type) {
        var ChapterTemp = new Object();
        var num = 0;
        if (type == 1) {
            var ChapterPre = null;
            for (var i = 0; i < $scope.ChapterList.length; i++) {
                if ($scope.ChapterList[i].ParentID == Chapter.ParentID) {
                    if ($scope.ChapterList[i].Orde < Chapter.Orde) {
                        if (ChapterPre == null || $scope.ChapterList[i].Orde > ChapterPre.Orde) {
                            ChapterPre = $scope.ChapterList[i];
                            num++;
                        }
                    }
                }
            }
            if (num > 0)     //存在它前面的章节
            {
                if (Chapter.ParentID == 0)    //章移动,章下面的节Orde也要改变
                {
                    var diffNum = Chapter.Orde - ChapterPre.Orde;
                    ChapterTemp.Orde = Chapter.Orde;
                    Chapter.Orde = ChapterPre.Orde;
                    ChapterPre.Orde = ChapterTemp.Orde;
                    for (var i = 0; i < $scope.ChapterList.length; i++) {
                        if ($scope.ChapterList[i].ParentID == Chapter.ChapterID) {
                            $scope.ChapterList[i].Orde = $scope.ChapterList[i].Orde - diffNum;
                        }
                        if ($scope.ChapterList[i].ParentID == ChapterPre.ChapterID) {
                            $scope.ChapterList[i].Orde = $scope.ChapterList[i].Orde + diffNum;
                        }
                    }
                }
                else {
                    ChapterTemp.Orde = Chapter.Orde;
                    Chapter.Orde = ChapterPre.Orde;
                    ChapterPre.Orde = ChapterTemp.Orde;
                }
            }
        }
        else if (type == 2) {
            var ChapterNext = null;
            for (var i = 0; i < $scope.ChapterList.length; i++) {
                if ($scope.ChapterList[i].ParentID == Chapter.ParentID) {
                    if ($scope.ChapterList[i].Orde > Chapter.Orde) {
                        if (ChapterNext == null || $scope.ChapterList[i].Orde < ChapterNext.Orde) {
                            ChapterNext = $scope.ChapterList[i];
                            num++;
                        }
                    }
                }
            }
            if (num > 0)     //存在它后面的章节
            {
                if (Chapter.ParentID == 0)    //章移动,章下面的节Orde也要改变
                {
                    var diffNum = ChapterNext.Orde - Chapter.Orde;
                    ChapterTemp.Orde = Chapter.Orde;
                    Chapter.Orde = ChapterNext.Orde;
                    ChapterNext.Orde = ChapterTemp.Orde;
                    for (var i = 0; i < $scope.ChapterList.length; i++) {
                        if ($scope.ChapterList[i].ParentID == Chapter.ChapterID) {
                            $scope.ChapterList[i].Orde = $scope.ChapterList[i].Orde + diffNum;
                        }
                        if ($scope.ChapterList[i].ParentID == ChapterNext.ChapterID) {
                            $scope.ChapterList[i].Orde = $scope.ChapterList[i].Orde - diffNum;
                        }
                    }
                }
                else {
                    ChapterTemp.Orde = Chapter.Orde;
                    Chapter.Orde = ChapterNext.Orde;
                    ChapterNext.Orde = ChapterTemp.Orde
                }
            }

        }
    }

    //切换课程目-录课程资料
    $scope.ChangeType = function (type) {
        $scope.type = type;
        if (type == 2 && $scope.OCMoocFileList == null) //首次进入，加载课程资料列表
        {
            OCMoocFile_List();
        }
    }

    var OCMoocFile_List = function () {     //加载文件列表
        var url = moocProviderUrl + "/OCMoocFile_List";
        var param = { OCID: $scope.OCID, ChapterID: -1 }
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                $scope.OCMoocFileList = data.d;
                for (var i = 0; i < $scope.OCMoocFileList.length; i++) {
                    $scope.OCMoocFileList[i].TimelimitMin = round($scope.OCMoocFileList[i].Timelimit / 60, 2);
                }
            }
        });
    }

    var round = function (v, e) {
        var t = 1;
        for (; e > 0; t *= 10, e--);
        for (; e < 0; t /= 10, e++);
        return Math.round(v * t) / t;

    }

    //创建见面课
    $scope.OCMoocOffline_Add = function () {
        var OCMoocOfflineModel = new Object();
        OCMoocOfflineModel.MoocOfflineID = -1;
        OCMoocOfflineModel.OCID = 1;
        OCMoocOfflineModel.Title = "";
        OCMoocOfflineModel.TaskType = "";
        OCMoocOfflineModel.Hours = 0;
        OCMoocOfflineModel.UserName = "";
        OCMoocOfflineModel.ChapterID = -1;
        OCMoocOfflineModel.Purpose = "";
        OCMoocOfflineModel.Points = "";
        OCMoocOfflineModel.Grouping = "";
        OCMoocOfflineModel.Score = "";
        OCMoocOfflineModel.Resource = "";
        OCMoocOfflineModel.Assess = "";
        $scope.OCMoocOffline = OCMoocOfflineModel;
        $scope.Data = 1;
        document.getElementById("frmoEditor1").contentWindow.setHTML("");
        $('#div_OCMoocOffline').modal('show');
    }

    //删除见面课
    $scope.OCMoocOffline_Del = function (OCMoocOffline) {

        //var url = moocProviderUrl + "/OCMoocOffline_Del";
        //var param = { model: $scope.OCMoocOffline }
        //$scope.baseService.post(url, param, function (data) {

        //});
        for (var i = 0; i < $scope.OCMoocOfflineList.length; i++) {
            if ($scope.OCMoocOfflineList[i].MoocOfflineID == OCMoocOffline.MoocOfflineID) {
                $scope.OCMoocOfflineList.splice(i, 1);
            }
        }
    }

    //编辑面授课
    $scope.OCMoocOffline_Edit = function (OCMoocOffline) {
        $scope.OCMoocOffline = OCMoocOffline;
        $('#div_OCMoocOffline').modal('show');

        $scope.Data = 1;
        document.getElementById("frmoEditor1").contentWindow.setHTML($scope.OCMoocOffline.Purpose);
    }

    //更新面授课
    $scope.OCMoocOffline_Upd = function () {
        //var url = moocProviderUrl + "/OCMoocOffline_Upd";
        //var param = { model: $scope.OCMoocOffline }
        //$scope.baseService.post(url, param, function (data) {

        //});

        $('#div_OCMoocOffline').modal('hide');
    }

    //面授课编辑器内容绑定
    $scope.OCMoocOffline_Bind = function (DataType) {
        var html = document.getElementById("frmoEditor1").contentWindow.getHTML();
        var html2 = "";
        if ($scope.Data == 1) {
            $scope.OCMoocOffline.Purpose = html;
        }
        else if ($scope.Data == 2) {
            $scope.OCMoocOffline.Points = html;
        }
        else if ($scope.Data == 3) {
            $scope.OCMoocOffline.Grouping = html;
        }
        else if ($scope.Data == 4) {
            $scope.OCMoocOffline.Score = html;
        }
        else if ($scope.Data == 5) {
            $scope.OCMoocOffline.Resource = html;
        }
        else if ($scope.Data == 6) {
            $scope.OCMoocOffline.Assess = html;
        }
        $scope.Data = DataType;
        if (DataType == 1) {
            html2 = $scope.OCMoocOffline.Purpose;
        }
        else if (DataType == 2) {
            html2 = $scope.OCMoocOffline.Points;
        }
        else if (DataType == 3) {
            html2 = $scope.OCMoocOffline.Grouping;
        }
        else if (DataType == 4) {
            html2 = $scope.OCMoocOffline.Score;
        }
        else if (DataType == 5) {
            html2 = $scope.OCMoocOffline.Resource;
        }
        else if (DataType == 6) {
            html2 = $scope.OCMoocOffline.Assess;
        }
        if (html2 == null) {
            document.getElementById("frmoEditor1").contentWindow.setHTML("");
        }
        else {
            document.getElementById("frmoEditor1").contentWindow.setHTML(html2);
        }
    }


    $scope.TeachingPlan = function () {
        $('#div_TeachingPlan').modal('show');
        DataTotal();
    }


    //=======================================↑方法 ↓过滤器、指令
    //过滤出当前章的文件列表
    $scope.fileFiter = function (e) {
        if ($scope.Chapter == null) {
            return;
        }
        return e.ChapterID == $scope.Chapter.ChapterID;
    }



    $scope.$on('ngColumnGet', function (ngRepeatFinishedEvent) {
        //$('.first_chapter').hover(function () {
        //    $(this).find('.operation_btn').show();
        //    $(this).css('background', '#f2f2f2');
        //}, function () {
        //    $(this).find('.operation_btn').hide();
        //    $(this).css('background', '#fff');
        //})
    });

    $scope.$on('ngOCMoocOfflineGet', function (ngRepeatFinishedEvent) {
        $('.group_discuss a').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $(this).parents('.chapt_box').next().slideDown();
                $(this).text('[收起]');
            } else {
                $(this).removeClass('click');
                $(this).parents('.chapt_box').next().slideUp();
                $(this).text('[展开]');
            }
        })
    });
    OCMoocInfo_Get();


}]);



//从我的资料库选择
moocModule.controller('MyResourceController', ['$scope', '$state', 'moocProviderUrl', function ($scope, $state, moocProviderUrl) {

    $scope.OCFileList == null;  //课程资料文件列表
    $scope.MyFileList == null;  //个人资料文件列表
    $scope.KeyWords = "";
    $scope.OCID = 1;
    $scope.PageIndex = 1;
    $scope.PageSize = 2;
    $scope.PageSum = 0;
    $scope.count = 0;   //判断是否是首次加载

    $scope.OCFile_List = function () {
        var FileMode = new Object();
        FileMode.OCID = $scope.OCID;
        FileMode.FileTitle = $scope.KeyWords;
        FileMode.UploadTime = '2000-01-01';
        FileMode.FolderID = -1; //获取全部目录的文件
        FileMode.FileType = -1; //获取全部类型的图片
        FileMode.ShareRange = -1; //共享条件没有限制
        FileMode.ShareRange = -1; //共享条件没有限制
        FileMode.CreateUserID = 1;//**先伪造个创建者ID
        var url = moocProviderUrl + "/File_Search";
        var param = { file: FileMode, PageSize: $scope.PageSize, PageIndex: $scope.PageIndex }
        var fileList = $scope.baseService.postPromise(url, param);
        $scope.baseService.runPromises({
            fileList: fileList
        }, function (data) {
            if (data.fileList.d != null) {
                $scope.OCFileList = data.fileList.d;
                $scope.PageSum = Math.ceil($scope.OCFileList[0].RowsCount / $scope.PageSize);
                if ($scope.count == 0) {        //分页控件只加载一次
                    FilePage($scope.PageSum)
                }
            }
        });
    }
    var FilePage = function (PageSum) {
        laypage({
            cont: $('#page'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: PageSum, //总页数
            skip: true, //是否开启跳页
            skin: '#AF0000', //选中的颜色
            groups: 5,//连续显示分页数
            first: false, //若不显示，设置false即可  
            last: false, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                $scope.PageIndex = e.curr;
                if ($scope.count > 0) {
                    $scope.OCFile_List();
                }
                $scope.count++;
            }
        });
    }
    $scope.OCFile_List();

}]);


moocModule.filter('MoocFilter', function () {
    return function (arr, ope, num) {
        if (arr == null || arr == "") {
            return;
        }
        return arr.filter(function (item) {
            if (ope == '>') {
                return item.ParentID > num;
            }
            if (ope == '=') {
                return item.ParentID == num;
            }
            if (ope == '<') {
                return item.ParentID < num;
            }
        });
    }
});

moocModule.filter('MoocOfflineFilter', function () {
    return function (arr, ope, num) {
        if (arr == null || arr == "") {
            return;
        }
        return arr.filter(function (item) {
            if (ope == '>') {
                return item.ChapterID > num;
            }
            if (ope == '=') {
                return item.ChapterID == num;
            }
            if (ope == '<') {
                return item.ChapterID < num;
            }
        });
    }
});

moocModule.directive('onChapterGet', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngColumnGet');
                });
            }
        }
    };
});

moocModule.directive('onOfflineGet', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngOCMoocOfflineGet');
                });
            }
        }
    };
});

moocModule.directive('focusMe', ['$timeout', function ($timeout) {
    return {
        scope: { trigger: '@focusMe' },
        link: function (scope, element) {
            scope.$watch('trigger', function (value) {
                if (value) {
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
        }
    };
}]);

//moocModule.directive('contenteditable', function () {
//    return function (scope, element) {
//        element[0].focus();
//    };
//});

moocModule.directive('setFocus', function () {
    return function (scope, element) {
        element[0].focus();
    };
});



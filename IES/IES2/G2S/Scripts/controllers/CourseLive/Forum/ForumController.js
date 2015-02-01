var forumModule = angular.module('app.forumm', []);


// index/topiclist
forumModule.controller('ForumCtrl', ['$scope', '$state', 'forumProviderUrl', function ($scope, $state, forumProviderUrl) {
    $scope.Forum_HotUser_Item = [];  //牛人
    $scope.ForumType_Item = [];//论坛版块
    $scope.Title = "创建讨论模块";
    $scope.flag = false;
    $scope.selected = 0;
    $scope.OCClass_Dropdown_Item = [];  //网络教学班
    $scope.ForumTopic_Active_Item = [];   //活跃
    $scope.ForumTopic_Item = []; //论题
    $scope.ForumMy = {
        TopicID: 3,
        ResponseID: 0,// 0表示为主题加赞 ,否则为回复点赞 
        UserID: 4
    };
    $scope.active = {
        All: true,
        Essence: false,
        MyStart: false,
        MyJoin: false
    }

    $scope.ForumType = {
        OCID: 1,
        CourseID: 1,
        Title: "",
        IsEssence: false,
        Brief: "",
        IsPublic: false,
        UserID: 4,
        TeachingClassID: 2
    };

    $scope.ForumTopic = {
        SearchKey: '',
        OCID: 1,
        UserID: 4,
        ForumTypeID: 0,
        IsEssence: false,
        IsMyStart: false,
        IsMyJoin: false,
        ResponseStatus: 0,
        Order: 0
    };

    $scope.activeInit = function () {
        $scope.active = {
            All: false,
            Essence: false,
            MyStart: false,
            MyJoin: false
        }
    }
    //牛人列表
    $scope.Forum_HotUser_List = function () {
        var url = forumProviderUrl + "/Forum_HotUser_List";
        var para = { OCID: 1, Top: 10 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.Forum_HotUser_Item = data.d;
            }
        });
    }

    //版块列表
    $scope.ForumType_List = function () {
        var url = forumProviderUrl + "/ForumType_List";
        var para = { ft: { OCID: 1, UserID: 4 } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType_Item = data.d;
            }
        });
    }
    //新增版块
    $scope.ForumType_ADD = function () {
        if (!$scope.ForumType.IsPublic) {
            if ($scope.selected == 0 || $scope.selected == null || $scope.selected == "") {
                alert("请选择版块!");
                return;
            }
        } else {
            $scope.selected = 0;
        }
        $scope.ForumType.TeachingClassID = $scope.selected;
        var url = forumProviderUrl + "/ForumType_ADD";
        var para = { model: $scope.ForumType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType = {};//清空
                $scope.ForumType_Item.push(data.d);
            }
        });
    }

    //删除讨论模块
    $scope.ForumType_Del = function (m) {
        var url = forumProviderUrl + "/ForumType_Del";
        var para = { model: m };
        if (!confirm("确定要删除吗?")) { return; }
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null || data.d === false) {

            } else {
                //从数组中移除
                for (var i = 0; i < $scope.ForumType_Item.length; i++) {
                    if ($scope.ForumType_Item[i].ForumTypeID == m.ForumTypeID) {
                        $scope.ForumType_Item.splice(i, 1);
                    }
                }
            }
        });
    }

    //编辑讨论模块
    $scope.ForumType_Upd = function () {
        if (!$scope.ForumType.IsPublic) {
            if ($scope.selected == 0 || $scope.selected == null || $scope.selected == "") {
                alert("请选择版块!");
                return;
            }
        } else {
            $scope.selected = 0;
        }
        $scope.ForumType.TeachingClassID = $scope.selected;
        var url = forumProviderUrl + "/ForumType_Upd";
        var para = { model: $scope.ForumType };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null || data.d === false) {
                $scope.ForumType_List();
            } else {
                alert("编辑成功!");
            }
        });
    }

    //编辑或新增
    $scope.AddOrEdit = function () {
        $scope.flag ? $scope.ForumType_ADD() : $scope.ForumType_Upd();
    }
    //显示 创建/编辑讨论模块
    $scope.EditOrAdd = function (flag, m) {
        $scope.OCClass_Dropdown_List();  //教学班下拉列表
        $scope.Title = flag ? "创建讨论模块" : "编辑讨论模块";
        $scope.flag = flag ? true : false;
        $scope.selected = 0;
        if (flag) {
            $scope.ForumType = {
                OCID: 1,
                CourseID: 1,
                Title: "",
                IsEssence: false,
                Brief: "",
                IsPublic: false,
                UserID: 4
            };
        } else {
            $scope.ForumType = m;
            $scope.selected = m.TeachingClassID;
        }

    }

    //教学班下拉列表
    $scope.OCClass_Dropdown_List = function () {
        var url = forumProviderUrl + "/OCClass_Dropdown_List";
        var para = { OCID: 1 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.OCClass_Dropdown_Item = data.d;
            }
        });
    }

    //活跃论题列表
    $scope.ForumTopic_Active_List = function () {
        var url = forumProviderUrl + "/ForumTopic_Active_List";
        var para = { OCID: 1, UserID: 4, Top: 5 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumTopic_Active_Item = data.d;
            }
        });
    }

    //话题列表
    $scope.ForumTopic_Search = function () {
        var url = forumProviderUrl + "/ForumTopic_Search";
        var para = { model: $scope.ForumTopic, PageIndex: 1, PageSize: 20 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumTopic_Item = data.d;
            }
        });
    }
    //$scope.DateMoRen = (new Date()).getFullYear() + "-" + (new Date()).getMonth() + 1 + "-" + (new Date()).getDay();
    //laydate({
    //    elem: '#date', //目标元素。由于laydate.js封装了一个轻量级的选择器引擎，因此elem还允许你传入class、tag但必须按照这种方式 '#id .class'
    //    event: 'focus', //响应事件。如果没有传入event，则按照默认的click
    //    festival: true, //显示节日
    //    istime: true,
    //    format: "YYYY-MM-DD" //日期格式
    //});
    //laydate.skin('molv')//墨绿皮肤
    $scope.AllTopic = function () {
        $scope.ForumTopic = {
            SearchKey: '',
            OCID: 1,
            UserID: 4,
            ForumTypeID: 0,
            IsEssence: false,
            IsMyStart: false,
            IsMyJoin: false,
            ResponseStatus: 0,
            Order: 0
        };
    }


    //点赞或取消
    $scope.ForumMy_IsGood_Upd = function (m) {
        m.IsGood = !m.IsGood
        $scope.ForumMy.TopicID = m.TopicID;
        $scope.ForumMy.ResponseID = (m.ResponseID == null || m.ResponseID == "" || m.ResponseID == undefined) ? 0 : m.ResponseID;
        var url = forumProviderUrl + "/ForumMy_IsGood_Upd";
        var para = { model: $scope.ForumMy };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
            }
        });
    }

    //版块的详细信息
    $scope.ForumTypeInfo_Get = function () {
        var url = forumProviderUrl + "/ForumTypeInfo_Get";
        var para = { model: { ForumTypeID: 6 } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
                $scope.ForumType = data.d.forumtype;
            }
        });
    }

    $scope.ForumTypeInfo_Get();
    $scope.Forum_HotUser_List();
    $scope.ForumType_List();
    $scope.ForumTopic_Active_List();
    $scope.ForumTopic_Search();
}]);


//topicadd
forumModule.controller('ForumTopicCtrl', ['$scope', '$state', 'forumProviderUrl', function ($scope, $state, forumProviderUrl) {
    $scope.ForumType_Item = [];//论坛版块
    $scope.selected = "";
    //标签
    $scope.Tags = {
        num: 1,
        Tags: [
            { show: true, text: '' },
            { show: false, text: '' },
            { show: false, text: '' },
            { show: false, text: '' },
            { show: false, text: '' }
        ]
    };
    $scope.ForumTopic = {
        SearchKey: '',
        OCID: 1,
        ForumTypeID: 0, // 如果是小组讨论中发帖，则ForumTypeID = 0 
        IsEssence: false,
        IsMyStart: false,
        IsMyJoin: false,
        ResponseStatus: 0,
        Order: 0,
        //上面是搜索条件
        Title: '',
        Conten: '',
        Tags: '',
        ForumTypeID: 1,
        UserID: 4,
        UserName: '小花',
        TopicID: 0,
        CourseID: 0,
        GroupTaskID: 0, // 小组讨论的编号，如果不是小组讨论中发帖，该编号设置为0
        TopicType: 0  // 发帖的主题类型；  PBL中发帖，该编号设置为3；
    };

    //新增标签
    $scope.SowTags = function (num) {
        if (num >= 5) {
            alert("最多5个标签");
            return;
        }
        $scope.Tags.Tags[num].show = true;
        $scope.Tags.num++;
    }

    //版块列表
    $scope.ForumType_List = function () {
        var url = forumProviderUrl + "/ForumType_List";
        var para = { ft: { OCID: 1, UserID: 4 } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType_Item = data.d;
            }
        });
    }

    //发帖
    $scope.ForumTopic_Add = function () {
        $scope.ForumTopic.ForumTypeID = $scope.selected;
        $scope.ForumTopic.Tags = '';
        for (var i = 0; i < $scope.Tags.Tags.length; i++) {
            if ($scope.Tags.Tags[i].show && $scope.Tags.Tags[i].text != "") {
                $scope.ForumTopic.Tags += $scope.Tags.Tags[i].text + "*#0086#"
            }
        }
        $scope.ForumTopic.UpdateTime = new Date();
        $scope.ForumTopic.LastUpdateTime = new Date();
        $scope.ForumTopic.Conten = document.getElementById("frmoEditor1").contentWindow.getHTML();
        var url = forumProviderUrl + "/ForumTopic_Add";
        var para = { model: $scope.ForumTopic };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                alert("新增成功!");
            }
        });
    }

    //编辑帖子
    $scope.ForumTopic_Upd = function () {
        $scope.ForumTopic.ForumTypeID = $scope.selected;
        $scope.ForumTopic.Tags = '';
        for (var i = 0; i < $scope.Tags.Tags.length; i++) {
            if ($scope.Tags.Tags[i].show && $scope.Tags.Tags[i].text != "") {
                $scope.ForumTopic.Tags += $scope.Tags.Tags[i].text + "*#0086#"
            }
        }
        $scope.ForumTopic.UpdateTime = new Date();
        $scope.ForumTopic.LastUpdateTime = new Date();
        $scope.ForumTopic.Conten = document.getElementById("frmoEditor1").contentWindow.getHTML();
        var url = forumProviderUrl + "/ForumTopic_Upd";
        var para = { model: $scope.ForumTopic };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                alert("编辑成功!");
            }
        });
    }
    //论坛主题的详细信息
    $scope.ForumTopic_Get = function () {
        var url = forumProviderUrl + "/ForumTopic_Get";
        var para = { TopicID: 7 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumTopic = data.d;

                var tags = $scope.ForumTopic.Tags.split("*#0086#");
                for (var i = 0; i < tags.length - 1; i++) {
                    $scope.Tags.Tags[i].show = true;
                    $scope.Tags.Tags[i].text = tags[i];
                }
                $scope.selected = $scope.ForumTopic.ForumTypeID;
            }
        });
    }
    if (true) {
        $scope.ForumTopic_Get();
    }
    $scope.ForumType_List();
}]);


//topicdetail
forumModule.controller('ForumTopicDetialCtrl', ['$scope', '$state', 'forumProviderUrl', function ($scope, $state, forumProviderUrl) {
    $scope.ForumType_Item = [];//论坛版块
    $scope.ForumResponseInfo_Item = [];  //回复列表
    $scope.selected = '';
    $scope.UserInfo = { UserName: "徐卫", UserID: 4, OCID: 1 };
    $scope.ForumMy = {
        TopicID: 3,
        ResponseID: 0,// 0表示为主题加赞 ,否则为回复点赞 
        UserID: 4
    };
    $scope.ForumResponse = {
        ResponseID: 0,
        TopicID: 3,
        ParentID: 0,
        Conten: "",
        UserID: 4,
        UserName: "小花"
    }
    $scope.ForumTopic = {
        SearchKey: '',
        OCID: 1,
        ForumTypeID: 0, // 如果是小组讨论中发帖，则ForumTypeID = 0 
        IsEssence: false,
        IsMyStart: false,
        IsMyJoin: false,
        ResponseStatus: 0,
        Order: 0,
        //上面是搜索条件
        IsTop: false,
        Title: '',
        Conten: '12345,上山打老虎!',
        Tags: '',
        ForumTypeID: 1,
        UserID: 4,
        UserName: '攻城狮',
        TopicID: 0,
        CourseID: 0,
        GroupTaskID: 0, // 小组讨论的编号，如果不是小组讨论中发帖，该编号设置为0
        TopicType: 0  // 发帖的主题类型；  PBL中发帖，该编号设置为3；
    };

    //版块列表
    $scope.ForumType_List = function () {
        var url = forumProviderUrl + "/ForumType_List";
        var para = { ft: { OCID: 1, UserID: 4 } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumType_Item = data.d;
            }
        });
    }

    //获取主题回复列表及关注的列表
    $scope.ForumResponseInfo_List = function () {
        var url = forumProviderUrl + "/ForumResponseInfo_List";
        var para = { model: { TopicID: 3, UserID: 4 }, PageIndex: 1, PageSize: 10 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumResponseInfo_Item = data.d.forumresponselist;
            }
        });
    }
    //论坛主题的详细信息
    $scope.ForumTopic_Get = function () {
        var url = forumProviderUrl + "/ForumTopic_Get";
        var para = { TopicID: 17 };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumTopic = data.d;
            }
        });
    }
    //添加回复
    $scope.ForumResponse_ADD = function () {
        debugger;
        if ($scope.ForumResponse.ParentID == 0) {
            $scope.ForumResponse.Conten = document.getElementById("frmoEditor1").contentWindow.getHTML();
            if ($scope.ForumResponse.Conten == "") {
                alert("请输入内容！");
                return;
            }
        }
        var url = forumProviderUrl + "/ForumResponse_ADD";
        var para = { model: { ParentID: $scope.ForumResponse.ParentID, Conten: $scope.ForumResponse.Conten, UserName: $scope.UserInfo.UserName, UserID: $scope.UserInfo.UserID } };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumResponseInfo_Item.push(data.d);
                $scope.ForumResponse.Conten = '';
                document.getElementById("frmoEditor1").contentWindow.setHTML("");
            }
        });
    }

    //初始化回复
    $scope.ResponseInit = function (m) {
        if (m == 0) {
            //alert(m);
            $scope.ForumResponse.ParentID = 0;
        } else {
            $scope.ForumResponse.UserName = m.UserName;
            $scope.ForumResponse.ParentID = m.ResponseID;
        }
    }

    //点赞或取消
    $scope.ForumMy_IsGood_Upd = function (m) {
        m.IsGood = !m.IsGood
        $scope.ForumMy.TopicID = m.TopicID;
        $scope.ForumMy.ResponseID = (m.ResponseID == null || m.ResponseID == "" || m.ResponseID == undefined) ? 0 : m.ResponseID;
        var url = forumProviderUrl + "/ForumMy_IsGood_Upd";
        var para = { model: $scope.ForumMy };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
            }
        });
    }

    //置顶或取消置顶
    $scope.ForumTopic_IsTop_Upd = function () {
        $scope.ForumTopic.IsTop = !$scope.ForumTopic.IsTop;
        var url = forumProviderUrl + "/ForumTopic_IsTop_Upd";
        var para = { TopicID: $scope.ForumTopic.TopicID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
            }
        });
    }

    //设置或取消精华 
    $scope.ForumTopic_IsEssence_Upd = function () {
        $scope.ForumTopic.IsEssence = !$scope.ForumTopic.IsEssence;
        var url = forumProviderUrl + "/ForumTopic_IsEssence_Upd";
        var para = { TopicID: $scope.ForumTopic.TopicID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
            }
        });
    }
    //移动版块
    $scope.ForumTopic_ForumTypeID_Upd = function () {
        if ($scope.selected == null || $scope.selected == "") {
            alert("请选择版块!");
            return;
        }
        var url = forumProviderUrl + "/ForumTopic_ForumTypeID_Upd";
        var para = { TopicID: $scope.ForumTopic.TopicID, ForumTypeID: $scope.selected.ForumTypeID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
            }
        });
    }

    //删除帖子
    $scope.ForumTopic_Del = function () {

        if (!confirm("确定删除吗?")) {
            return;
        }
        var url = forumProviderUrl + "/ForumTopic_Del";
        var para = { TopicID: $scope.ForumTopic.TopicID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
                $scope.ForumTopic = {};//暂时这样写
            }
        });
    }

    //删除回复
    $scope.ForumResponse_Del = function (m) {

        if (!confirm("确定删除吗?")) {
            return;
        }
        var url = forumProviderUrl + "/ForumResponse_Del";
        var para = { ResponseID: m.ResponseID };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {
            } else {
                for (var i = 0; i < $scope.ForumResponseInfo_Item.length; i++) {
                    if ($scope.ForumResponseInfo_Item[i].ResponseID == m.ResponseID) {
                        $scope.ForumResponseInfo_Item.splice(i, 1);
                    }
                }
            }
        });
    }


    //$scope.ActiveFilter3 = function () {
    //    return function (e) {
    //        e.active = false;
    //        return e;
    //    }
    //}

    //计算时间差
    $scope.getDateDiff = function (desc, dateTimeStamp) {
        if (dateTimeStamp == null || dateTimeStamp == '') {
            return;
        }
        var re = /-?\d+/;
        var m = re.exec(dateTimeStamp);
        if (m < 0) { return ""; }
        var minute = 1000 * 60;
        var hour = minute * 60;
        var day = hour * 24;
        var halfamonth = day * 15;
        var month = day * 30;
        var now = new Date().getTime();
        var diffValue = now - m;

        var monthC = diffValue / month;
        var weekC = diffValue / (7 * day);
        var dayC = diffValue / day;
        var hourC = diffValue / hour;
        var minC = diffValue / minute;

        if (monthC >= 1) {
            result = desc + parseInt(monthC) + "个月前";
        }
        else if (weekC >= 1) {
            result = desc + parseInt(weekC) + "个星期前";
        }
        else if (dayC >= 1) {
            result = desc + parseInt(dayC) + "天前";
        }
        else if (hourC >= 1) {
            result = desc + parseInt(hourC) + "个小时前";
        }
        else if (minC >= 1) {
            result = desc + parseInt(minC) + "分钟前";
        } else {
            result = "刚刚发表";
        }
        return result;
    }
    $scope.ForumType_List();
    $scope.ForumTopic_Get();
    $scope.ForumResponseInfo_List();
}]);

//过滤回复列表
forumModule.filter('responseFilter', function () {
    return function (arr, num) {
        if (arr == null || arr == '') {
            return;
        }
        return arr.filter(function (item) {
            return item.ParentID == num;
        });
    }
});



//过滤版块,给对象添加一个属性
forumModule.filter('addActive', function () {
    return function (v) {
        v.active = false;
        return v;
    }
});

//过滤版块,给对象添加一个属性
forumModule.filter('addResponseShow', function () {
    return function (v) {
        if (v == undefined || v == '' || v == null) {
            return;
        }
        v.ResponseShow = false;
        //alert(v.ResponseShow);
        return v;
    }
});

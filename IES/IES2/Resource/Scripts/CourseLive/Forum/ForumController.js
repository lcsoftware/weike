var forumModule = angular.module('app.forumm', []);

//topicadd
forumModule.controller('ForumTopicCtrl', ['$scope', '$state', 'forumProviderUrl', 'FileUploader', 'uploadfileProviderUrl', function ($scope, $state, forumProviderUrl, FileUploader, uploadfileProviderUrl) {
    $scope.ForumType_Item = [];//论坛版块
    $scope.selected = "";
    $scope.fileList = [];  //文件列表
    $scope.OCID = 1;// $(".exercise_nav_list .active a").attr("href").split("?")[1].split("=")[1];
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
        OCID: $scope.OCID,
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
        var para = { ft: { OCID: $scope.OCID, UserID: 4 } };
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
        var para = {
            model: $scope.ForumTopic
        };
        $scope.baseService.post(url, para, function (data) {
            if (data.d === null) {

            } else {
                $scope.ForumTopic = data.d;
                $scope.File_Upload();
            }
        });
    }

    //关联文件
    $scope.File_Upload = function () {
        var url = uploadfileProviderUrl + "/File_Upload";
        var para = {
            source_id: $scope.ForumTopic.TopicID,
            sourceName:"ForumTopic",
            list: $scope.fileList
        };
        $scope.baseService.post(url, para, function (data) {
            if (data.d != false) {
                alert("发布成功!");
                //window.location.href = "index";
            }
        })
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


    //----------上传文件start--------
    var uploader = $scope.uploader = new FileUploader({
        url: '/DataProvider/FileUpload.ashx'
    });

    // FILTERSE:
    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
        }
    });
    uploader.filters.push({
        name: 'fileSuffix',//过滤器名称 过滤文件类型
        fn: function (item, options) {
            var fileName = item.name;
            var suffix = fileName.substring(fileName.lastIndexOf('.'), fileName.length);
            debugger;
            //return suffix == ".txt" || suffix == ".jpg" || suffix == ".gif";
            return true;
        }
    });
    uploader.filters.push({
        name: 'fileSize',//最大5M
        fn: function (item, options) {
            var fileSize = item.size;
            if (fileSize <= 5 * 1024 * 1024) {
                return true;
            } else {
                alert("文件大小必须小于5M!");
                return false;
            }
        }
    });

    // CALLBACKS
    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
        //console.info('onWhenAddingFileFailed', item, filter, options);
    };
    uploader.onAfterAddingFile = function (fileItem) {
        //console.info('onAfterAddingFile', fileItem);
        //alert("添加文件触发!")
    };
    uploader.onAfterAddingAll = function (addedFileItems) {
        //console.info('onAfterAddingAll', addedFileItems);
        //alert("添加多个文件触发!")
    };
    uploader.onBeforeUploadItem = function (item) {
        //console.info('onBeforeUploadItem', item);
    };
    uploader.onProgressItem = function (fileItem, progress) {
        //console.info('onProgressItem', fileItem, progress);
    };
    uploader.onProgressAll = function (progress) {
        //console.info('onProgressAll', progress);
    };
    uploader.onSuccessItem = function (fileItem, response, status, headers) {
        $scope.fileList.push(response[0]);
        //alert("上传成功")
    };
    uploader.onErrorItem = function (fileItem, response, status, headers) {
        //console.info('onErrorItem', fileItem, response, status, headers);
        // alert("上传失败!");
    };
    uploader.onCancelItem = function (fileItem, response, status, headers) {
        //console.info('onCancelItem', fileItem, response, status, headers);
        // alert("取消成功触发事件!");
    };
    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        //console.info('onCompleteItem', fileItem, response, status, headers);
        //alert("上传完成!");
    };
    uploader.onCompleteAll = function () {
        //alert("全部上传完成!");
        console.info($scope.fileList);
    };
    //-----------上传文件End--------


    if (false) {  //编辑
        $scope.ForumTopic_Get();
    }
    $scope.ForumType_List();
}]);


//topicdetail
forumModule.controller('ForumTopicDetialCtrl', ['$scope', '$state', 'forumProviderUrl', function ($scope, $state, forumProviderUrl, $stateParams) {
    //console.log($stateParams);
    $scope.ForumType_Item = [];//论坛版块
    $scope.ForumResponseInfo_Item = [];  //回复列表
    $scope.selected = '';
    $scope.OCID = $G2S.request("OCID");// $(".exercise_nav_list .active a").attr("href").split("?")[1].split("=")[1];
    $scope.TopicID = $G2S.request("TopicID");
    $scope.UserInfo = { UserName: "徐卫", UserID: 4, OCID: $scope.OCID };
    $scope.ForumMy = {
        TopicID: $scope.TopicID,
        ResponseID: 0,// 0表示为主题加赞 ,否则为回复点赞 
        UserID: 4
    };
    $scope.ForumResponse = {
        ResponseID: 0,
        TopicID: $scope.TopicID,
        ParentID: 0,
        Conten: "",
        UserID: 4,
        UserName: "小花"
    }
    $scope.ForumTopic = {
        SearchKey: '',
        OCID: $scope.OCID,
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
        var para = { ft: { OCID: $scope.OCID, UserID: 4 } };
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
        var para = { model: { TopicID: $scope.TopicID, UserID: 4 }, PageIndex: 1, PageSize: 10 };
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
        var para = { TopicID: $scope.TopicID };
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
        var para = { model: { TopicID: $scope.TopicID, ParentID: $scope.ForumResponse.ParentID, Conten: $scope.ForumResponse.Conten, UserName: $scope.UserInfo.UserName, UserID: $scope.UserInfo.UserID } };
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
        for (var i = 0; i < $scope.ForumResponseInfo_Item.length; i++) {
            $scope.ForumResponseInfo_Item[i].ResponseShow = false;
        }
        if (m == 0) {
            //alert(m);
            $scope.ForumResponse.ParentID = 0;
        } else {
            $scope.ForumResponse.UserName = m.UserName;
            $scope.ForumResponse.ParentID = m.ResponseID;
            $scope.ForumResponse.Conten = "";
            m.ResponseShow = true;
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


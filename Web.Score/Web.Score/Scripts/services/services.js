'use strict';

var aService = angular.module('app.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('softname', '成绩分析系统');

aService.constant('utilProviderUrl', '/DataProvider/Util.aspx');
aService.constant('adminProviderUrl', '/DataProvider/Admin.aspx');
aService.constant('schoolProviderUrl', '/DataProvider/School.aspx');
aService.constant('queryProviderUrl', '/DataProvider/Query.aspx');

///XHR调用
aService.factory('baseService', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }

    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) { if (thenFn) { thenFn(data); } })
            .error(function (reason) { if (errFn) { errFn(reason); } });
    }

    service.postPromise = function (url, param) {
        var deferred = $q.defer();

        $http.post(url, param)
            .success(function (data) { deferred.resolve(data); })
            .error(function (reason) { deferred.reject(reason) });

        return deferred.promise;
    }

    service.runPromises = function (promises, thenFn) {
        $q.all(promises).then(function (results) {
            thenFn(results);
        });
    }

    return service;
}]);

//常量
aService.factory('constService', function () {
    var service = {};

    service.ScoreSorts = [
      { code: 1, name: '原始成绩' },
      { code: 2, name: '转换成绩' }
    ];

    service.ScoreTypes = [
     { code: 1, name: '原始分数' },
     { code: 2, name: '标准分数' }
    ];

    service.Terms = [
       { Code: 0, Name: '上学期期中' },
       { Code: 1, Name: '上学期期末' },
       { Code: 2, Name: '下学期期中' },
       { Code: 3, Name: '下学期期末' }
    ];
    return service;
});

///工具类
aService.factory('utilService', ['baseService', 'utilProviderUrl', function (baseService, utilProviderUrl) {
    var service = {};

    service.GetNations = function (callback) {
        var url = utilProviderUrl + '/GetNations';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.GetPolitics = function (callback) {
        var url = utilProviderUrl + '/GetPolitics';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.GetResidenceType = function (callback) {
        var url = utilProviderUrl + '/GetResidenceType';
        var param = null;
        baseService.post(url, param, callback);
    }
    service.locate = function (collections, locateProp, locateValue) {
        var length = collections.length;
        for (var i = 0; i < length; i++) {
            var obj = collections[i];
            if (obj[locateProp] === locateValue) {
                return obj;
            }
        }
        return null;
    }
    //获得学年 Academicyear 
    service.GetAcademicYears = function (callback) {
        var url = utilProviderUrl + '/GetAcademicyear';
        var param = null;
        baseService.post(url, param, callback);
    }
    //获得年级代码 GradeCode
    service.GetGradeCodes = function (callback) {
        var url = utilProviderUrl + '/GetGradeCodes';
        var param = null;
        baseService.post(url, param, callback);
    }

    //获得年级代码 GradeCourse flag = -1 全部
    service.GetGradeCourse = function (gradeCode, flag, callback) {
        var url = utilProviderUrl + '/GetGradeCourse';
        var param = { gradeCode: gradeCode, flag: flag };
        baseService.post(url, param, callback);
    }

    //获得班级
    service.GetClassByScope = function (micYear, teacher, callback) {
        var url = utilProviderUrl + '/GetClassByScope';
        var param = { academicYear: micYear, teacher: teacher };
        baseService.post(url, param, callback);
    }


    //获得考试类型 TestType
    service.GetTestType = function (callback) {
        var url = utilProviderUrl + '/GetTestType';
        var param = null;
        baseService.post(url, param, callback);
    }

    //获得考试号 TestLogin
    service.GetTestLogin = function (academicyear, gradeNo, courseCode, testType, callback) {
        var url = utilProviderUrl + '/GetTestLogin';
        var param = { academicyear: academicyear, gradeNo: gradeNo, courseCode: courseCode, testType: testType };
        baseService.post(url, param, callback);
    }

    //获得学生 StdName
    service.GetStudent = function (academicyear, classcode, callback) {
        var url = utilProviderUrl + '/GetStudent';
        var param = { academicyear: academicyear, classcode: classcode };
        baseService.post(url, param, callback);
    }
    //获得所有学生
    service.GetStudents = function (callback) {
        var url = utilProviderUrl + '/GetStudents';
        var param = {};
        baseService.post(url, param, callback);
    }
    //根据班级获得学生
    service.GetStudentsByGrade = function (academicyear, classNo, callback) {
        var url = utilProviderUrl + '/GetStudentsByGrade';
        var param = { academicyear: academicyear, classNo: classNo };
        baseService.post(url, param, callback);
    }

    //获得所有教师
    service.GerTeacherAll = function(callback)
    {
        var url = utilProviderUrl + '/GerTeacherAll';
        var param = {};
        baseService.post(url, param, callback);
    }
    
    //获得所有年级
    service.GetGradeAll = function(callback)
    {
        var url = utilProviderUrl + '/GetGradeAll';
        var param = {};
        baseService.post(url, param, callback);
    }
    //获得所有课程
    service.GetCourseCodeAll = function (callback) {
        var url = utilProviderUrl + '/GetCourseCodeAll';
        var param = {};
        baseService.post(url, param, callback);
    }
    //获得课程（根据年和班级）
    service.GetGradeCourse = function (academicYear, gradeCode, callback) {
        var url = utilProviderUrl + '/GetGradeCourse';
        var param = { micYear: academicYear, gradeCode: gradeCode };
        baseService.post(url, param, callback);
    }
    //获取数组中最大值
    service.getMax = function (value) {
        var max = value[0].NumScore;
        var len = value.length;
        for (var i = 1; i < len; i++) {
            if (value[i].NumScore > max) {
                max = value[i].NumScore;
            }
        }
        return max;
    }
    //获取数组中最大值
    service.getMin = function (value) {
        var min = value[0].NumScore;
        var len = value.length;
        for (var i = 1; i < len; i++) {
            if (value[i].NumScore < min) {
                min = value[i].NumScore;
            }
        }
        return min;
    }
    //获取数组中平均分
    service.getAve = function (value) {
        var count = 0;
        for (var i = 0; i < value.length; i++) {
            count += value[i].NumScore
        }
        return (count / value.length).toFixed(2);
    }
    //获得数组中优良人次
    service.getGood = function (value, a) {
        var rs = [];
        for (var i = 0; i < value.length; i++) {
            var markcode = value[i].markcode.substr(1, 3) * a;
            if (value[i].NumScore >= markcode) {
                rs.push(value[i].NumScore);
            }
        }
        return rs.length;
    }
    //获得数组中不及格人次
    service.getFail = function (value, a) {
        var rs = [];
        for (var i = 0; i < value.length; i++) {
            var markcode = value[i].markcode.substr(1, 3) * a;
            if (value[i].NumScore < markcode) {
                rs.push(value[i].NumScore);
            }
        }
        return rs.length;
    }
    //显示灰色 jQuery 遮罩层 
    service.showBg = function() {
        var bh = $("body").height();
        var bw = $("body").width();
        $("#fullbg").css({
            height: bh,
            width: bw,
            display: "block"
        });
        $("#dialog").show();
    }
    //关闭灰色 jQuery 遮罩 
    service.closeBg = function() {
        $("#fullbg,#dialog").hide();
    }
    //获得复选框值
    service.getlist = function (value) {
        var selected = "";
        for (var i = 0; i < value.length ; i++) {
            if (value[i].checked) {
                selected += value[i].value + ",";
            }
        }
        selected = selected.substring(0, selected.length - 1);
        if (selected.length == 0) {
            selected = "";
        }
        return selected;
    }
    return service;
}]);

///分页
aService.factory('pageService', function () {
    var service = {};

    service.pages = 1;

    service.allData = [];

    service.data = [];

    service.index = 1;

    service.size = 20;

    var navToPage = function (index) {
        if (index > 0 && index <= service.pages) {
            service.data.length = 0;
            service.index = index;
            var start = (service.index - 1) * service.size;
            var end = service.index * service.size;
            end = service.allData.length < end ? service.allData.length : end;
            var d = service.allData.slice(start, end);
            service.data = service.allData.slice(start, end);
        }
    }

    service.reset = function () {
        service.index = 1;
        service.size = 20;
        service.allData.length = 0;
        service.data.length = 0;
    }

    service.init = function (data, size) {
        service.index = 1;
        service.size = size;
        service.allData = data;
        service.pages = Math.ceil(service.allData.length / service.size);
        navToPage(1);
    }

    service.next = function () {
        navToPage(service.index + 1);
    }

    service.previous = function () {
        navToPage(service.index - 1);
    }

    return service;
});

/// 用户
aService.factory('userService', ['baseService', 'adminProviderUrl', 'appUtils', function (baseService, adminProviderUrl, appUtils) {
    var service = {};

    service.verify = function (userName, pwd, callback) {
        var url = adminProviderUrl + '/Verify';
        var param = { user: userName, pwd: pwd };
        baseService.post(url, param, callback);
    }

    service.logout = function (callback) {
        var url = adminProviderUrl + '/Logout';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.isAuthorized = function (callback) {
        var url = adminProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getUser = function (callback) {
        var url = adminProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, function (data) {
            callback(data.d !== '' ? JSON.parse(data.d) : null);
        });
    }

    service.getFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    service.getFuncTree = function (callback) {
        var url = adminProviderUrl + '/GetFuncTree';
        var param = null;
        baseService.post(url, param, callback);
    }



    service.changePwd = function (teacher, oldPwd, newPwd, status, callback) {
        var url = adminProviderUrl + '/ChangePwd';
        var param = { teacherID: teacher, oldPwd: oldPwd, newPwd: newPwd, status: status };
        baseService.post(url, param, callback);
    }

    service.getUserGroups = function (userOrGroup, callback) {
        var url = adminProviderUrl + '/GetUserGroups';
        var param = { userOrGroup: userOrGroup };
        baseService.post(url, param, callback);
    }

    service.getGroupAndUsers = function (teacher, callback) {
        var url = adminProviderUrl + '/GetGroupAndUsers';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getAllUsers = function (callback) {
        var url = adminProviderUrl + '/GetAllUsers';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getAllUserGroups = function (callback) {
        var url = adminProviderUrl + '/GetAllUserGroups';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getUserAuths = function (teacher, callback) {
        var url = adminProviderUrl + '/GetUserAuths';
        var param = { teacher: teacher };
        baseService.post(url, param, callback);
    }

    service.getUsersOfGroup = function (groupID, callback) {
        var url = adminProviderUrl + '/GetUsersOfGroup';
        var param = { groupID: groupID };
        baseService.post(url, param, callback);
    }

    service.joinGroup = function (teacher, groupID, callback) {
        var url = adminProviderUrl + '/JoinGroup';
        var param = { teacher: teacher, groupID: groupID };
        baseService.post(url, param, callback);
    }

    service.leaveGroup = function (teacher, groupID, callback) {
        var url = adminProviderUrl + '/LeaveGroup';
        var param = { teacher: teacher, groupID: groupID };
        baseService.post(url, param, callback);
    }

    service.addUserGroup = function (category, callback) {
        var url = adminProviderUrl + '/AddUserGroup';
        var param = { category: category };
        baseService.post(url, param, callback);
    }

    service.removeUserGroup = function (userGroup, callback) {
        var url = adminProviderUrl + '/RemoveUserGroup';
        var param = { userGroup: userGroup };
        baseService.post(url, param, callback);
    }

    service.saveUserGroup = function (userGroup, callback) {
        var url = adminProviderUrl + '/SaveUserGroup';
        var param = { userGroup: userGroup };
        baseService.post(url, param, callback);
    }

    service.grant = function (teacher, func, rtype, sysNo, callback) {
        var url = adminProviderUrl + '/Grant';
        var param = { teacher: teacher, funcID: func, rtype: rtype, sysNo: sysNo };
        baseService.post(url, param, callback);
    }

    service.revoke = function (teacher, funcEntry, callback) {
        var url = adminProviderUrl + '/Revoke';
        var param = { teacher: teacher, funcEntry: funcEntry };
        baseService.post(url, param, callback);
    }

    service.buildGroupUserTree = function (callback) {
        var url = adminProviderUrl + '/GetGroupAndUsers';
        var param = null;
        var groupAndUserPromise = appUtils.createPromise(url, param);

        var url = adminProviderUrl + '/GetUserGroups';
        var param = { userOrGroup: '1' };
        var userPromise = appUtils.createPromise(url, param);

        appUtils.runPromises({
            GroupAndUsers: groupAndUserPromise,
            Users: userPromise
        }, function (results) {
            callback(results.GroupAndUsers.d, results.Users.d);
        });
    }

    return service;
}]);

aService.factory('uploadService', ['FileUploader', function (FileUploader) {
    var service = {};

    service.create = function (param, onCompleteItem) {
        var queryString = "?name='upload'";
        if (param) {
            angular.forEach(param, function (v, k) {
                queryString += '&' + k + '=' + v;
            });
        }
        var uploader = new FileUploader({ url: '/DataProvider/UploadHandler.ashx' + queryString });

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            if (onCompleteItem) onCompleteItem(fileItem, response, status, headers);
        };
        return uploader;
    }

    //$scope.fileUploader.filters.push({
    //    name: 'customFilter',
    //    fn: function (item /*{File|FileLikeObject}*/, options) {
    //        return this.queue.length < 10;
    //    }
    //});

    //uploader.onWhenAddingFileFailed = function (item /*{ Excel| *.xls}*/, filter, options) {
    //    console.info('onWhenAddingFileFailed', item, filter, options);
    //};
    //uploader.onAfterAddingFile = function (fileItem) {
    //    console.info('onAfterAddingFile', fileItem);
    //};
    //uploader.onAfterAddingAll = function (addedFileItems) {
    //    console.info('onAfterAddingAll', addedFileItems);
    //};
    //uploader.onBeforeUploadItem = function (item) {
    //    console.info('onBeforeUploadItem', item);
    //};
    //uploader.onProgressItem = function (fileItem, progress) {
    //    console.info('onProgressItem', fileItem, progress);
    //};
    //uploader.onProgressAll = function (progress) {
    //    console.info('onProgressAll', progress);
    //};
    //uploader.onSuccessItem = function (fileItem, response, status, headers) {
    //    console.info('onSuccessItem', fileItem, response, status, headers);
    //};
    //uploader.onErrorItem = function (fileItem, response, status, headers) {
    //    console.info('onErrorItem', fileItem, response, status, headers);
    //};
    //uploader.onCancelItem = function (fileItem, response, status, headers) {
    //    console.info('onCancelItem', fileItem, response, status, headers);
    //};
    //$scope.fileUploader.onCompleteItem = function (fileItem, response, status, headers) {
    //    console.info('onCompleteItem', fileItem, response, status, headers);
    //};
    //uploader.onCompleteAll = function () {
    //    console.info('onCompleteAll');
    //};
    return service;
}]);

aService.factory('chartService', ['baseService', function (baseService) {
    var service = {};

    var option = {
        tooltip: {
            trigger: 'axis'
        },

        toolbox: {
            show: true,
            feature: {
                mark: { show: false },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: false },
                saveAsImage: { show: false }
            }
        },
     
        calculable: true
    };

    service.chartCreate = function (id, callback) {
        require(['echarts', 'echarts/chart/line', 'echarts/chart/pie', 'echarts/chart/bar'], function (ec) {
            var myChart = ec.init(document.getElementById(id));
            if (callback) callback(myChart); 
        });
    }

    service.refresh = function (chart, legend, xAxis, series) {
        var newOption = {};
        angular.extend(newOption, option, legend, xAxis, series);
        chart.setOption(newOption);
    }

    service.changeOption = function (chart, chartOption) {
        var newOption = {};
        angular.extend(newOption, option, chartOption);
        chart.setOption(newOption);
    }


    return service;
}]);

aService.factory('menuService', ['baseService', 'adminProviderUrl', 'appUtils', function (baseService, adminProviderUrl, appUtils) {
    var service = {};

    service.getMenus = function (callback) {
        var url = adminProviderUrl + '/GetMenus';
        var param = null;
        baseService.post(url, param, callback);
    }


    service.getFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    service.getUserFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetUserFuncs';
        var param = { teacher: teacher };
        baseService.post(url, param, callback);
    }

    var filterMenus = function (funcs, menus) {
        var length = funcs.length;
        for (var i = 0; i < length; i++) {
            var func = funcs[i];
            var count = menus.length;
            for (var j = 0; j < count; j++) {
                var menu = menus[j];
                menu.visable = func.FuncID === menu.FuncID;
            }
        }
    }

    service.initMenus = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        var funcPromise = appUtils.createPromise(url, param);

        url = adminProviderUrl + '/GetMenus';
        param = null;
        var menuPromise = baseService.post(url, param, callback);

        appUtils.runPromises({ funcs: funcPromise, menus: menuPromise }, function (results) {
            var aMenus = [];
            var aFuncs = null;
            if (results.menus.d !== null) {
                aMenus = JSON.parse(results.menus.d);
            }
            if (results.funcs.d !== null) {
                aFuncs = results.funcs.d;
            }

            if (aFuncs.length > 0 && aMenus.children.length > 0) {
                for (var menu in aMenus.children) {
                    filterMenus(aFuncs, menu.children);
                }
            }
            callback(aMenus);
        });
    }

    return service;
}]);